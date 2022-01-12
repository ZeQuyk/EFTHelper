using Caliburn.Micro;
using EFTHelper.Services;

namespace EFTHelper.ViewModels
{
    public class VersionViewModel : Screen
    {
        bool _needUpdate;
        UpdateManagerService _updateManagerService;

        public VersionViewModel(UpdateManagerService updateManagerService)
        {
            _updateManagerService = updateManagerService;
        }

        public string Version
        {
            get
            {
                var version = _updateManagerService.GetVersion().ToString(3);
                return $"Version {version}";
            }
        }

        public bool NeedUpdate
        {
            get
            {
                return _needUpdate;
            }

            private set
            {
                _needUpdate = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => UpToDate);
            }
        }

        public bool UpToDate => !NeedUpdate;        

        public async void UpdateApplication()
        {
            _needUpdate = await _updateManagerService.CheckForUpdate();
            if (_needUpdate)
            {
                await _updateManagerService.Update();
            }
            else
            {
                _needUpdate = false;
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            NeedUpdate = await _updateManagerService.CheckForUpdate();
        }
    }
}
