using castledice_game_logic_tests.Mocks;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;
public class CaptureMoveTests
{
    [Fact]
    public void Accept_ShouldCallVisitMethod_ForCaptureMove()
    {
        var move = new CaptureMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedMethodType = VisitMethodType.Capture;
        
        move.Accept(moveVisitor);
        var actualMethodType = moveVisitor.CalledMethodType;
        
        Assert.Equal(expectedMethodType, actualMethodType);
    }
}