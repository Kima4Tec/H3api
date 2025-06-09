using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H3api.Entities
{
    /// <summary>
    /// Representing the cover of the book
    /// </summary>
    public class Cover
    {
        /// <summary>
        /// Cover Id
        /// </summary>
        public int CoverId { get; set; }
        /// <summary>
        /// Gets or sets the design ideas or concepts for the cover.
        /// This can include notes or inspirations related to the cover design.
        /// </summary>
        public string DesignIdeas { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets a value indicating whether the cover is digital only.
        /// True if the cover exists only in digital form; otherwise, false.
        /// </summary>
        public bool DigitalOnly { get; set; }
        /// <summary>
        /// Sets an ID for the book where the cover implies
        /// Foreign key linking the cover to a its book
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="Book"/> entity for this cover.
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// Gets or sets the list of artists who contributed to the cover design.
        /// </summary>
        public List<Artist> Artists { get; set; } = new List<Artist>();
    }
}
