using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._04.Data.Entities
{
    public class User
    {
        public Guid     Id            { get; set; }
        public string    Name         { get; set; } = null!;
        public string    Email        { get; set; } = null!;
        public DateTime? BirthDate    { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? DeletedAt    { get; set; }

        //інверсна навігаційна властивість - властивість у іншому Entity, що посилається на даний Entity

        public List<UserAccess> userAccesses { get; set; } = new List<UserAccess>();


    }
}
