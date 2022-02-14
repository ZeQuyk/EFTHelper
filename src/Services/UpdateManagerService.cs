using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Squirrel;

namespace EFTHelper.Services
{
    /// <summary>
    /// Represents the update manager.
    /// </summary>
    public class UpdateManagerService
    {
        #region Fields

        private static readonly string GithubUrl = "https://github.com/ZeQuyk/EFTHelper";
        public string ReleaseUrl => $"{GithubUrl}/releases/tag/v{GetVersion().ToString(3)}";
        #endregion

        #region Methods

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns>The task awaiter.</returns>
        public async Task Update()
        {
            using (var updateManager = await UpdateManager.GitHubUpdateManager(GithubUrl))
            {
                await updateManager.UpdateApp();
                UpdateManager.RestartApp();
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns>True if needs update.</returns>
        public async Task<bool> CheckForUpdate()
        {
#if DEBUG
            return false;
#endif

#pragma warning disable CS0162
            try
            {
                using (var updateManager = await CreateUpdateManagerAsync())
                {
                    var information = await updateManager.CheckForUpdate();
                    return information.ReleasesToApply.Any();
                }
            }
            catch
            {
                return false;
            }
#pragma warning restore CS0162
        }

        /// <summary>
        /// Get the AssemblyVersion of this instance.
        /// </summary>
        /// <returns></returns>
        public Version GetVersion() => Assembly.GetExecutingAssembly().GetName().Version;

        public void HandleSquirrel()
        {
            SquirrelAwareApp.HandleEvents(onInitialInstall: OnInstall, onAppUninstall: OnUninstall);
        }

        private async void OnInstall(System.Version version)
        {
            using var updateManager = new Squirrel.UpdateManager(GithubUrl, "EFTHelper");
            await updateManager.CreateUninstallerRegistryEntry();
            updateManager.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private void OnUninstall(System.Version version)
        {
            using var updateManager = new Squirrel.UpdateManager(GithubUrl, "EFTHelper");
            updateManager.RemoveUninstallerRegistryEntry();
            updateManager.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private Task<UpdateManager> CreateUpdateManagerAsync() => UpdateManager.GitHubUpdateManager(GithubUrl);

        #endregion
    }
}
