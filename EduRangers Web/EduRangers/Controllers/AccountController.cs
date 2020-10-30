using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using EduRangers.Models;
using EduRangers.Providers;
using EduRangers.Results;
using BL.Interfaces;
using BL.Identity;
using BinderLayer.Models;
using BL.Infrastructures;
using BinderLayer.DTO;

namespace EduRangers.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserService UserService;
        public AccountController(IUserService UserService)
        {
            this.UserService = UserService;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {

            return this.UserService.GetUsers();
        }

        [HttpPut]
        public async Task<OperationDetails> Update(string id, UserDTO user)
        {

            return await this.UserService.Update(id, user);
        }

        [Route("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<OperationDetails> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return new OperationDetails(true, "You've been successfully logged in");
                }
            }
            return new OperationDetails(false, "You were banned");
        }

        public OperationDetails Logout()
        {
            AuthenticationManager.SignOut();
            return new OperationDetails(true, "You have successfuly logged out");
        }

        [Route("RegisterProfessor")]
        //[ValidateAntiForgeryToken]
        public async Task<OperationDetails> RegisterProfessor(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return new OperationDetails(true, "Successful Registration");
                else
                    ModelState.AddModelError(operationDetails.Message, operationDetails.Message);
            }
            return new OperationDetails(false, "404, you have died");
        }
        [Route("RegisterStudent")]
        //[ValidateAntiForgeryToken]
        public async Task<OperationDetails> RegisterStudent(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return new OperationDetails(true, "Successful Registration");
                else
                    ModelState.AddModelError(operationDetails.Message, operationDetails.Message);
            }
            return new OperationDetails(false, "404, you have died");
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Семен Семенович Tushe",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}
