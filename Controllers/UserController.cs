using Microsoft.AspNetCore.Mvc;
using IdentityFramework.Interfaces;
using IdentityFramework.ViewModels;
using Microsoft.AspNetCore.Identity;
using IdentityFramework.Models;


namespace IdentityFramework.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Mileage,
                    ProfileImageUrl = user.ProfileImageUrl, 
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State,
            };
            return View(userDetailViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var delUser = await _userRepository.GetUserById(id);
            var DeleteUserResponse = await _userManager.DeleteAsync(delUser);
            if (DeleteUserResponse.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["Error"] = "Account deletion failed.";
                return View();
            }
        }
        public async Task<IActionResult> Edit(string id)
        {
            throw new NotImplementedException();
        }
    }
}
