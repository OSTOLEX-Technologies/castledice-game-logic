using castledice_game_logic;
using castledice_game_logic.GameObjects;

using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;

public class CellTests
{
    [Fact]
    public void TestGetContentReturnsEmptyListIfNoContentAdded()
    {
        var cell = new Cell();

        var cellContent = cell.GetContent();
        
        Assert.Empty(cellContent);
    }

    [Fact]
    public void TestGetContentReturnsListWithAddedContent()
    {
        var cell = new Cell();
        var contentToAdd = new GameObject();

        cell.AddContent(contentToAdd);
        var cellContentList = cell.GetContent();

        Assert.Contains(contentToAdd, cellContentList);
    }

    [Fact]
    public void TestGetContentReturnsListWithoutRemovedContent()
    {
        var cell = new Cell();
        var content = new GameObject();
        cell.AddContent(content);

        cell.RemoveContent(content);
        var contentList = cell.GetContent();
        
        Assert.DoesNotContain(content, contentList);
    }

    [Fact]
    public void TestRemoveContentReturnsFalseIfContentWasAbsent()
    {
        var cell = new Cell();
        var content = new GameObject();
        
        Assert.False(cell.RemoveContent(content));
    }

    [Fact]
    public void TestRemoveContentReturnsTrueIfContentWasRemoved()
    {
        var cell = new Cell();
        var content = new GameObject();
        cell.AddContent(content);
        
        Assert.True(cell.RemoveContent(content));
    }

    [Fact]
    public void TestHasContentReturnsTrueIfFoundAppropriateContent()
    {
        var cell = new Cell();
        var castle = new CastleGO(new Player());
        cell.AddContent(castle);
        Func<GameObject, bool> predicate = o => o is CastleGO;
        
        
        Assert.True(cell.HasContent(predicate));
    }

    [Fact]
    public void TestHasContentReturnsFalseIfNoAppropriateContentFound()
    {
        var cell = new Cell();
        Func<GameObject, bool> predicate = o => o is CastleGO;
        
        Assert.False(cell.HasContent(predicate));
    }

    [Fact]
    public void TestHasContentWithoutArgumentsReturnsFalseIfNoContentOnCell()
    {
        var cell = new Cell();
        
        Assert.False(cell.HasContent());
    }

    [Fact]
    public void TestHasContentWithoutArgumentsReturnsTrueIfCellHasContent()
    {
        var cell = new Cell();
        cell.AddContent(new Tree());
        
        Assert.True(cell.HasContent());
    }
}