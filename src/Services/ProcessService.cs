//-----------------------------------------------------------------------
// <copyright file="ProcessLurker.cs" company="Wohs Inc.">
//     Copyright © Wohs Inc.
// </copyright>
//-----------------------------------------------------------------------

namespace EFTHelper.Services;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents the process lurker.
/// </summary>
/// <seealso cref="System.IDisposable" />
public class ProcessService : IDisposable
{
    #region Fields

    private static readonly int WaitingTime = 2000;
    private IEnumerable<string> _processNames;
    private CancellationTokenSource _tokenSource;
    private Process _activeProcess;
    private int _processId;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessService"/> class.
    /// </summary>
    /// <param name="processName">Name of the process.</param>
    public ProcessService(string processName)
        : this(new string[] { processName })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessService"/> class.
    /// </summary>
    /// <param name="processNames">The process names.</param>
    public ProcessService(IEnumerable<string> processNames)
    {
        _processNames = processNames;
        _tokenSource = new CancellationTokenSource();
    }

    #endregion

    #region Events

    /// <summary>
    /// The process ended
    /// </summary>
    public event EventHandler ProcessClosed;

    #endregion

    #region Methods

    /// <summary>
    /// Gets my process by identifier.
    /// </summary>
    /// <param name="processId">The process identifier.</param>
    /// <returns>The process.</returns>
    public static Process GetProcessById(int processId)
    {
        try
        {
            return Process.GetProcessById(processId);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the current process identifier.
    /// </summary>
    public static int CurrentProcessId => Process.GetCurrentProcess().Id;

    /// <summary>
    /// Waits for process.
    /// </summary>
    /// <returns>The process.</returns>
    public virtual async Task<int> WaitForProcess()
    {
        var process = GetProcess();

        while (process is null)
        {
            await Task.Delay(WaitingTime);
            process = GetProcess();
        }

        WaitForExit();
        return WaitForWindowHandle();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    /// Called when [exit].
    /// </summary>
    protected virtual void OnExit()
    {
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _tokenSource.Cancel();

            _activeProcess?.Dispose();
        }
    }

    /// <summary>
    /// Gets the process.
    /// </summary>
    /// <returns>The process.</returns>
    private Process GetProcess()
    {
        if (_activeProcess is not null)
        {
            _activeProcess.Dispose();
        }

        foreach (var processName in _processNames)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process is not null)
            {
                _activeProcess = process;
                return process;
            }
        }

        return null;
    }

    /// <summary>
    /// Waits for exit.
    /// </summary>
    private async void WaitForExit()
    {
        await Task.Run(() =>
        {
            try
            {
                var token = _tokenSource.Token;
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var process = GetProcess();
                while (process is not null)
                {
                    process.WaitForExit(WaitingTime);
                    process = GetProcess();
                }
            }
            catch
            {
            }

            OnExit();
            ProcessClosed?.Invoke(this, EventArgs.Empty);
        });
    }

    /// <summary>
    /// Gets the window handle.
    /// </summary>
    /// <returns>
    /// The process id.
    /// </returns>
    private int WaitForWindowHandle()
    {
        Process currentProcess;

        try
        {
            do
            {
                var process = GetProcess();
                Thread.Sleep(200);
                currentProcess = process ?? throw new InvalidOperationException();
            }
            while (currentProcess.MainWindowHandle == IntPtr.Zero);

            _processId = currentProcess.Id;
        }
        catch
        {
            _processId = WaitForWindowHandle();
        }

        return _processId;
    }

    #endregion
}