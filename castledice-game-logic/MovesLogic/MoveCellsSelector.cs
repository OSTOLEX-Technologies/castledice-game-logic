using castledice_game_logic;

namespace castledice_game_logic.MovesLogic;

public class MoveCellsSelector
{
    private Board _board;

    public MoveCellsSelector(Board board)
    {
        _board = board;
    }

    public List<MoveCell> SelectMoveCells(Player player)
    {
        //Проверь текущую клетку. Если на ней есть юнит игрока, то проверь улучшаемый ли он.
        //Если да, то добавь его в список клеток с пометкой Upgrade.
        //Если нет, то проверь, есть ли юниты игрока в соседних клетках.
        //Если нет, то двигайся к следующей клетке.
        //Если да, то проверь, есть ли на клетке разрушаемый объект.
        //Если да, то добавь клетку в список с пометкой Destroy.
        //Если нет, то добавь клетку в список с пометкой Placement.
        //
        return null;
    }
}