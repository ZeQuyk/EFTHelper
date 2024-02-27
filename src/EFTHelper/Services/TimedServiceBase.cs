using System;
using System.Timers;

namespace EFTHelper.Services;

public abstract class TimedServiceBase
{
    private Timer _timer;

    public void Watch()
    {
        if (_timer is not null)
        {
            DisposeTimer();
        }

        _timer = new Timer(TimeSpan.FromMinutes(5).TotalMilliseconds);
        _timer.Elapsed += Timer_Elapsed;
        _timer.Start();
    }

    protected abstract void Timer_Elapsed(object sender, ElapsedEventArgs e);

    protected void DisposeTimer()
    {
        _timer.Stop();
        _timer.Elapsed -= Timer_Elapsed;
        _timer.Dispose();
        _timer = null;
    }
}
