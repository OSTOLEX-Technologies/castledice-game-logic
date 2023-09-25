using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using Moq;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class BoardConfigTests
{
    [Fact]
    public void Properties_ShouldReturnObjects_GivenInConstructor()
    {
        var contentSpawnersList = new List<IContentSpawner>();
        var cellsGenerator = new Mock<ICellsGenerator>().Object;
        var cellType = CellType.Square;
        
        var boardConfig = new BoardConfig(contentSpawnersList, cellsGenerator, cellType);
        
        Assert.Same(contentSpawnersList, boardConfig.ContentSpawners);
        Assert.Same(cellsGenerator, boardConfig.CellsGenerator);
        Assert.Equal(cellType, boardConfig.CellType);
    }
}