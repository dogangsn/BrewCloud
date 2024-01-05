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
using VetSystems.Vet.Application.Models.Dashboards;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Dashboards.Queries
{
    public class GetDashBoardQuery : IRequest<Response<DashboardsDto>>
    {
    }

    public class GetDashBoardQueryHandler : IRequestHandler<GetDashBoardQuery, Response<DashboardsDto>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetAppointments> _vetAppointmentsRepository;
        private readonly IRepository<VetCustomers> _vetCustomersRepository;
        private readonly IRepository<VetSaleBuyOwner> _vetSaleBuyOwnerRepository;

        public GetDashBoardQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetAppointments> vetAppointmentsRepository , IRepository<VetCustomers> vetCustomersRepository, IRepository<VetSaleBuyOwner> vetSaleBuyOwnerRepository)
        {
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _vetAppointmentsRepository = vetAppointmentsRepository ?? throw new ArgumentNullException(nameof(vetAppointmentsRepository));
            _vetCustomersRepository = vetCustomersRepository ?? throw new ArgumentNullException(nameof(vetCustomersRepository));
            _vetSaleBuyOwnerRepository = vetSaleBuyOwnerRepository;
        }

        public async Task<Response<DashboardsDto>> Handle(GetDashBoardQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<DashboardsDto>();
            try
            {
                response.Data = new DashboardsDto();

                var dailyAppointments = await _vetAppointmentsRepository.GetAsync(x => x.Deleted == false && x.CreateDate.Date == DateTime.Today);
                var _totalCount = new DashboardCountTotal();
                _totalCount.DailyAddAppointmentCount = dailyAppointments.Count();
                _totalCount.DailyAddAppointmentCompletedCount = dailyAppointments.Where(x => x.IsCompleted.GetValueOrDefault()).Count();

                var customers = await _vetCustomersRepository.GetAsync(x => x.Deleted == false && x.CreateDate.Date == DateTime.Today);
                _totalCount.DailyAddCustomerCount = customers.Count();
                _totalCount.DailyAddCustomerYestardayCount = (await _vetCustomersRepository.GetAsync(x => x.Deleted == false && x.CreateDate.Date == DateTime.Today.AddDays(-1))).Count();

                var saleBuy = await _vetSaleBuyOwnerRepository.GetAsync(x => x.Deleted == false && x.CreateDate.Date == DateTime.Today);
                _totalCount.DailyTurnoverAmount = saleBuy.Sum(x => x.Total);


                response.Data.TotalCount = _totalCount;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
