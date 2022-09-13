using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginModule
{
    public class MainBootstrapper : PrismBootstrapper
    {
        IMainShellInitializer _mainShellInitializer;

        public MainBootstrapper(IMainShellInitializer mainShellInitializer)
        {
            _mainShellInitializer = mainShellInitializer;
        }
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainModule.Views.MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _mainShellInitializer?.RegisterType(containerRegistry);
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            _mainShellInitializer?.ConfigModuleCatalog(moduleCatalog);
        }
    }
}
