namespace connect_.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
