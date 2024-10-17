using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Giftify.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
