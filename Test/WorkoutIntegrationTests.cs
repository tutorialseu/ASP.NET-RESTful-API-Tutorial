using System.Text;
using AutoFixture.Xunit2;
using Newtonsoft.Json;

namespace WorkoutApi;

public class WorkoutIntegrationTests : WorkoutTests
{

    [Theory, AutoData]
    public async Task Gets_all_workouts(Workout[] workoutSeeds)
    {
        await workoutService.Seed(workoutSeeds);

        var response = await httpClient.GetAsync("/api/workouts/");

        var responseContent = await response.Content.ReadAsStringAsync();
        var downloadedWorkouts = JsonConvert.DeserializeObject<Workout[]>(responseContent);
        Assert.Equivalent(workoutSeeds, downloadedWorkouts);
    }



    [Theory, AutoData]
    public async Task Finds_a_workout_by_its_id(Workout workoutSeed)
    {
        await workoutService.Seed(workoutSeed);

        var response = await httpClient.GetAsync($"/api/workouts/{workoutSeed.Id}");

        var responseContent = await response.Content.ReadAsStringAsync();
        var workout = JsonConvert.DeserializeObject<Workout>(responseContent);
        Assert.Equivalent(workoutSeed, workout);
    }



    [Theory, AutoData]
    public async Task Creates_new_workouts(Workout workoutSeed)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(workoutSeed), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/api/workouts/", requestContent);

        var createdWorkout = JsonConvert.DeserializeObject<Workout>(await response.Content.ReadAsStringAsync());
        var workoutInDb = await workoutService.GetById(createdWorkout.Id); workoutSeed.Id = workoutInDb!.Id;
        Assert.Equivalent(workoutSeed, workoutInDb);
    }



    [Theory, AutoData]
    public async Task Updates_existing_workouts(Workout originalWorkout, Workout workoutUpdate)
    {
        workoutUpdate.Id = originalWorkout.Id;
        await workoutService.Seed(originalWorkout);
        var requestContent = new StringContent(JsonConvert.SerializeObject(workoutUpdate), Encoding.UTF8, "application/json");

        await httpClient.PutAsync($"/api/workouts/{originalWorkout.Id}", requestContent);

        var workoutInDb = await workoutService.GetById(originalWorkout.Id);
        Assert.Equivalent(workoutUpdate, workoutInDb);

    }



    [Theory, AutoData]
    public async Task Deletes_workouts(Workout existingWorkout)
    {
        await workoutService.Seed(existingWorkout);
        var response = await httpClient.DeleteAsync($"/api/workouts/{existingWorkout.Id}");

        var workout = await workoutService.GetById(existingWorkout.Id);

        Assert.Null(workout);
    }

}


