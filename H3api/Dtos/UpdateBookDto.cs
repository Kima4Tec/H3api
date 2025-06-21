namespace H3api.Dtos
{
    public class UpdateBookDto
    {
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateOnly PublishDate { get; set; }

        public double Price { get; set; }

        public int AuthorId { get; set; }

    }

}
