using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class CoordinateContentSpawnerTests
{
    [Theory]
    [MemberData(nameof(SpawnContentTestCases))]
    public void SpawnContent_ShouldAddGivenContent_ToCellsWithGivenCoordinates(
        List<ContentToCoordinate> contentToCoordinates, Board board)
    {
        var spawner = new CoordinateContentSpawner(contentToCoordinates);
        
        spawner.SpawnContent(board);

        foreach (var contentToCoordinate in contentToCoordinates)
        {
            var cell = board[contentToCoordinate.Coordinate];
            Assert.Contains(contentToCoordinate.Content, cell.GetContent());
        }
    }

    public static IEnumerable<object[]> SpawnContentTestCases()
    {
        yield return new object[]
        {
            new List<ContentToCoordinate>
            {
                new ((0, 0), GetCellContent()),
                new ((1, 1), GetCellContent()),
                new ((2, 2), GetCellContent()),
                new ((3, 3), GetCellContent())
            },
            GetFullNByNBoard(5)
        };
        
        yield return new object[]
        {
            new List<ContentToCoordinate>
            {
                new ((0, 1), GetCellContent()),
                new ((2, 5), GetCellContent()),
                new ((1, 3), GetCellContent())
            },
            GetFullNByNBoard(6)
        };
    }
    
    [Theory]
    [MemberData(nameof(InvalidBoardTestCases))]
    public void SpawnContent_ShouldThrowArgumentException_IfGivenBoardHasNoCellsOnCoordinates(
        List<ContentToCoordinate> contentToCoordinates, Board board)
    {
        var spawner = new CoordinateContentSpawner(contentToCoordinates);
        
        Assert.Throws<ArgumentException>(() => spawner.SpawnContent(board));
    }

    public static IEnumerable<object[]> InvalidBoardTestCases()
    {
        yield return new object[]
        {
            new List<ContentToCoordinate>
            {
                new ((10, 10), GetCellContent()),
            },
            GetFullNByNBoard(3)
        };
        
        var boardWithAbsentCells = new Board(CellType.Square);
        boardWithAbsentCells.AddCell(0, 0);
        boardWithAbsentCells.AddCell(1, 2);
        yield return new object[]
        {
            new List<ContentToCoordinate>
            {
                new((0, 0), GetCellContent()),
                new((2, 1), GetCellContent())
            }
        };
    }

    [Theory]
    [MemberData(nameof(InvalidCoordinatesTestCases))]
    public void Constructor_ShouldThrowArgumentException_IfGivenCoordinatesAreInvalid(List<ContentToCoordinate> invalidContentToCoordinates)
    {
        Assert.Throws<ArgumentException>(() => new CoordinateContentSpawner(invalidContentToCoordinates));
    }
    
    public static IEnumerable<object[]> InvalidCoordinatesTestCases()
    {
        yield return new object[]
        {
            new List<ContentToCoordinate>
            {
                new ((-1, 0), GetCellContent()),
                new ((0, -2), GetCellContent()),
                new ((-3, 0), GetCellContent()),
                new ((0, 0), GetCellContent())
            }
        };
        
        yield return new object[]
        {
            new List<ContentToCoordinate>
            {
                new ((0, 0), GetCellContent()),
                new ((1, 1), GetCellContent()),
                new ((2, -4), GetCellContent()),
                new ((0, 0), GetCellContent())
            }
        };
    }
}