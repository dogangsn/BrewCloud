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
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;

namespace VetSystems.Account.Application.Features.Account.Commands
{
    public class ComplateSubscriptionCommand : IRequest<Shared.Dtos.Response<bool>>
    {
        public Guid RecId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string ConnectionString { get; set; }
        public bool IsFirstCreate { get; set; }
        public string FirstLastName { get; set; }
    }
    public class ComplateSubscriptionCommandHandler : IRequestHandler<ComplateSubscriptionCommand, Shared.Dtos.Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Enterprise> _enterPriseRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Company> _companyRepository;

        private readonly ILogger<ComplateSubscriptionCommandHandler> _logger;
        private readonly IdentityGrpService _identityGrpService;
        private readonly IMediator _mediatR;
        private readonly IRepository<Rolesetting> _roleSettingRepository;

        public ComplateSubscriptionCommandHandler(IUnitOfWork uow, 
                                                  ILogger<ComplateSubscriptionCommandHandler> logger, 
                                                  IdentityGrpService identityGrpService, 
                                                  IRepository<Customer> customerRepository, 
                                                  IMediator mediatR, 
                                                  IRepository<User> userRepository, 
                                                  IRepository<Enterprise> enterPriseRepository,
                                                  IRepository<Company> companyRepository,
                                                  IRepository<Rolesetting> roleSettingRepository)        
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityGrpService = identityGrpService ?? throw new ArgumentNullException(nameof(identityGrpService));
            _customerRepository = customerRepository;
            _mediatR = mediatR;
            _userRepository = userRepository;
            _enterPriseRepository = enterPriseRepository;
            _companyRepository = companyRepository;
            _roleSettingRepository = roleSettingRepository;
        }


        public async Task<Shared.Dtos.Response<bool>> Handle(ComplateSubscriptionCommand request, CancellationToken cancellationToken)
        {

            var response = Shared.Dtos.Response<bool>.Success(200);
            _uow.CreateTransaction();

            try
            {
                var entity = CreateEnterprise(request);

                var property = CreateProperty(request, entity);

                var adminSetting = CreateDefaultRole(entity, "Admin", true);

                var company = CreateCompany(request);

                var companyResult = await _companyRepository.AddAsync(company);

                var roleSettingResult = await _roleSettingRepository.AddAsync(adminSetting);

                await _uow.SaveChangesAsync(cancellationToken);

                var userResult = await _identityGrpService.CreateUserAsync(request.Username,
                                                                  request.Password,
                                                                  request.Username,
                                                                  entity.Id.ToString(),
                                                                  request.FirstLastName,
                                                                  "",
                                                                  adminSetting.Id.ToString(),
                                                                  request.RecId.ToString(),
                                                                  Identity.Api.SignupRequest.Types.AccountType.Admin, request.IsFirstCreate);


                if (!userResult.IsSuccess)
                {
                    return Shared.Dtos.Response<bool>.Fail(userResult.Message, (int)ResponseType.Error);
                }

                await _userRepository.AddAsync(new User
                {
                    Lastname = request.FirstLastName,
                    Id = Guid.Parse(userResult.Id),
                    RoleId = adminSetting.Id,
                    Email = request.Email,
                    EnterprisesId = property.EnterprisesId,
                    Authorizeenterprise = true
                });
                //var user = new Userauthorization
                //{
                //    CreateDate = DateTime.Now,
                //    EnterprisesId = entity.Id,
                //    PropertyId = property.Id,
                //    UsersId = Guid.Parse(userResult.Id),
                //    RoleId = adminSetting.Id
                //};
                //entity.Userauthorizations.Add(user);
                //_enterPriseRepository.Update(entity);

                response.Data = true;
                response.IsSuccessful = true;
                response.ResponseType = ResponseType.Ok;

                await _uow.SaveChangesAsync(cancellationToken);
                _uow.Commit();
            }
            catch (Exception)
            {
                _uow.Rollback();
                response.ResponseType = ResponseType.Error;
                response.Data = false;

            }

            return response;


        }

        private Domain.Entities.Rolesetting CreateDefaultRole(Enterprise entity, string code, bool isAdmin)
        {
            return new Domain.Entities.Rolesetting
            {
                Id = Guid.NewGuid(),
                Installdevice = true,
                Rolecode = code,//"Standart",
                EnterprisesId = entity.Id,
                IsEnterpriseAdmin = isAdmin,
                CreateDate = DateTime.Now,
                DashboardPath = "",
                CreateUser = "",

            };
        }

        private Enterprise CreateEnterprise(ComplateSubscriptionCommand request)
        {
            var entity = new Enterprise
            {
                Id = request.RecId,
                Currencycode = "TRY",//request.Currencycode,
                Phone = request.Phone,
                Enterprisename = request.Company,
                Defaultlanguage = "".ToLower(),
                Translationlanguage = $"[\"{"".ToLower()}\"]",
                Timezone = "",
                Reasons = new List<Reason>
                {
                    new Reason
                    {
                        EnterprisesId = request.RecId,
                        Kind=KindType.Returned,
                        Name = "Geç Servis"
                    },

                },
                Abilitygroups = new List<Abilitygroup>
                {
                    new Abilitygroup
                    {
                        Id = Guid.NewGuid(),
                        Groupname ="Kullanıcı"
                    },
                    new Abilitygroup
                    {
                       Id = Guid.NewGuid(),
                       Groupname ="Yönetici"
                    },
                    new Abilitygroup
                    {
                       Id = Guid.NewGuid(),
                        Groupname ="Pos Kullanıcısı Değil"
                    },
                }

            };



            return entity;
        }

        private Domain.Entities.Property CreateProperty(ComplateSubscriptionCommand request, Enterprise entity)
        {
            Guid propertyId = Guid.NewGuid();
            Guid revenuCenterId = Guid.NewGuid();
            var property = new Domain.Entities.Property
            {
                Id = propertyId,
                Defaultlang = "",
                Currency = "TRY",
                Propertyname = request.Company,
                CompanyTitle = request.Company,
                Timezone = "",
                Endoftheday = "",
                TimezoneownerId = Guid.NewGuid(), //request.TimeZoneOwnerId,
                Thousandseperator = ".",
                Symbolseperator = ",",
                TaxNumber = "11111111111",
                DefaultDate = DateTime.Today.Date
            };

            return property;
        }

        private Company CreateCompany(ComplateSubscriptionCommand request)
        {
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                CompanyName = request.Company,
                CreateUser = request.Email,
                CreateDate= DateTime.Today.Date,
                CompanyTitle = request.Company,
                Deleted = false,
                UpdateDate = new DateTime(1900,1,1)
            };
            return company;
        }

    }
}
