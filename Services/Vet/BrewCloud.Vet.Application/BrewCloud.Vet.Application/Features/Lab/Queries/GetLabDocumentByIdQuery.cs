using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Application.Models.Lab;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Lab.Queries
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
        private readonly IRepository<VetLabDocument> _vetLabDocumentRepository;

        public GetLabDocumentByIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetLabDocument> vetLabDocumentRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _vetLabDocumentRepository = vetLabDocumentRepository;
        }

        public async Task<Response<LabDocumentDetailDto>> Handle(GetLabDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<LabDocumentDetailDto>();
            try
            {
                string query = "SELECT        vetpatients.name as PatientName, vetpatients.id, vetpatients.customerid, (vetcustomers.firstname + ' ' + vetcustomers.lastname) as CustomerName ,  "
                    + " vetcustomers.phonenumber as CustomerPhone, vetcustomers.email as CustomerEmail, "
                    + "                          vetanimalstype.name AS PatientType, vetanimalbreedsdef.breedname as PatientBreed "
                    + " FROM            vetanimalstype INNER JOIN"
                    + "                          vetanimalbreedsdef INNER JOIN"
                    + "                          vetpatients INNER JOIN"
                    + "                          vetcustomers ON vetpatients.customerid = vetcustomers.id ON vetanimalbreedsdef.RecId = vetpatients.animalbreed ON vetanimalstype.RecId = vetpatients.animaltype"
                    + " 						 where vetpatients.id = @patientId ";
                var _data = _uow.Query<LabDocumentDetailDto>(query, new { patientId = request.PatientId }).FirstOrDefault();

                if (_data != null)
                {
                    var _document = (await _vetLabDocumentRepository.GetAsync(x => x.PatientId == request.PatientId)).ToList();
                    _data.LabDocuments = _mapper.Map<List<LabDocumentDto>>(_document);

                }
                response.Data = _data;
            }
            catch (Exception ex)
            {
                return Response<LabDocumentDetailDto>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
