using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Squirrel;

namespace EFTHelper.Services;

/// <summary>
/// Represents the update manager.
/// </summary>
public class UpdateManagerService : TimedServiceBase, IUpdateManagerService
{
    #region Fields

    private static readonly string GithubUrl = "https://github.com/ZeQuyk/EFTHelper";

    #endregion

    #region Events

    public event EventHandler UpdateAvailable;

    #endregion

    #region Properties

    public string ReleaseUrl => $"{GithubUrl}/releases/tag/v{GetVersion().ToString(3)}";

    #endregion

    #region Methods

    /// <summary>
    /// Updates this instance.
    /// </summary>
    /// <returns>The task awaiter.</returns>
    public async Task UpdateAsync()
    {
        using var updateManager = await CreateUpdateManagerAsync();
        await updateManager.UpdateApp();
        UpdateManager.RestartApp();
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    /// <returns>True if needs update.</returns>
    public async Task<bool> CheckForUpdateAsync()
    {
        if (Debugger.IsAttached)
        {
            return false;
        }

        try
        {
            using var updateManager = await CreateUpdateManagerAsync();
            var information = await updateManager.CheckForUpdate();
            return information.ReleasesToApply.Any();
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get the AssemblyVersion of this instance.
    /// </summary>
    /// <returns></returns>
    public Version GetVersion() => Assembly.GetExecutingAssembly().GetName().Version;

    public void Initialize()
    {
        SquirrelAwareApp.HandleEvents(onInitialInstall: OnInstall, onAppUninstall: OnUninstall);
    }

    protected async override void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        var needUpdate = await CheckForUpdateAsync();
        if (needUpdate)
        {
            UpdateAvailable?.Invoke(this, EventArgs.Empty);
            DisposeTimer();
        }
    }

    private async void OnInstall(Version version)
    {
        using var updateManager = await CreateUpdateManagerAsync();
        await updateManager.CreateUninstallerRegistryEntry();
        updateManager.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
    }

    private async void OnUninstall(Version version)
    {
        using var updateManager = await CreateUpdateManagerAsync();
        updateManager.RemoveUninstallerRegistryEntry();
        updateManager.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
    }

    private static Task<UpdateManager> CreateUpdateManagerAsync() => UpdateManager.GitHubUpdateManager(GithubUrl);

    #endregion
}
