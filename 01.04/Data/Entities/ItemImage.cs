using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._04.Data.Entities
{
    public class ItemImage
    {
        public Guid ItemId { get; set; }
        public String ImageUrl { get; set; } = null!;
        public int? Order { get; set; }
    }
}
