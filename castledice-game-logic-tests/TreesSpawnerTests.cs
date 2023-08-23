using castledice_game_logic.Board;
using castledice_game_logic.Board.CellsGeneration;
using castledice_game_logic.Board.ContentGeneration;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests;

public class TreesSpawnerTests
{
    
    //TODO: Need help with testing trees spawner
    [Fact]
    public void SpawnContent_ShouldSpawnExactNumberOfTrees_IfOneNumberRangeIsGiven()
    {
        var board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(10, 10);
        cellsGenerator.GenerateCells(board);
        int treesAmount = 3;
        var treesSpawner = new TreesSpawner(treesAmount, treesAmount, 3);
        treesSpawner.SpawnContent(board);

        int actualTreesAmount = 0;
        foreach (var cell in board)
        {
            if (cell.GetContent().Any(c => c is Tree))
            {
                actualTreesAmount++;
            }
        }
        
        Assert.Equal(treesAmount, actualTreesAmount);
    }

    [Fact]
    public void SpawnContent_ThrowsInvalidOperationException_IfImpossibleToSpawnTreesWithGivenConfiguration()
    {
        var board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(5, 5);
        cellsGenerator.GenerateCells(board);
        int treesAmount = 5;
        int minDistanceBetweenTrees = 3;
        var treesSpawner = new TreesSpawner(treesAmount, treesAmount, minDistanceBetweenTrees);

        Assert.Throws<InvalidOperationException>(() => treesSpawner.SpawnContent(board));
    }
}