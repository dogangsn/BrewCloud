using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Store
{
    public class StoreListDto
    {
        public Guid Id { get; set; }
        public string DepotCode { get; set; }
        public string DepotName { get; set; }
        public bool Active { get; set; }

    }
}
