using LoginModule;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Prism_WPFScbOri
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMainShellInitializer, MainShellInitializer>();
        }

        protected override Window CreateShell()
        {
            //return Container.Resolve<MainModule.Views.MainWindow>();
            return Container.Resolve<LoginModule.Views.LoginWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<MainModule.MainModule>();
            moduleCatalog.AddModule<LoginModule.LoginModules>();
        }
    }
}
