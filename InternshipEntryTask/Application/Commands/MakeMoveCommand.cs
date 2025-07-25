using InternshipEntryTask.Application.DTOs;
using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public record MakeMoveCommand(int GameId, byte X, byte Y) : IRequest<Result<MoveDto>>;