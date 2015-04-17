using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPluginMasterApp.DomainClasses
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(400)]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}