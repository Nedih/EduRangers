using BinderLayer.DTO;
using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Identity;
using BL.Infrastructures;
using BL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
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

        private readonly IClientManager clientManager;

        private readonly IRepository repo;

        private readonly UserSlotManager userSlotManager;

        public UserService(IClientManager clientManager, IRepository repo, UserManager userManager, RoleManager roleManager, UserSlotManager userSlotManager)
        {
            this.clientManager = clientManager;
            this.repo = repo;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userSlotManager = userSlotManager;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = userManager.Users.ToList();
            var mapper = MapHelper.Mapping<User, UserDTO>();
            return mapper.Map<List<UserDTO>>(users);
        }

        public async Task<OperationDetails> Update(string id, UserDTO userDto)
        {
            var user = this.userManager.FindById(id);
            if (user == null)
                throw new NullReferenceException();
            user.Email = userDto.Email;
            user.UserName = userDto.UserName;
            var result = await userManager.UpdateAsync(user);
            ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
            clientManager.Update(clientProfile);
            await repo.SaveAsync();
            return new OperationDetails(true, "The user was succesfully updated");
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                // добавляем роль
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                clientManager.Create(clientProfile);
                await repo.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует");
            }
        }

        public async Task<OperationDetails> CreateStudent(StudentDTO userDto)
        {
            User user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new Student { Email = userDto.Email, UserName = userDto.Email };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                // добавляем роль
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                clientManager.Create(clientProfile);
                await repo.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует");
            }
        }

        public async Task<OperationDetails> CreateProfessor(ProfessorDTO userDto)
        {
            User user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new Professor { Email = userDto.Email, UserName = userDto.Email };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                // добавляем роль
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                clientManager.Create(clientProfile);
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
            // var mapper = MapHelper.Mapping<User, Professor>();
            //var professor = mapper.Map<Professor>(user);
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
