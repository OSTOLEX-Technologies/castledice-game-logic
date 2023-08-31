namespace castledice_game_logic;

public class ActionsHistory
{
    public List<IActionSnapshot> History { get; } = new List<IActionSnapshot>();
}