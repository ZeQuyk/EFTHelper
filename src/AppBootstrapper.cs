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
        #region Fields

        private SimpleContainer container;
        private UpdateManagerService _updateManager;

        #endregion

        #region Constructors

        public AppBootstrapper()
        {
            _updateManager = new UpdateManagerService();
            _updateManager.HandleSquirrel();
            Initialize();
        }

        #endregion

        #region Methods

        protected override void Configure()
        {
            this.container = new SimpleContainer();
            AddServices();
            AddViewModels();
            container.Singleton<IWindowManager, WindowManager>();          
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
            container.Singleton<ItemsListViewModel, ItemsListViewModel>();
            container.Singleton<ItemTypeViewModel, ItemTypeViewModel>();
        }

        private void AddServices()
        {
            container.Singleton<SettingsService, SettingsService>();
            container.Singleton<TarkovToolsService, TarkovToolsService>();
        }

        #endregion
    }
}
