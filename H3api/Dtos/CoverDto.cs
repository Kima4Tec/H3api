namespace H3api.Dtos
{
    public class CoverDto
    {
        public int CoverId { get; set; }
        public string DesignIdeas { get; set; } = string.Empty;
        public bool DigitalOnly { get; set; }
        public int BookId { get; set; }
        public string? BookTitle { get; set; }

        public List<string> ArtistNames { get; set; } = new();
    }

}
