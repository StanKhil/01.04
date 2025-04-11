using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._04.Data.Entities
{
    public class ProductGroup
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;
        public String Slug { get; set; } = null!;  // частина посилання на ресурс
        public String ImageUrl { get; set; } = null!;
        public DateTime? DeletedAt { get; set; }


        public ProductGroup? ParentGroup { get; set; }
        public List<Product> Products { get; set; } = [];
        public List<ItemImage> Images { get; set; } = [];
    }
}
