namespace WorkoutApi;

public interface IWorkoutService
{
    Task<IEnumerable<Workout>> GetAll();
    Task<Workout?> GetById(Guid id);
    Task Create(Workout workout);
    Task Update(Workout workout);
    Task Delete(Guid id);

}
