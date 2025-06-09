using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H3api.Entities
{
    /// <summary>
    /// Represents a book entity in the system.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Book Id
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// Title of book
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Published year
        /// </summary>
        public DateOnly PublishDate { get; set; }

        /// <summary>
        /// Basic price of book
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Gets or sets the cover information for the book.
        /// This includes design details and associated artists.
        /// </summary>
        public Cover Cover { get; set; }
        /// <summary>
        /// Gets or sets the ID of the author who wrote the book.
        /// This is a foreign key linking the book to its author.
        /// </summary>
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
