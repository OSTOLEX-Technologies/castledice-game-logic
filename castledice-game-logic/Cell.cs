namespace castledice_game_logic;

public class Cell
{
    public CellContent Content { get; set; }

    public void RemoveContent()
    {
        Content = null;
    }
}