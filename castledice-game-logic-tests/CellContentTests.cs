using castledice_game_logic;

namespace castledice_game_logic_tests;

public class CellContentTests
{
    [Fact]
    public void TestCellContentReturnsGivenContentType()
    {
        var expectedType = ContentType.Tree;
        var content = new CellContent(-1, expectedType);

        var contentType = content.Type;
        
        Assert.Equal(contentType, expectedType);
    }

    [Fact]
    public void TestCellContentReturnsGivenPlayerId()
    {
        var expectedId = 1;
        var content = new CellContent(expectedId, ContentType.Castle);

        var id = content.PlayerId;
        
        Assert.Equal(id, expectedId);
    }
}