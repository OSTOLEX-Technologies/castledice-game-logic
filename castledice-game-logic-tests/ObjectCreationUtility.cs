using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests;

public static class ObjectCreationUtility
{
    public static Board GetFullNByNBoard(int size)
    {
        var board = new Board(CellType.Square);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board.AddCell(i, j);
            }
        }
        return board;
    }
    
    public static Cell GetCell()
    {
        return new Cell(new Vector2Int(0, 0));
    }
    
    public static List<Player> GetPlayersList(int length)
    {
        List<Player> players = new List<Player>();
        for (int i = 0; i < length; i++)
        {
            players.Add(GetPlayer());
        }

        return players;
    }

    public static BoardConfig GetDefaultBoardConfig(Player firstPlayer, Player secondPlayer)
    {
        var cellsGenerator = new RectCellsGenerator(10, 10);
        var castlesSpawner = new CastlesSpawner(new Dictionary<Player, Vector2Int>()
        {
            {firstPlayer, new Vector2Int(0, 0)},
            {secondPlayer, new Vector2Int(9, 9)}
        });
        var cellType = CellType.Square;
        var boardConfig = new BoardConfig()
        {
            CellsGenerator = cellsGenerator,
            ContentSpawners = new List<IContentSpawner>()
            {
                castlesSpawner
            },
            CellType = cellType
        };
        return boardConfig;
    }
    
    public static Player GetPlayer()
    {
        return new Player();
    }

    public static Content GetCellContent()
    {
        return new Tree();
    }

    public static Content GetObstacle()
    {
        return new Tree();
    }
}