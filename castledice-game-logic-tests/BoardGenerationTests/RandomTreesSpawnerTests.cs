using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class RandomTreesSpawnerTests
{
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(3, 2, 3)]
    [InlineData(2, 1, 3)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int maxTreesCount, int minTreesCount,
        int minDistanceBetweenTrees)
    {
        var expectedTreesFactory = new Mock<ITreesFactory>();
        var treesSpawner = new RandomTreesSpawner(minTreesCount, maxTreesCount, minDistanceBetweenTrees,
            expectedTreesFactory.Object);
        
        Assert.Equal(maxTreesCount, treesSpawner.MaxTreesCount);
        Assert.Equal(minTreesCount, treesSpawner.MinTreesCount);
        Assert.Equal(minDistanceBetweenTrees, treesSpawner.MinDistanceBetweenTrees);
        Assert.Same(expectedTreesFactory.Object, treesSpawner.Factory);
    }
    
    [Fact]
    public void SpawnContent_ShouldSpawnExactNumberOfTrees_IfOneNumberRangeIsGiven()
    {
        var board = GetFullNByNBoard(10);
        int treesAmount = 3;
        var treesSpawner = new RandomTreesSpawner(treesAmount, treesAmount, 3, GetTreesFactory());
        treesSpawner.SpawnContent(board);

        int actualTreesAmount = 0;
        foreach (var cell in board)
        {
            if (cell.GetContent().Exists(c => c is Tree))
            {
                actualTreesAmount++;
            }
        }
        
        Assert.Equal(treesAmount, actualTreesAmount);
    }

    [Fact]
    public void SpawnContent_ThrowsInvalidOperationException_IfImpossibleToSpawnTreesWithGivenConfiguration()
    {
        var board = GetFullNByNBoard(5);
        int treesAmount = 5;
        int minDistanceBetweenTrees = 3;
        var treesSpawner = new RandomTreesSpawner(treesAmount, treesAmount, minDistanceBetweenTrees, GetTreesFactory());

        Assert.Throws<InvalidOperationException>(() => treesSpawner.SpawnContent(board));
    }

    [Fact]
    public void SpawnContent_ShouldNotSpawnTrees_IfZeroAmountIsGiven()
    {
        var board = GetFullNByNBoard(10);
        int treesAmount = 0;
        var treesSpawner = new RandomTreesSpawner(treesAmount, treesAmount, 3, GetTreesFactory());
        treesSpawner.SpawnContent(board);
        
        foreach (var cell in board)
        {
            if (cell.GetContent().Exists(c => c is Tree))
            {
                Assert.Fail("Tree was spawned");
            }
        }
    }

    [Fact]
    public void SpawnContent_ShouldUseFactory_GivenInConstructor()
    {
        var board = GetFullNByNBoard(1);
        var factoryMock = new Mock<ITreesFactory>();
        var expectedTree = new Tree(1, false);
        factoryMock.Setup(f => f.GetTree()).Returns(expectedTree);
        var treesSpawner = new RandomTreesSpawner(1, 1, 1, factoryMock.Object);
        
        treesSpawner.SpawnContent(board);
        var cellContent = board[0, 0].GetContent();

        Assert.Contains(expectedTree, cellContent);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_IfMaxIsLessThenMin()
    {
        Assert.Throws<ArgumentException>(() => new RandomTreesSpawner(1, 0, 1, GetTreesFactory()));
    }
}