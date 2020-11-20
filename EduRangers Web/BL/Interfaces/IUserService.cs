using BinderLayer.DTO;
using BinderLayer.Models;
using BL.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUsers(string email);
        Task<OperationDetails> RemoveUser(string id);
        Task<OperationDetails> Update(string id, UserDTO userDto);
        Task<OperationDetails> Create(UserDTO userDto);
        Task<OperationDetails> CreateStudent(UserDTO userDto);
        Task<OperationDetails> CreateProfessor(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        //Task UseAbility(StudentDTO userDto, AbilityModel ability);
        Task<OperationDetails> VerifyUser(string userId);
    }
}
