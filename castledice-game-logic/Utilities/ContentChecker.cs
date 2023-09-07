using castledice_game_logic.GameObjects;

namespace castledice_game_logic.Utilities;

public static class ContentChecker
{
    public static bool ContentBelongsToPlayer(Content content, Player player)
    {
        if (content is not IPlayerOwned ownedContent) return false;
        var owner = ownedContent.GetOwner();
        return player == owner;
    }
}