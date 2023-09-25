using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class UnitsFactoryTests
{
    [Fact]
    public void CreatePlaceable_ShouldReturnKnight_IfKnightPlacementTypeIsGiven()
    {
        var knightsFactoryMock = new Mock<IKnightsFactory>();
        knightsFactoryMock.Setup(m => m.GetKnight(It.IsAny<Player>())).Returns(GetKnight(GetPlayer()));
        var factory = new UnitsFactory(knightsFactoryMock.Object);
        
        var placeable = factory.CreatePlaceable(PlacementType.Knight, GetPlayer());
        
        Assert.IsType<Knight>(placeable);
    }
    
    [Fact]
    //This rule is not applied to content that can be placed by player but is not player owned.
    public void CreatePlaceable_ShouldReturnPlaceable_WithGivenCreatorAsOwner()
    {
        var knightsFactoryMock = new Mock<IKnightsFactory>();
        var creator = GetPlayer();
        knightsFactoryMock.Setup(m => m.GetKnight(creator)).Returns(GetKnight(creator));
        var factory = new UnitsFactory(knightsFactoryMock.Object);
        
        var placeable = factory.CreatePlaceable(PlacementType.Knight, creator);
        if (placeable is IPlayerOwned owned)
        {
            Assert.Same(creator, owned.GetOwner());
        }
    }
}