using InternshipEntryTask.Application.Commands;
using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using Moq;

namespace InternshipEntryTask.Tests.Application.Commands;

public class MakeMoveCommandHandlerTests
{
    Mock<ITicTacToeGameRepository> _mockRepository = new();
    Mock<ITicTacToeGameService> _mockGameService = new();
    
    [Fact]
    public async Task Handler_ShouldMakeMove_WhenOptionsAreValid()
    {
        // Arrange
        var command = new MakeMoveCommand(0, 0, 0);        
        var game = new TicTacToeGame(3, 3);
        _mockRepository.Setup(r => r.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<TicTacToeGame>.Success(game));
        _mockGameService.Setup(gs => 
                gs.Move(It.IsNotNull<TicTacToeGame>(), It.IsAny<byte>(), It.IsAny<byte>()))
            .Returns(Result<TicTacToeGame>.Success(game));
        var handler = new MakeMoveCommandHandler(_mockRepository.Object, _mockGameService.Object);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value, game);        
        _mockGameService.Verify(gs => 
            gs.Move(It.IsNotNull<TicTacToeGame>(), It.IsAny<byte>(), It.IsAny<byte>()), Times.Once);
        _mockRepository.Verify(r => 
            r.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);        
        _mockRepository.Verify(r => 
            r.UpdateAsync(It.IsNotNull<TicTacToeGame>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handler_ShouldReturnResultError_WhenGameNotFound()
    {
        // Arrange
        var command = new MakeMoveCommand(0, 0, 0);        
        var game = new TicTacToeGame(3, 3);
        _mockRepository.Setup(r => r.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<TicTacToeGame>.Failure());
        _mockGameService.Setup(gs => 
                gs.Move(It.IsNotNull<TicTacToeGame>(), It.IsAny<byte>(), It.IsAny<byte>()))
            .Returns(Result<TicTacToeGame>.Success(game));
        var handler = new MakeMoveCommandHandler(_mockRepository.Object, _mockGameService.Object);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);        
        _mockRepository.Verify(r => 
            r.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);        
        _mockRepository.Verify(r => 
            r.UpdateAsync(It.IsNotNull<TicTacToeGame>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockGameService.Verify(gs => 
            gs.Move(It.IsNotNull<TicTacToeGame>(), It.IsAny<byte>(), It.IsAny<byte>()), Times.Never);
    }
    
    
    [Fact]
    public async Task Handler_ShouldReturnResultError_WhenMoveFailed()
    {
        // Arrange
        var command = new MakeMoveCommand(0, 0, 0);        
        var game = new TicTacToeGame(3, 3);
        _mockRepository.Setup(r => r.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<TicTacToeGame>.Success(game));
        _mockGameService.Setup(gs => 
                gs.Move(It.IsNotNull<TicTacToeGame>(), It.IsAny<byte>(), It.IsAny<byte>()))
            .Returns(Result<TicTacToeGame>.Failure());
        var handler = new MakeMoveCommandHandler(_mockRepository.Object, _mockGameService.Object);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        _mockGameService.Verify(gs => 
            gs.Move(It.IsNotNull<TicTacToeGame>(), It.IsAny<byte>(), It.IsAny<byte>()), Times.Once);
        _mockRepository.Verify(r => 
            r.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockRepository.Verify(r => 
            r.UpdateAsync(It.IsNotNull<TicTacToeGame>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}