using JWT.Auth.Entities;
using JWT.Auth.Models.Requests;
using JWT_Auth.Microservice.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Modules.Interafaces
{
    public interface IUserModule
    {
        Task<User> AddAsync(CreateUserRequest CreateUserRequest);
        Task<bool> Update(UpdateUserRequest UpdateUserRequest);
        Task<User> Get(long? Id);
        Task<User> GetByCredentials(UserGetByCredentialsRequest Request);
        Task<long> Delete(long? id);
        Task<long> Quantity();
    }
}
