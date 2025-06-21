namespace H3api.Dtos
{
    public class CreateCoverDto
    {
        public string DesignIdeas { get; set; } = string.Empty;
        public bool DigitalOnly { get; set; }
        public int BookId { get; set; }
        public List<int> ArtistIds { get; set; } = new();
    }
}
