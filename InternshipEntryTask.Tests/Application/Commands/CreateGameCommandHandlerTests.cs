using InternshipEntryTask.Application.Commands;
using InternshipEntryTask.Application.Interfaces;
using InternshipEntryTask.Application.Settings;
using InternshipEntryTask.Core.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace InternshipEntryTask.Tests.Application.Commands;

public class CreateGameCommandHandlerTests
{
    private readonly Mock<IOptions<TicTacToeGameOptions>> _mockOptions = new();
    private readonly Mock<ITicTacToeGameRepository> _mockRepository = new();

    [Fact]
    public async Task Handler_ShouldCreateGame_WhenOptionsAreValid()
    {
        // Arrange
        CreateGameCommand command = new();
        var options = new TicTacToeGameOptions { Size = 3, WinLineSize = 3 };
        _mockOptions.SetupGet(o => o.Value)
            .Returns(options);
        var handler = new CreateGameCommandHandler(_mockRepository.Object, _mockOptions.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.Size, options.Size);
        Assert.Equal(result.WinLineSize, options.WinLineSize);
        _mockRepository.Verify(r => 
            r.AddAsync(It.IsNotNull<TicTacToeGame>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}