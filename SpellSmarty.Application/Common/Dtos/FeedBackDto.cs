using SpellSmarty.Application.Common.Response;


namespace SpellSmarty.Application.Common.Dtos
{
    public class FeedBackDto
    {
        public int FeedbackId { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime Date { get; set; }
    }
}
