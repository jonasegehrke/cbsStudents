using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cbsStudents.Data;
using cbsStudents.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace cbsStudents.Controllers
{

    [Authorize]
    public class EventController : Controller
    {
        private readonly CbsStudentsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EventController(CbsStudentsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Event

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var cbsStudentsContext = _context.Event.Include(r => r.Room).Include(u => u.User);
            var vm = new EventIndexVm
            {
                Events = cbsStudentsContext.ToList(),
            };
            return View(vm);
        }

        // GET: Event/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var @event = await _context.Event
                .Include(r => r.Room).Include(u => u.User)
                .FirstOrDefaultAsync(m => m.EventId == id);




            if (@event == null)
            {
                return NotFound();
            }


            var vm = new EventIndexVm
            {
                Event = @event,
            };

            return View(vm);
        }

        // GET: Event/Create

        public IActionResult Create()
        {
            ViewData["Location"] = new SelectList(_context.Room, "RoomId", "Location");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Title,StartDate,EndDate,Description,RoomId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                @event.UserId = user.Id;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", @event.RoomId);
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", @event.RoomId);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Title,StartDate,EndDate,Description,RoomId")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", @event.RoomId);
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}
