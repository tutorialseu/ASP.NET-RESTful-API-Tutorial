namespace WorkoutApi;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public int Weight { get; set; }
    public int Duration { get; set; }
}