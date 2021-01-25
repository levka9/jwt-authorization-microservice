using JWT.Auth.Models.Requests;
using JWT_Auth.Microservice.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT_Auth.Microservice.Entities;

namespace JWT.Auth.Modules.Interafaces
{
    public interface IUserModule
    {
        Task<User> AddAsync(CreateUserRequest CreateUserRequest);
        Task<bool> Update(UpdateUserRequest UpdateUserRequest, long UserId);
        Task<bool> SendPassword(SendPasswordRequest Request);
        Task UpdatePasswordAsync(long? UserId, string Password);
        Task<User> Get(long? Id);
        Task<User> GetByCredentials(UserGetByCredentialsRequest Request);
        Task<long> Delete(long? id);
        Task<long> Quantity();
    }
}
