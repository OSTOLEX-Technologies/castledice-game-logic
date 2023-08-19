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

    public Board SpawnContent(Board board)
    {
        throw new NotImplementedException();
    }
}