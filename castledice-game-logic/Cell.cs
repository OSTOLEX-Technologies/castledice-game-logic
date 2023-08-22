using castledice_game_logic.GameObjects;
using Microsoft.VisualBasic;

namespace castledice_game_logic;

public class Cell
{
    private List<GameObject> _content = new();

    public void AddContent(GameObject content)
    {
        _content.Add(content);
    }

    public bool RemoveContent(GameObject content)
    {
        return _content.Remove(content);
    }

    public List<GameObject> GetContent()
    {
        return _content;
    }

    public bool HasContent(Func<GameObject, bool> predicate)
    {
        return _content.Any(predicate);
    }

    public bool HasContent()
    {
        return _content.Count > 0;
    }
}