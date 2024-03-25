using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemTimer = System.Timers.Timer;

namespace CakeMachine
{
    public class CakeFactory : IDisposable
    {
        private bool _isDisposed = false;
        private readonly SystemTimer _notificationTimer = new SystemTimer();
        private readonly List<StepBalancer> _balancers = new List<StepBalancer>();
        private long _createdCakeCount;
        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;

        public Action<string> OnReceiveStatusNotification { get; set; }
        
        public CakeFactory(TimeSpan notificationMessageinterval)
        {
            _cancellationToken = _cancelTokenSource.Token;
            _notificationTimer.Interval = notificationMessageinterval.TotalMilliseconds;
            _notificationTimer.AutoReset = true;
            _notificationTimer.Elapsed += (sender, elapsed) => OnReceiveStatusNotification?.Invoke(GetStatusMessage());
        }

        private string GetStatusMessage()
        {
            var builder = new StringBuilder();
            var dateFormat = DateTime.Now.ToString("MM/dd/yyyy HH:mm", DateTimeFormatInfo.CurrentInfo);
            builder.AppendLine($"--- Actual Status {dateFormat} ---");
            builder.AppendLine($"Finished cakes: {Interlocked.Read(ref _createdCakeCount)}");
            foreach (var balancer in _balancers)
            {
                builder.AppendLine(balancer.GetStatusMessage());
            }
            builder.AppendLine($"----------------------------------");
            return builder.ToString();
        }

        public void Stop()
        {
            _notificationTimer.Stop();
            _cancelTokenSource.Cancel(false);
            Dispose();          
        }

        public void AddStepBalancing(StepBalancer balancer)
        {
            if (balancer == null)
                throw new ArgumentNullException(nameof(balancer));

            if(_balancers.Any())
            {
                _balancers.Last().OnElementComplete = balancer.Produce;
            }
            _balancers.Add(balancer);
        }

        public void Run()
        {
            if (_balancers.Any())
            {
                _balancers.Last().OnElementComplete = () => Interlocked.Increment(ref _createdCakeCount);

                foreach (var balancer in _balancers)
                {
                    balancer.Run();
                }

                Task.Factory.StartNew(() => Produce(), _cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
                _notificationTimer.Start();
            }
        }

        private async Task Produce()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                _balancers.First().Produce();
                await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                _cancelTokenSource.Dispose();
                _notificationTimer.Dispose();
                foreach (var balancer in _balancers)
                {
                    balancer.Dispose();
                }
            }
            _isDisposed = disposing;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
