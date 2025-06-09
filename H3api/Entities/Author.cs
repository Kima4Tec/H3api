namespace H3api.Entities
{
    /// <summary>
    /// Representing an author
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Author Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Author First Name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Author Last Name
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// List of books by author
        /// </summary>
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
