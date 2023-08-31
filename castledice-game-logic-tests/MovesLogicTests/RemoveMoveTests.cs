using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class RemoveMoveTests
{
    [Fact]
    public void ReplacementProperty_ShouldReturnContent_GivenInConstructor()
    {
        var content = GetPlaceable();
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        var move = new RemoveMove(player, position, content);

        var actualContent = move.Replacement;
        
        Assert.Same(content, actualContent);
    }

    [Fact]
    public void Accept_ShouldCallVisitMethod_ForRemoveMove()
    {
        var move = new RemoveMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedType = VisitMethodType.Remove;
        
        move.Accept(moveVisitor);
        var actualType = moveVisitor.CalledMethod;
        
        Assert.Equal(expectedType, actualType);
    }
}