using System;
using CakeMachine;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Application started !");
        var factory = new CakeFactory(TimeSpan.FromMinutes(1));

        factory.OnReceiveStatusNotification = (createdCookies) => Console.WriteLine(createdCookies);

        var preparing = new StepBalancer(3, "On Preparing", new StepDefinition { MinimalTimeProcessing = 5, MaximalTimeProcessing = 8 });
        var cook = new StepBalancer(5, "On cook", new StepDefinition { MinimalTimeProcessing = 10, MaximalTimeProcessing = 10 });
        var package = new StepBalancer(2, "On package", new StepDefinition { MinimalTimeProcessing = 2, MaximalTimeProcessing = 2 });

        factory.AddStepBalancing(preparing);
        factory.AddStepBalancing(cook);
        factory.AddStepBalancing(package);

        factory.Run();
        Console.WriteLine("Factory started !");
        // Console.Read();
        Console.WriteLine("Stopping factory");
        factory.Stop();
        factory.Dispose();
    }
}