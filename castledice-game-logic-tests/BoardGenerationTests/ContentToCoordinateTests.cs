using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.Math;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class ContentToCoordinateTests
{
    [Fact]
    public void ContentProperty_ShouldReturnContent_GivenInConstructor()
    {
        var content = GetCellContent();
        
        var contentToCoordinate = new ContentToCoordinate((0, 0), content);
        
        Assert.Same(content, contentToCoordinate.Content);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 4)]
    public void CoordinateProperty_ShouldReturnVector2Int_GivenInConstructor(int x, int y)
    {
        var coordinate = new Vector2Int(x, y);
        
        var contentToCoordinate = new ContentToCoordinate(coordinate, GetCellContent());
        
        Assert.Equal(coordinate, contentToCoordinate.Coordinate);
    }
}