﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.Service.Nav
{
    public class ModuleService
    {
        public ModuleService()
        {
        }
        private List<string> general = new List<string> { "vet", "farm", "appoman", "beautycenter", "labman" , "gymman", "connecthub" };

        public List<string> GetModule()
        {
            //Yetkilendirme işlemi yapılacak daha sonra 
            List<string> modules;
            modules = general;
            return modules;
        }
    }
}
