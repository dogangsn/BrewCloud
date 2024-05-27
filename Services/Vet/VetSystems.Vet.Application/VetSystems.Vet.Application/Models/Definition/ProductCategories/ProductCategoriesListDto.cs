using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.ProductCategories
{
    public class ProductCategoriesListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryCode { get; set; }
    }
}
