using castledice_game_logic.Extensions;
using castledice_game_logic.Math;

namespace castledice_game_logic.BoardGeneration.ContentGeneration;

/// <summary>
/// Helper class that provides tools for excluding cells from possible selection with different conditions.
/// </summary>
// This class can be divided into two other classes: CellsExclusionUtility and CellsSelectionUtility
public class CellsPickingUtility
{
    private struct CellPick
    {
        public Vector2Int CellPosition;
        public int IntersectionsCount;
    }
    
    private readonly Board _board;
    private readonly bool[,] _availabilityMatrix;
    private Vector2Int _lastPickedCellPosition;
    private readonly IRangeRandomNumberGenerator _rangeRandomNumberGenerator;
    private bool _cellPicked;

    public CellsPickingUtility(Board board)
    {
        _board = board;
        _availabilityMatrix = new bool[_board.GetLength(0), _board.GetLength(1)];
        _rangeRandomNumberGenerator = new RangeRandomNumberGenerator();
        IncludeExistingCells();
    }

    public CellsPickingUtility(Board board, IRangeRandomNumberGenerator generator) : this(board)
    {
        _rangeRandomNumberGenerator = generator;
    }

    private void IncludeExistingCells()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_board.HasCell(i, j))
                {
                    _availabilityMatrix[i, j] = true;
                }
            }
        }
    }

    public void IncludeCell(Vector2Int cellPosition)
    {
        if (cellPosition.X < 0 || cellPosition.Y < 0)
        {
            throw new ArgumentException("Cell position cannot be negative!");
        }
        if (cellPosition.X >= _board.GetLength(0) || cellPosition.Y > _board.GetLength(1))
        {
            throw new ArgumentException("Cell position is outside of the board boundaries!");
        }
        if (!_board.HasCell(cellPosition))
        {
            throw new InvalidOperationException("Board doesnt have cell on this position: " + cellPosition);
        }
        _availabilityMatrix[cellPosition.X, cellPosition.Y] = true;
    }

    public void ExcludeCell(Vector2Int cellPosition)
    {
        if (cellPosition.X < 0 || cellPosition.Y < 0)
        {
            throw new ArgumentException("Cell position cannot be negative!");
        }
        if (cellPosition.X >= _board.GetLength(0) || cellPosition.Y > _board.GetLength(1))
        {
            throw new ArgumentException("Cell position is outside of the board boundaries!");
        }
        _availabilityMatrix[cellPosition.X, cellPosition.Y] = false;
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
        if (index > _availabilityMatrix.GetLength(0))
        {
            return;
        }
        for (int i = 0; i < _availabilityMatrix.GetLength(1); i++)
        {
            _availabilityMatrix[index, i] = false;
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
        if (index > _availabilityMatrix.GetLength(1))
        {
            return;
        }
        for (int i = 0; i < _availabilityMatrix.GetLength(0); i++)
        {
            _availabilityMatrix[i, index] = false;
        }
    }

    public bool[,] GetAvailabilityMatrix()
    {
        return _availabilityMatrix;
    }

    public Cell PickRandom()
    {
        int availableCellsCount = CountAvailableCells();
        if (availableCellsCount < 1)
        {
            throw new InvalidOperationException("No available cells left!");
        }
        int cellNumber = _rangeRandomNumberGenerator.GetRandom(1, availableCellsCount + 1);
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
                if (_availabilityMatrix[i, j])
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
                if (_availabilityMatrix[i, j])
                {
                    cellNumber--;
                    if (cellNumber == 0)
                    {
                        return new Vector2Int(i, j);
                    }
                }
            }
        }
        throw new ArgumentException("Cell number is bigger than amount of available cells!");
    }

    public void ExcludePicked()
    {
        if (!_cellPicked)
        {
            throw new InvalidOperationException("Cell haven't been picked yet!");
        }

        _availabilityMatrix[_lastPickedCellPosition.X, _lastPickedCellPosition.Y] = false;
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
                    _availabilityMatrix[i, j] = false;
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
        int minI = System.Math.Clamp(cellPosition.X - radius, 0, _board.GetLength(0) - 1);
        int maxI = System.Math.Clamp(cellPosition.X + radius, 0, _board.GetLength(0) - 1);
        int minJ = System.Math.Clamp(cellPosition.Y - radius, 0, _board.GetLength(1) - 1);
        int maxJ = System.Math.Clamp(cellPosition.Y + radius, 0, _board.GetLength(1) - 1);
        for (int i = minI; i <= maxI; i++)
        {
            for (int j = minJ; j <= maxJ; j++)
            {
                if (i == cellPosition.X && j == cellPosition.Y)
                {
                    continue;
                }
                float distanceToCell = DistanceBetween(cellPosition, new Vector2Int(i, j));
                int roundedDistance = Round(distanceToCell);
                if (roundedDistance <= radius)
                {
                    _availabilityMatrix[i, j] = false;
                }
            }
        }
    }

    private static float DistanceBetween(Vector2Int a, Vector2Int b)
    {
        Vector2Int distanceVector = new Vector2Int(a.X - b.X, a.Y - b.Y);
        float distance = MathF.Sqrt(distanceVector.X * distanceVector.X + distanceVector.Y * distanceVector.Y);
        return distance;
    }

    private static int Round(float toRound)
    {
        float wholePart = MathF.Floor(toRound);
        float decimalPart = toRound - wholePart;
        if (decimalPart >= 0.5f)
        {
            return (int)MathF.Ceiling(toRound);
        }
        else
        {
            return (int)MathF.Floor(toRound);
        }
    }
    
    /// <summary>
    /// Picks such cell so that calling ExcludeAroundPicked for it will exclude the least amount of cells possible.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public Cell PickSmart(int radius)
    {
        if (CountAvailableCells() < 1)
        {
            throw new InvalidOperationException("No cells available to pick!");
        }
        List<CellPick> pickVariants = new List<CellPick>();
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (!_availabilityMatrix[i, j])
                {
                    continue;
                }
                var cellPosition = new Vector2Int(i, j);
                var intersectionsCount = CountIntersectionsForCell(cellPosition, radius);
                pickVariants.Add(new CellPick(){CellPosition = cellPosition, IntersectionsCount = intersectionsCount});
            }
        }

        int maxIntersectionCount = pickVariants.Max(c => c.IntersectionsCount);
        var optimalVariants = pickVariants.Where(c => c.IntersectionsCount == maxIntersectionCount);
        var pickedCellPosition = optimalVariants.GetRandom().CellPosition;
        _lastPickedCellPosition = pickedCellPosition;
        _cellPicked = true;
        return _board[pickedCellPosition];
    }

    public int CountIntersectionsForCell(Vector2Int cellPosition, int radius)
    {
        if (cellPosition.X < 0 || cellPosition.Y < 0)
        {
            throw new ArgumentException("Cell coordinates must be positive!");
        }
        if (cellPosition.X >= _board.GetLength(0) || cellPosition.Y >= _board.GetLength(1))
        {
            throw new ArgumentException("Cell position is outside of board boundaries!");
        }
        if (radius < 0)
        {
            throw new ArgumentException("Radius cannot be less than zero!");
        }

        return CountIntersections(cellPosition, radius);
    }

    private int CountIntersections(Vector2Int cellPosition, int radius)
    {
        int intersectionsCount = 0;
        for (int i = cellPosition.X - radius; i <= cellPosition.X + radius; i++)
        {
            for (int j = cellPosition.Y - radius; j <= cellPosition.Y + radius; j++)
            {
                if (i >= 0 && j >= 0  && 
                    i < _board.GetLength(0) && j < _board.GetLength(1) && 
                    _availabilityMatrix[i, j])
                {
                    continue;
                }
                float distanceToCell = DistanceBetween(cellPosition, new Vector2Int(i, j));
                int roundedDistance = Round(distanceToCell);
                if (roundedDistance <= radius)
                {
                    intersectionsCount++;
                }
            }
        }
        return intersectionsCount;
    }
}