using InternshipEntryTask.Application.Settings;

namespace InternshipEntryTask.Tests.Application.Settings;

public class TicTacToeGameOptionsValidationTests
{
    private readonly TicTacToeGameOptionsValidation _validator = new ();
    
    [Fact]
    public void Validate_ShouldReturnError_WhenSizeLessThanWinLineSize()
    {
        // Arrange
        var options = new TicTacToeGameOptions()
        {
            Size = 3,
            WinLineSize = 4
        };
        
        // Act
        var result = _validator.Validate(null, options);
        
        // Assert
        Assert.False(result.Succeeded);
    }
    
    [Fact]
    public void Validate_ShouldReturnSuccess_WhenSizeAndWinLineSizeValid()
    {
        // Arrange
        var options = new TicTacToeGameOptions()
        {
            Size = 3,
            WinLineSize = 3
        };
        
        // Act
        var result = _validator.Validate(null, options);
        
        // Assert
        Assert.True(result.Succeeded);
    }
    
    [Theory]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    public void Validate_ShouldReturnError_WhenSizeAndWinLineSizeInvalid(byte size, byte winLineSize)
    {
        // Arrange
        var options = new TicTacToeGameOptions()
        {
            Size = size,
            WinLineSize = winLineSize
        };
        
        // Act
        var result = _validator.Validate(null, options);
        
        // Assert
        Assert.False(result.Succeeded);
    }
}