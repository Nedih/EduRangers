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
using System.Web.Http.Cors;

namespace EduRangers.Controllers
{
    //[Authorize]
    //[EnableCors(origins: "http://localhost:3000/", headers: "", methods: "")]
    [RoutePrefix("api/Account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        ///[EnableCors(origins: "http://localhost:3000/", headers: "", methods: "")]
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
        //[EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
        public IEnumerable<UserDTO> Get()
        {

            return this.UserService.GetUsers();
        }

        [Route("Profile")]
        [HttpGet]
        //[EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
        public UserDTO GetUser(string email)
        {

            return this.UserService.GetUsers(email);
        }

        [HttpDelete]
        public async Task<OperationDetails> Delete(string id)
        {

            return await this.UserService.RemoveUser(id);
        }

        [HttpPut]
        public async Task<OperationDetails> Update(string email, UserDTO user)
        {

            return await this.UserService.Update(email, user);
        }

        [Route("Login")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //[ValidateAntiForgeryToken]
        public async Task<OperationDetails> Login(LoginModel model)
        {
            //await SetInitialDataAsync();
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

        [Route("Register")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //[ValidateAntiForgeryToken]
        public async Task<OperationDetails> Register([FromBody]RegisterModel model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    IsProfessor = model.IsProfessor,
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
        public async Task<OperationDetails> Verify(string id)
        {
             return await UserService.VerifyUser(id);
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
