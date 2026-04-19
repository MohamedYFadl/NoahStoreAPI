using AdminDashboard.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
              var result = await  _roleManager.DeleteAsync(role);
                if(result.Succeeded)
                    TempData["success"] = "The role has been deleted successfully";

            }
            return RedirectToAction("Index");   
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(string? id)
        {
            if(id == null)
                return View(new RoleViewModel());

            return View(new RoleViewModel(){
                Id = id,
                Name =   _roleManager.FindByIdAsync(id).Result.Name
                
            });
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(RoleViewModel role)
        {
            if(role is not null)
            {
                var RolesInDb = await _roleManager.Roles.ToListAsync();
               if(string.IsNullOrEmpty(role.Id)) // Add
                {
                    if (! await _roleManager.RoleExistsAsync(role.Name.Trim()))
                    {
                        var result  =await _roleManager.CreateAsync(new IdentityRole { Name = role.Name.Trim() });
                        if(result.Succeeded)
                             TempData["success"] = "The role has been created successfully";
                       

                    }
                    else
                    {
                        ModelState.AddModelError("Name", "Role is already exists!");
                        return View(role);
                    }
                }
                else
                {//update
                    var oldRole = await _roleManager.FindByIdAsync(role.Id);
                    if(oldRole != null)
                    {
                        oldRole.Name = role.Name;
                       var result = await _roleManager.UpdateAsync(oldRole);
                        if(result.Succeeded)
                             TempData["success"] = "The role has been updated successfully";

                    }
                }
                return RedirectToAction("Index");
            }
            return View(role);
        }
    }
}
