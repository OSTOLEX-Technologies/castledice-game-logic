using castledice_game_logic.GameObjects;

namespace castledice_game_logic.BoardGeneration.ContentGeneration;

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

    //Algorithm of getting cells for spawning is following:
    //Step 1: Obtain amount of trees as a random number between min and max trees count.
    //Step 2: Try to get cells for spawning using a random method. If random method succeeds, then return list of cells and finish the algorithm.
    //Else, go to the next step.
    //Step 3: Try to get cells using a smart method. Smart method is more likely to succeed, but the cells arrangement will look less natural.
    //If smart method succeeds, then return lis of cells and finish the algorithm. Otherwise go to step 4.
    //Step 4: Reduce needed amount of trees by one and check if new amount is less than minimum trees amount. If it is, then 
    //it is impossible to spawn trees with given configuration and Exception should be raised. Otherwise, go to step 2.
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

    //This algorithm searches for exact amount of cells.
    //It uses CellsPickingUtility - a tool that provides methods for selecting cells (PickRandom and PickSmart) as well as
    //methods for excluding cells from selection. The algorithm is following:
    //Step 1: Prepare picking utility by excluding rows and columns that contain castles as well as rows and columns that are
    //adjacent to those with castles. Then exclude all cells that have content on them.
    //Step 2: Check if picking utility has available cells. If not, return false and finish the algorithm.
    //Step 3: Pick cell with picking utility by using either Smart or Random methods, depending on flag trySmart.
    //Add cell to the list.
    //Step 4: Exclude area around picked cell as well as picked cell itself.
    //Step 5: If more cells needed, go to step 2. Else, return true.
    private bool TryGetCellsToSpawnOn(int cellsCount, Board board, bool trySmart, out List<Cell> cells)
    {
        cells = new List<Cell>();
        CellsPickingUtility pickingUtility = new CellsPickingUtility(board);
        PreparePicker(pickingUtility, board);
        while (cellsCount > 0)
        {
            cellsCount--;
            if (pickingUtility.AvailableCellsCount() < 1)
            {
                cells.Clear();
                return false;
            }
            Cell cell;
            if (trySmart)
            {
                cell = pickingUtility.PickSmart(_minDistanceBetweenTrees);
            }
            else
            {
                cell = pickingUtility.PickRandom();
            }
            pickingUtility.ExcludePicked();
            pickingUtility.ExcludeAroundPicked(_minDistanceBetweenTrees);
            cells.Add(cell);
        }
        return true;
    }

    private void PreparePicker(CellsPickingUtility pickingUtility, Board board)
    {        
        var castlesPositions = board.GetCellsPositions(c => c.HasContent(ct => ct is Castle));
        foreach (var position in castlesPositions)
        {
            pickingUtility.ExcludeRows(position.Y - 1, position.Y, position.Y + 1);
            pickingUtility.ExcludeColumns(position.X - 1, position.X, position.X + 1);
        }
        pickingUtility.ExcludeCells(c => c.HasContent());
    }
}