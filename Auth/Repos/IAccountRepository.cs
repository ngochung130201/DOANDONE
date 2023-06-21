
using Auth.Hepers;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share.ViewModels;
namespace Auth.Repos
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        public Task<string> SignInAsync(SignInModel signInModel);
        public Task<ResponseViewModel> LogoutAsync(string email);
        public Task<IList<string>> GetUserRolesAsync(string email);
        public Task<List<IdentityRole>> GetAllRolesAsync();
        public Task<List<UserDentity>> GetAllUsersAsync();
        public Task<UserDentity> GetUserWithEmailAsync(string email);
        public Task<bool> UpdateUserWithEmailAsync(string email, UserModelUpdate userDentity);
        public Task<ResponseViewModel> DeleteUserAsync(string email);
        public Task<string> CreateRoleAsync(string roleName);
        public Task<ResponseViewModel> ChangeRoleAsync(string roleName,string newRoleName);
        public Task<string> CreateUserRoleAsync(string userName, string email, string roleName);
        public Task<ResponseViewModel> EditUserRoleAsync(string email, EnumRoles roleName);
        public Task<string> RemoveUserRoleAsync(string email,string roleName);
        public Task<bool> ChangePasswordAsync(ChangePasswordModel changePasswordModel);

        public Task<ResponseViewModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
        public Task<string> ForgotPasswordTokenAsync(string email);

        public Task  HandleActive(string email, bool checkActive);
        public Task HandleBlock(string email, bool checkBlock);
    }
}
