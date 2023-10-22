using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Microsoft.VisualBasic;

namespace castledice_game_logic;

public class Cell
{
    public event EventHandler<Content>? ContentRemoved;
    public event EventHandler<Content>? ContentAdded;
    
    private readonly List<Content> _content = new();
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
        ContentAdded?.Invoke(this, content);
    }

    public bool RemoveContent(Content content)
    {
        if (!_content.Remove(content)) return false;
        ContentRemoved?.Invoke(this, content);
        return true;
    }

    public List<Content> GetContent()
    {
        return _content;
    }

    public bool HasContent(Func<Content, bool> predicate)
    {
        return _content.Exists(c => predicate(c));
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