namespace castledice_game_logic.Board.ContentGeneration;

public class TreesSpawner : IContentSpawner
{
    private int _minTreesCount;
    private int _maxTreesCount;
    private int _minDistanceBetweenTrees;

    private bool[,] _cellsAvailability;

    public TreesSpawner(int minTreesCount, int maxTreesCount, int minDistanceBetweenTrees)
    {
        _minTreesCount = minTreesCount;
        _maxTreesCount = maxTreesCount;
        _minDistanceBetweenTrees = minDistanceBetweenTrees;
    }

    public void SpawnContent(Board board)
    {
        var cellsToSpawnTreesOn = GetCellsToSpawnTreesOn(board);
        foreach (var cell in cellsToSpawnTreesOn)
        {
            //TODO: Finish trees generation
            //cell.AddContent(new Tree());
        }
    }

    private List<Cell> GetCellsToSpawnTreesOn(Board board)
    {
        _cellsAvailability = new bool[board.GetMaxLength(), board.GetMaxWidth()];
        ExcludeNotAvailableCells(board);
        int spawnCount = GetTreesSpawnCount();
        int cellsCount = GetBoardCellsCount(board);
        var firstCell = board.First();
        //Проверить, присутствует ли индекс клетки в списке запрещенных индексов. Если да, пропустить
        //Проверить, присутствует ли на клетке замок. Если да, пометить клетку и её соседей как запрещенные и пропустить.
        //Проверить, присутствует ли замок на соседних клетках. Если да, пометить клетку с замком как запрещенную и пропустить.
        //
        //
        //
        //
        //
        return null;
    }

    private void ExcludeNotAvailableCells(Board board)
    {
        for (int i = 0; i < board.GetMaxLength(); i++)
        {
            for (int j = 0; j < board.GetMaxWidth(); j++)
            {
                if (board.HasCell(i, j))
                {
                    var cell = board[i, j];

                }
            }
        }
    }

    private int GetTreesSpawnCount()
    {
        var rnd = new Random();
        return rnd.Next(_minTreesCount, _maxTreesCount);
    }

    private int GetBoardCellsCount(Board board)
    {
        int count = 0;
        foreach (var cell in board)
        {
            count++;
        }

        return count;
    }
}