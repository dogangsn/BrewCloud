using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Demands.DemandProducts;
using VetSystems.Vet.Application.Models.Parameters;
using VetSystems.Vet.Domain.Contracts;
namespace VetSystems.Vet.Application.Features.Settings.Parameters.Queries
{

    public class ParametersListQuery : IRequest<Response<List<ParametersDto>>>
    {
        //public int RecId { get; set; }
        public Guid? id { get; set; }
        public int? appointmentReminderDuration { get; set; }
        public int? agendaNoteReminder { get; set; }
        public string days { get; set; }
        public Guid? smsCompany { get; set; }
        public Guid? cashAccount { get; set; }
        public Guid? creditCardCashAccount { get; set; }
        public Guid? bankTransferCashAccount { get; set; }
        public Guid? whatsappTemplate { get; set; }
        public Guid? customerWelcomeTemplate { get; set; }
        public Guid? automaticAppointmentReminderMessageTemplate { get; set; }
        public bool? isOtoCustomerWelcomeMessage { get; set; }
        public bool? displayVetNo { get; set; }
        public bool? autoSms { get; set; }
        public bool? IsAnimalsBreeds { get; set; } 
        public string appointmentBeginDate { get; set; }
        public string appointmentEndDate { get; set; }
    }

    public class ParametersListQueryHandler : IRequestHandler<ParametersListQuery, Response<List<ParametersDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ParametersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<ParametersDto>>> Handle(ParametersListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ParametersDto>>();
            try
            {
                string query = "Select * from vetparameters where Deleted = 0";
                var _data = _uow.Query<ParametersDto>(query).ToList();
                response = new Response<List<ParametersDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                //response.Errors = ex.ToString();
            }

            return response;


        }
    }
}
