namespace castledice_game_logic.Board;

public class BoardGenerator
{
    public IBoard GenerateRectBoard(RectBoardGenerationConfig config)
    {
        var board = new RectBoard(config.Width, config.Height);

        foreach (var castlePosition in config.CastleCoordinates)
        {
            var cell = board.GetCell(castlePosition.Value.X, castlePosition.Value.Y);
            var content = new CellContent(castlePosition.Key, ContentType.Castle);
            cell.Content = content;
        }

        return board;
    }
}