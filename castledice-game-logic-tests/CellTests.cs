using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class CellTests
{
    [Fact]
    public void PositionProperty_ShouldHaveCoordinates_GivenInConstructor()
    {
        var cell = new Cell(1, 3);
        Vector2Int expectedPosition = (1, 3);

        var actualPosition = cell.Position;
        
        Assert.Equal(expectedPosition, actualPosition);
    }
    
    [Fact]
    public void PositionProperty_ShouldReturnPosition_GivenInConstructor()
    {
        var position = new Vector2Int(0, 1);
        var cell = new Cell(position);
        
        Assert.Equal(position, cell.Position);
    }
    
    [Fact]
    public void GetContent_ShouldReturnEmptyList_IfNoContentAdded()
    {
        var cell = GetCell();

        var cellContent = cell.GetContent();
        
        Assert.Empty(cellContent);
    }

    [Fact]
    public void GetContent_ShouldReturnList_WithAddedContent()
    {
        var cell = GetCell();
        var contentToAdd = GetCellContent();

        cell.AddContent(contentToAdd);
        var cellContentList = cell.GetContent();

        Assert.Contains(contentToAdd, cellContentList);
    }

    [Fact]
    public void GetContent_ShouldReturnList_WithoutRemovedContent()
    {
        var cell = GetCell();
        var content = GetCellContent();
        cell.AddContent(content);

        cell.RemoveContent(content);
        var contentList = cell.GetContent();
        
        Assert.DoesNotContain(content, contentList);
    }

    [Fact]
    public void ReturnContent_ShouldReturnFalse_IfNoContentRemoved()
    {
        var cell = GetCell();
        var content = GetCellContent();
        
        Assert.False(cell.RemoveContent(content));
    }

    [Fact]
    public void RemoveContent_ShouldReturnTrue_IfContentWasRemoved()
    {
        var cell = GetCell();
        var content = GetCellContent();
        cell.AddContent(content);
        
        Assert.True(cell.RemoveContent(content));
    }

    [Fact]
    public void RemoveContent_ShouldInvokeContentRemoved_IfContentWasRemoved()
    {
        var cell = GetCell();
        var content = GetCellContent();
        cell.AddContent(content);
        var contentRemovedInvoked = false;
        cell.ContentRemoved += (sender, args) => contentRemovedInvoked = true;
        
        cell.RemoveContent(content);
        
        Assert.True(contentRemovedInvoked);
    }

    [Fact]
    public void RemoveContent_ShouldNotInvokeContentRemoved_IfContentWasNotRemoved()
    {
        var cell = GetCell();
        var content = GetCellContent();
        var contentRemovedInvoked = false;
        cell.ContentRemoved += (sender, args) => contentRemovedInvoked = true;
        
        cell.RemoveContent(content);
        
        Assert.False(contentRemovedInvoked);
    }

    [Fact]
    public void ContentRemovedEvent_ShouldHaveRemovedContent_AsAnArgument()
    {
        var cell = GetCell();
        var content = GetCellContent();
        cell.AddContent(content);
        Content? removedContent = null;
        cell.ContentRemoved += (sender, args) => removedContent = args;
        
        cell.RemoveContent(content);
        
        Assert.Same(content, removedContent);
    }

    [Fact]
    public void AddContent_ShouldInvokeContentAddedEvent_WithAddedContentAsAnArgument()
    {
        var cell = GetCell();
        var content = GetCellContent();
        Content? addedContent = null;
        cell.ContentAdded += (sender, args) => addedContent = args;
        
        cell.AddContent(content);
        
        Assert.Same(content, addedContent);
    }

    [Fact]
    public void HasContent_ShouldReturnTrue_IfSomeContentOnCellMeetsGivenCondition()
    {
        var cell = GetCell();
        var castle = new ReplaceableMock();
        cell.AddContent(castle);
        Func<Content, bool> predicate = o => o is IReplaceable;
        
        
        Assert.True(cell.HasContent(predicate));
    }

    [Fact]
    public void HasContent_ShouldReturnFalse_IfNoContentOnCellMeetGivenCondition()
    {
        var cell = GetCell();
        Func<Content, bool> predicate = o => o is CastleGO;
        
        Assert.False(cell.HasContent(predicate));
    }

    [Fact]
    public void HasContentWithoutArguments_ShouldReturnFalse_IfNoContentOnCell()
    {
        var cell = GetCell();
        
        Assert.False(cell.HasContent());
    }

    [Fact]
    public void HasContentWithoutArguments_ShouldReturnTrue_IfSomeContentIsOnCell()
    {
        var cell = GetCell();
        cell.AddContent(GetCellContent());
        
        Assert.True(cell.HasContent());
    }


}