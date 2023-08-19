﻿using System.Collections;
using castledice_game_logic.Exceptions;
using castledice_game_logic.Math;

namespace castledice_game_logic.Board;

public class Board : IEnumerable<Cell>
{
    private CellType _cellType;
    private Cell[,] _cells;

    public Cell this[int i, int j]
    {
        get
        {
            if (_cells == null  || i >= _cells.GetLength(0) || 
                j >= _cells.GetLength(1))
            {
                throw new CellNotFoundException();
            }
            
            var cell = _cells[i, j];

            if (cell == null)
            {
                throw new CellNotFoundException();
            }

            return cell;
        }
    }

    public int GetMaxLength()
    {
        return GetCellArrayDimensionLength(0);
    }

    public int GetMaxWidth()
    {
        return GetCellArrayDimensionLength(1);
    }

    private int GetCellArrayDimensionLength(int dimensionIndex)
    {
        if (_cells == null)
        {
            return 0;
        }

        return _cells.GetLength(dimensionIndex);
    }
    
    public Board(CellType cellType)
    {
        _cellType = cellType;
    }

    public CellType GetCellType()
    {
        return _cellType;
    }

    public void AddCell(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            throw new IndexOutOfRangeException();
        }

        if (_cells == null)
        {
            _cells = new Cell[x + 1, y + 1];
        }

        if (_cells.GetLength(0) <= x || _cells.GetLength(1) <= y)
        {
            int newLength = x + 1 > _cells.GetLength(0) ? x + 1 : _cells.GetLength(0);
            int newWidth = y + 1 > _cells.GetLength(1) ? y + 1 : _cells.GetLength(1);
            ExpandCellsArray(newLength, newWidth);
        }

        if (_cells[x, y] != null)
        {
            return;
        }
        _cells[x, y] = new Cell();
    }

    private void ExpandCellsArray(int newLength, int newWidth)
    {
        var oldCells = _cells;
        _cells = new Cell[newLength, newWidth];
        for (int i = 0; i < oldCells.GetLength(0); i++)
        {
            for (int j = 0; j < oldCells.GetLength(1); j++)
            {
                _cells[i, j] = oldCells[i, j];
            }
        }
    }

    public bool HasCell(int x, int y)
    {
        if (_cells == null || x >= _cells.GetLength(0) || y >= _cells.GetLength(1))
        {
            return false;
        }

        var cell = _cells[x, y];
        return cell != null;
    }

    public IEnumerator<Cell> GetEnumerator()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                if (_cells[i, j] != null)
                {
                    yield return _cells[i, j];
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}