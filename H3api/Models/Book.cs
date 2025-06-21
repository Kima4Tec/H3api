using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H3api.Entities
{

    public class Book
    {

        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateOnly PublishDate { get; set; }

        public double Price { get; set; }

        public Cover? Cover { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
