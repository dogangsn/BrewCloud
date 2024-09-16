using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.FileManager
{
    public class FileManagerResponse
    {
        public byte[] FileData { get; set; }
        public string FileName { get; set; }

    }
}
