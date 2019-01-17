using System.Collections.Generic;
using Ncx.Exam.Api.Models;

namespace Ncx.Exam.Api.Responses
{
    public class GetAllBooksResponse
    {
        public IEnumerable<Book> Books { get; set; }
    }
}