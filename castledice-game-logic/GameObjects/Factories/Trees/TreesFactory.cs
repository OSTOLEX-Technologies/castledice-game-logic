using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameObjects.Factories;

public class TreesFactory : ITreesFactory
{
    private TreeConfig _config;
    
    public TreesFactory(TreeConfig config)
    {
        _config = config;
    }
    
    public Tree GetTree()
    {
        return new Tree(_config.RemoveCost, _config.CanBeRemoved);
    }
}