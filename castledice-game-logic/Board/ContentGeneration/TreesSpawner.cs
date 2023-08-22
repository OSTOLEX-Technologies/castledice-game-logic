using castledice_game_logic.GameObjects;

namespace castledice_game_logic.Board.ContentGeneration;

public class TreesSpawner : IContentSpawner
{
    private int _minTreesCount;
    private int _maxTreesCount;
    private int _minDistanceBetweenTrees;

    private bool[,] _availableCells;

    public TreesSpawner(int minTreesCount, int maxTreesCount, int minDistanceBetweenTrees)
    {
        _minTreesCount = minTreesCount;
        _maxTreesCount = maxTreesCount;
        _minDistanceBetweenTrees = minDistanceBetweenTrees;
    }

    
    //TODO: Discuss trees spawning algorithm
    public void SpawnContent(Board board)
    {
        CellsPicker picker = new CellsPicker(board);
        var castlesPositions = board.GetCellsPositions(c => c.HasContent(ct => ct is Castle));
        //Step 1: Excluding castles rows and columns and their neighbours
        foreach (var position in castlesPositions)
        {
            picker.ExcludeRows(position.Y - 1, position.Y, position.Y + 1);
            picker.ExcludeColumns(position.X - 1, position.X, position.X + 1);
        }
        //Step 2: Excluding cells with other content
        picker.ExcludeCells(c => c.HasContent());
        int treesAmount = GetTreesSpawnCount();
        while (treesAmount > 0)
        {
            treesAmount--;
            var cell = picker.PickRandomCell();
            cell.AddContent(new Tree());
            picker.ExcludePicked();
            picker.ExcludeAroundPicked(_minDistanceBetweenTrees);
            if (picker.AvailableCellsCount() < 1)
            {
                break;
            }
        }
    }

    private int GetTreesSpawnCount()
    {
        var rnd = new Random();
        return rnd.Next(_minTreesCount, _maxTreesCount);
    }
}