using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BrewCloud.Shared.Data
{
    public class ConfigurationDatabaseService : IDisposable
    {
        private readonly Assembly _assembly;

        public ConfigurationDatabaseService(Assembly assembly)
        {
            _assembly = assembly;
        }

        public string GetSqlText(string path)
        {
            var resources = GetManifestResourceNames();
            resources.RemoveAll(r => !r.Contains(path));
            var filePath = resources.FirstOrDefault();
            if (filePath == null)
            {
                return "";
            }
            string commandText = "";
            //Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream s = _assembly.GetManifestResourceStream(filePath))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    commandText = sr.ReadToEnd();
                }
            }

            return commandText;
        }

        public List<string> GetManifestResourceNames()
        {

            //Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resources = _assembly.GetManifestResourceNames();
            return resources.ToList().OrderBy(r => r).ToList();

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
