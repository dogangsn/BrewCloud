﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Application.Models.Definition.Product;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.ProductDescription.Queries
{
    public class ProductDescriptionListQuery : IRequest<Response<List<ProductDescriptionsDto>>>
    {
    }

    public class ProductDescriptionListQueryHandler : IRequestHandler<ProductDescriptionListQuery, Response<List<ProductDescriptionsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductDescriptionListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductDescriptionsDto>>> Handle(ProductDescriptionListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProductDescriptionsDto>>();
            try
            {
                string query = "Select * from VetProducts where Deleted = 0";
                var _data = _uow.Query<ProductDescriptionsDto>(query).ToList();
                response = new Response<List<ProductDescriptionsDto>>
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