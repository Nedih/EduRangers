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
        Task<OperationDetails> Update(string id, UserDTO userDto);
        Task<OperationDetails> Create(UserDTO userDto);
        Task<OperationDetails> CreateStudent(StudentDTO userDto);
        Task<OperationDetails> CreateProfessor(ProfessorDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task UseAbility(StudentDTO userDto, AbilityModel ability);
    }
}
