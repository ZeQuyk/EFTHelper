using System;
using Caliburn.Micro;
using EFTHelper.Helpers;
using EFTHelper.Services;

namespace EFTHelper.ViewModels;

public class VersionViewModel : Screen
{
    #region Fields

    private readonly IUpdateManagerService _updateManagerService;
    private bool _needUpdate;

    #endregion

    #region Constructors

    public VersionViewModel(IUpdateManagerService updateManagerService)
    {
        _updateManagerService = updateManagerService;
    }

    #endregion

    #region Events

    public event EventHandler OnUpdateRequested;

    #endregion

    #region Properties

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

        set
        {
            _needUpdate = value;
            NotifyOfPropertyChange();
            NotifyOfPropertyChange(() => UpToDate);
        }
    }

    public bool UpToDate => !NeedUpdate;

    #endregion

    #region Methods

    public void UpdateApplication()
    {
        OnUpdateRequested?.Invoke(this, EventArgs.Empty);
    }

    public void OpenReleasePage()
    {
        ProcessHelper.StartProcess(_updateManagerService.ReleaseUrl);
    }

    protected override async void OnViewLoaded(object view)
    {
        NeedUpdate = await _updateManagerService.CheckForUpdateAsync();
        if (!NeedUpdate)
        {
            _updateManagerService.UpdateAvailable += UpdateManagerService_UpdateAvailable;
            _updateManagerService.Watch();
        }
    }

    private void UpdateManagerService_UpdateAvailable(object sender, EventArgs e)
    {
        NeedUpdate = true;
    }

    #endregion
}