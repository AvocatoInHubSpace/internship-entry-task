using System.Text;
using InternshipEntryTask.Core.Validators;

namespace InternshipEntryTask.Core.Models;

public class TicTacToeGame
{
    public const char X = 'X';
    public const char O = 'O';
    public const char Empty = '_';
    
    public int Id { get; init; }
    public byte Size { get; private set; }
    public byte WinLineSize { get; private set; }
    public bool IsXMove { get; private set; } = true;

    public ushort MovesCount { get; private set; } = 0;
    public GameState State { get; private set; } = GameState.InProgress;
    
    public string Field { get; private set; }
    
    
    public TicTacToeGame(byte size, byte winLineSize)
    {
        var result = TicTacToeGameParametersValidator.Validate(size, winLineSize);
        if (result.IsSuccess is false) throw new ArgumentException(result.Error);
        
        Size = size;
        WinLineSize = winLineSize;
        Field = new string(Empty, size * size);
    }
    
    
    public Result<bool> Move(byte x, byte y, Func<bool> randomChangeToMove)
    {
        int position = Size * y + x;
        var validationResult = Validate(x, y, position);
        if(validationResult.IsSuccess is false) 
            return Result<bool>.Failure(validationResult.Error);
        
        MovesCount++;
        var isXMove = DetermineCurrentMoveIsX(randomChangeToMove);
        var unit = isXMove ? X : O;

        Field = new StringBuilder().Append(Field.AsSpan(0, position))
            .Append(unit)
            .Append(Field.AsSpan(position + 1))
            .ToString();
        IsXMove = !IsXMove;

        if (CheckWinAt(x, y, unit))
        {
            State = isXMove ? GameState.XWon : GameState.OWin;
        }
        else if (MovesCount == Size * Size)
        {
            State = GameState.Draw;
        }
        
        return Result<bool>.Success(isXMove);
    }

    private Result Validate(byte x, byte y, int position)
    {
        if(x >= Size || y >= Size) return Result.Failure("Cell position ({x}, {y}) is outside the game field.");
        if(State != GameState.InProgress) return Result.Failure("Game is end");
        if(Field[position] != Empty) return Result.Failure("Cell is not empty");
        return Result.Success();
    }

    private bool DetermineCurrentMoveIsX(Func<bool> randomChangeToMove)
    {
        var isXMove = IsXMove;
        if (MovesCount % 3 == 0)
        {
            var shouldInvertMove = randomChangeToMove();    
            if (shouldInvertMove) isXMove = !isXMove;
        }
        return isXMove;
    }


    private bool CheckWinAt(byte x, byte y, char symbol)
    {
        return (CountInDirection(y, x, 0, 1, symbol) + CountInDirection(y, x, 0, -1, symbol) - 1 >= WinLineSize) || 
               (CountInDirection(y, x, 1, -1, symbol) + CountInDirection(y, x, -1, 1, symbol) - 1 >= WinLineSize) || 
               (CountInDirection(y, x, 1, 0, symbol) + CountInDirection(y, x, -1, 0, symbol) - 1 >= WinLineSize) || 
               (CountInDirection(y, x, 1, 1, symbol) + CountInDirection(y, x, -1, -1, symbol) - 1 >= WinLineSize);   
    }

    private int CountInDirection(byte startY, byte startX, sbyte offsetY, sbyte offsetX, char symbol)
    {
        short y = startY, x = startX;
        ushort count = 0;
        while (y >= 0 && y < Size && x >= 0 && x < Size && Field[ToPosition(x, y, Size)] == symbol)
        {
            count++;
            y += offsetY;
            x += offsetX;
        }
        return count;
    }

    private int ToPosition(short x, short y, byte size)
    {
        return size * y + x;
    }
}