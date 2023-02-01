namespace WorkoutApi;

public class Workout
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Exercise> Exercises { get; set; }
}
