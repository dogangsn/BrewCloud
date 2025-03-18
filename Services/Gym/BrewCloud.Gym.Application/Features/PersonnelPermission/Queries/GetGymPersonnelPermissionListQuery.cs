using AutoMapper;
using BrewCloud.Gym.Application.Models;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.PersonnelPermission.Queries
{
    public class GetGymPersonnelPermissionListQuery : IRequest<Response<List<GymPersonnelPermissionDto>>>
    {
    }
    public class GetGymPersonnelPermissionListHandler : IRequestHandler<GetGymPersonnelPermissionListQuery, Response<List<GymPersonnelPermissionDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGymPersonnelPermissionListHandler> _logger;
        private readonly IRepository<GymPersonnelPermission> _personnelPermissionRepository;
        private readonly IRepository<GymPersonnel> _personnelRepository;

        public GetGymPersonnelPermissionListHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<GetGymPersonnelPermissionListHandler> logger, IRepository<GymPersonnelPermission> personnelPermissionRepository, IRepository<GymPersonnel> personnelRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelPermissionRepository = personnelPermissionRepository ?? throw new ArgumentNullException(nameof(personnelPermissionRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _personnelRepository = personnelRepository;
        }

        public async Task<Response<List<GymPersonnelPermissionDto>>> Handle(GetGymPersonnelPermissionListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<GymPersonnelPermissionDto>>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true
            };
            try
            {
                string query = @" select pp.id, pp.isapproved, pp.permissiontype, pp.begindate, pp.enddate, pp.note, pp.personnelid, CONCAT(p.name, ' ', p.surname) AS name,
                                CASE 
                                    WHEN pp.permissiontype = 0 THEN 'Yillik İzin'
                                    WHEN pp.permissiontype = 1 THEN 'Dogum İzni'
                                    WHEN pp.permissiontype = 2 THEN 'Sut İzni'
                                    WHEN pp.permissiontype = 3 THEN 'Babalik İzni'
                                    WHEN pp.permissiontype = 4 THEN 'Hastalik İzni'
                                    WHEN pp.permissiontype = 5 THEN 'Olum İzni'
                                    WHEN pp.permissiontype = 6 THEN 'Yeni İş Arama İzni'
                                    WHEN pp.permissiontype = 7 THEN 'Resmi Tatil İzni'
                                    WHEN pp.permissiontype = 8 THEN 'Haftalik Tatil İzni'
                                    WHEN pp.permissiontype = 9 THEN 'Evlilik İzni'
                                    WHEN pp.permissiontype = 10 THEN 'Evlat Edinme İzni'
                                    WHEN pp.permissiontype = 11 THEN 'Mazeret İzni'
                                    WHEN pp.permissiontype = 12 THEN 'Refakat İzni'
                                    ELSE 'Bilinmeyen İzin'
                                END AS permissiontypename
                                FROM gympersonnelpermission pp
                                left join gympersonnel p on p.id = pp.personnelid and p.deleted = 0
                                where pp.deleted = 0 ";

                response.Data = _uow.Query<GymPersonnelPermissionDto>(query);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
