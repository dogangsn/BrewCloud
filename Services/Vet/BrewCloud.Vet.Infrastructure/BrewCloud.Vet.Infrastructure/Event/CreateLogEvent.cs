using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.Vet.Infrastructure.Event
{
    public class CreateLogEvent : INotification
    {
        public List<LogDto> Logs { get; set; }
    }
}
