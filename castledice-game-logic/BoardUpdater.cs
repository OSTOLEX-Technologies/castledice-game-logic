namespace castledice_game_logic;

public class BoardUpdater
{
    private readonly Board _board;

    public BoardUpdater(Board board)
    {
        _board = board;
    }

    public void UpdateBoard()
    {
        foreach (var cell in _board)
        {
            foreach (var content in cell.GetContent())
            {
                content.Update();
            }
        }
    }
}