using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Users;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToArrayAsync();
            return View(users.Select(x => new UserViewModel
            {
                Age = x.Age,
                Id = x.Id,
                Email = x.Email,
            }).ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            return View("UserDetails", new UserViewModel
            {
                Age = user.Age,
                Email = user.Email,
                Id = user.Id,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            return View(new UserViewModel
            {
                Age = user.Age,
                Email = user.Email,
                Id = user.Id,
                RowVersion = user.RowVersion,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userToUpdate = await _userManager.FindByIdAsync(userViewModel.Id);
            if (userToUpdate == null)
            {
                return BadRequest();
            }

            userToUpdate.Age = userViewModel.Age.Value;
            userToUpdate.Email = userViewModel.Email;
            userToUpdate.UserName = userViewModel.Email;
            userToUpdate.RowVersion = userViewModel.RowVersion;
            var result = await _userManager.UpdateAsync(userToUpdate);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "User has been modified, please refresh page and try again.");
            return View("Edit", userViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User
            {
                Age = model.Age.Value,
                Email = model.Email,
                UserName = model.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, $"(${error.Code}) ${error.Description})");

            }

            return View(model);
        }
    }
}
