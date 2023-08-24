﻿using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;

public class CellTests
{
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
        var contentToAdd = new Content();

        cell.AddContent(contentToAdd);
        var cellContentList = cell.GetContent();

        Assert.Contains(contentToAdd, cellContentList);
    }

    [Fact]
    public void GetContent_ShouldReturnList_WithoutRemovedContent()
    {
        var cell = GetCell();
        var content = new Content();
        cell.AddContent(content);

        cell.RemoveContent(content);
        var contentList = cell.GetContent();
        
        Assert.DoesNotContain(content, contentList);
    }

    [Fact]
    public void ReturnContent_ShouldReturnFalse_IfNoContentRemoved()
    {
        var cell = GetCell();
        var content = new Content();
        
        Assert.False(cell.RemoveContent(content));
    }

    [Fact]
    public void RemoveContent_ShouldReturnTrue_IfContentWasRemoved()
    {
        var cell = GetCell();
        var content = new Content();
        cell.AddContent(content);
        
        Assert.True(cell.RemoveContent(content));
    }

    [Fact]
    public void HasContent_ShouldReturnTrue_IfSomeContentOnCellMeetsGivenCondition()
    {
        var cell = GetCell();
        var castle = new CastleGO(new Player());
        cell.AddContent(castle);
        Func<Content, bool> predicate = o => o is CastleGO;
        
        
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
        cell.AddContent(new Tree());
        
        Assert.True(cell.HasContent());
    }

    private Cell GetCell()
    {
        return new Cell(new Vector2Int(0, 0));
    }
}