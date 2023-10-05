using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class CoordinateTreesSpawnerTests
{
    [Theory]
    [MemberData(nameof(BoardWithNoCellsOnCoordinatesTestCases))]
    public void SpawnContent_ShouldThrowArgumentException_IfGivenBoardHasNoCellsOnCoordinates(Board board, List<Vector2Int> coordinates)
    {
        var generator = new CoordinateTreesSpawner(coordinates, GetTreesFactory());

        Assert.Throws<ArgumentException>(() => generator.SpawnContent(board));
    }

    public static IEnumerable<object[]> BoardWithNoCellsOnCoordinatesTestCases()
    {
        yield return new object[]
        {
            GetFullNByNBoard(3),
            new List<Vector2Int>()
            {
                (0, 0),
                (1, 7),
                (2, 2),
                (5, 5)
            }
        };

        var boardWithAbsentCells = new Board(CellType.Square);
        boardWithAbsentCells.AddCell(0, 0);
        boardWithAbsentCells.AddCell(1, 2);
        boardWithAbsentCells.AddCell(3, 3);
        yield return new object[]
        {
            boardWithAbsentCells,
            new List<Vector2Int>()
            {
                (1, 2),
                (2, 2),
                (4, 4)
            }
        };

        yield return new object[]
        {
            new Board(CellType.Square),
            new List<Vector2Int>()
            {
                (0, 0)
            }
        };
    }

    
    [Theory]
    [MemberData(nameof(CoordinatesListWithDuplicatesTestCases))]
    public void Constructor_ShouldThrowArgumentException_IfCoordinatesListWithDuplicatesGiven(List<Vector2Int> coordinatesListWithDuplicates)
    {
        Assert.Throws<ArgumentException>(() => new CoordinateTreesSpawner(coordinatesListWithDuplicates, GetTreesFactory()));
    }

    public static IEnumerable<object[]> CoordinatesListWithDuplicatesTestCases()
    {
        yield return new object[]
        {
            new List<Vector2Int>()
            {
                (0, 0),
                (0, 0)
            }
        };
        yield return new object[]
        {
            new List<Vector2Int>()
            {
                (0, 0),
                (1, 2),
                (2, 1),
                (1, 2)
            }
        };
    }

    [Theory]
    [MemberData(nameof(ListWithNegativeCoordinatesTestCases))]
    public void Constructor_ShouldThrowArgumentException_IfListWithNegativeCoordinatesGiven(List<Vector2Int> listWithNegativeCoordinates)
    {
        Assert.Throws<ArgumentException>(() => new CoordinateTreesSpawner(listWithNegativeCoordinates, GetTreesFactory()));
    }

    public static IEnumerable<object[]> ListWithNegativeCoordinatesTestCases()
    {
        yield return new object[]
        {
            new List<Vector2Int>()
            {
                (-1, 0),
                (0, 0)
            }
        };
        yield return new object[]
        {
            new List<Vector2Int>()
            {
                (0, 0),
                (1, -2),
                (2, 1),
            }
        };
        yield return new object[]
        {
            new List<Vector2Int>()
            {
                (-2, -3)
            }
        };
        yield return new object[]
        {
            new List<Vector2Int>()
            {
                (0, 0),
                (1, 1),
                (3, 3),
                (-1, -1)
            }
        };
    }
    
    [Fact]
    public void SpawnContent_ShouldUseFactory_GivenInConstructor()
    {
        var board = GetFullNByNBoard(1);
        var spawnPosition = new Vector2Int(0, 0);
        var factoryMock = new Mock<ITreesFactory>();
        var expectedTree = new Tree(1, false);
        factoryMock.Setup(f => f.GetTree()).Returns(expectedTree);
        var treesSpawner = new CoordinateTreesSpawner(new List<Vector2Int>{spawnPosition}, factoryMock.Object);
        
        treesSpawner.SpawnContent(board);
        var cellContent = board[spawnPosition].GetContent();

        Assert.Contains(expectedTree, cellContent);
    }

    [Theory]
    [MemberData(nameof(SpawnOnGivenCoordinatesTestCases))]
    public void SpawnContent_ShouldSpawnTrees_OnGivenCoordinates(Board board, List<Vector2Int> coordiantes)
    {
        var treesSpawner = new CoordinateTreesSpawner(coordiantes, GetTreesFactory());
        
        treesSpawner.SpawnContent(board);
        foreach (var cell in board)
        {
            if (coordiantes.Contains(cell.Position))
            {
                Assert.True(cell.HasContent(c => c is Tree));
            }
            else
            {
                Assert.False(cell.HasContent(c => c is Tree));
            }
        }
    }

    public static IEnumerable<object[]> SpawnOnGivenCoordinatesTestCases()
    {
        yield return new object[]
        {
            GetFullNByNBoard(5),
            new List<Vector2Int>()
            {
                (0, 0),
                (3, 3),
                (4, 2)
            }
        };
        yield return new object[]
        {
            GetFullNByNBoard(10),
            new List<Vector2Int>()
            {
                (4, 5),
                (6, 7),
                (3, 9)
            }
        };
    }
}