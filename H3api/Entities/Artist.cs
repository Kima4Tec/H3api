using System.Text.Json.Serialization;

namespace H3api.Entities
{
    /// <summary>
    /// Represents a Cover Artist
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Artist id
        /// </summary>
        public int ArtistId { get; set; }
        /// <summary>
        /// Artist First Name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Artist Last Name
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// List of covers by artist
        /// </summary>
        public List<Cover> Covers { get; set; } = new List<Cover>();
    }
}
