using AutoMapper;
using JWT.Auth.Entities;
using JWT.Auth.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JWT.Auth.Entities.Context;
using JWT.Auth.Modules.Interafaces;

namespace JWT.Auth.Modules
{
    public class UserModule : IUserModule
    {
        #region Properties
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        IMapper mapper;
        JWTAuthContext context;
        #endregion

        #region Constractor
        public UserModule(JWTAuthContext Context, IMapper Mapper)
        {
            this.mapper = Mapper;
            this.context = Context;
        }
        #endregion

        #region Public Methods
        public async Task<User> AddAsync(CreateUserRequest CreateUserRequest)
        {
            var user = MapUserData(CreateUserRequest);
            
            AddRoles(user, CreateUserRequest);

            AddUserFields(user, CreateUserRequest);

            await context.User.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Update(UpdateUserRequest UpdateUserRequest)
        {
            //var userDetails = mapper.Map<User>(UpdateUserRequest);
            
            var user = await context.User.FirstOrDefaultAsync();
            user.IsActive = UpdateUserRequest.IsActive;

            return true;
        }

        public async Task<User> Get(long? Id)
        {
            if (Id == null) throw new NullReferenceException("Parameter Id is null.");

            var user = await context.User.FirstOrDefaultAsync(x => x.Id == Id);
            user.Password = string.Empty;

            ValidateIfUserFound(user);

            return user;
        }

        public async Task<long> Delete(long? id)
        {
            var user = await context.User.FirstOrDefaultAsync(x => x.Id == id);

            ValidateIfUserFound(user);

            user.IsActive = false;

            //context.Update(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<User> GetByCredentials(string Username, string Password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentNullException("Username or Password are empty.");

            return await context.User.Include(x => x.UserUserRole).ThenInclude(x => x.UserRole)
                                     .FirstOrDefaultAsync(x => x.Username == Username &&
                                                               x.Password == Password);
        }

        public async Task<long> Quantity()
        {
            return await context.Set<User>()
                                .AsQueryable()
                                .Where(x => x.IsActive == true)
                                .LongCountAsync();
        }
        #endregion

        #region Private Methods
        private User MapUserData(CreateUserRequest Request)
        {
            var user = mapper.Map<CreateUserRequest, User>(Request);

            user.IsActive = true;
            user.LastPasswordChangedDate = DateTime.UtcNow;
            user.CreateDate = DateTime.UtcNow;

            return user;
        }

        private void AddRoles(User User, CreateUserRequest Request)
        {
            foreach (var userUserRole in Request.UserRoles)
            {
                User.UserUserRole.Add(new UserUserRole()
                {
                    UserId = User.Id,
                    UserRoleId = userUserRole.UserRoleId,
                });
            }
        }

        private void AddUserFields(User User, CreateUserRequest Request)
        {
            foreach (var userFields in Request.UserFields)
            {
                User.UserField.Add(new UserField()
                {
                    FieldType = userFields.FieldType,
                    FieldName = userFields.FieldName,
                    FieldData = userFields.FieldData
                });
            }
        }

        private void ValidateIfUserFound(User user)
        {
            if (user == null)
                throw new Exception("User not found.");
        } 
        #endregion
    }
}
