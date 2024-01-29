using System;
using System.Collections.Generic;

namespace Produ_project.Enitity
{
    public partial class User
    {
        public string UserId { get; set; } = null!;
        public string? NameUser { get; set; }
        public bool? Role { get; set; }
        public string? Password { get; set; }
    }
}
