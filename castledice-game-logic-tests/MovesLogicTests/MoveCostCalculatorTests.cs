using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Rules;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.MovesLogicTests;

public class MoveCostCalculatorTests
{
    private class GetMoveCostTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return GetPlaceMoveCase(placementCost: 3);
            yield return GetPlaceMoveCase(placementCost: 1);
            yield return GetCaptureMoveCase((p) => 3);
            yield return GetCaptureMoveCase((p) => p.ActionPoints.Amount);
            yield return GetReplaceMoveCase(3, 1);
            yield return GetReplaceMoveCase(2, 1);
            yield return GetUpgradeMoveCase(1);
            yield return GetUpgradeMoveCase(2);
            yield return GetRemoveMoveCase(3);
            yield return GetRemoveMoveCase(1);
        }

        private static object[] GetPlaceMoveCase(int placementCost)
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(new PlayerUnitMock(){Owner = player});
            var placement = new PlaceableMock() { Cost = placementCost };
            var placeMove = new PlaceMove(player, (1, 1), placement);
            return new object[] { board, placeMove, placementCost };
        }

        private static object[] GetCaptureMoveCase(Func<Player, int> getCaptureCostFunc)
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(new PlayerUnitMock(){Owner = player});
            var capturable = new CapturableMock() { GetCaptureCostFunc = getCaptureCostFunc };
            board[1, 1].AddContent(capturable);
            var captureMove = new CaptureMove(player, (1, 1));
            return new object[] { board, captureMove, getCaptureCostFunc(player) };
        }

        private static object[] GetReplaceMoveCase(int replaceCost, int replacementCost)
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(new PlayerUnitMock(){Owner = player});
            var replaceable = new ReplaceableMock() { ReplaceCost = replaceCost };
            var replacement = new PlaceableMock() { Cost = replacementCost };
            board[1, 1].AddContent(replaceable);
            var replaceMove = new ReplaceMove(player, (1, 1), replacement);
            return new object[] { board, replaceMove, ReplaceRules.GetReplaceCost(board, (1,1), replacement) };
        }

        private static object[] GetUpgradeMoveCase(int upgradeCost)
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(new UpgradeableMock(){Owner = player, UpgradeCost = upgradeCost});
            var upgradeMove = new UpgradeMove(player, (0, 0));
            return new object[] { board, upgradeMove, upgradeCost };
        }

        private static object[] GetRemoveMoveCase(int removeCost)
        {
            var board = GetFullNByNBoard(2);
            var player = GetPlayer();
            board[0, 0].AddContent(new PlayerUnitMock(){Owner = player});
            var removable = new RemovableMock() { RemoveCost = removeCost };
            board[1, 1].AddContent(removable);
            var removeMove = new RemoveMove(player, (1, 1));
            return new object[] { board, removeMove, removeCost };
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private class ImpossibleMovesTestCases : IEnumerable<object[]>
    {
        private static readonly Board Board = GetFullNByNBoard(2);
        private static readonly Player Player = GetPlayer();

        static ImpossibleMovesTestCases()
        {
            Board[0, 0].AddContent(new PlayerUnitMock(){Owner = Player});
        }
        
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return ReplaceMoveCase();
            yield return RemoveMoveCase();
            yield return UpgradeMoveCase();
            yield return CaptureMoveCase();
        }

        private static object[] ReplaceMoveCase()
        {
            var move = new ReplaceMove(Player, (1, 1), new PlaceableMock());
            return new object[] { Board, move };
        }

        private static object[] RemoveMoveCase()
        {
            var move = new RemoveMove(Player, (1, 1));
            return new object[] { Board, move };
        }

        private static object[] UpgradeMoveCase()
        {
            var move = new UpgradeMove(Player, (1, 1));
            return new object[] { Board, move };
        }
        
        private static object[] CaptureMoveCase()
        {
            var move = new CaptureMove(Player, (1, 1));
            return new object[] { Board, move };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [Theory]
    [ClassData(typeof(GetMoveCostTestData))]
    public void GetMoveCost_ShouldReturnCorrectCost(Board board, AbstractMove move, int expectedCost)
    {
        var moveCostCalculator = new MoveCostCalculator(board);
        var actualCost = moveCostCalculator.GetMoveCost(move);
        
        Assert.Equal(expectedCost, actualCost);
    }

    [Theory]
    [ClassData(typeof(ImpossibleMovesTestCases))]
    public void GetMoveCost_ShouldThrowArgumentException_IfCannotCalculateMoveCost(Board board, AbstractMove move)
    {
        var movesCalculator = new MoveCostCalculator(board);
        Assert.Throws<ArgumentException>(() => movesCalculator.GetMoveCost(move));
    }
}