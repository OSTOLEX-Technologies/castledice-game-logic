using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class TreesSpawnerTests
{
    
    //TODO: Need help with testing trees spawner
    [Fact]
    public void SpawnContent_ShouldSpawnExactNumberOfTrees_IfOneNumberRangeIsGiven()
    {
        var board = new Board(CellType.Square);
        var factoryMock = new Mock<ITreesFactory>();
        factoryMock.Setup(f => f.GetTree()).Returns(new Tree(1, false));
        var cellsGenerator = new RectCellsGenerator(10, 10);
        cellsGenerator.GenerateCells(board);
        int treesAmount = 3;
        var treesSpawner = new TreesSpawner(treesAmount, treesAmount, 3, factoryMock.Object);
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
        var factoryMock = new Mock<ITreesFactory>();
        factoryMock.Setup(f => f.GetTree()).Returns(new Tree(1, false));
        var treesSpawner = new TreesSpawner(treesAmount, treesAmount, minDistanceBetweenTrees, factoryMock.Object);

        Assert.Throws<InvalidOperationException>(() => treesSpawner.SpawnContent(board));
    }

    [Fact]
    public void SpawnContent_ShouldUseFactory_GivenInConstructor()
    {
        var board = GetFullNByNBoard(1);
        var factoryMock = new Mock<ITreesFactory>();
        var expectedTree = new Tree(1, false);
        factoryMock.Setup(f => f.GetTree()).Returns(expectedTree);
        var treesSpawner = new TreesSpawner(1, 1, 1, factoryMock.Object);
        
        treesSpawner.SpawnContent(board);
        var cellContent = board[0, 0].GetContent();

        Assert.Contains(expectedTree, cellContent);
    }
}