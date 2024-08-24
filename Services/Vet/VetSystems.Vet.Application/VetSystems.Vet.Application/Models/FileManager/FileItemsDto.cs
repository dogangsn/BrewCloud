using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.FileManager
{
    public class FileItemsDto
    {
        public FileItemsDto()
        {
            Folders = new List<Item>();
            Files = new List<Item>();
        }
        public List<Item> Folders { get; set; }
        public List<Item> Files { get; set; }
        public string Path { get; set; } = string.Empty;
    }

    public class Item
    {
        public Guid Id { get; set; }
        public Guid FolderId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public string ModifiedAt { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Contents { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}
