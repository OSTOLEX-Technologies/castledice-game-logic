using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class PlaceMoveTests
{
    [Fact]
    public void ContentProperty_ShouldReturnContent_GivenInConstructor()
    {
        var expectedContent = GetPlaceable();
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        var move = new PlaceMove(player, position, expectedContent);

        var actualContent = move.ContentToPlace;
        
        Assert.Same(expectedContent, actualContent);
    }

    [Fact]
    public void Accept_ShouldCallVisitMethod_ForPlaceMove()
    {
        var move = new PlaceMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedMethodType = VisitMethodType.Place;
        
        move.Accept(moveVisitor);
        var actualMethodType = moveVisitor.CalledMethodType;
        
        Assert.Equal(expectedMethodType, actualMethodType);
    }
}