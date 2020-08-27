using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlannerLib.DataBase;
using PlannerLib.Model;

namespace Planner.Controllers
{
    public class LoginController : Controller
    {

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        private IConfiguration _config;
        MysqlController<User> userDAL = new MysqlController<User>();

        [HttpGet]
        public IActionResult Login()
        {
            //List<User> users = userDAL.Reed().ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("Email", "Password")] User user)
        {
            IActionResult response = Unauthorized();

            //по каким пораметрам будем искать кортеж в таблице
            User existUser = userDAL.Reed(user, "Email", "Password");
            if (existUser.Id > 0)
            {
                var tokenStr = GenerateJSONWebToken(existUser);
                response = Ok(new { token = tokenStr });

                return RedirectToAction("Reg");
            }

            //return response;

            ModelState.AddModelError("Password", "You are not registered yet");
            return View();
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }


        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {

            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = indentity.Claims.ToList();

            var userName = claim[0].Value;

            return "Welcom " + userName;
        }


        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }

        //("name", "password", "email", "phone")
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reg([Bind] User user)
        {

            //добавить проверку на валидность данных

            //добавить проверку на существование почты

            //if (user.Name.Length > 5)
            //{
            //    ModelState.AddModelError("Name", "to long");
            //}
            if (ModelState.IsValid)
            {
                userDAL.Create(user);
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Some wrong " + user.ToString();
            }

            return View(user);
        }

        
        public IActionResult Edit(int id)
        {
            User user = userDAL.Reed(id);

            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] User user, int id = 0)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Valid";
                userDAL.Update(user);
                return RedirectToAction("Login");
            }
            ViewBag.Message = "No valid";
            return View(userDAL);
        }


        public IActionResult Delete(int id = 0)
        {
            int b = userDAL.Delete(id);
            return RedirectToAction("Login");

        }


        public IActionResult Error()
        {
            return NotFound();
            //return View();
        }
    }
}