using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Definition.ProductCategory.Queries;
using VetSystems.Vet.Application.Models.Definition.PaymentMethods;
using VetSystems.Vet.Application.Models.Definition.ProductCategories;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.PaymentMethods.Queries
{
    public class PaymentMethodListQuery : IRequest<Response<List<PaymentMethodsDto>>>
    {
    }

    public class PaymentMethodListQueryHandler : IRequestHandler<PaymentMethodListQuery, Response<List<PaymentMethodsDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PaymentMethodListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<PaymentMethodsDto>>> Handle(PaymentMethodListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PaymentMethodsDto>>();
            try
            {
                string query = "Select * from VetPaymentMethods where Deleted = 0";
                var _data = _uow.Query<PaymentMethodsDto>(query).ToList();
                response = new Response<List<PaymentMethodsDto>>
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
