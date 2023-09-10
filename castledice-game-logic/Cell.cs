using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Microsoft.VisualBasic;

namespace castledice_game_logic;

public class Cell
{
    private List<Content> _content = new();
    private readonly Vector2Int _position;

    public Vector2Int Position => _position;

    public Cell(Vector2Int position)
    {
        _position = position;
    }

    public Cell(int x, int y)
    {
        _position = new Vector2Int(x, y);
    }

    public void AddContent(Content content)
    {
        _content.Add(content);
    }

    public bool RemoveContent(Content content)
    {
        return _content.Remove(content);
    }

    public List<Content> GetContent()
    {
        return _content;
    }

    public bool HasContent(Func<Content, bool> predicate)
    {
        return _content.Any(predicate);
    }

    /// <summary>
    /// Returns true if cell has any content.
    /// </summary>
    /// <returns></returns>
    public bool HasContent()
    {
        return _content.Count > 0;
    }
}