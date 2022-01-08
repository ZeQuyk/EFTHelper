using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using EFTHelper.Services;
using EFTHelper.ViewModels;
using Squirrel;

namespace EFTHelper
{
    class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer container;
        private UpdateManagerService _updateManager;

        public AppBootstrapper()
        {
            _updateManager = new UpdateManagerService();
            _updateManager.HandleSquirrel();
            Initialize();
        }

        protected override void Configure()
        {
            this.container = new SimpleContainer();
            container.PerRequest<ShellViewModel, ShellViewModel>();
            container.PerRequest<LocationSelectorViewModel, LocationSelectorViewModel>();
            container.PerRequest<LocationViewModel, LocationViewModel>();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<SettingsService, SettingsService>();
            container.Instance(Application);
            container.Instance(_updateManager);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
