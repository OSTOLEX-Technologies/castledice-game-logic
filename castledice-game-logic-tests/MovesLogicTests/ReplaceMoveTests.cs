using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class ReplaceMoveTests
{
    [Fact]
    public void ReplacementProperty_ShouldReturnContent_GivenInConstructor()
    {
        var content = GetPlaceable();
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        var move = new ReplaceMove(player, position, content);

        var actualContent = move.Replacement;
        
        Assert.Same(content, actualContent);
    }

    [Fact]
    public void Accept_ShouldCallVisitMethod_ForReplaceMove()
    {
        var move = new ReplaceMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedMethodType = VisitMethodType.Replace;
        
        move.Accept(moveVisitor);
        var actualMethodType = moveVisitor.CalledMethodType;
        
        Assert.Equal(expectedMethodType, actualMethodType);
    }
}