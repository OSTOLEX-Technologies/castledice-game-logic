namespace castledice_game_logic.Board.ContentGeneration;

/// <summary>
/// Class responsible for spawning initial content on the board, that is content which is not created by player.
/// </summary>
public interface IContentSpawner
{
    void SpawnContent(Board board);
}