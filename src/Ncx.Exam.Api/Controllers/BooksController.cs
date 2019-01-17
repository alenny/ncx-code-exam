using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ncx.Exam.Api.Models;
using Ncx.Exam.Api.Services;
using Ncx.Exam.Api.Responses;
using Ncx.Exam.Api.Requests;

namespace Ncx.Exam.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET api/books
        [HttpGet]
        public async Task<ActionResult<GetAllBooksResponse>> Get()
        {
            var res = new GetAllBooksResponse
            {
                Books = await _bookService.GetAllAsync()
            };
            return Ok(res);
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(long id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public async Task<ActionResult> Post([Bind("Title, Category, Price, PublishDate, Rating")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _bookService.CreateAsync(book);
            return Ok();
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [Bind("Title, Category, Price, PublishDate, Rating")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Category = book.Category;
            existingBook.Price = book.Price;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Rating = book.Rating;
            await _bookService.UpdateAsync(existingBook);
            return Ok();
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookService.DeleteAsync(book);
            return Ok();
        }
    }
}
