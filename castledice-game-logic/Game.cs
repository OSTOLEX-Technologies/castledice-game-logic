using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;

namespace castledice_game_logic;

/// <summary>
/// This is an entry point for castledice game which provides functionality for initializing game with certain parameters and
/// processing player actions.
/// </summary>
public class Game
{
    private List<Player> _players;
    private Board _board;

    public Game(List<Player> players, BoardConfig boardConfig)
    {
        _players = players;
        _board = new Board(boardConfig.CellType);
        boardConfig.CellsGenerator.GenerateCells(_board);
        foreach (var contentGenerator in boardConfig.ContentSpawners)
        {
            contentGenerator.SpawnContent(_board);
        }

        ValidateBoard();
    }

    private void ValidateBoard()
    {
        BoardValidator validator = new BoardValidator();
        if (!validator.BoardCastlesAreValid(_players, _board))
        {
            throw new ArgumentException("Castles on the board do not correspond to players list!");
        }
    }

    public List<Player> GetAllPlayers()
    {
        return _players;
    }
}