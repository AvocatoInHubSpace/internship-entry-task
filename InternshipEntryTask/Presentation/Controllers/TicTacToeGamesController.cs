using InternshipEntryTask.Application.Commands;
using InternshipEntryTask.Presentation.DTOs;
using InternshipEntryTask.Presentation.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternshipEntryTask.Presentation.Controllers;

[Route("api/games")]
public class TicTacToeGamesController(IMediator mediator) : ControllerBase
{
    [HttpPost("{gameId:int}/moves")]
    public async Task<ActionResult<MoveResponse>> MakeMove([FromRoute]int gameId, [FromBody]MoveRequest request, CancellationToken ct)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Move failed",
                Detail = "Invalid request",
                Instance = HttpContext.Request.Path
            });
        }
        
        var result = await mediator.Send(new MakeMoveCommand(gameId, request.X, request.Y), ct);
        if(result.IsSuccess is false) return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Move failed",
            Detail = result.Error,
            Instance = HttpContext.Request.Path
        });
        
        var move = result.Value!;
        return Ok(new MoveResponse
        {
            GameId = move.Game.Id,
            IsXMoved = move.IsXMove,
            State = move.Game.State,
            X = move.X,
            Y = move.Y
        });
    }
    
    
    [HttpPost]
    public async Task<ActionResult<GameResponse>> CreateGame(CancellationToken ct)
    {
        var result = await mediator.Send(new CreateGameCommand(), ct);
        
        return Ok(result.MapToGameResponse());
    }
    
    
    [HttpGet("{gameId:int}")]
    public async Task<ActionResult<GameResponse>> Get(int gameId, CancellationToken ct)
    {
        var result = await mediator.Send(new GetGameCommand(gameId), ct);
        if(result.IsSuccess is false) return NotFound(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Get game failed",
            Detail = result.Error,
            Instance = HttpContext.Request.Path
        });
        
        
        return Ok(result.Value!.MapToGameResponse());
    }
}