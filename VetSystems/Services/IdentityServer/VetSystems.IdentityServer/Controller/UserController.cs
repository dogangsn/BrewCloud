using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Services.Interface;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Controllers;
using VetSystems.Shared.Service;

namespace VetSystems.IdentityServer.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : VetSystemsController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;
        private readonly IIdentityRepository _identityRepository;
        private readonly IAccountService _accountService;


        public UserController(UserManager<ApplicationUser> userManager,
                                IPublishEndpoint publishEndpoint,
                                IConfiguration configuration,
                                IAccountService accountService,
                                IIdentityRepository identityRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new Infrastructure.Entities.ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                Account = new Accounts
                {
                    CompanyId = signupDto.CompanyId,
                    AccountType = AccountType.User,
                    FirstName = signupDto.FirstName,
                    LastName = signupDto.LastName,
                    ContactEmail = "",
                    UserName = signupDto.Email

                }
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }
            return Ok(Shared.Dtos.Response<bool>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SignupDto signupDto)
        {

            var user = await _userManager.FindByIdAsync(signupDto.UserId);

            if (user == null)
                return BadRequest(Shared.Dtos.Response<bool>.Fail("user not found", 400));

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }


            return Ok(Shared.Dtos.Response<bool>.Success(204));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] SignupDto signupDto)
        {
            if (ModelState.IsValid)
            {
                var checkUser = await _userManager.FindByEmailAsync(signupDto.Email.Trim());
                if (checkUser != null && !String.IsNullOrEmpty(checkUser.Email))
                {
                    return ReturnResult(Shared.Dtos.Response<bool>.Fail("E-mail address is already taken ", 200));
                }


                var user = new Infrastructure.Entities.ApplicationUser
                {
                    UserName = signupDto.UserName,
                    Email = signupDto.Email,
                    Account = new Accounts
                    {
                        CompanyId = signupDto.CompanyId,//Guid.NewGuid().ToString(),
                        AccountType = AccountType.User,
                        FirstName = signupDto.FirstName,
                        LastName = signupDto.LastName,
                        AuthorizeEnterprise = signupDto.AuthorizeEnterprise,
                        RoleId = signupDto.RoleId,
                        Passive = false,
                        CreatedDate = DateTime.Now,
                        CreatedBy = ""
                    }
                };


                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    ////var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    ////code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    ////string codeHtmlVersion = code;// WebUtility.UrlEncode(code);// code.EncryptStringAsUrl(); //WebUtility.UrlEncode(code);
                    ////string baseUrl = _configuration["VeboniSPAUrl"];
                    ////string callbackUrl = HtmlEncoder.Default.Encode($"{baseUrl}/auth/reset-password/{user.Id}/{codeHtmlVersion}/1");
                    ////var eventMessage = new SendMailEvent
                    ////{
                    ////    To = user.Email,
                    ////    Subject = "Account Confirmation.",
                    ////    Body = "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\" target=\"_blank\">link</a>"
                    ////};
                    ////await _publishEndpoint.Publish<SendMailEvent>(eventMessage);
                    ////return ReturnResult(Shared.Dtos.Response<bool>.Success(200));
                }
                else
                {
                    return ReturnResult(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
                }
                //AddErrors(result);
            }


            // If we got this far, something failed, redisplay form
            return ReturnResult(Shared.Dtos.Response<bool>.Success(200));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody] SignupDto signupDto)
        {
            if (ModelState.IsValid)
            {


                var user = await _userManager.FindByIdAsync(signupDto.UserId);

                if (user == null)
                {
                    return ReturnResult(Shared.Dtos.Response<bool>.Fail("user not found", 400));
                }

                user.Account = await _accountService.GetAccountById(user.Id);
                user.Account.FirstName = signupDto.FirstName;
                user.Account.LastName = signupDto.LastName;
                //  user.Account.AccountType = (AccountType)signupDto.AccountType;
                user.Account.AuthorizeEnterprise = signupDto.AuthorizeEnterprise;
                user.Account.RoleId = signupDto.RoleId;
                user.Account.ModifiedBy = "";//_identityRepository.Account.UserId.ToString();
                user.Account.ModifiedDate = DateTime.Now;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return ReturnResult(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
                }

                return ReturnResult(Shared.Dtos.Response<bool>.Success(200));

                //AddErrors(result);
            }


            // If we got this far, something failed, redisplay form
            return ReturnResult(Shared.Dtos.Response<bool>.Success(200));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            if (ModelState.IsValid)
            {



                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return ReturnResult(Shared.Dtos.Response<bool>.Fail("user not found", 400));

                ////var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                ////code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                ////string codeHtmlVersion = code;// WebUtility.UrlEncode(code);// code.EncryptStringAsUrl(); //WebUtility.UrlEncode(code);
                ////string baseUrl = _configuration["VeboniSPAUrl"];
                ////string callbackUrl = HtmlEncoder.Default.Encode($"{baseUrl}/auth/reset-password/{user.Id}/{codeHtmlVersion}");
                ////var eventMessage = new SendMailEvent
                ////{
                ////    To = user.Email,
                ////    Subject = "Reset Password. ",
                ////    Body = "Please click this link: <a href=\"" + callbackUrl + "\" target=\"_blank\">link</a>"
                ////};
                ////await _publishEndpoint.Publish<SendMailEvent>(eventMessage);


                ////return ReturnResult(Shared.Dtos.Response<bool>.Success(200));


            }


            // If we got this far, something failed, redisplay form
            return ReturnResult(Shared.Dtos.Response<bool>.Success(200));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ReturnResult(Shared.Dtos.Response<bool>.Fail("user not found", 400));

            var b = WebEncoders.Base64UrlDecode(code);
            string token = Encoding.UTF8.GetString(b);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return ReturnResult(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }


            return Ok(Shared.Dtos.Response<bool>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] Models.ResetPasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return ReturnResult(Shared.Dtos.Response<bool>.Fail("user not found", 400));



            IdentityResult result;


            if (model.Confirm == "1")
                result = await _userManager.AddPasswordAsync(user, model.Password);
            else
            {
                var b = WebEncoders.Base64UrlDecode(model.Token);
                string token = Encoding.UTF8.GetString(b);
                result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            }

            if (!result.Succeeded)
            {
                return ReturnResult(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }


            return Ok(Shared.Dtos.Response<bool>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] Models.ChangePasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ReturnResult(Shared.Dtos.Response<bool>.Fail("user not found", 400));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, model.Password);


            if (!result.Succeeded)
            {
                return ReturnResult(Shared.Dtos.Response<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }


            return Ok(Shared.Dtos.Response<bool>.Success(204));
        }
    }
}
