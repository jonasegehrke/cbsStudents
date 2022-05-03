#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cbsStudents.Models.Entities;
using cbsStudents.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace cbsStudents.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly CbsStudentsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PostsController(CbsStudentsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Posts
        [AllowAnonymous]
        public async Task<IActionResult> Index(string SearchString = "")
        {


            if (SearchString == null)
            {
                SearchString = "";
            }

            var posts = _context.Post.Include(u => u.User).Where(x => x.Title.Contains(SearchString));

            ViewBag.SearchString = SearchString;

            var vm = new PostIndexVm
            {
                Posts = posts.ToList(),
                SearchString = SearchString
            };

            return View(vm);

        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Post p = _context.Post.Include(u => u.User).Include(x => x.Comments).ThenInclude(x => x.User).First(x => x.Id == id);

            var vm = new PostIndexVm
            {
                Post = p
            };

            return View(vm);
        }

        // GET: Posts/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,Status")] Post post)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                post.UserId = user.Id;

                post.Created = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            Post p = _context.Post.Include(x => x.Comments).ThenInclude(x => x.User).First(x => x.Id == id);

            return View(p);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,Created,Status")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }


        public IActionResult RedirectToCreateComment(int Id){
            return RedirectToAction("Create", "Comments", new { PostId = Id });
        }
        
        public IActionResult RedirectToDeleteComment(int Id){
            return RedirectToAction("Delete", "Comments", new { id = Id });
        }

    }

    
}
