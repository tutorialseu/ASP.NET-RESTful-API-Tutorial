namespace WorkoutApi;

public class WorkoutService : IWorkoutService
{
    private readonly List<Workout> workouts;

    public WorkoutService()
    {
        workouts = new List<Workout>();
    }

    public Task<IEnumerable<Workout>> GetAll()
    {
        return Task.FromResult(workouts.AsEnumerable());
    }

    public Task<Workout?> GetById(Guid id)
    {
        return Task.FromResult(workouts.SingleOrDefault(w => w.Id == id));
    }

    public Task Create(Workout workout)
    {
        workouts.Add(workout);
        return Task.CompletedTask;
    }

    public Task Update(Workout updatedWorkout)
    {
        var oldWorkout = workouts.SingleOrDefault(w => w.Id == updatedWorkout.Id);
        if (oldWorkout == null)
        {
            throw new ArgumentException("Workout not found.");
        }
        workouts.Remove(oldWorkout);
        workouts.Add(updatedWorkout);
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var existingWorkout = workouts.SingleOrDefault(w => w.Id == id);
        if (existingWorkout != null)
        {
            workouts.Remove(existingWorkout);
        }
        return Task.CompletedTask;
    }
}