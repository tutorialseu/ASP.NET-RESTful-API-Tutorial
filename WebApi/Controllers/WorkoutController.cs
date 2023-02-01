using Microsoft.AspNetCore.Mvc;

namespace WorkoutApi;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService workoutService;

    public WorkoutsController(IWorkoutService workoutService)
    => this.workoutService = workoutService;


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Workout>>> GetAll()
    => Ok(await workoutService.GetAll());


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Workout>> GetById(Guid id)
    {
        var workout = await workoutService.GetById(id);
        if (workout == null)
        {
            return NotFound();
        }
        return workout;
    }

    /// <summary>
    /// Creates a workout.
    /// </summary>
    /// <param name="plan"></param>
    /// <returns>A newly created workout</returns>
    /// <remarks>
    /// Request Example:
    ///
    ///     POST /api/workout
    ///     {
    ///       "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///       "name": "string",
    ///       "description": "string",
    ///       "date": "2023-01-26T17:33:50.275Z",
    ///       "exercises": [
    ///         {
    ///           "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///           "name": "string",
    ///           "sets": 0,
    ///           "repetitions": 0,
    ///           "weight": 0,
    ///           "duration": 0
    ///         }
    ///       ]
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created plan</response> 
    /// <response code="400">If the posted plan is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)] // Affects /swagger => the responses section
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(Workout plan)
    {
        plan.Id = Guid.NewGuid();
        await workoutService.Create(plan);
        return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(Guid id, [FromBody] Workout workout)
    {
        if (id != workout.Id)
        {
            return BadRequest();
        }

        try
        {
            await workoutService.Update(workout);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("not found"))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(Guid id)
    {
        await workoutService.Delete(id);
        return NoContent();
    }

    // [HttpHead]
    // public IActionResult Head()
    // => Ok();
}