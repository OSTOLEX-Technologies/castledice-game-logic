using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.Math;

using static castledice_game_logic_tests.ObjectCreationUtility;
using CastleGO = castledice_game_logic.GameObjects.Castle;
namespace castledice_game_logic_tests;

public class CastleSpawnerTests
{
    [Fact]
    public void SpawnContent_ShouldSpawnCastles_OnGivenPositions()
    {
        var firstPlayer = GetPlayer();
        var secondPlayer = GetPlayer();
        var firstPlayerCastlePosition = new Vector2Int(0, 0);
        var secondPlayerCastlePosition = new Vector2Int(9, 9);
        var castlesSpawnData = new Dictionary<Player, Vector2Int>()
        {
            { firstPlayer,  firstPlayerCastlePosition},
            { secondPlayer,  secondPlayerCastlePosition}
        };
        var board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(10, 10);
        cellsGenerator.GenerateCells(board);
        var castlesSpawner = new CastlesSpawner(castlesSpawnData);
        
        castlesSpawner.SpawnContent(board);
        
        Assert.Contains(board[firstPlayerCastlePosition].GetContent(), c => c is CastleGO);
        Assert.Contains(board[secondPlayerCastlePosition].GetContent(), c => c is CastleGO);
    }

    [Fact]
    public void SpawnContent_ShouldSpawnCastles_WithAppropriatePlayersAssigned()
    {
        var firstPlayer = GetPlayer();
        var secondPlayer = GetPlayer();
        var firstPlayerCastlePosition = new Vector2Int(0, 0);
        var secondPlayerCastlePosition = new Vector2Int(9, 9);
        var castlesSpawnData = new Dictionary<Player, Vector2Int>()
        {
            { firstPlayer,  firstPlayerCastlePosition},
            { secondPlayer,  secondPlayerCastlePosition}
        };
        var board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(10, 10);
        cellsGenerator.GenerateCells(board);
        var castlesSpawner = new CastlesSpawner(castlesSpawnData);
        
        castlesSpawner.SpawnContent(board);

        var firstPlayerCastle =
            board[firstPlayerCastlePosition].GetContent().FirstOrDefault(c => c is CastleGO) as CastleGO;
        var secondPlayerCastle =
            board[secondPlayerCastlePosition].GetContent().FirstOrDefault(c => c is CastleGO) as CastleGO;
        Assert.Same(firstPlayer, firstPlayerCastle.GetOwner());
        Assert.Same(secondPlayer, secondPlayerCastle.GetOwner());
    }
}