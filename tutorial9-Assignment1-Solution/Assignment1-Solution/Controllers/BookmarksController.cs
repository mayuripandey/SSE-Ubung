using BookmarkService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookmarkService.Controllers
{
    [Route("[controller]")]
    public class BookmarksController : Controller
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        
        public BookmarksController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        // GET bookmarks
        [HttpGet]
        public Bookmark[] Get()
        {
            return _bookmarkRepository.GetAll();
        }

        // GET bookmarks/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bookmark = _bookmarkRepository.GetById(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            return Json(bookmark);
        }

        // PUT bookmarks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Bookmark bookmark)
        {
            _bookmarkRepository.Update(bookmark);
        }

        // DELETE bookmarks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bookmarkRepository.Delete(id);
        }
    }
}
