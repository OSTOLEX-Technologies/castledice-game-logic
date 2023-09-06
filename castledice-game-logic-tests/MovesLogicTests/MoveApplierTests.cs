﻿using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
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
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] PlaceMoveCase()
        {
            var board = GetFullNByNBoard(2);
            var contentToPlace = new PlaceableMock() { Cost = 3 };
            var player = GetPlayer(actionPoints: 6);
            var move = new PlaceMoveBuilder() { Content = contentToPlace, Player = player, Position = (0, 0)}.Build();
            int expectedActionPoints = 3;

            return new object[] { board, player, move, expectedActionPoints };
        }

        private static object[] ReplaceMoveCase()
        {
            var board = GetFullNByNBoard(2);
            var replaceable = new ReplaceableMock() { ReplaceCost = 3 };
            var player = GetPlayer(actionPoints: 6);
            var replacement = new PlaceableMock() { Cost = 2 };
            board[0, 0].AddContent(replaceable);
            var move = new ReplaceMoveBuilder()
                {
                    Player = player, Replacement = replacement, Position = (0, 0)
                }.Build();
            int expectedActionPoints = 2;

            return new object[] { board, player, move, expectedActionPoints };
        }

        private static object[] UpgradeMoveCase()
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer(actionPoints: 6);
            var upgradeable = new UpgradeableMock() { Owner = player, UpgradeCost = 4 };
            board[0, 0].AddContent(upgradeable);
            var move = new UpgradeMoveBuilder() { Player = player, Position = (0, 0) }.Build();
            int expectedActionPoints = 2;
            
            return new object[] { board, player, move, expectedActionPoints };
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
        int expectedActionPoints)
    {
        var applier = new MoveApplier(board);
        applier.ApplyMove(move);

        int actualActionPoints = player.ActionPoints.Amount;
        
        Assert.Equal(expectedActionPoints, actualActionPoints);
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
    
    //This test determines whether Capture method was called by checking Owner property of CapturableMock
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
}