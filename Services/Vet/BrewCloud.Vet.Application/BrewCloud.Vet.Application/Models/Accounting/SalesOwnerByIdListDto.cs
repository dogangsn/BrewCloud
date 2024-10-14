using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Accounting
{
    public class SalesOwnerByIdListDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; } = string.Empty;
        public List<SaleTransRequestDto> Trans { get; set; }
    }
}
