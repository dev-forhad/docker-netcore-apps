using BookManager.Data;
using BookManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, $"Something went wrong.. {ex.Message}");
                }
            }
            
            ModelState.AddModelError(String.Empty, $"Something went wrong.");
            return View(book);
        }
    }
}
