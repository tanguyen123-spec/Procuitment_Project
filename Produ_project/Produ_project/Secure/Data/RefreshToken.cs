using Produ_project.Enitity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Produ_project.Secure.Data
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        public string MaAccount { get; set; } //ma User
        [ForeignKey(nameof(MaAccount))]
        public User user { get; set; }

        //refresh token
        public string Token { get; set; }
        //access token
        public string JwtID { get; set; }

        //đã sử dụng ?
        public bool IsUsed { get; set; }
        //đã thu hồi ?
        public bool IsRevoked { get; set; }
        //tạo ra ngày ?
        public DateTime IssuedAt { get; set; }
        //hết hạn lúc ?
        public DateTime ExpireAt { get; set; }
    }
}
