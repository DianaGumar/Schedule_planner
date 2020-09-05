using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
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

                //return RedirectToAction("Main", "Table", new { userName = user.Name });
                return RedirectToAction("Main", "Task");
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



        private bool CheckUserValid(User user)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            if (user.Name == null)
                d.Add("Name", "name is requirement field");
            else if (user.Name.Length > 50)
                d.Add("Name", "to long");

            if (user.Password == null)
                d.Add("Password", "password is requirement field");
            else if (user.Password.Length > 50)
                d.Add("Password", "to long");

            if (user.Email == null)
                d.Add("Email", "email is requirement field");
            else if (user.Email.Length > 50)
                d.Add("Email", "to long");

            if (user.Phone == null) { }

            else if (user.Phone.Length > 20)
                d.Add("Phone", "to long");


            try
            {
                MailAddress m = new MailAddress(user.Email);
            }
            catch (FormatException e)
            {
                if (d.ContainsKey("Email"))
                {
                    d["Email"] = d["Email"] + ";\n" + e.Message;
                }
                else
                {
                    d.Add("Email", e.Message);
                }
                
            }

            if (d.Count != 0)
            {
                foreach (KeyValuePair<string, string> entry in d)
                {
                    ModelState.AddModelError(entry.Key, entry.Value);
                }
                
                return false;
            }
            else return true;

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reg([Bind] User user)
        {

            if (ModelState.IsValid && CheckUserValid(user))
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
            if (ModelState.IsValid && CheckUserValid(user))
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