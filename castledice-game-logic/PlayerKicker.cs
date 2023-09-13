using castledice_game_logic.GameObjects;

namespace castledice_game_logic;

public class PlayerKicker
{
    private readonly Board _board;

    public PlayerKicker(Board board)
    {
        _board = board;
    }

    /// <summary>
    /// When player is kicked from board all his castles are freed and all his units are removed.
    /// However, objects like traps and bombs, that is objects which are not units are not removed.
    /// </summary>
    /// <param name="player"></param>
    public void KickFromBoard(Player player)
    {
        foreach (var cell in _board)
        {
            RemovePlayerUnits(cell, player);
            FreeCapturablesOwnedByPlayer(cell, player);
        }
    }

    private static void RemovePlayerUnits(Cell cell, Player player)
    {
        var cellContent = cell.GetContent();
        var contentToRemove = cellContent.Where(c => ContentIsPlayerUnit(c, player)).ToList();
        foreach (var content in contentToRemove)
        {
            cellContent.Remove(content);
        }
    }

    private static bool ContentIsPlayerUnit(Content content, Player player)
    {
        if (content is IPlayerOwned playerOwned and IReplaceable)
        {
            return playerOwned.GetOwner() == player;
        }

        return false;
    }

    private static void FreeCapturablesOwnedByPlayer(Cell cell, Player player)
    {
        var cellContent = cell.GetContent();
        var capturablesToFree = cellContent.Where(c => ContentIsCapturableOwnedByPlayer(c, player)).Cast<ICapturable>();
        foreach (var capturable in capturablesToFree)
        {
            capturable.Free();
        }
    }

    private static bool ContentIsCapturableOwnedByPlayer(Content content, Player player)
    {
        return content is ICapturable and IPlayerOwned playerOwned && playerOwned.GetOwner() == player;
    }
}