using System.Collections;
using castledice_game_logic;
using castledice_game_logic.Math;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class PlayerKickerTests
{
    private class PlayerCastlesData : IEnumerable<object[]>
    {
        private Player _player = GetPlayer();
        
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return OneCastleCase();
            yield return MultipleCastlesCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private object[] OneCastleCase()
        {
            var castle = GetCastle(_player);
            var board = GetFullNByNBoard(2);
            board[0, 0].AddContent(castle);
            var castlesList = new List<CastleGO>() { castle };
            return new object[] { _player, board, castlesList };
        }

        private object[] MultipleCastlesCase()
        {
            var board = GetFullNByNBoard(10);
            var castlePositions = new List<Vector2Int>()
            {
                (0, 1),
                (2, 0),
                (3, 3),
                (9, 9)
            };
            var castlesList = new List<CastleGO>();
            foreach (var position in castlePositions)
            {
                var castle = GetCastle(_player);
                board[position].AddContent(castle);
                castlesList.Add(castle);
            }
            return new object[] { _player, board, castlesList };
        }
    }

    private class OtherCastlesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return GetCase(board: GetFullNByNBoard(1), 
                                 positions: new Vector2Int[]{(0, 0)});
            yield return GetCase(board: GetFullNByNBoard(3), 
                                positions: new Vector2Int[]
                                {
                                    (1, 0), (2, 2), (0, 0)
                                });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private object[] GetCase(Board board, Vector2Int[] positions)
        {
            var otherCastles = new List<CastleGO>();
            foreach (var pos in positions)
            {
                var castle = GetCastle(GetPlayer());
                otherCastles.Add(castle);
                board[pos].AddContent(castle);
            }

            return new object[] { board, otherCastles };
        }
    }
    
    [Theory]
    [ClassData(typeof(PlayerCastlesData))]
    public void KickFromBoard_ShouldFreeAllCastles_OwnedByPlayer(Player player, Board board, List<CastleGO> playerCastles)
    {
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);

        foreach (var castle in playerCastles)
        {
            Assert.True(castle.GetOwner().IsNull);
        }
    }

    [Theory]
    [ClassData(typeof(OtherCastlesData))]
    public void KickFromBoard_ShouldNotFreeCastles_OfOtherPlayers(Board board, List<CastleGO> otherPlayersCastles)
    {
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(GetPlayer());

        foreach (var castle in otherPlayersCastles)
        {
            Assert.False(castle.GetOwner().IsNull);
        }
    }

    [Fact]
    public void KickFromBoard_ShouldRemoveUnits_OwnedByPlayer()
    {
        var player = GetPlayer();
        var board = GetFullNByNBoard(3);
        board[0, 0].AddContent(GetUnit(player));
        board[1, 2].AddContent(GetUnit(player));
        board[2, 2].AddContent(GetUnit(player));
        var kicker = new PlayerKicker(board);
        
        kicker.KickFromBoard(player);
        
        Assert.False(board[0, 0].HasContent());
        Assert.False(board[1, 2].HasContent());
        Assert.False(board[2, 2].HasContent());
    }
}