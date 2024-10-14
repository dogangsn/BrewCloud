using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;


namespace BrewCloud.Vet.Application.Features.Reports.Commands
{
   
    public class CreateReportFilterCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public string FilterJson { get; set; }
    }
    public class CreateReportFilterCommandHandler : IRequestHandler<CreateReportFilterCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<VetReportFilter> _reportFilterRepository;

        public CreateReportFilterCommandHandler(IUnitOfWork uow, IIdentityRepository identityRepository, IRepository<VetReportFilter> reportFilterRepository)
        {
            _uow = uow;
            _identityRepository = identityRepository;
            _reportFilterRepository = reportFilterRepository;
        }

        public async Task<Response<Guid>> Handle(CreateReportFilterCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<Guid>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var account = _identityRepository.Account;
                var reportFilter = new VetReportFilter
                {   
                    FilterJson = request.FilterJson,
                    Name = request.Name,
                    EnterprisesId = account.EnterpriseId,
                    CreateDate = DateTime.Now,
                    //CreatedBy = account.Email

                };

                await _reportFilterRepository.AddAsync(reportFilter);
                await _uow.SaveChangesAsync();
                response.Data = reportFilter.Id;
            }
            catch (Exception ex)
            {

                response.ResponseType = ResponseType.Error;
                response.IsSuccessful = false;
                //response.Message = ex.Message;
            }

            return response;
        }
    }
}
