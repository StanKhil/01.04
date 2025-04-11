using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._04.Data.Entities
{
    public class UserAccess
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RoleId { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string Dk { get; set; } = null!; // Derived key by Rfc2898
        public User User { get; set; } = null!; // Навігаційна властивість - посилання на інший Entity
        public UserRole UserRole { get; set; } = null!; // Навігаційна властивість - посилання на інший Entity
    }
}
