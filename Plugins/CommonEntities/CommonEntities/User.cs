using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonEntities
{
    public class User
    {
        public int Id { set; get; }

        [Index(IsUnique = true)]
        [MaxLength(450)]
        [Required]
        public string Name { set; get; }
    }
}