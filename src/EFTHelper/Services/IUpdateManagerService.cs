using System;
using System.Threading.Tasks;

namespace EFTHelper.Services;

public interface IUpdateManagerService
{
    string ReleaseUrl { get; }

    event EventHandler UpdateAvailable;

    void Watch();

    Task<bool> CheckForUpdateAsync();

    Version GetVersion();

    void Initialize();

    Task UpdateAsync();
}
