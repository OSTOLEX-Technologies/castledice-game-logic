using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace castledice_game_logic.GameConfiguration;

public class BoardConfig
{
    public List<IContentSpawner> ContentSpawners { get; }
    public ICellsGenerator CellsGenerator { get; }
    public CellType CellType { get; }

    public BoardConfig(List<IContentSpawner> contentSpawners, ICellsGenerator cellsGenerator, CellType cellType)
    {
        ContentSpawners = contentSpawners;
        CellsGenerator = cellsGenerator;
        CellType = cellType;
    }
}