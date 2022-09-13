using LoginModule;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_WPFScbOri
{
    public class MainShellInitializer : IMainShellInitializer
    {
        public void ConfigModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<LoginModule.LoginModules>();
            moduleCatalog.AddModule<MainModule.MainModules>();
        }

        public void RegisterType(IContainerRegistry containnerRegister)
        {
            
        }
    }
}
