using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDOperations.Models;
using CRUDOperations.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CRUDOperations.Controllers;

public class AssetController : Controller
{

    private ApplicationDbContext _db;

    public AssetController(ApplicationDbContext db)
    {
        _db = db;
    }


    //** 2nd Way
    // public IActionResult Index(){

    //     var Assets = _db.Users.ToList();
    //     ViewBag.user = Assets;
    //     return View();
    // }

    public IActionResult Index()
    {
        var users = _db.Users.ToList(); // Fetch data from the database
        if (users.Count == 0)
        {
            // Log if no data is fetched
            Console.WriteLine("No users found.");
        }
        else
        {
            foreach (var user in users)
            {
                // Log each user fetched
                Console.WriteLine($"User: {user.user_id}, {user.username}, {user.email}, {user.password}");
            }
        }
        return View(users); // Pass data to the view
    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public IActionResult CreateUser(UsersEntity NewUser)
    {
        _db.Users.Add(NewUser);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    // [HttpPost]
    // public IActionResult UpdateUser(UsersEntity NewUser)
    // {
    //     _db.Users.Update(NewUser);
    //     _db.SaveChanges();

    //     return RedirectToAction("Index");
    // }

    

    public IActionResult Update(){

        return View();
    }

    // public IActionResult Update()
    // {
    //     var UpdateUserInfo = _db.Users.FromSqlRaw("UpdateUserInfo").ToList();
    //     return View(UpdateUserInfo);
    // }

    public IActionResult GetDetails(int? id)
    {
        var GetSingleInfo = _db.Users.FromSqlRaw($"GetSingleInfo {id}").AsEnumerable().FirstOrDefault();
        return View(GetSingleInfo);
    }

        public IActionResult Edit(int id)
    {
        var user = _db.Users.Find(id); // Fetch user from the database by ID
        if (user == null)
        {
            return NotFound(); // Handle case where user is not found
        }
        return View(user); // Pass the user data to the view
    }


    [HttpPost]
    public async Task<IActionResult> UpdateDetails(int id, string username, string password, string email)
    {

        var param = new SqlParameter[]
        {
            new SqlParameter()
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
            },
            new SqlParameter()
            {
                ParameterName = "@username",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = username
            },
            new SqlParameter()
            {
                ParameterName = "@password",
                SqlDbType=System.Data.SqlDbType.VarChar,
                Value = password
            },
             new SqlParameter()
             {
                ParameterName = "@email",
                SqlDbType=System.Data.SqlDbType.VarChar,
                Value = email
             }
        };

        var UpdateUserDetails = await _db.Database.ExecuteSqlRawAsync($"Exec UpdateUserInfo @id, @username, @password, @email, param");

        if (UpdateUserDetails == 1)
        {
            return RedirectToAction("Index");
            
        }
        else
        {
            return View();
        }
    }

    public IActionResult Delete()
    {
        return View();
    }

}


//     [HttpPost]
// public IActionResult CreateUser(UsersEntity NewUser)
// {
//     try
//     {
//         if (ModelState.IsValid)
//         {
//             _db.Users.Add(NewUser);
//             _db.SaveChanges();

//             return RedirectToAction("Index");
//         }
//         // If ModelState.IsValid is false, return the view with validation errors
//         return View(NewUser);
//     }
//     catch (Exception ex)
//     {
//         // Handle or log the exception
//         ModelState.AddModelError("", "An error occurred while saving data: " + ex.Message);
//         return View(NewUser);
//     }
// }