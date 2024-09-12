using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Models.Definition.PrintTemplate
{
    public class PrintTemplateListDto
    {
        public Guid Id { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public PrintType Type { get; set; }
        public string HtmlContent { get; set; } = string.Empty;
    }
}
