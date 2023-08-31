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
        var content = GetPlaceable();
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        var move = new PlaceMove(player, position, content);

        var actualContent = move.ContentToPlace;
        
        Assert.Same(content, actualContent);
    }

    [Fact]
    public void Accept_ShouldCallVisitMethod_ForPlaceMove()
    {
        var move = new PlaceMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedType = VisitMethodType.Place;
        
        move.Accept(moveVisitor);
        var actualType = moveVisitor.CalledMethod;
        
        Assert.Equal(expectedType, actualType);
    }
}