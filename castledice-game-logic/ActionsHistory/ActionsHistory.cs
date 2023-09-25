namespace castledice_game_logic;

public class ActionsHistory
{
    public event EventHandler<IActionSnapshot>? SnapshotAdded; 
    private readonly List<IActionSnapshot> _history = new();

    public void AddActionSnapshot(IActionSnapshot snapshot)
    {
        _history.Add(snapshot);
        SnapshotAdded?.Invoke(this, snapshot);
    }
    
    public List<IActionSnapshot> GetHistory()
    {
        return _history;
    }
}