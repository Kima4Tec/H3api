namespace H3api.Dtos
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishYear { get; set; }
        public double Price { get; set; }
        public string AuthorFullName { get; set; } = string.Empty;
        public string? CoverDesignIdeas { get; set; }
        public List<string>? ArtistFullNames { get; set; }
    }

}
