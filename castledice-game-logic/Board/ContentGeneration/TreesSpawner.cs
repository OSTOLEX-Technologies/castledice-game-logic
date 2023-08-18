namespace castledice_game_logic.Board.ContentGeneration;

public class TreesSpawner : IContentSpawner
{
    private int _minTreesCount;
    private int _maxTreesCount;

    public TreesSpawner(int minTreesCount, int maxTreesCount)
    {
        _minTreesCount = minTreesCount;
        _maxTreesCount = maxTreesCount;
    }

    public IBoard SpawnContent(IBoard board)
    {
        throw new NotImplementedException();
    }
}