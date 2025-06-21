namespace H3api.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public int PublishYear { get; set; }
        public double Price { get; set; }
        public int AuthorId { get; set; }
    }

}
