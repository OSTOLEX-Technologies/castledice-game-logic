using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace castledice_game_logic.GameConfiguration;

public struct BoardConfig
{
    public List<IContentSpawner> ContentSpawners;
    public ICellsGenerator CellsGenerator;
    public CellType CellType;
}