using castledice_game_logic_tests.Mocks;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;
public class UpgradeMoveTests
{
    [Fact]
    public void Accept_ShouldCallVisitMethod_ForUpgradeMove()
    {
        var move = new UpgradeMoveBuilder().Build();
        var moveVisitor = new MoveVisitorMock();
        var expectedMethodType = VisitMethodType.Upgrade;
        
        move.Accept(moveVisitor);
        var actualMethodType = moveVisitor.CalledMethodType;
        
        Assert.Equal(expectedMethodType, actualMethodType);
    }
}