using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CakeMachine
{
    public class ProcessingStep
    {
        private int _minimalTimeProcessing;
        private int _maximalTimeProcessing;

        public ProcessingStep(StepDefinition definition)
        {
            if(definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }

            _minimalTimeProcessing = definition.MinimalTimeProcessing;
            // Add one because random generator maximum value is exclusive
            _maximalTimeProcessing = definition.MaximalTimeProcessing + 1;
        }

        public async Task ProcessAsync()
        {
            await Task.Delay(RandomNumberGenerator.GetInt32(_minimalTimeProcessing, _maximalTimeProcessing) * 1000).ConfigureAwait(false);
        }
    }
}
