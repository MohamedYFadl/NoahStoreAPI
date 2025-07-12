using AdminDashboard.Models;
using AdminDashboard.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using NoahStore.Core.Entities.Identity;
using StackExchange.Redis;

namespace AdminDashboard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
           _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var DbUsers = await _userManager.Users.Select( u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Name = u.DisplayName,
                Role =  _userManager.GetRolesAsync(u).Result.FirstOrDefault()
            }).ToListAsync();

            return View(DbUsers);
        }
        public  IActionResult Create()
        {
            var allRoles = _roleManager.Roles
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();
            return View(new UserRoleViewModel()
            {
                RolesList = allRoles,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleViewModel user)
        {
            if(user is not null)
            {
                if(await _userManager.FindByNameAsync(user.UserName) is not null)
                {
                    ModelState.AddModelError("UserName", "user name is already exists");
                }
                if (await _userManager.FindByEmailAsync(user.Email) is not null)
                {
                    ModelState.AddModelError("Email", "user name is already exists");
                }
                else
                {
                   
                    var newUser = new AppUser
                    {
                        DisplayName = user.Name,
                        Email = user.Email,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                    };
                   
                    var result = await _userManager.CreateAsync(newUser,user.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, user.Role);
                        TempData["Success"] = "User has been created successfuly";
                        return RedirectToAction("Index");
                    }

                }
                
                return View(new UserRoleViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber= user.PhoneNumber,
                    Name = user.Name,
                    RolesList = _roleManager.Roles
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList()
                }
                );

            }
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userRole =  _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            var userVM = new UserRoleViewModel
            {
                Email = user.Email,
                Id = id,
                Name = user.DisplayName,
                Role = userRole,
                PhoneNumber = user.PhoneNumber, 
                UserName = user.UserName,
                RolesList = _roleManager.Roles
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList()
            };

            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel user)
        {
            if (user is not null)
            {

                var FindUser = await _userManager.FindByIdAsync(user.Id);
                if(FindUser != null)
                {
                    var oldRole = _userManager.GetRolesAsync(FindUser).Result.FirstOrDefault();

                    FindUser.PhoneNumber = user.PhoneNumber;
                    if (user.Role != oldRole)
                    {
                       await _userManager.RemoveFromRoleAsync(FindUser, oldRole);
                      await  _userManager.AddToRoleAsync(FindUser, user.Role);
                    }
                   await _userManager.UpdateAsync(FindUser);
                    return RedirectToAction("Index");
                }
                
            }
            return View(new UserRoleViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                RolesList = _roleManager.Roles
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList()
            });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
              var result =   await  _userManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    TempData["success"] = "The user has been deleted successfully";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                TempData["error"] = "User not found";
                return View(email);
            }

            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, "123456@Ma");
            if (!result.Succeeded)
            {
                TempData["error"] = $"Failed to reset  password: {string.Join(", ", result.Errors.Select(e => e.Description))}";
            }
            TempData["success"] = "The Password has been reset successfully";
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> UserSearch(string SearchTerm,string Role)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u => u.Email.Contains(SearchTerm) ||
                u.DisplayName.Contains(SearchTerm));
            }
            if (!string.IsNullOrEmpty(Role))
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(Role);
                var userIds = usersInRole.Select(u => u.Id).ToList();
                query = query.Where(u => userIds.Contains(u.Id));
            }

            var users = await query.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Name = u.DisplayName,
                Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault()
            }).ToListAsync();
            return RedirectToAction(nameof(Index), users);
        }
    }
}
