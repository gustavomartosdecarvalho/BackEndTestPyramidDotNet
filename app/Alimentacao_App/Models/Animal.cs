using System.ComponentModel.DataAnnotations;

namespace Alimentacao_App.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name must contain 2 to 30 characters")]
        [MinLength(2, ErrorMessage = "name must contain 2 to 30 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Portions is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Portions must be greater than zero")]
        public int Portions { get; set; }
        
        [Required(ErrorMessage = "WeightPortions is required")]
        [Range(1, int.MaxValue, ErrorMessage = "WeightPortions must be greater than zero")]
        public int WeightPortions { get; set; }
        
    }
}