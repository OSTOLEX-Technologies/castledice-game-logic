using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.Utilities;

namespace castledice_game_logic.MovesLogic.Rules;

public static class ReplaceRules
{
    public static bool CanReplaceOnCell(Board board, Vector2Int position, Player player)
    {
        if (!CanReplaceOnCellIgnoreNeighbours(board, position, player))
        {
            return false;
        }

        return CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player);
    }

    public static bool CanReplaceOnCellIgnoreNeighbours(Board board, Vector2Int position, Player player)
    {
        if (!board.HasCell(position))
        {
            return false;
        }
        var cell = board[position];
        return cell.HasContent(c => ContentCanBeReplacedByPlayer(c, player));
    }
    
    private static bool ContentCanBeReplacedByPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned playerOwned && playerOwned.GetOwner() == player)
        {
            return false;
        }
        if (content is not IReplaceable replaceable) return false;
        int replaceCost = CalculateReplaceCost(replaceable.GetReplaceCost(), 1); //One is a minimum possible replacement place cost.
        int playerActionPoints = player.ActionPoints.Amount;
        return replaceCost <= playerActionPoints;
    }
    
    public static int GetReplaceCost(Board board, Vector2Int position, IPlaceable replacement)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }

        var replaceable = GetReplaceableOnPosition(board, position);


        return CalculateReplaceCost(replaceable.GetReplaceCost(), replacement.GetPlacementCost());
    }
    
    private static IReplaceable GetReplaceableOnPosition(Board board, Vector2Int position)
    {
        var cell = board[position];
        var replaceable = cell.GetContent().Find(c => c is IReplaceable) as IReplaceable;
        if (replaceable == null)
        {
            throw new ArgumentException("No replaceable objects on position: " + position);
        }

        return replaceable;
    }

    private static int CalculateReplaceCost(int removeCost, int replaceablePlaceCost)
    {
        return removeCost + replaceablePlaceCost;
    }
}