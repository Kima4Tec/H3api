using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H3api.Entities
{

    public class Cover
    {

        public int CoverId { get; set; }

        public string DesignIdeas { get; set; } = string.Empty;

        public bool DigitalOnly { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public List<Artist> Artists { get; set; } = new List<Artist>();
    }
}
