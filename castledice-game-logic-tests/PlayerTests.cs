using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Time;
using Moq;

namespace castledice_game_logic_tests;

public class PlayerTests
{
    [Fact]
    public void ActionPointsProperty_ShouldReturnPlayerActionPoints_GivenInConstructor()
    {
        var expectedPoints = new PlayerActionPoints();
        var player = new PlayerBuilder{ActionPoints = expectedPoints }.Build();

        var actualPoints = player.ActionPoints;
        
        Assert.Same(expectedPoints, actualPoints);
    }

    [Fact]
    public void IdProperty_ShouldReturnId_GivenInConstructor()
    {
        var expectedId = new Random().Next();
        var player = new PlayerBuilder { Id = expectedId }.Build();

        var actualId = player.Id;
        
        Assert.Equal(expectedId, actualId);
    }
    
    [Fact]
    public void TimerProperty_ShouldReturnPlayerTimer_GivenInConstructor()
    {
        var expectedTimer = new Mock<IPlayerTimer>().Object;
        var player = new PlayerBuilder{Timer = expectedTimer }.Build();

        var actualTimer = player.Timer;
        
        Assert.Same(expectedTimer, actualTimer);
    }
    
    [Fact]
    public void DeckProperty_ShouldReturnDeck_GivenInConstructor()
    {
        var expectedDeck = new List<PlacementType>();
        var player = new PlayerBuilder{Deck = expectedDeck }.Build();

        var actualDeck = player.Deck;
        
        Assert.Same(expectedDeck, actualDeck);
    }

    private class PlayerBuilder
    {
        public PlayerActionPoints ActionPoints { get; set; } = new();
        public IPlayerTimer Timer { get; set; } = new Mock<IPlayerTimer>().Object;
        public List<PlacementType> Deck { get; set; } = new();
        public int Id { get; set; } = 0;
        
        public Player Build()
        {
            return new Player(ActionPoints, Timer, Deck, Id);
        }
    }
}