using castledice_game_logic.Math;

namespace castledice_game_logic.Board.ContentGeneration;

public class CellsPicker
{
    private Board _board;
    private bool[,] _availablilityMatrix;
    private Vector2Int _lastPickedCellPosition;
    private IRandomNumberGenerator _randomNumberGenerator;
    private bool _cellPicked;

    public CellsPicker(Board board)
    {
        _board = board;
        _availablilityMatrix = new bool[_board.GetLength(0), _board.GetLength(1)];
        _randomNumberGenerator = new RandomNumberGenerator();
        IncludeExistingCells();
    }

    public CellsPicker(Board board, IRandomNumberGenerator generator) : this(board)
    {
        _randomNumberGenerator = generator;
    }

    private void IncludeExistingCells()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_board.HasCell(i, j))
                {
                    _availablilityMatrix[i, j] = true;
                }
            }
        }
    }

    public void ExcludeRows(params int[] indices)
    {
        foreach (var index in indices)
        {
            if (index < 0)
            {
                continue;
            }
            ExcludeRow(index);
        }
    }

    private void ExcludeRow(int index)
    {
        if (index > _availablilityMatrix.GetLength(0))
        {
            return;
        }
        for (int i = 0; i < _availablilityMatrix.GetLength(1); i++)
        {
            _availablilityMatrix[index, i] = false;
        }
    }

    public void ExcludeColumns(params int[] indices)
    {
        foreach (var index in indices)
        {
            if (index < 0)
            {
                continue;
            }
            ExcludeColumn(index);
        }
    }

    private void ExcludeColumn(int index)
    {
        if (index > _availablilityMatrix.GetLength(1))
        {
            return;
        }
        for (int i = 0; i < _availablilityMatrix.GetLength(0); i++)
        {
            _availablilityMatrix[i, index] = false;
        }
    }

    public bool[,] GetAvailabilityMatrix()
    {
        return _availablilityMatrix;
    }

    public Cell PickRandomCell()
    {
        int availableCellsCount = CountAvailableCells();
        if (availableCellsCount < 1)
        {
            throw new InvalidOperationException("No available cells left!");
        }
        int cellNumber = _randomNumberGenerator.Range(1, availableCellsCount + 1);
        var pickedCellPosition = GetCellPositionByNumber(cellNumber);
        _lastPickedCellPosition = pickedCellPosition;
        _cellPicked = true;
        return _board[pickedCellPosition];
    }

    public int AvailableCellsCount()
    {
        return CountAvailableCells();
    }
    
    private int CountAvailableCells()
    {
        int count = 0;
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_availablilityMatrix[i, j])
                {
                    count++;
                }
            }
        }
        return count;
    }

    private Vector2Int GetCellPositionByNumber(int cellNumber)
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_availablilityMatrix[i, j])
                {
                    cellNumber--;
                    if (cellNumber == 0)
                    {
                        return new Vector2Int(i, j);
                    }
                }
            }
        }
        throw new IndexOutOfRangeException("Cell number is bigger than amount of available cells!");
    }

    public void ExcludePicked()
    {
        if (!_cellPicked)
        {
            throw new InvalidOperationException("Cell haven't been picked yet!");
        }

        _availablilityMatrix[_lastPickedCellPosition.X, _lastPickedCellPosition.Y] = false;
    }

    public void ExcludeAroundPicked(int radius)
    {
        if (!_cellPicked)
        {
            throw new InvalidOperationException("Cell haven't been picked yet!");
        }
        if (radius < 0)
        {
            throw new ArgumentException("Radius cannot be less than zero!");
        }
        ExcludeCellsAround(_lastPickedCellPosition, radius);
    }

    public void ExcludeCells(Func<Cell, bool> predicate)
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (!_board.HasCell(i, j))
                {
                    continue;
                }
                if (predicate(_board[i, j]))
                {
                    _availablilityMatrix[i, j] = false;
                }
            }
        }
    }

    public void ExcludeCellsAround(Func<Cell, bool> predicate, int radius)
    {
        if (radius < 0)
        {
            throw new ArgumentException("Radius cannot be less than zero!");
        }

        var cellsPositions = _board.GetCellsPositions(predicate);
        foreach (var cellPosition in cellsPositions)
        {
            ExcludeCellsAround(cellPosition, radius);
        }
    }

    private void ExcludeCellsAround(Vector2Int cellPosition, int radius)
    {
        for (int i = cellPosition.X - radius; i <= cellPosition.X + radius; i++)
        {
            if (i < 0 || i > _board.GetLength(0))
            {
                continue;
            }
            for (int j = cellPosition.Y - radius; j <= cellPosition.Y + radius; j++)
            {
                if (j < 0 || j > _board.GetLength(1))
                {
                    continue;
                }

                _availablilityMatrix[i, j] = false;
            }
        }
        _availablilityMatrix[cellPosition.X, cellPosition.Y] = true;
    }
}