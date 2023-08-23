using castledice_game_logic;
using castledice_game_logic.GameObjects;

using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;

public class CellTests
{
    [Fact]
    public void GetContent_ShouldReturnEmptyList_IfNoContentAdded()
    {
        var cell = new Cell();

        var cellContent = cell.GetContent();
        
        Assert.Empty(cellContent);
    }

    [Fact]
    public void GetContent_ShouldReturnList_WithAddedContent()
    {
        var cell = new Cell();
        var contentToAdd = new GameObject();

        cell.AddContent(contentToAdd);
        var cellContentList = cell.GetContent();

        Assert.Contains(contentToAdd, cellContentList);
    }

    [Fact]
    public void GetContent_ShouldReturnList_WithoutRemovedContent()
    {
        var cell = new Cell();
        var content = new GameObject();
        cell.AddContent(content);

        cell.RemoveContent(content);
        var contentList = cell.GetContent();
        
        Assert.DoesNotContain(content, contentList);
    }

    [Fact]
    public void ReturnContent_ShouldReturnFalse_IfNoContentRemoved()
    {
        var cell = new Cell();
        var content = new GameObject();
        
        Assert.False(cell.RemoveContent(content));
    }

    [Fact]
    public void RemoveContent_ShouldReturnTrue_IfContentWasRemoved()
    {
        var cell = new Cell();
        var content = new GameObject();
        cell.AddContent(content);
        
        Assert.True(cell.RemoveContent(content));
    }

    [Fact]
    public void HasContent_ShouldReturnTrue_IfSomeContentOnCellMeetsGivenCondition()
    {
        var cell = new Cell();
        var castle = new CastleGO(new Player());
        cell.AddContent(castle);
        Func<GameObject, bool> predicate = o => o is CastleGO;
        
        
        Assert.True(cell.HasContent(predicate));
    }

    [Fact]
    public void HasContent_ShouldReturnFalse_IfNoContentOnCellMeetGivenCondition()
    {
        var cell = new Cell();
        Func<GameObject, bool> predicate = o => o is CastleGO;
        
        Assert.False(cell.HasContent(predicate));
    }

    [Fact]
    public void HasContentWithoutArguments_ShouldReturnFalse_IfNoContentOnCell()
    {
        var cell = new Cell();
        
        Assert.False(cell.HasContent());
    }

    [Fact]
    public void HasContentWithoutArguments_ShouldReturnTrue_IfSomeContentIsOnCell()
    {
        var cell = new Cell();
        cell.AddContent(new Tree());
        
        Assert.True(cell.HasContent());
    }
}