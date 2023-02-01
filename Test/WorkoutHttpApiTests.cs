using System.Net;
using System.Text;
using AutoFixture.Xunit2;
using Newtonsoft.Json;

namespace WorkoutApi;

public class WorkoutHttpApiTests : WorkoutTests
{
    [Fact]
    public async Task GetAll_Returns200OK()
    {
        var response = await httpClient.GetAsync("/api/workouts/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Theory, AutoData]
    public async Task GetById_Returns200OK(Workout workoutSeed)
    {
        await workoutService.Seed(workoutSeed);

        var response = await httpClient.GetAsync($"/api/workouts/{workoutSeed.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Theory, AutoData]
    public async Task GetById_WithNonExistingWorkoutId_Returns404NotFound(Workout NonExistentWorkout)
    {
        var response = await httpClient.GetAsync($"/api/workouts/{NonExistentWorkout.Id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Theory, AutoData]
    public async Task Create_Returns201Created(Workout workoutSeed)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(workoutSeed), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/api/workouts/", requestContent);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }


    [Theory, AutoData]
    public async Task Update_Returns200OK(Workout workoutSeed)
    {
        await workoutService.Seed(workoutSeed);
        var requestContent = new StringContent(JsonConvert.SerializeObject(workoutSeed), Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync($"/api/workouts/{workoutSeed.Id}", requestContent);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }


    [Theory, AutoData]
    public async Task Update_WithWrongId_Returns400BadRequest(Workout dummyWorkout, Guid wrongWorkoutId)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(dummyWorkout), Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync($"/api/workouts/{wrongWorkoutId}", requestContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory, AutoData]
    public async Task Update_NonExisting_Returns404NotFound(Workout nonExistingWorkout)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(nonExistingWorkout), Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync($"/api/workouts/{nonExistingWorkout.Id}", requestContent);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Theory, AutoData]
    public async Task Delete_Returns200OK(Workout workoutSeed)
    {
        await workoutService.Seed(workoutSeed);
        var response = await httpClient.DeleteAsync($"/api/workouts/{workoutSeed.Id}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

}


