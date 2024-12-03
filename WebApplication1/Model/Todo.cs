using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(5)]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
