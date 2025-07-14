using System.Text;
using InternshipEntryTask.Infrastructure;
using InternshipEntryTask.Infrastructure.Extensions;
using InternshipEntryTask.Interfaces;
using InternshipEntryTask.Models;

namespace InternshipEntryTask.Services;

public class TicTacToeGameService : ITicTacToeGameService
{
    private Random _random = new ();
    private const byte Probability = 10;
    private const char X = 'X';
    private const char O = 'O';
    private const char Empty = '_';
    
    public Result Move(TicTacToeGame ticTacToeGame, byte x, byte y)
    {
        int position = ticTacToeGame.Size * y + x;
        var validationResult = Validate(ticTacToeGame, x, y, position);
        if(validationResult.IsSuccess is false) return validationResult;
        
        ticTacToeGame.MovesCount++;
        var isXMove = DetermineCurrentMoveIsX(ticTacToeGame);
        var unit = isXMove ? X : O;

        ticTacToeGame.Field = new StringBuilder().Append(ticTacToeGame.Field.AsSpan(0, position))
            .Append(unit)
            .Append(ticTacToeGame.Field.AsSpan(position + 1))
            .ToString();

        if (CheckWinAt(ticTacToeGame, y, x, unit))
        {
            ticTacToeGame.State = isXMove ? GameState.WinningX : GameState.WinningO;
        }
        else if (ticTacToeGame.MovesCount == ticTacToeGame.Size * ticTacToeGame.Size)
        {
            ticTacToeGame.State = GameState.Draw;
        }
        
        return Result.Success();
    }

    private static Result Validate(TicTacToeGame ticTacToeGame, byte x, byte y, int position)
    {
        if(x >= ticTacToeGame.Size || y >= ticTacToeGame.Size) return Result.Failure("Cell position ({x}, {y}) is outside the game field.");
        if(ticTacToeGame.State != GameState.Process) return Result.Failure("Game is end");
        if(ticTacToeGame.Field[position] != Empty) return Result.Failure("Cell is not empty");
        return Result.Success();
    }

    private bool DetermineCurrentMoveIsX(TicTacToeGame ticTacToeGame)
    {
        var isXMove = ticTacToeGame.IsXMove;
        if (ticTacToeGame.MovesCount % 3 == 0)
        {
            var shouldInvertMove = _random.NextProbability(Probability);    
            if (shouldInvertMove) isXMove = !isXMove;
        }
        return isXMove;
    }


    private static bool CheckWinAt(TicTacToeGame ticTacToeGame, byte y, byte x, char symbol)
    {
        var size = ticTacToeGame.Size;
        return (CountInDirection(ticTacToeGame, y, x, 0, 1, symbol) + CountInDirection(ticTacToeGame, y, x, 0, -1, symbol) - 1 >= size) || 
               (CountInDirection(ticTacToeGame, y, x, 1, 0, symbol) + CountInDirection(ticTacToeGame, y, x, -1, 0, symbol) - 1 >= size) || 
               (CountInDirection(ticTacToeGame, y, x, 1, 1, symbol) + CountInDirection(ticTacToeGame, y, x, -1, -1, symbol) - 1 >= size) || 
               (CountInDirection(ticTacToeGame, y, x, 1, -1, symbol) + CountInDirection(ticTacToeGame, y, x, -1, 1, symbol) - 1 >= size);  
    }

    private static int CountInDirection(TicTacToeGame ticTacToeGame, byte startY, byte startX, sbyte offsetY, sbyte offsetX, char symbol)
    {
        var size = ticTacToeGame.Size;
        short y = startY, x = startX;
        ushort count = 0;
        while (y >= 0 && y < size && x >= 0 && x < size && ticTacToeGame.Field[ToPosition(y, x, size)] == symbol)
        {
            count++;
            y += offsetY;
            x += offsetX;
        }
        return count;
    }

    private static int ToPosition(short x, short y, byte size)
    {
        return size * y + x;
    }
}