using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public record CreateGameCommand : IRequest<TicTacToeGame>;