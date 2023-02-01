using Microsoft.AspNetCore.Mvc.Testing;

namespace WorkoutApi;
public abstract class WorkoutTests
{
    protected readonly HttpClient httpClient;
    protected readonly IWorkoutService workoutService;

    public WorkoutTests()
    {
        var factory = new WebApplicationFactory<Program>();
        httpClient = factory.CreateClient();
        workoutService = factory.Services.GetService(typeof(IWorkoutService))
                            as IWorkoutService
                            ?? throw new SystemException(nameof(IWorkoutService)
                                                                + " is not registered.");
    }
}


