namespace WorkoutApi;

public static class WorkoutServiceTestExtensions
{
    public static async Task Seed(this IWorkoutService service, params Workout[] workouts)
    {
        foreach (var workout in workouts)
            await service.Create(workout);
    }
}


