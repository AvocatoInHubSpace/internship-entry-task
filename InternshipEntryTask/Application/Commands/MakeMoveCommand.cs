using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Application.DTOs;
using InternshipEntryTask.Core.Common;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public record MakeMoveCommand(int GameId, byte X, byte Y) : IRequest<Result<MoveDto, AppErrors>>;