using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Vaccine;

namespace VetSystems.Vet.Application.Features.Vaccine.Commands
{
    public class CreateVaccineCommand : IRequest<Response<bool>>
    {
        public int AnimalType { get; set; }
        public string VaccineName { get; set; }
        public int TimeDone { get; set; }
        public Guid RenewalOption { get; set; }
        public int Obligation { get; set; }
        public List<VetVaccineMedicineListDto> VaccineMedicine { get; set; }
    }

    public class CreateVaccineCommandHandler : IRequestHandler<CreateVaccineCommand, Response<bool>>
    {
         




        public Task<Response<bool>> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
