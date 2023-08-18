using castledice_game_logic;
using castledice_game_logic.Board;
using castledice_game_logic.Board.ContentGeneration;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests;

public class BoardGenerationTests
{
    [Fact]
    public void TestCastleSpawnerGivesAppropriatePlayerIdsToCastles()
    {
        var castlesPositions = new Dictionary<int, Vector2Int>()
        {
            { 0, new Vector2Int(0, 0) },
            { 1, new Vector2Int(1, 1) }
        };
        var board = new RectBoard(10, 10);
        var castlesSpawner = new CastlesSpawner(castlesPositions);

        castlesSpawner.SpawnContent(board);
        
        foreach (var castleCoordinate in castlesPositions)
        {
            var cell = board.GetCell(castleCoordinate.Value.X, castleCoordinate.Value.Y);
            var cellContent = cell.Content;
            Assert.Equal(castleCoordinate.Key, cellContent.PlayerId);
        }
    }

    [Fact]
    public void TestCastlesSpawnerSpawnsCastlesOnAppropriatePositions()
    {
        var castlesPositions = new Dictionary<int, Vector2Int>()
        {
            { 0, new Vector2Int(0, 0) },
            { 1, new Vector2Int(1, 1) }
        };
        var board = new RectBoard(10, 10);
        var castlesSpawner = new CastlesSpawner(castlesPositions);

        castlesSpawner.SpawnContent(board);

        foreach (var castleCoordinate in castlesPositions.Values)
        {
            var cell = board.GetCell(castleCoordinate.X, castleCoordinate.Y);
            var cellContent = cell.Content;
            Assert.Equal(ContentType.Castle, cellContent.Type);
        }
    }

    [Theory]
    [Repeat(10)]
    public void TestTreesSpawnerSpawnsAmountOfTreesInGivenRange()
    {
        int minTreesCount = 0;
        int maxTreesCount = 3;
        var board = new RectBoard(10, 10);
        var treesSpawner = new TreesSpawner(minTreesCount, maxTreesCount);

        treesSpawner.SpawnContent(board);

        int spawnedCount = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (board.GetCell(i, j).Content.Type == ContentType.Tree)
                {
                    spawnedCount++;
                }
            }
        }
        
        Assert.InRange(spawnedCount, minTreesCount, maxTreesCount);
    }
}