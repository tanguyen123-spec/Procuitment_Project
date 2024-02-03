using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Produ_project.Enitity;
using Produ_project.Helper;
using Produ_project.Secure.Data;
using Produ_project.Secure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Produ_project.Secure.Service
{
    public interface ITokenServices
    {
        Task<ApiResponse> Validate(LoginModels model);
        Task<ApiResponse> RenewToken(TokenModels tokenModels);
    }
    public class TokenServices : ITokenServices
    {
        private readonly Produ_projectContext context_;
        private readonly AppSettings appSettings_;
        public TokenServices(Produ_projectContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            context_ = context;
            appSettings_ = optionsMonitor.CurrentValue;
        }


        public async Task<ApiResponse> Validate(LoginModels model)
        {
            //tìm các tài khoản có email trùng với email
            //lấy các password của các tài khoản đem so sánh.
            IQueryable<User> query = context_.Users;
            var password = model.Passwords;
            var accs = await (from pw in query
                              where pw.NameUser == model.UserName
                              select pw).ToListAsync();
            foreach (User acc in accs)
            {
                string hashpasswordfromDb = acc.Password;
                if (hashpasswordfromDb == null)
                {
                    Console.WriteLine("danh sách pass null");
                }
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashpasswordfromDb);
                if (passwordMatches)
                {
                    var token = await GeneratedToken(acc);
                    return new ApiResponse
                    {
                        Success = true,
                        userName = acc.NameUser,
                        Message = "Authenticate success",
                        Data = token
                    };
                }
            }
            return new ApiResponse
            {
                Success = false,
                Message = "Invalid username/password"
            };

            //cấp token 
        }

        //GeneratedToken tạo ra 1 cặp token(access và refresh)
        private async Task<TokenModels> GeneratedToken(User user)
        {
            // Khởi tạo đối tượng JwtSecurityTokenHandler để tạo và xử lý JWT
            var jWtTokenHandler = new JwtSecurityTokenHandler();
            // Chuyển đổi chuỗi SecretKey từ cài đặt thành mảng byte
            var secretkeyBytes = Encoding.UTF8.GetBytes(appSettings_.SecretKey);
            // Mô tả thông tin về token
            var tokenDescription = new SecurityTokenDescriptor
            {
                // Subject chứa danh sách các Claims (khẳng định) về người dùng
                Subject = new ClaimsIdentity(new[]
                {
                   // Thêm claim "UserName" với giá trị là tên người dùng từ đối tượng Account
                    new Claim("UserName", user.NameUser),
                   // Thêm claim "maAccount" với giá trị là MaAccount từ đối tượng Account, chuyển đổi thành chuỗi
                    new Claim("maUser", user.UserId.ToString()),
                    //new Claim(JwtRegisteredClaimNames.Email, account.Email),
                    //Jti được set bằng Guid để xác định token là duy nhất
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                    //role (quyen)
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                // Token sẽ hết hạn sau một khoảng thời gian 
                Expires = DateTime.UtcNow.AddMinutes(20),
                // Đặt thuật toán và khóa để ký và xác thực token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
               (secretkeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            //tạo token dựa trên mô tả của tokenDescription
            var token = jWtTokenHandler.CreateToken(tokenDescription);
            //viết var token thành một chuỗi token
            var accessToken = jWtTokenHandler.WriteToken(token);
            //tạo refresh token
            var refreshToken = GenerateRefreshToken();
            //tạo data token để thêm vào database
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtID = token.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddHours(1),
                MaAccount = user.UserId
            };
            await context_.AddAsync(refreshTokenEntity);
            await context_.SaveChangesAsync();
            return new TokenModels
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        //hàm tạo refreshToken
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<ApiResponse> RenewToken(TokenModels tokenModels)
        {
            // Khởi tạo đối tượng JwtSecurityTokenHandler để tạo và xử lý JWT
            var jWtTokenHandler = new JwtSecurityTokenHandler();
            // Chuyển đổi chuỗi SecretKey từ cài đặt thành mảng byte
            var secretkeyBytes = Encoding.UTF8.GetBytes(appSettings_.SecretKey);
            var tokenValidateparam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,
                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretkeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, //không kiểm tra token hết hạn
            };
            try
            {
                //check 1: AccessToken valid format
                var tokenInverification = jWtTokenHandler.ValidateToken(tokenModels.AccessToken,
                    tokenValidateparam, out var validatedToken);
                //check 2: check thuật toán
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid Token"
                        };
                    }
                }
                //check 3: Check access token expire?
                var utcExpireDate = long.Parse(tokenInverification.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnitTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Accses token has not yet expired"
                    };
                }
                // check 4: check refreshtoken exits in DB
                var storedToken = context_.RefreshTokens
                    .FirstOrDefault(x => x.Token == tokenModels.RefreshToken);
                if (storedToken == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token does not exist"
                    };
                }
                // check 5 : check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token has been used"
                    };
                }
                //check bị thu hồi chưa
                if (storedToken.IsRevoked)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token has been revoked"
                    };
                }
                // check 6: Access token id == Jwt in RefreshToken
                var jti = tokenInverification.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtID != jti)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Token doesn't not match"
                    };
                }
                //--------------------------------------
                //update Token is used
                storedToken.IsUsed = true;
                storedToken.IsRevoked = true;
                context_.Update(storedToken);
                await context_.SaveChangesAsync();
                //create new token
                var user = await context_.Users.SingleOrDefaultAsync(a => a.UserId == storedToken.MaAccount);
                var token = await GeneratedToken(user);
                return new ApiResponse
                {
                    Success = true,
                    Message = "renew token success"

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        private DateTime ConvertUnitTimeToDateTime(long utcExpireDate)
        {
            DateTime datetimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // Thêm số giây từ Unix timestamp
            return datetimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
        }
    }
}
