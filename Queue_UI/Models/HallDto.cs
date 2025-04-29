using System.ComponentModel.DataAnnotations;

namespace Queue_UI.Models
{
    public class HallDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Название зала обязательно")]
        [StringLength(100, ErrorMessage = "Название зала не должно превышать 100 символов")]
        public string? Name { get; set; }
    }
}
