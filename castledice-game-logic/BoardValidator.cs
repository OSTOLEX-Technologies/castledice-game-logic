using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic;

public class BoardValidator
{
    public bool BoardCastlesAreValid(List<Player> players, Board board)
    {
        List<Player> castlesOwners = new List<Player>();
        foreach (var cell in board)
        {
            var castle = cell.GetContent().Find(c => c is Castle) as Castle;
            if (castle == null) continue;
            var owner = castle.GetOwner();
            if (!castlesOwners.Contains(owner))
            {
                castlesOwners.Add(owner);
            }
        }
        if (castlesOwners.Count != players.Count)
        {
            return false;
        }

        return castlesOwners.TrueForAll(players.Contains);
    }
    
    
}