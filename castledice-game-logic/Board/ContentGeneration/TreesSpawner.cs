using castledice_game_logic.GameObjects;

namespace castledice_game_logic.Board.ContentGeneration;

public class TreesSpawner : IContentSpawner
{
    private int _minTreesCount;
    private int _maxTreesCount;
    private int _minDistanceBetweenTrees;

    public TreesSpawner(int minTreesCount, int maxTreesCount, int minDistanceBetweenTrees)
    {
        _minTreesCount = minTreesCount;
        _maxTreesCount = maxTreesCount;
        _minDistanceBetweenTrees = minDistanceBetweenTrees;
    }

    
    //TODO: Discuss trees spawning algorithm
    public void SpawnContent(Board board)
    {
        var cells = GetCellsToSpawnOn(board);
        foreach (var cell in cells)
        {
            cell.AddContent(new Tree());
        }
    }

    private List<Cell> GetCellsToSpawnOn(Board board)
    {
        List<Cell> cells;
        var rnd = new Random();
        int treesCount = rnd.Next(_minTreesCount, _maxTreesCount + 1);
        while (true)
        {
            if (TryGetCellsToSpawnOn(treesCount, board, false, out cells))
            {
                break;
            }
            else if (TryGetCellsToSpawnOn(treesCount, board, true, out cells))
            {
                break;
            }
            treesCount--;
            if (treesCount < _minTreesCount)
            {
                throw new InvalidOperationException("Impossible to spawn trees with given configuration!");
            }
        }
        return cells;
    }

    private bool TryGetCellsToSpawnOn(int cellsCount, Board board, bool trySmart, out List<Cell> cells)
    {
        cells = new List<Cell>();
        CellsPicker picker = new CellsPicker(board);
        PreparePicker(picker, board);
        while (cellsCount > 0)
        {
            cellsCount--;
            if (picker.AvailableCellsCount() < 1)
            {
                cells.Clear();
                return false;
            }
            Cell cell;
            if (trySmart)
            {
                cell = picker.PickSmart(_minDistanceBetweenTrees);
            }
            else
            {
                cell = picker.PickRandom();
            }
            picker.ExcludePicked();
            picker.ExcludeAroundPicked(_minDistanceBetweenTrees);
            cells.Add(cell);
        }
        return true;
    }

    private void PreparePicker(CellsPicker picker, Board board)
    {        
        var castlesPositions = board.GetCellsPositions(c => c.HasContent(ct => ct is Castle));
        foreach (var position in castlesPositions)
        {
            picker.ExcludeRows(position.Y - 1, position.Y, position.Y + 1);
            picker.ExcludeColumns(position.X - 1, position.X, position.X + 1);
        }
        picker.ExcludeCells(c => c.HasContent());
    }

    private int GetTreesSpawnCount()
    {
        var rnd = new Random();
        return rnd.Next(_minTreesCount, _maxTreesCount);
    }
}