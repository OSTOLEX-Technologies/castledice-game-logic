using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Rules;
using Moq;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class MoveApplierTests
{
    public class MovesActionPointsDecreaseTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return PlaceMoveCase();
            yield return ReplaceMoveCase();
            yield return UpgradeMoveCase();
            yield return RemoveMoveCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] PlaceMoveCase()
        {
            int playerActionPoints = 6;
            var board = GetFullNByNBoard(2);
            var movePosition = new Vector2Int(0, 0);
            var contentToPlace = new PlaceableMock() { Cost = 3 };
            var player = GetPlayer(actionPoints: playerActionPoints);
            var move = new PlaceMoveBuilder() { Content = contentToPlace, Player = player, Position = movePosition}.Build();
            int expectedActionPointsLast = playerActionPoints - PlaceRules.GetPlaceCost(contentToPlace);

            return new object[] { board, player, move, expectedActionPointsLast };
        }

        private static object[] ReplaceMoveCase()
        {
            int playerActionPoints = 6;
            var board = GetFullNByNBoard(2);
            var movePosition = new Vector2Int(0, 0);
            var replaceable = new ReplaceableMock() { ReplaceCost = 3 };
            var player = GetPlayer(actionPoints: playerActionPoints);
            var replacement = new PlaceableMock() { Cost = 2 };
            board[movePosition].AddContent(replaceable);
            var move = new ReplaceMoveBuilder()
                {
                    Player = player, Replacement = replacement, Position = movePosition
                }.Build();
            int expectedActionPointsLast = playerActionPoints - ReplaceRules.GetReplaceCost(board, move.Position, replacement);

            return new object[] { board, player, move, expectedActionPointsLast };
        }

        private static object[] UpgradeMoveCase()
        {
            int playerActionPoints = 6;
            var board = GetFullNByNBoard(2);
            var movePosition = new Vector2Int(0, 0);
            var player = GetPlayer(actionPoints: playerActionPoints);
            var upgradeable = new UpgradeableMock() { Owner = player, UpgradeCost = 4 };
            board[movePosition].AddContent(upgradeable);
            var move = new UpgradeMoveBuilder() { Player = player, Position = movePosition }.Build();
            int expectedActionPointsLast = playerActionPoints - UpgradeRules.GetUpgradeCost(board, move.Position);
            
            return new object[] { board, player, move, expectedActionPointsLast };
        }

        private static object[] RemoveMoveCase()
        {
            int playerActionPoints = 6;
            var board = GetFullNByNBoard(2);
            var movePosition = new Vector2Int(0, 0);
            var player = GetPlayer(actionPoints: playerActionPoints);
            var removable = new RemovableMock() { RemoveCost = 4};
            board[movePosition].AddContent(removable);
            var move = new RemoveMoveBuilder() { Player = player, Position = movePosition }.Build();
            int expectedActionPointsLast = playerActionPoints - RemoveRules.GetRemoveCost(board, move.Position);
            
            return new object[] { board, player, move, expectedActionPointsLast };
        }
        
    }
    
    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfNegativeMovePosition()
    {
        var board = GetFullNByNBoard(2);
        var move = new TestMoveBuilder() { Position = (-1, 0) }.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }
    
    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfNoCellOnPosition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        var move = new TestMoveBuilder() { Position = (1, 1) }.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }
    
    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfPositionOutsideOfBoard()
    {
        var board = GetFullNByNBoard(2);
        var move = new TestMoveBuilder() { Position = (10, 10) }.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfPlaceMovePlaceableIsNotContent()
    {
        var board = GetFullNByNBoard(2);
        var placeable = new Mock<IPlaceable>().Object;
        var move = new PlaceMoveBuilder() { Content = placeable }.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Theory]
    [ClassData(typeof(MovesActionPointsDecreaseTestCases))]
    public void ApplyMove_ShouldDecreasePlayerActionPoints_AccordingToMove(
        Board board, Player player, AbstractMove move,
        int expectedActionPointsLast)
    {
        var applier = new MoveApplier(board);
        applier.ApplyMove(move);

        int actualActionPoints = player.ActionPoints.Amount;
        
        Assert.Equal(expectedActionPointsLast, actualActionPoints);
    }

    [Fact]
    public void ApplyMove_ShouldPlaceContentOnBoard_IfPlaceMoveApplied()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var expectedContent = new PlaceableMock();
        var move = new PlaceMoveBuilder() { Player = player, Position = (0, 0), Content = expectedContent }.Build();
        var applier = new MoveApplier(board);
        
        applier.ApplyMove(move);
        var actualContentList = board[0, 0].GetContent();

        Assert.Contains(expectedContent, actualContentList);
    }

    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfReplaceMoveOnCellWithNoReplaceable()
    {
        var board = GetFullNByNBoard(2);
        var move = new ReplaceMoveBuilder(){Position = (0, 0)}.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfReplaceMoveReplacementIsNotContent()
    {
        var board = GetFullNByNBoard(2);
        var replaceable = new ReplaceableMock();
        board[0, 0].AddContent(replaceable);
        var replacement = new Mock<IPlaceable>().Object;
        var move = new ReplaceMoveBuilder(){Position = (0, 0), Replacement = replacement}.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Fact]
    public void ApplyMove_ShouldRemoveOldContentFromCell_IfReplaceMoveApplied()
    {
        var board = GetFullNByNBoard(2);
        var oldContent = new ReplaceableMock();
        var position = new Vector2Int(0, 0);
        board[position].AddContent(oldContent);
        var move = new ReplaceMoveBuilder().Build();
        var applier = new MoveApplier(board);
        
        applier.ApplyMove(move);
        var cellContent = board[position].GetContent();
        
        Assert.DoesNotContain(oldContent, cellContent);
    }

    [Fact]
    public void ApplyMove_ShouldPutNewContentOnCell_IfReplaceMoveApplied()
    {
        var board = GetFullNByNBoard(2);
        var replaceable = new ReplaceableMock();
        var newContent = new PlaceableMock();
        var position = new Vector2Int(0, 0);
        board[position].AddContent(replaceable);
        var move = new ReplaceMoveBuilder(){Replacement = newContent}.Build();
        var applier = new MoveApplier(board);
        
        applier.ApplyMove(move);
        var cellContent = board[position].GetContent();
        
        Assert.Contains(newContent, cellContent);
    }

    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfUpgradeMoveOnCellWithoutUpgradeable()
    {
        var board = GetFullNByNBoard(2);
        var move = new UpgradeMoveBuilder(){Position = (0, 0)}.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Fact]
    
    //This test determines whether Upgrade method was called by checking change of Level property
    //of UpgradeableMock class.
    public void ApplyMove_ShouldCallUpgradeMethodOnUpgradeable_IfUpgradeMoveApplied()
    {
        var board = GetFullNByNBoard(2);
        var upgradeable = new UpgradeableMock();
        board[0, 0].AddContent(upgradeable);
        int expectedLevel = upgradeable.Level + 1;
        var move = new UpgradeMoveBuilder() { Position = (0, 0) }.Build();
        var applier = new MoveApplier(board);
        
        applier.ApplyMove(move);
        int actualLevel = upgradeable.Level;

        Assert.Equal(expectedLevel, actualLevel);
    }

    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfCaptureMoveOnCellWithoutCapturable()
    {
        var board = GetFullNByNBoard(2);
        var move = new CaptureMoveBuilder().Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Fact]
    
    //This test determines whether CaptureHit method was called by checking Owner property of CapturableMock
    public void ApplyMove_ShouldCallCaptureMethodOnCapturableObject_IfCaptureMoveApplied()
    {
        var board = GetFullNByNBoard(2);
        var owner = GetPlayer();
        var capturer = GetPlayer();
        var capturable = new CapturableMock() { Owner = owner };
        board[0, 0].AddContent(capturable);
        var move = new CaptureMoveBuilder() { Player = capturer, Position = (0, 0) }.Build();
        var applier = new MoveApplier(board);
        
        applier.ApplyMove(move);
        var newOwner = capturable.Owner;
        
        Assert.Same(capturer, newOwner);
    }

    [Fact]
    public void ApplyMove_ShouldThrowArgumentException_IfRemoveMoveOnCellWithNoRemovables()
    {
        var board = GetFullNByNBoard(2);
        var move = new RemoveMoveBuilder() { Position = (0, 0) }.Build();
        var applier = new MoveApplier(board);

        Assert.Throws<ArgumentException>(() => applier.ApplyMove(move));
    }

    [Fact]
    public void ApplyMove_ShouldRemoveContent_IfRemoveMoveApplied()
    {
        var board = GetFullNByNBoard(2);
        var removable = new RemovableMock();
        board[0, 0].AddContent(removable);
        var move = new RemoveMoveBuilder() { Position = (0, 0) }.Build();
        var applier = new MoveApplier(board);
        
        applier.ApplyMove(move);
        var cellContent = board[0, 0].GetContent();
        
        Assert.DoesNotContain(removable, cellContent);
    }
}