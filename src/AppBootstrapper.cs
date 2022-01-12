using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using EFTHelper.Services;
using EFTHelper.ViewModels;

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

            AddViewModels();
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

        private void AddViewModels()
        {
            container.Singleton<ShellViewModel, ShellViewModel>();
            container.Singleton<LocationSelectorViewModel, LocationSelectorViewModel>();
            container.Singleton<LocationViewModel, LocationViewModel>();
            container.Singleton<VersionViewModel, VersionViewModel>();
        }
    }
}
