using BinderLayer.DTO;
using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Identity;
using BL.Infrastructures;
using BL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager userManager;

        private readonly RoleManager roleManager;

        private readonly IRepository repo;


        public UserService(IRepository repo, UserManager userManager, RoleManager roleManager)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<OperationDetails> RemoveUser(string id)
        {
            var user = this.userManager.FindById(id);
            if (user == null)
                return new OperationDetails(false, "There is no such user");
            var result = await this.userManager.DeleteAsync(user);
            await repo.SaveAsync();
            return new OperationDetails(true, "User was deleted");
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = userManager.Users.ToList();
            var mapper = MapHelper.Mapping<User, UserDTO>();
            var userModels = mapper.Map<List<UserDTO>>(users);
            foreach (UserDTO user in userModels)
            {
                var us = userManager.FindById(user.Id);
                user.Name = us.Name;
                user.Password = us.PasswordHash;
                var roles = us.Roles.ToList();
                foreach (IdentityUserRole r in roles)
                {
                    user.Role = roleManager.FindById(r.RoleId).Name.ToString();
                }             
            }
            return userModels;
        }

        public UserDTO GetUsers(string email)
        {
            var user = userManager.FindByEmail(email);
            var mapper = MapHelper.Mapping<User, UserDTO>();
            return mapper.Map<UserDTO>(user);

        }

        public async Task<OperationDetails> Update(string id, UserDTO userDto)
        {
            var user = this.userManager.FindById(id);
            if (user == null)
                throw new NullReferenceException();
            if(userDto.Name != null)
                user.Name = userDto.Name;
            if (userDto.UserAvatar != null)
                user.UserAvatar = userDto.UserAvatar;
            var result = await userManager.UpdateAsync(user);
            await repo.SaveAsync();
            return new OperationDetails(true, "The user was succesfully updated");
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            if (userDto.IsProfessor)
            {
                return await CreateProfessor(userDto);
            }
            else
            {
                return await CreateStudent(userDto);
            }
        }

        public async Task<OperationDetails> CreateStudent(UserDTO userDto)
        {
            User user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new Student { Email = userDto.Email, UserName = userDto.Email };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
                await repo.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует");
            }
        }

        public async Task<OperationDetails> CreateProfessor(UserDTO userDto)
        {
            User user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new Professor { Email = userDto.Email, UserName = userDto.Email };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
                await repo.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await userManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await userManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new Role { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        /* public async Task UseAbility(StudentDTO userDto, AbilityModel ability) {
             User user = await userManager.FindByEmailAsync(userDto.Email);

             //await userSlotManager.GetUserSlot(); user.Id;
         }*/

        public async Task<OperationDetails> VerifyUser(string userId) {
            User user = userManager.FindById(userId);
            Professor professor = (Professor)user;
            professor.IsApplied = true;
            await userManager.UpdateAsync(professor);
            await repo.SaveAsync();
            return new OperationDetails(true, "The user was succesfully verified");
        }

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}
