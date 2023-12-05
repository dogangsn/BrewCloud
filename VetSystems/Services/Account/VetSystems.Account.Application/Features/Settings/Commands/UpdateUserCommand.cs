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
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class UpdateUserCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string AppKey { get; set; }
        public bool? AuthorizeEnterprise { get; set; }
        public bool IsLicenceAccount { get; set; }
        public Guid? TitleId { get; set; }
        public bool? Active { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<bool>>
    {

        private readonly IIdentityRepository _identity;
        private readonly IdentityGrpService _identityGrpService;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IRepository<User> _userrepository;
        private readonly IUnitOfWork _uow;

        public UpdateUserCommandHandler(IIdentityRepository identity, IdentityGrpService identityGrpService, ILogger<UpdateUserCommandHandler> logger, IRepository<Domain.Entities.Userauthorization> userAuthorizationRepository, IUnitOfWork uow, IRepository<User> userrepository)
        {
            _identity = identity;
            _identityGrpService = identityGrpService;
            _logger = logger;
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _userrepository = userrepository;
        }


        public async Task<Response<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var enterpriseId = _identity.Account.EnterpriseId;
                var userResult = await _identityGrpService.UpdateUserAsync(request.UserName,
                                                  "1",
                                                  request.UserName,
                                                  enterpriseId.ToString(),
                                                  "",
                                                  "",
                                                  request.RoleId,
                                                  request.AuthorizeEnterprise.GetValueOrDefault(), request.IsLicenceAccount, "", request.AppKey);

                if (userResult.IsSuccess == true)
                {
                    var user = await _userrepository.GetByIdAsync(Guid.Parse(request.UserId));
                    if (user != null)
                    {
                        user.Firstname = request.FirstName;
                        user.Lastname = request.LastName;
                        user.RoleId = Guid.Parse(request.RoleId);
                        user.Authorizeenterprise = request.AuthorizeEnterprise.GetValueOrDefault();
                        user.Title = request.TitleId;
                        user.Active = request.Active.GetValueOrDefault();
                    }
                    else
                    {
                        await _userrepository.AddAsync(new User
                        {
                            Firstname = request.FirstName,
                            Lastname = request.LastName,
                            Email = request.Email,
                            Authorizeenterprise = request.AuthorizeEnterprise.GetValueOrDefault(),
                            RoleId = Guid.Parse(request.RoleId),
                            EnterprisesId = enterpriseId,
                            CreateUser = _identity.Account.UserName,
                            CreateDate = DateTime.Now,
                            Id = Guid.Parse(request.UserId),
                            Title = request.TitleId,
                            Active = request.Active.GetValueOrDefault(),
                        });
                    }
                }
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (RpcException ex)
            {
                _logger.LogInformation($"user can not saved : {ex.Message}");
                return Response<bool>.Fail("user cannot saved ", 500);

            }
            return Response<bool>.Success(200);
        }
    }
}
