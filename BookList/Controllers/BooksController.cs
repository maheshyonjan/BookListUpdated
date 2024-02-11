using BookList.Data;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        //inject Dbcontext inside the controller
        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddBook()
        {   
           

            return View();
        }

        [HttpPost]
        
        public  async Task<IActionResult> AddBook(AddBookViewModel viewModel) 
        {
            var Book = new Book
            {
                Name = viewModel.Name,
                Author = viewModel.Author,
                ISBN = viewModel.ISBN
            };

            await dbContext.Books.AddAsync(Book);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("ListBooks", "books");
        }


        [HttpGet]

        public async Task<IActionResult> ListBooks()
        {
            try
            {
                var books = await dbContext.Books.AsNoTracking().ToListAsync();
                return View(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]

        public async Task<IActionResult> EditBook(int id)
        {
            var student = await dbContext.Books.FindAsync(id);

            return View(student);

             
        }

        [HttpPost]

        public async Task<IActionResult> EditBook(Book viewModel)
        {
           var Book = await dbContext.Books.FindAsync(viewModel.Id);

           if (Book != null)
            {
                Book.Name = viewModel.Name;
                Book.Author = viewModel.Author;
                Book.ISBN = viewModel.ISBN;

                await dbContext.SaveChangesAsync();
            }
           return RedirectToAction("ListBooks", "books");
        }

        [HttpPost]

        public async Task<IActionResult> DeleteBook(int Id )
        {
           var Book = await dbContext.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == Id);
           if(Book != null)
            {
                dbContext.Books.Remove(Book);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListBooks", "books");

        }
    }


}
