using ThoughtBox.App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThoughtBox.App.ViewModels
{
    public class ThoughtViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Content is required."), Display(Name = "Thought")]
        public string Content { get; set; }
        public long Views { get; set; }
        [Required(ErrorMessage = "Tags are required."), Display(Name = "Tags (Comma Seperated)")]
        public string Tags { get; set; }
        public List<Thought> Thoughts { get; set; }
        public int TotalThoughts { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
