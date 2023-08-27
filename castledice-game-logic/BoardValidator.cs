using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic;

public class BoardValidator
{
    public bool BoardCastlesAreValid(List<Player> players, Board board)
    {
        int castlesCount = 0;
        List<Player> castlesOwners = new List<Player>();
        foreach (var cell in board)
        {
            if (cell.HasContent(c => c is Castle))
            {
                var castle = cell.GetContent().FirstOrDefault(c => c is Castle) as Castle;
                var owner = castle.GetOwner();
                if (!castlesOwners.Contains(owner))
                {
                    castlesOwners.Add(owner);
                }
            }
        }
        if (castlesOwners.Count != players.Count)
        {
            return false;
        }
        foreach (var owner in castlesOwners)
        {
            if (!players.Contains(owner))
            {
                return false;
            }
        }
        return true;
    }
    
    
}