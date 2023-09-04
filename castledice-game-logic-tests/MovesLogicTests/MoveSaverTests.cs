using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class MoveSaverTests
{
    private class SaveMoveTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return PlaceMoveCase();
            yield return ReplaceMoveCase();
            yield return UpgradeMoveCase();
            yield return CaptureMoveCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] PlaceMoveCase()
        {
            var board = GetFullNByNBoard(2);
            int moveCost = 3;
            var move = new PlaceMoveBuilder()
            {
                Player = GetPlayer(id: 1234),
                Position = (1, 1),
                Content = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Knight, Cost = moveCost}
            }.Build();
            var snapshot = new PlaceMoveSnapshot(move, moveCost);
            return new object[] { board, move, snapshot };
        }

        private static object[] ReplaceMoveCase()
        {
            int moveCost = 4;
            var board = GetFullNByNBoard(3);
            var position = new Vector2Int(2, 1);
            var replaceable = new ReplaceableMock() { RemoveCost = 3 };
            board[position].AddContent(replaceable);
            var move = new ReplaceMoveBuilder()
            {
                Player = GetPlayer(id: 2345),
                Position = position,
                Replacement = new PlaceableMock() { PlacementTypeToReturn = PlacementType.HeavyKnight, Cost = 2}
            }.Build();
            var snapshot = new ReplaceMoveSnapshot(move, moveCost);
            return new object[] { board, move, snapshot };
        }

        private static object[] UpgradeMoveCase()
        {
            int moveCost = 3;
            var board = GetFullNByNBoard(3);
            var upgradeable = new UpgradeableMock() { UpgradeCost = moveCost };
            var position = new Vector2Int(2, 2);
            board[position].AddContent(upgradeable);
            var move = new UpgradeMoveBuilder()
            {
                Player = GetPlayer(id: 3456),
                Position = position,
            }.Build();
            var snapshot = new UpgradeMoveSnapshot(move, moveCost);
            return new object[] { board, move, snapshot };
        }

        private static object[] CaptureMoveCase()
        {
            int moveCost = 3;
            var board = GetFullNByNBoard(3);
            var capturable = new CapturableMock() { GetCaptureCostFunc = (p) => moveCost };
            var position = new Vector2Int(1, 2);
            board[position].AddContent(capturable);
            var move = new CaptureMoveBuilder()
            {
                Player = GetPlayer(id: 4567),
                Position = position,
            }.Build();
            var snapshot = new CaptureMoveSnapshot(move, moveCost);
            return new object[] { board, move, snapshot };
        }
    }
    
    [Theory]
    [ClassData(typeof(SaveMoveTestCases))]
    public void SaveMove_ShouldAddSnapshotToHistory(Board board, AbstractMove move, AbstractMoveSnapshot expectedSnapshot)
    {
        var history = new ActionsHistory();
        var saver = new MoveSaver(history, board);
        
        saver.SaveMove(move);

        Assert.Contains(expectedSnapshot, history.History);
    }
}