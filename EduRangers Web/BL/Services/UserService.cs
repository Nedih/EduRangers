using BinderLayer.Interfaces;
using BL.DTO;
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

        public UserService(IClientManager clientManager, IRepository repo, UserManager userManager, RoleManager roleManager)
        {
            this.clientManager = clientManager;
            this.repo = repo;
            this.userManager = userManager;
            this.roleManager = roleManager;

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

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}
