using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Application.GrpServices;
using VetSystems.Account.Application.Models.Accounts;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class CreateUserCommand : IRequest<Response<bool>>
    {
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
        public string AppKey { get; set; }
        public string UserName { get; set; }
        public string[] Properties { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<bool>>
    {
        private readonly IIdentityRepository _identity;
        private readonly IdentityGrpService _identityGrpService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Domain.Entities.Userauthorization> _userAuthorizationRepository;

        public CreateUserCommandHandler(IIdentityRepository identity, IdentityGrpService identityGrpService, ILogger<CreateUserCommandHandler> logger, IRepository<Domain.Entities.Userauthorization> userAuthorizationRepository, IUnitOfWork uow, IRepository<User> userRepository)
        {
            _identity = identity;
            _identityGrpService = identityGrpService;
            _logger = logger;
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _userRepository = userRepository;
            _userAuthorizationRepository = userAuthorizationRepository ?? throw new ArgumentNullException(nameof(userAuthorizationRepository));
        }
        public async Task<Response<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var enterpriseId = _identity.Account.EnterpriseId;
                var userId = _identity.Account.UserId;

                var userRequest = new SignupRequestDto
                {
                    Password = "123654Dg",
                    Email = request.Email,
                    CompanyId = _identity.Account.EnterpriseId.ToString(),
                    FirtsName = request.FirstName,
                    LastName = request.LastName ?? "",
                    Roleid = request.RoleId ?? "",
                    TenantId = _identity.Account.TenantId.ToString(),
                    UserName = request.UserName,
                    AppKey = request.AppKey,
                    VknNumber = string.Empty,
                    //AuthorizeEnterprise = request.AuthorizeEnterprise.GetValueOrDefault(),
                    //IsLicenceAccount = request.IsLicenceAccount,
                    //ContactEmail = request.ContactEmail,
                    //AccountType = request.AccountType,
                };
                var userResult = await _identityGrpService.RegisterUserAsync(userRequest);
                if (userResult.IsSuccess)
                {
                    await _userRepository.AddAsync(new User
                    {
                        Email = request.Email,
                        RoleId =request.RoleId == null ? Guid.NewGuid() : Guid.Parse(request.RoleId),
                        EnterprisesId = enterpriseId,
                        CreateUser = _identity.Account.UserName,
                        CreateDate = DateTime.Now,
                        Firstname = request.FirstName,
                        Lastname = request.LastName ?? "",
                        Id = Guid.Parse(userResult.Id),
                        //Authorizeenterprise = request.AuthorizeEnterprise.GetValueOrDefault()
                    });
                    if (!String.IsNullOrEmpty(userResult.Id) && userResult.IsSuccess == true)
                    {
                        if (request.Properties != null)
                        {
                            if (request.Properties.Length > 0)
                            {
                                foreach (var item in request.Properties)
                                {
                                    var model = new Userauthorization()
                                    {
                                        UsersId = Guid.Parse(userResult.Id),
                                        PropertyId = Guid.Parse(item),
                                        RoleId = Guid.Parse(request.RoleId),
                                        EnterprisesId = enterpriseId,
                                        CreateDate = DateTime.Now,
                                    };
                                    await _userAuthorizationRepository.AddAsync(model);
                                }
                            } 
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(userResult.Message))
                        {
                            return Response<bool>.Fail($"user cannot saved : {userResult.Message}", 500);
                        }
                    }
                    await _uow.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    return Response<bool>.Fail($"user cannot saved : {userResult.Message}", 500);
                }



            }
            catch (RpcException ex)
            {
                _logger.LogInformation($"user can not saved : {ex.Message}");

                return Response<bool>.Fail($"user cannot saved : {ex.Message}", 500);

            }
            return Response<bool>.Success(200);
        }
    }
}
