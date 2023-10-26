using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace castledice_game_logic_tests;

public class CoordinateContentSpawnerTests
{
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
}