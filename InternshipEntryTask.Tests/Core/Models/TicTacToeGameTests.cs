using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Tests.Core.Models;

public class TicTacToeGameTests
{
    public static IEnumerable<object[]> MoveCases =>
        new List<object[]>
        {
            new object[] { "XO_" +
                           "_X_" +
                           "XOO", 3, 3, 2, 0},
            new object[] { "XOX" +
                           "_X_" +
                           "_OO", 3, 3, 0, 2},            
            new object[] { "_OX" +
                           "_X_" +
                           "OOX", 3, 3, 0, 0},
            new object[] { "XOX" +
                           "_X_" +
                           "OO_", 3, 3, 2, 2},
            new object[] { "_XX" +
                           "_O_" +
                           "OOX", 3, 3, 0, 0},
            new object[] { "X__" +
                           "_OX" +
                           "OOX", 3, 3, 2, 0},
            new object[] { "XOX_X_" +
                           "_OOOXX" +
                           "_X__OO" +
                           "XOX_X_" +
                           "_OOXOX" +
                           "OX_O_O", 6, 4, 4, 5},
        };
    
    [Theory]
    [MemberData(nameof(MoveCases))]
    public void Move_ShouldXWinGame_WhenDoLastMove(string field, byte size, byte winLine, byte x, byte y)
    {
        // Arrange
        var game = new TicTacToeGame(size, winLine);
        LoadGame(game, field);

        // Act
        var result = game.Move(x, y,  () => false);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(GameState.WinningX, game.State);
        
    }
    
    [Theory]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void Move_ShouldArgumentException_WhenSizeOrWinLineIsIncorrect(byte size, byte winLine)
    {
        // Act
        void Act()
        {
            _ = new TicTacToeGame(size, winLine);
        }

        // Assert
        Assert.Throws<ArgumentException>(Act);
        
    }

    [Theory]
    [InlineData("XO_" + 
                "_X_" +
                "XO_", 3, 3, 2, 2)]
    [InlineData("_OX" +
                "_X_" +
                "_OX", 3, 3, 0, 0)]
    public void Move_ShouldXWinGame_WhenODoMoveAndRandomChangeToXMove(string field, byte size, byte winLine, byte x, byte y)
    {
        // Arrange
        var game = new TicTacToeGame(size, winLine);
        LoadGame(game, field);
        
        // Act
        var result = game.Move(x, y,  () => true);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(GameState.WinningX, game.State);
        
    }

    [Theory]
    [InlineData("XOX_" + 
                "_O__" +
                "X_X_" +
                "OO__", 4, 3, 1, 2)]
    [InlineData("_OXX" + 
                "X__O" +
                "X___" +
                "OO__", 4, 3, 2, 2)]
    public void Move_ShouldOWinGame_WhenXDoMoveAndRandomChangeToOMove(string field, byte size, byte winLine, byte x, byte y)
    {
        // Arrange
        var game = new TicTacToeGame(size, winLine);
        LoadGame(game, field);
        
        // Act
        var result = game.Move(x, y,  () => true);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(GameState.WinningO, game.State);
    }
    
    [Fact]
    public void Move_ShouldDrawGame_WhenDoLastMove()
    {
        // Arrange
        var game = new TicTacToeGame(3, 3);
        var field = 
            "XOX" +
            "OO_" +
            "XXO";
        LoadGame(game, field);
        
        // Act
        var result = game.Move(2, 1,  () => false);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(GameState.Draw, game.State);
    }
    
    [Fact]
    public void Move_ShouldError_WhenGameIsEnd()
    {
        // Arrange
        var game = new TicTacToeGame(3, 3);
        var field = 
            "XOX" +
            "OOX" +
            "XXO";
        LoadGame(game, field);
        
        // Act
        var result = game.Move(2, 1,  () => false);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(GameState.Draw, game.State);
    }
    
    [Fact]
    public void Move_ShouldError_WhenXOutOfRange()
    {
        // Arrange
        var game = new TicTacToeGame(3, 3);
        
        // Act
        var result = game.Move(4, 1,  () => false);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(GameState.Process, game.State);
    }
    
    [Fact]
    public void Move_ShouldError_WhenYOutOfRange()
    {
        // Arrange
        var game = new TicTacToeGame(3, 3);
        
        // Act
        var result = game.Move(1, 4,  () => false);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(GameState.Process, game.State);
    }
    
    [Fact]
    public void Move_ShouldError_WhenMoveToNotEmptyCell()
    {
        // Arrange
        var game = new TicTacToeGame(3, 3);
        game.Move(0, 0,  () => false);
        
        
        // Act
        var result = game.Move(0, 0,  () => false);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(GameState.Process, game.State);
    }

    private static void LoadGame(TicTacToeGame game, string field)
    {
        var xMoves = GetMoves(field, TicTacToeGame.X);
        var oMoves = GetMoves(field, TicTacToeGame.O);
        
        for(byte i = 0; i < xMoves.Count; i++)
        {
            ToXY(xMoves[i], game.Size, out var x, out var y);
            game.Move(x, y, () => false);

            if (oMoves.Count > i)
            {
                ToXY(oMoves[i], game.Size, out x, out y);
                game.Move(x, y, () => false);
            }
        }
    }

    private static List<int> GetMoves(string field, char symbol)
    {
        return field.Select((value, index) => new { value, index })
            .Where(x => x.value == symbol)
            .Select(x => x.index).ToList();
    }

    private static void ToXY(int position, byte size, out byte x, out byte y)
    {
        x = (byte)(position % size);
        y = (byte)(position / size);
    }
}