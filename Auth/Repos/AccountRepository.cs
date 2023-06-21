using Auth.AuthExeptions;
using Auth.Exceptions;
using Auth.Hepers;
using Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Share.Models;
using Share.UnitOfWork;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repos
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<UserDentity> _userManager;
        private readonly SignInManager<UserDentity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountRepository(
            UserManager<UserDentity> userManager,
            SignInManager<UserDentity> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager







        )
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;



        }
        // thay đổi password    Change Password (itself, SuperAdmin)
        public async Task<bool> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {

            var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);
            if (user == null)
            {
                return false;

            }
            else if (changePasswordModel.NewPassword != changePasswordModel.ConfirmNewPassword)
            {
                return false;
            }
            else
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, changePasswordModel.Password);
                if (checkPassword)
                {
                    await _userManager.ChangePasswordAsync(user, changePasswordModel.Password, changePasswordModel.NewPassword);
                  return true;
                }
            return false;
            }
        }
        // Start : thay đổi tên role ex user : users
        public async Task<ResponseViewModel> ChangeRoleAsync(string roleName, string newRoleName)
        {


            var roleCheck = await _roleManager.RoleExistsAsync(roleName);
            var getRoleId = await _roleManager.FindByNameAsync(roleName);

            if (roleCheck != null)
            {
                getRoleId.Name = newRoleName;
                var result = await _roleManager.UpdateAsync(getRoleId);
                if (result.Succeeded)
                {
                    throw new AuthOKException("Cập nhật quyền thành công");

                }
                else
                {
                    throw new AuthBadRequestException("Có lỗi khi được cập nhật quyền");
                }
            }
            else
            {
                // var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                throw new AuthNotFoundException($"Quyền {roleName} chưa  tồn tại");
            }



        }
        // End : thay đổi role
        // Start : Thêm mới role 
        public async Task<string> CreateRoleAsync(string roleName)
        {

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (roleResult.Succeeded)
                {
                    throw new AuthOKException($"Tạo quyền {roleName} thành công");
                }
                else
                {
                    throw new AuthBadRequestException($"Có lỗi khi tạo quyền {roleName}");
                }
            }
            throw new AuthBadRequestException($" {roleName} đã tồn tại");
        }
        // End : tạo mới role

        // Start :  SPAdmin Cấp tài khoản cho người dùng User 
        //If SuperAdmin/Manager register a account for other user will provide:
        //UserName
        //Email
        //Role
        //Status of user's account is InActive
        public async Task<string> CreateUserRoleAsync(string userName, string email, string roleName)
        {
            StringBuilder mess = new StringBuilder();
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.Active = false;
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    throw new AuthOKException($"Đã thêm {user.Email} vào quuyền {roleName}");

                }
                else
                {
                    throw new AuthBadRequestException($"Có lỗi khi thêm quyền {roleName} vào {userName}");
                }
            }
            throw new AuthBadRequestException($"{email} không tồn tại");
        }
        // End :  SPAdmin Cấp tài khoản cho người dùng User 

        // Start : Delete Account (SuperAdmin): SuperAdmin cannot remove itself
        public async Task<ResponseViewModel> DeleteUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user); // kiem tra role co ton tai chua\

            if (user != null)
            {
                foreach (var role in roles)
                {
                    if (role == "SuperAdmin")
                    {

                        throw new AuthBadRequestException($" {role} tồn tại không thể xóa!");
                    } // khong the xoa role SuperAdmin
                }
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {

                    throw new AuthOKException($"Xóa tài khoản thành công");
                }

                throw new AuthBadRequestException($"Có lỗi khi Xóa tài khoản có email là {email}");
            }

            throw new AuthNotFoundException($"Không tìm thấy email {email} !");
        }
        // End : Delete Account (SuperAdmin): SuperAdmin cannot remove itself
        // Start : Change Role for a user (SuperAdmin)

        public async Task<ResponseViewModel> EditUserRoleAsync(string email, EnumRoles roleNames)
        {
            // roleName = 1 SPAdmin, 2,Manage,3:User
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user); // kiem tra role co ton tai chua


            if (user != null)
            {


                foreach (var role in roles)
                {

                    if (role != roleNames.ToString())
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                        var result = await _userManager.AddToRoleAsync(user, roleNames.ToString());
                        if (result.Succeeded)
                        {

                            throw new AuthOKException($"Cập nhật thành công quyền {roleNames} cho {email}");
                        }
                    }
                    else
                    {
                        throw new AuthBadRequestException($" Quyền {roleNames} đã tồn tại trong {email}");
                    }



                }



            }
            throw new AuthNotFoundException($"Có lỗi khi cấp nhật quyền cho {email} !");
        }
        // End : Change Role for a user (SuperAdmin)
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<List<UserDentity>> GetAllUsersAsync()
        {
            var user = await _userManager.Users.ToListAsync();
            return user;
        }

        public async Task<IList<string>> GetUserRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            // Get the roles for the user
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
        // get user with email
        public async Task<UserDentity> GetUserWithEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        // update use with email
        public async Task<bool> UpdateUserWithEmailAsync(string email, UserModelUpdate userDentity)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            existingUser.FirstName = userDentity.FirstName;
            existingUser.LastName = userDentity.LastName;
            existingUser.Birthday = userDentity.Birthday;
            existingUser.PhoneNumber = userDentity.PhoneNumber;
         

            try
            {
                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                   return true;
                }
                else
                {
                   return false;

                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
    
                var updatedUser = await _userManager.FindByEmailAsync(email);
                await _userManager.UpdateAsync(updatedUser);

               return true;
            }
        }


        public async Task<ResponseViewModel> LogoutAsync(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {

                await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", ".AspNetCore.Identity.Application");

                throw new AuthOKException($"Đăng xuất thành công");
            }
            throw new AuthBadRequestException("Co lỗi xảy ra khi đăng xuất");
        }

        public async Task<string> RemoveUserRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            StringBuilder mess = new StringBuilder();
            if (user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {


                    throw new AuthOKException($"Đã xóa quuyền {roleName} của {user.Email} ");

                }
                else
                {
                    throw new AuthOKException($"Có lỗi khi  xóa quuyền {roleName} của {user.Email} ");
                }

            }
            throw new AuthNotFoundException($"Không tìm thấy email {email} !");
        }

        public async Task<ResponseViewModel> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                throw new AuthNotFoundException($"Không tìm thấy {resetPasswordModel.Email}!");
            }
            else if (resetPasswordModel.NewPassword != resetPasswordModel.ConfirmNewPassword)
            {
                throw new AuthBadRequestException($"Mật khẩu không khớp !");
            }
            else
            {
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
                if (result.Succeeded)
                {

                    throw new AuthOKException($"Reset mật khẩu thành công !");
                }
                else
                {
                    throw new AuthOKException($"Reset mật khẩu không thành công !");
                }
            }

        }
        // 
        public async Task<string> ForgotPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {

                throw new AuthNotFoundException($"Không tìm thấy {email}!");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {


            // lockoutOnFailure = true cho phép sửa đổi db AccessFailedCount và LockoutEnd trong bảng AspNetUsers
            var user = await _userManager.FindByEmailAsync(model.Email);
            var maxFailed = _configuration.GetSection("MaxFailed").Value;


            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var roles = _userManager.GetRolesAsync(user).Result.ToList();
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,model.Email),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    foreach (var userRole in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        expires: DateTime.Now.AddMinutes(10),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                    );
                    await _signInManager.SignInAsync(user, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                        IsPersistent = false,
                        AllowRefresh = false
                    });

                    return new JwtSecurityTokenHandler().WriteToken(token);

                }

                else if (result.IsLockedOut)
                {
                    throw new AuthBadRequestException($"Tai khoản đã bị khóa do nhập sai {maxFailed} lần");
                }


                throw new AuthBadRequestException($"Mật khẩu sai vui lòng nhập mật khẩu đúng  !");

            }
            else
            {
                throw new AuthNotFoundException($"{model.Email} không tồn tại !");

            };


        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new UserDentity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                DateCreated = DateTime.Now,
                Active = model.Active

            };
            if (model.Password != model.ConfirmPassword)
            {
                throw new AuthBadRequestException("Mật khẩu xác nhận không khớp");
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            // dafault role user
            if (result.Succeeded)
            {
                var userManager = await _userManager.FindByEmailAsync(model.Email);

                await _userManager.AddToRoleAsync(userManager, "user");
            }
            else
            {
                throw new AuthUnauthorizedAccessException("Đăng kí thất bại");
            }
            return result;
        }

        public async Task HandleActive(string email, bool checkActive)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.Active = checkActive;


        }

        public async Task HandleBlock(string email, bool checkBlock)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (checkBlock)
            {
                // Khóa
                var endDate = DateTime.Now + TimeSpan.FromMinutes(1);
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, endDate);
                throw new AuthOKException("Đã khóa tài khoản thành công ");


            }
            else
            {
                //Mở khóa
                await _userManager.SetLockoutEnabledAsync(user, false);
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now - TimeSpan.FromMinutes(1));
                throw new AuthOKException("Đã mở khóa thành công ");
            }

        }
    }
}
