using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecipeBox.Models;
using System.Threading.Tasks;
using RecipeBox.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  public class AccountController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }
    
    public async Task<ActionResult> Details()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      return View(currentUser);
    } 

    // public async Task<ActionResult> Edit(string Id)
    // {
    //   // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   var currentUser = await _userManager.FindByIdAsync(Id);      
    //   return View(currentUser);
    // }

    // [HttpPost]
    // public ActionResult Edit(ApplicationUser currentUser)
    // {
    //   _db.Entry(currentUser).State = EntityState.Modified;
    //   _db.Update(currentUser);
    //   // _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };  
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else 
      {
        return View();
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    public ActionResult LogOff()
    {
      return View();
    }

    [HttpPost, ActionName("LogOff")]
    public async Task<ActionResult> LogOffConfirmed()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}