namespace ProjectX.App.Models
{
    public class RequesterInfo
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public Thought Thoughts { get; set; }
        public int ThoughtId { get; set; }
    }
}
