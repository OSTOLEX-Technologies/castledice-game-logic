using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
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
            yield return RemoveMoveCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] PlaceMoveCase()
        {
            var move = new PlaceMoveBuilder()
            {
                Player = GetPlayer(id: 1234),
                Position = (1, 2),
                Content = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Knight }
            }.Build();
            var snapshot = new PlaceMoveSnapshot(move);
            return new object[] { move, snapshot };
        }

        private static object[] ReplaceMoveCase()
        {
            var move = new ReplaceMoveBuilder()
            {
                Player = GetPlayer(id: 2345),
                Position = (3, 4),
                Replacement = new PlaceableMock() { PlacementTypeToReturn = PlacementType.HeavyKnight }
            }.Build();
            var snapshot = new ReplaceMoveSnapshot(move);
            return new object[] { move, snapshot };
        }

        private static object[] UpgradeMoveCase()
        {
            var move = new UpgradeMoveBuilder()
            {
                Player = GetPlayer(id: 3456),
                Position = (5, 6),
            }.Build();
            var snapshot = new UpgradeMoveSnapshot(move);
            return new object[] { move, snapshot };
        }

        private static object[] CaptureMoveCase()
        {
            var move = new CaptureMoveBuilder()
            {
                Player = GetPlayer(id: 4567),
                Position = (7, 8),
            }.Build();
            var snapshot = new CaptureMoveSnapshot(move);
            return new object[] { move, snapshot };
        }

        private static object[] RemoveMoveCase()
        {
            var move = new RemoveMoveBuilder()
            {
                Player = GetPlayer(id: 12345),
                Position = (2, 3),
            }.Build();
            var snapshot = new RemoveMoveSnapshot(move);
            return new object[] { move, snapshot };
        }
    }
    
    [Theory]
    [ClassData(typeof(SaveMoveTestCases))]
    public void SaveMove_ShouldAddSnapshotToHistory(AbstractMove move, AbstractMoveSnapshot expectedSnapshot)
    {
        var history = new ActionsHistory();
        var saver = new MoveSaver(history);
        
        saver.SaveMove(move);

        Assert.Contains(expectedSnapshot, history.History);
    }
}