using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Core.Common;
using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public record GetGameCommand(int Id) : IRequest<Result<TicTacToeGame, AppErrors>>;