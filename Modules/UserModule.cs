using AutoMapper;
using JWT.Auth.Entities;
using JWT.Auth.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JWT.Auth.Modules
{
    public class UserModule
    {
        #region Properties
        IMapper mapper;
        JWTAuthContext context;
        #endregion
        
        public UserModule(JWTAuthContext Context, IMapper Mapper)
        {
            this.mapper = Mapper;
            this.context = Context;
        }

        public async Task<User> Add(CreateUserRequest CreateUserRequest)
        {
            var user = mapper.Map<CreateUserRequest, User>(CreateUserRequest);

            user.IsActive = true;
            user.LastPasswordChangedDate = DateTime.UtcNow;            
            user.CreateDate = DateTime.UtcNow;
            
            try
            {
                await context.User.AddAsync(user);
                await context.SaveChangesAsync();

                foreach (var userUserRole in CreateUserRequest.UserRoles)
                {
                    user.UserUserRole.Add(new UserUserRole()
                    {
                        UserId = user.Id,
                        UserRoleId = userUserRole.UserRoleId
                    });
                }

                foreach (var userFields in CreateUserRequest.UserFields)
                {
                    user.UserField.Add(new UserField()
                    { 
                        FieldTypeName = userFields.FieldTypeName,
                        FieldData = userFields.FieldData
                    });
                }

                context.User.Update(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO: log
                throw ex;
            }
            
            return user;
        }
    }
}
