using FluentAssertions;
using MeraStore.Shared.Kernel.Persistence.Enums;
using MeraStore.Shared.Kernel.Persistence.Interfaces;
using MeraStore.Shared.Kernel.Persistence.Strategy;
using Moq;

namespace MeraStore.Shared.Kernel.Persistence.Tests.Strategy;

public class StrategyResolverTests
{
    [Fact]
    public void Resolve_Should_Return_Correct_Strategy_When_Registered()
    {
        // Arrange
        var mockStrategy = new Mock<ICommitStrategy>();
        mockStrategy.Setup(s => s.StrategyType).Returns(CommitType.Custom);

        var resolver = new StrategyResolver(new List<ICommitStrategy> { mockStrategy.Object });

        // Act
        var resolved = resolver.Resolve(CommitType.Custom);

        // Assert
        resolved.Should().Be(mockStrategy.Object);
    }

    [Fact]
    public void Resolve_Should_Throw_NotSupportedException_When_Strategy_Not_Registered()
    {
        // Arrange
        var mockStrategy = new Mock<ICommitStrategy>();
        mockStrategy.Setup(s => s.StrategyType).Returns(CommitType.Custom);

        var resolver = new StrategyResolver(new List<ICommitStrategy> { mockStrategy.Object });

        // Act
        var act = () => resolver.Resolve(CommitType.Default);

        // Assert
        act.Should().Throw<NotSupportedException>()
            .WithMessage("Save strategy Default not registered.");
    }
}