using castledice_game_logic;

namespace castledice_game_logic_tests;

public class CellTests
{
    [Fact]
    public void TestCellContentIsNullAfterRemove()
    {
        var content = new CellContent(1, ContentType.Castle);
        var cell = new Cell();
        cell.Content = content;

        cell.RemoveContent();
        
        Assert.True(cell.Content == null);
    }
}