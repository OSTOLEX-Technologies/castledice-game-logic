using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.Utilities;

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
            var capturable = content as ICapturable;
            int captureCost = capturable.GetCaptureCost(player);
            bool canCapture = capturable.CanBeCaptured(player);
            bool canAfford = captureCost <= player.ActionPoints.Amount;
            return canCapture && canAfford;
        }
        return false;
    }

    public static int GetCaptureCost(Board board, Vector2Int position, Player player)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }
        var capturable = GetCapturableOnPosition(board, position);
        return capturable.GetCaptureCost(player);
    }

    private static ICapturable GetCapturableOnPosition(Board board, Vector2Int position)
    {
        var cell = board[position];
        var capturable = cell.GetContent().FirstOrDefault(c => c is ICapturable) as ICapturable;
        if (capturable == null)
        {
            throw new ArgumentException("No capturable on position: " + position);
        }
        return capturable;
    }
    
}