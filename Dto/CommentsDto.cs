namespace connect_.Dto
{
    public class CommentsDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
