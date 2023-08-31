using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic.Rules;

public static class CaptureRules
{
    public static bool CanCaptureOnCell(Board board, Vector2Int position, Player player)
    {
        if (!CanCaptureOnCellIgnoreNeighbours(board, position, player))
        {
            return false;
        }
        return CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player);
    }

    public static bool CanCaptureOnCellIgnoreNeighbours(Board board, Vector2Int position, Player player)
    {
        if (!board.HasCell(position))
        {
            return false;
        }
        var cell = board[position];
        return cell.HasContent(c => ContentCanBeCapturedByPlayer(c, player));
    }

    private static bool ContentCanBeCapturedByPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned)
        {
            var playerOwned = content as IPlayerOwned;
            if (playerOwned.GetOwner() == player)
            {
                return false;
            }
        }
        if (content is ICapturable)
        {
            return true;
        }
        return false;
    }
}