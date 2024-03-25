using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMachine
{
    public class StepBalancer : IDisposable
    {
        private bool _isDisposed;
        private readonly int _maxConcurrentWorking;
        private readonly string _balancingActionName;
        private readonly StepDefinition stepDefinition;
        private BlockingCollection<ProcessingStep> _workingQueue = new BlockingCollection<ProcessingStep>();
        private long _onWorkingElementCount;
        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;

        public Action OnElementComplete { get; set; }
        public long ProcessedStepCount => Interlocked.Read(ref _onWorkingElementCount);

        public StepBalancer(int maxConcurrentWorking, string balancingActionName, StepDefinition stepDefinition)
        {
            _cancellationToken = _cancelTokenSource.Token;
            _maxConcurrentWorking = maxConcurrentWorking;
            _balancingActionName = balancingActionName;
            this.stepDefinition = stepDefinition;
        }

        public void Run()
        {
            Task.Factory.StartNew(Consume, _cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public string GetStatusMessage()
        {
            return $"{_balancingActionName} : {Interlocked.Read(ref _onWorkingElementCount)}";
        }

        public void Stop()
        {
            _cancelTokenSource.Cancel(false);
        }

        public void Produce()
        {
            _workingQueue.Add(new ProcessingStep(stepDefinition));
        }

        private async Task Consume()
        {
            List<Task> tasksInFlight = new List<Task>(_maxConcurrentWorking);
            ProcessingStep currentStep;

            while (!_cancellationToken.IsCancellationRequested)
            {
                while (tasksInFlight.Count < _maxConcurrentWorking && _workingQueue.TryTake(out currentStep))
                {
                    tasksInFlight.Add(currentStep.ProcessAsync());
                    Interlocked.Increment(ref _onWorkingElementCount);
                }
                if (tasksInFlight.Any())
                {
                    Task completedTask = await Task.WhenAny(tasksInFlight).ConfigureAwait(false);
                    await completedTask.ConfigureAwait(false);
                    tasksInFlight.Remove(completedTask);
                    Interlocked.Decrement(ref _onWorkingElementCount);
                    OnElementComplete?.Invoke();
                }
                else
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                _cancelTokenSource.Dispose();
                _workingQueue.Dispose();
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
