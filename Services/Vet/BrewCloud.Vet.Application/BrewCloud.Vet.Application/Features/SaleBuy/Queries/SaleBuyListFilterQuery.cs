﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.SaleBuy;
using BrewCloud.Vet.Domain.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Vet.Application.Models.Definition.PaymentMethods;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Vet.Application.Features.SaleBuy.Queries
{
    public class SaleBuyListFilterQuery : IRequest<Response<List<SaleBuyListDto>>>
    {

        public int PaymentType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class SaleBuyListFilterQueryHandler : IRequestHandler<SaleBuyListFilterQuery, Response<List<SaleBuyListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Vet.Domain.Entities.VetPaymentMethods> _paymentMethodsRepository;
        private readonly ILogger<SaleBuyListFilterQueryHandler> _logger;

        public SaleBuyListFilterQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<Vet.Domain.Entities.VetPaymentMethods> paymentMethodsRepository, ILogger<SaleBuyListFilterQueryHandler> logger)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _paymentMethodsRepository = paymentMethodsRepository;
            _logger = logger;
        }

        public async Task<Response<List<SaleBuyListDto>>> Handle(SaleBuyListFilterQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<SaleBuyListDto>>();
            try
            {

                string _paymentFilter = string.Empty;
                if (request.PaymentType == 0)
                {
                    var _payments = _uow.Query<PaymentMethodsDto>("Select * from VetPaymentMethods where Deleted = 0").ToList();
                    if (_payments.Count > 0)
                    {
                        foreach (var item in _payments)
                        {
                            _paymentFilter += "'" + Convert.ToString(item.RecId).Trim() + "',";
                        }
                        _paymentFilter = _paymentFilter.Substring(0, _paymentFilter.Length - 1);
                    }
                }
                else
                    _paymentFilter = Convert.ToString(request.PaymentType);


                string query = string.Empty;

                #region Old
                //string query = "SELECT  vetsalebuyowner.id, vetsalebuyowner.type, vetsalebuyowner.date, vetsalebuyowner.invoiceno, vetpaymentmethods.name as PaymentName, " +
                //    "\r\nvetsalebuyowner.total as Total, \r\nvetsalebuyowner.discount, \r\nvetsalebuyowner.kdv, vetsalebuyowner.remark, \r\n                         " +
                //    "vetsalebuyowner.supplierid, vetsalebuyowner.netprice, vetsalebuyowner.customerid, \r\n\t\t\t\t\t\t CASE WHEN vetsalebuyowner.customerid IS NULL OR\r\n                         " +
                //    "vetsalebuyowner.customerid = '00000000-0000-0000-0000-000000000000' THEN 'PARAKENDE SATIŞ' ELSE vetcustomers.firstname + ' ' + vetcustomers.lastname  END AS customerName,\r\n\t\t\t\t\t\t  " +
                //    "CASE WHEN vetsalebuyowner.supplierid IS NULL OR\r\n                         " +
                //    "vetsalebuyowner.supplierid = '00000000-0000-0000-0000-000000000000' THEN '-' ELSE vetsuppliers.suppliername END AS supplierName\r\n\r\nFROM            " +
                //    "vetsalebuyowner INNER JOIN\r\n                         vetsalebuytrans ON vetsalebuyowner.id = vetsalebuytrans.ownerid LEFT JOIN\r\n                         " +
                //    "vetcustomers ON vetsalebuyowner.customerid = vetcustomers.id LEFT JOIN\r\n                         " +
                //    "vetsuppliers ON vetsalebuyowner.supplierid = vetsuppliers.id LEFT JOIN\r\n\t\t\t\t\t\t vetpaymentmethods ON vetsalebuyowner.paymenttype = vetpaymentmethods.RecId " +
                //    "\r\nWHERE        (vetsalebuyowner.deleted = 0) " +
                //    " and (vetsalebuyowner.paymenttype In (" + _paymentFilter + ")) " +
                //    " and (CONVERT(date, vetsalebuyowner.CreateDate) >= CONVERT(date, @BeginDate)) " +
                //    " and (CONVERT(date, vetsalebuyowner.CreateDate) <= CONVERT(date, @EndDate)) " +
                //    "order by vetsalebuyowner.CreateDate desc"; 
                #endregion

                query += "SELECT  vetsalebuyowner.id, vetsalebuyowner.type, vetsalebuyowner.date, vetsalebuyowner.invoiceno, vetpaymentmethods.name as PaymentName, "
                         + " vetsalebuyowner.total as Total,  "
                         + " vetsalebuyowner.discount,  "
                         + " vetsalebuyowner.kdv, vetsalebuyowner.remark,  "
                         + "                          vetsalebuyowner.supplierid, vetsalebuyowner.netprice, vetsalebuyowner.customerid,  "
                         + " 						 CASE WHEN vetsalebuyowner.customerid IS NULL OR "
                         + "                          vetsalebuyowner.customerid = '00000000-0000-0000-0000-000000000000' THEN 'PARAKENDE SATIŞ' ELSE vetcustomers.firstname + ' ' + vetcustomers.lastname  END AS customerName, "
                         + " 						  CASE WHEN vetsalebuyowner.supplierid IS NULL OR "
                         + "                          vetsalebuyowner.supplierid = '00000000-0000-0000-0000-000000000000' THEN '-' ELSE vetsuppliers.suppliername END AS supplierName,  "
                         + " 						 STRING_AGG(vetproducts.name + ' (' + CAST(vetsalebuytrans.amount AS VARCHAR(10)) + ' ' + vetunits.unitname +  ')', ', ') AS ProductName "
                         + " FROM            vetsalebuyowner INNER JOIN "
                         + "                          vetsalebuytrans ON vetsalebuyowner.id = vetsalebuytrans.ownerid LEFT JOIN "
                         + " 						  vetproducts ON vetsalebuytrans.productid = vetproducts.id LEFT JOIN "
                         + "                          vetunits ON vetunits.id = vetproducts.unitid and vetunits.deleted = 0 Left Join "
                         + "                          vetcustomers ON vetsalebuyowner.customerid = vetcustomers.id LEFT JOIN "
                         + "                          vetsuppliers ON vetsalebuyowner.supplierid = vetsuppliers.id LEFT JOIN "
                         + " 						 vetpaymentmethods ON vetsalebuyowner.paymenttype = vetpaymentmethods.RecId  "
                         + " WHERE        (vetsalebuyowner.deleted = 0)   "
                         + " and (vetsalebuyowner.paymenttype In (" + _paymentFilter + ")) " 
                         + " and (CONVERT(date, vetsalebuyowner.CreateDate) >= CONVERT(date, @BeginDate)) " 
                         + " and (CONVERT(date, vetsalebuyowner.CreateDate) <= CONVERT(date, @EndDate)) " 
                         + " GROUP BY  "
                         + "     vetsalebuyowner.id,  "
                         + "     vetsalebuyowner.type,  "
                         + "     vetsalebuyowner.date,  "
                         + "     vetsalebuyowner.invoiceno,  "
                         + "     vetpaymentmethods.name,  "
                         + "     vetsalebuyowner.total,  "
                         + "     vetsalebuyowner.discount,  "
                         + "     vetsalebuyowner.kdv,  "
                         + "     vetsalebuyowner.remark,  "
                         + "     vetsalebuyowner.supplierid,  "
                         + "     vetsalebuyowner.netprice,  "
                         + "     vetsalebuyowner.customerid,  "
                         + "     vetcustomers.firstname,  "
                         + "     vetcustomers.lastname,  "
                         + "     vetsuppliers.suppliername, "
                         + "     vetsalebuyowner.CreateDate " 
                         + "     ORDER BY vetsalebuyowner.CreateDate DESC"; 



                var _data = _uow.Query<SaleBuyListDto>(query, new { BeginDate = request.BeginDate, EndDate = request.EndDate }).ToList();
                response = new Response<List<SaleBuyListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                _logger.LogError($"Exception: {ex.Message}");
            }

            return response;
        }
    }
}
