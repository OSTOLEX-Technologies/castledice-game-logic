using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Math;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class UnitBranchesCutterTests
{
    private class CutUnconnectedBranchesTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return OneCastleCase();
            yield return TwoCastlesCase();
            yield return NoCastleCase();
            yield return TwoCastlesConnectedCase();
            yield return EnemyUnitsCase();
            yield return RealisticCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] OneCastleCase()
        {
            var player = GetPlayer();
            var castle = GetCastle(player);
            var board = GetFullNByNBoard(4);
            board[0, 0].AddContent(castle);

            var unitsMustStay = new List<Vector2Int>()
            {
                (1, 1),
                (2, 1),
                (3, 0)
            };
            var unitsMustDisappear = new List<Vector2Int>()
            {
                (0, 3),
                (1, 3),
                (2, 3)
            };
            foreach (var position in new List<Vector2Int>(unitsMustStay).Concat(unitsMustDisappear))
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = player});
            }

            return new object[] {player, board, unitsMustStay, unitsMustDisappear };
        }

        private static object[] NoCastleCase()
        {
            var player = GetPlayer();
            var board = GetFullNByNBoard(4);
            var unitsMustStay = new List<Vector2Int>();
            var unitsMustDisappear = new List<Vector2Int>()
            {
                (0, 0),
                (0, 3),
                (1, 3),
                (2, 3)
            };
            foreach (var position in new List<Vector2Int>(unitsMustStay).Concat(unitsMustDisappear))
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = player});
            }

            return new object[] {player, board, unitsMustStay, unitsMustDisappear };
        }
        
        private static object[] TwoCastlesCase()
        {
            var player = GetPlayer();
            var firstCastle = GetCastle(player);
            var secondCastle = GetCastle(player);
            var board = GetFullNByNBoard(5);
            board[0, 0].AddContent(firstCastle);
            board[0, 4].AddContent(secondCastle);
            var unitsMustStay = new List<Vector2Int>()
            {
                (1, 1),
                (2, 1),
                (1, 3),
                (2, 3)
            };
            var unitsMustDisappear = new List<Vector2Int>()
            {
                (4, 1),
                (4, 2),
                (4, 3)
            };
            foreach (var position in new List<Vector2Int>(unitsMustStay).Concat(unitsMustDisappear))
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = player});
            }

            return new object[] {player, board, unitsMustStay, unitsMustDisappear };
        }

        private static object[] TwoCastlesConnectedCase()
        {
            var player = GetPlayer();
            var firstCastle = GetCastle(player);
            var secondCastle = GetCastle(player);
            var board = GetFullNByNBoard(5);
            board[0, 0].AddContent(firstCastle);
            board[0, 4].AddContent(secondCastle);
            var unitsMustStay = new List<Vector2Int>()
            {
                (1, 1),
                (2, 1),
                (1, 3),
                (2, 3),
                (2, 2)
            };
            var unitsMustDisappear = new List<Vector2Int>()
            {
                (4, 1),
                (4, 2),
                (4, 3)
            };
            foreach (var position in new List<Vector2Int>(unitsMustStay).Concat(unitsMustDisappear))
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = player});
            }

            return new object[] {player, board, unitsMustStay, unitsMustDisappear };
        }

        //Enemy units should not be removed when cutting player units.
        private static object[] EnemyUnitsCase()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var enemy = GetPlayer();
            var playerCastle = GetCastle(player);
            var enemyCastle = GetCastle(enemy);
            board[0, 0].AddContent(playerCastle);
            board[9, 9].AddContent(enemyCastle);
            var unitsMustStay = new List<Vector2Int>()
            {
                (1, 1),
                (2, 1),
                (3, 2),
                (3, 3),
                (2, 4)
            };
            var enemyUnits = new List<Vector2Int>()
            {
                (0, 5),
                (1, 6),
                (2, 6),
                (3, 6),
                (4, 5),
            };
            var unitsMustDisappear = new List<Vector2Int>()
            {

                (5, 4),
                (8, 8),
                (8, 9),
                (9, 8)
            };
            foreach (var position in enemyUnits)
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = enemy});
            }
            foreach (var position in new List<Vector2Int>(unitsMustStay).Concat(unitsMustDisappear))
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = player});
            }
            unitsMustStay = unitsMustStay.Concat(enemyUnits).ToList();
            return new object[] {player, board, unitsMustStay, unitsMustDisappear };
        }

        private static object[] RealisticCase()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var enemy = GetPlayer();
            var playerCastle = GetCastle(player);
            var enemyCastle = GetCastle(enemy);
            board[0, 0].AddContent(playerCastle);
            board[9, 9].AddContent(enemyCastle);
            var unitsMustStay = new List<Vector2Int>()
            {
                (1, 1),
                (2, 1),
                (3, 2),
                (3, 3),
                (2, 4)
            };
            var unitsMustDisappear = new List<Vector2Int>()
            {
                (0, 5),
                (1, 6),
                (2, 6),
                (3, 6),
                (4, 5),
                (5, 4),
                (8, 8),
                (8, 9),
                (9, 8)
            };
            foreach (var position in new List<Vector2Int>(unitsMustStay).Concat(unitsMustDisappear))
            {
                board[position].AddContent(new PlayerUnitMock(){Owner = player});
            }
            return new object[] {player, board, unitsMustStay, unitsMustDisappear };
        }
    }

    [Fact]
    public void CutUnconnectedBranches_ShouldNotRemoveCastles()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var firstCastle = GetCastle(player);
        var secondCastle = GetCastle(player);
        board[0, 0].AddContent(firstCastle);
        board[2, 2].AddContent(secondCastle);
        var cutter = new UnitBranchesCutter(board);
        
        cutter.CutUnconnectedBranches(player);
        
        Assert.True(board[0, 0].HasContent(c => c == firstCastle));
        Assert.True(board[2, 2].HasContent(c => c == secondCastle));
    }

    [Fact]
    public void CutUnconnectedBranches_ShouldFreeUnconnectedCapturables()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var castle = GetCastle(player);
        var capturable = new CapturableMock(){Owner = player};
        board[0, 0].AddContent(castle);
        board[2, 2].AddContent(capturable);
        var cutter = new UnitBranchesCutter(board);
        
        cutter.CutUnconnectedBranches(player);
        
        Assert.True(capturable.GetOwner().IsNull);
    }
    
    [Theory]
    [ClassData(typeof(CutUnconnectedBranchesTestCases))]
    public void CutUnconnectedBranches_ShouldRemoveUnconnectedUnits(Player player, Board board, List<Vector2Int> unitsMustStay, List<Vector2Int> unitsMustDisappear)
    {
        var cutter = new UnitBranchesCutter(board);
        
        cutter.CutUnconnectedBranches(player);
        
        foreach (var position in unitsMustStay)
        {
            Assert.True(board[position].HasContent(c => c is PlayerUnitMock));
        }
        foreach (var position in unitsMustDisappear)
        {
            Assert.False(board[position].HasContent());
        }
    }
}