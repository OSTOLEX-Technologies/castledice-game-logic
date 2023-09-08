using castledice_game_logic_tests.Mocks;

namespace castledice_game_logic_tests.SnapshotsTests;
using static ObjectCreationUtility;

public class RemoveMoveTests
{
    [Fact]
    public void Accept_ShouldCallVisitMethod_ForRemoveMove()
    {
        var move = new RemoveMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedMethodType = VisitMethodType.Remove;
        
        move.Accept(moveVisitor);
        var actualMethodType = moveVisitor.CalledMethodType;
        
        Assert.Equal(expectedMethodType, actualMethodType);
    }
}