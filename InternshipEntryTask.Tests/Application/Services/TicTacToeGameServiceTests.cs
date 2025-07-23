using InternshipEntryTask.Application.Services;
using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using Moq;
using Range = Moq.Range;

namespace InternshipEntryTask.Tests.Application.Services;

public class TicTacToeGameServiceTests
{
    [Fact]
    public void Move_ShouldCallRandomService_WhenThirdMove()
    {
        // Arrange
        var mockRandomService = new Mock<IRandomService>();
        var gameService = new TicTacToeGameService(mockRandomService.Object);
        var game = new TicTacToeGame(3, 3);
        
        // Act
        gameService.Move(game, 0, 0);
        gameService.Move(game, 0, 1);
        gameService.Move(game, 0, 2);
        
        // Assert
        mockRandomService.Verify(rs => rs.NextProbability(It.IsInRange<byte>(0, 100, Range.Inclusive)), Times.Once);
    }
}