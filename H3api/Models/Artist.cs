using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3api.Entities
{
    public class Artist
    {
        public int ArtistId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<Cover> Covers { get; set; } = new List<Cover>();
    }
}
