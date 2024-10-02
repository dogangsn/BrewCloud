using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Application.Models.Lab;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Lab.Queries
{
    public class GetLabDocumentByIdQuery : IRequest<Response<LabDocumentDetailDto>>
    {
        public Guid PatientId { get; set; }
    }

    public class GetLabDocumentByIdQueryHandler : IRequestHandler<GetLabDocumentByIdQuery, Response<LabDocumentDetailDto>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetLabDocumentByIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<LabDocumentDetailDto>> Handle(GetLabDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<LabDocumentDetailDto>();
            try
            {
                string query = "Select * from VetCustomers where Deleted = 0 and CAST(createdate AS DATE) = CAST(GETDATE() AS DATE)  order by CreateDate ";
                var _data = _uow.Query<CustomersDto>(query).ToList();
            
                




            }
            catch (Exception ex)
            {
                return Response<LabDocumentDetailDto>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
