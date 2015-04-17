using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPluginMasterApp.DomainClasses
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(400)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
