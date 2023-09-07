using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.Utilities;

namespace castledice_game_logic.MovesLogic.Rules;

public static class PlaceRules
{
    public static bool CanPlaceOnCell(Board board, Vector2Int position, Player player)
    {
        if (!CanPlaceOnCellIgnoreNeighbours(board, position, player))
        {
            return false;
        }
        return CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player);
    }

    public static bool CanPlaceOnCellIgnoreNeighbours(Board board, Vector2Int position, Player player)
    {
        if (!board.HasCell(position))
        {
            return false;
        }

        var cell = board[position];

        return !cell.HasContent(c => ContentIsPlaceBlocking(c));
    }

    private static bool ContentIsPlaceBlocking(Content content)
    {
        if (content is IPlaceBlocking)
        {
            var blocking = content as IPlaceBlocking;
            return blocking.IsBlocking();
        }
        return false;
    }

    public static int GetPlaceCost(IPlaceable placeable)
    {
        return placeable.GetPlacementCost();
    }
}