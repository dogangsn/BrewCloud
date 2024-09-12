using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Patients.Examinations;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Patient.Examination.Queries
{
    public class GetExaminationsBySaleListQuery : IRequest<Response<List<ExaminationSalesListDto>>>
    {
        public Guid ExaminationId { get; set; }
    }

    public class GetExaminationsBySaleListQueryHandler : IRequestHandler<GetExaminationsBySaleListQuery, Response<List<ExaminationSalesListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetExaminationsBySaleListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ExaminationSalesListDto>>> Handle(GetExaminationsBySaleListQuery request, CancellationToken cancellationToken)
        {

            var response = Response<List<ExaminationSalesListDto>>.Success(200);
            try
            {
                string query = string.Empty;

                query += "SELECT 1 as type, vetsalebuyowner.id, vetsalebuyowner.date, vetsalebuyowner.invoiceno, vetsalebuyowner.kdv ,vetsalebuyowner.total, vetsalebuyowner.paymenttype as paymenttype "
                        + " FROM vetsalebuytrans  "
                        + " INNER JOIN vetsalebuyowner ON vetsalebuytrans.ownerid = vetsalebuyowner.id "
                        + " WHERE vetsalebuyowner.deleted = 0 and vetsalebuyowner.examinationsid =  @xExanimationId "
                        + " UNION ALL  "
                        + " SELECT 2 as type, vetpaymentcollection.id as id, vetpaymentcollection.date, '' as invoiceno, 0 as kdv, vetpaymentcollection.total, vetpaymentcollection.paymetntid as paymenttype "
                        + " FROM vetpaymentcollection "
                        + " INNER JOIN vetsalebuyowner ON vetpaymentcollection.salebuyid = vetsalebuyowner.id "
                        + " where vetpaymentcollection.deleted= 0";

                var _data = _uow.Query<ExaminationSalesListDto>(query, new { xExanimationId = request.ExaminationId }).ToList();
                response = new Response<List<ExaminationSalesListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                return Response<List<ExaminationSalesListDto>>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
