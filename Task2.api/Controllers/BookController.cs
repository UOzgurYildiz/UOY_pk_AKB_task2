using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Task1_api.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>()
    {
        new Book{
            Id = 1,
            Title = "Lean Startup",
            GenreId = 1, //PErsonal Growth
            PageCount = 200,
            PublishDate = new DateTime(2006, 1,12)
        },
        new Book{
            Id = 2,
            Title = "Herland",
            GenreId = 2,
            PageCount = 345,
            PublishDate = new DateTime(1995,6 , 4)
        },new Book{
            Id = 3,
            Title = "Dune",
            GenreId = 2,
            PageCount = 234,
            PublishDate = new DateTime(1654, 2, 3)
        }
    };

    [HttpGet]
    public List<Book> GetBooks(){
        var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        return bookList;
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if(book is null)
            return BadRequest();

        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount: book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        return Ok();
    } 

    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if(book is not null)
            return BadRequest();
        
        BookList.Add(newBook);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if(book is null)
            return BadRequest();

        BookList.Remove(book);
        return Ok();
    }
    
    
}