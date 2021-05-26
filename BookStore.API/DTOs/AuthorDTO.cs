using System.Collections.Generic;

namespace BookStore.API.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }
        public virtual IList<BookDTO> Books { get; set; }
    }
}
