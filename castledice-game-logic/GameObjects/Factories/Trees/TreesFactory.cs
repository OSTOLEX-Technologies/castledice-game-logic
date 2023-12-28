using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameObjects.Factories.Trees;

public class TreesFactory : ITreesFactory
{
    public TreeConfig Config => _config;
    private readonly TreeConfig _config;
    
    public TreesFactory(TreeConfig config)
    {
        _config = config;
    }
    
    public Tree GetTree()
    {
        return new Tree(_config.RemoveCost, _config.CanBeRemoved);
    }
}