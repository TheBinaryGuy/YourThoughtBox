using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectX.App.Models
{
    public class Thought
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Content is required."), Display(Name = "Thought")]
        public string Content { get; set; }
        [Display(Name = "Created At")]
        public DateTimeOffset CreatedAt { get; set; } = DateTime.Now;
        public long Views { get; set; }
        public char TagsDelimiter { get; set; } = ',';
        [Required(ErrorMessage = "Tags are required.")]
        public string Tags { get; set; }
        public bool IsDeleted { get; set; }
    }
}
