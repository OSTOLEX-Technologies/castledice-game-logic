using System.Xml.Schema;
using castledice_game_logic;
using castledice_game_logic.GameObjects;

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
    
    
}