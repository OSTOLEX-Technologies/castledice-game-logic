using System.Collections;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class BoardUpdaterTests
{
    public class UpdateableContent : Content
    {
        public int UpdatesCount = 0;
        public bool WasUpdated;
        
        public override void Update()
        {
            WasUpdated = true;
            UpdatesCount++;
        }
    }

    private class UpdateBoardTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return OneContentCase();
            yield return ContentOnEveryCellCase();
            yield return StackedContentCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] OneContentCase()
        {
            var board = GetFullNByNBoard(5);
            var content = new UpdateableContent();
            var contentOnBoard = new List<UpdateableContent>() { content };
            board[0, 0].AddContent(content);
            return new object[] { board, contentOnBoard };
        }

        private static object[] ContentOnEveryCellCase()
        {
            var board = GetFullNByNBoard(10);
            var contentOnBoard = new List<UpdateableContent>();
            foreach (var cell in board)
            {
                var content = new UpdateableContent();
                cell.AddContent(content);
                contentOnBoard.Add(content);
            }
            return new object[] { board, contentOnBoard };
        }

        private static object[] StackedContentCase()
        {
            var board = GetFullNByNBoard(5);
            var contentOnBoard = new List<UpdateableContent>();
            for (int i = 0; i < 3; i++)
            {
                var content = new UpdateableContent();
                board[1, 2].AddContent(content);
                contentOnBoard.Add(content);
            }
            return new object[] { board, contentOnBoard };
        }
    }
    
    [Theory]
    [ClassData(typeof(UpdateBoardTestCases))]
    public void UpdateBoard_ShouldCallUpdateMethod_OnEveryContent(Board boardWithContent, List<UpdateableContent> contentOnBoard)
    {
        var updater = new BoardUpdater(boardWithContent);
        
        updater.UpdateBoard();

        foreach (var content in contentOnBoard)
        {
            Assert.True(content.WasUpdated);
        }
    }
    
    [Theory]
    [ClassData(typeof(UpdateBoardTestCases))]
    public void UpdateBoard_ShouldCallUpdateMethod_OnceOnEachContent(Board boardWithContent, List<UpdateableContent> contentOnBoard)
    {
        var updater = new BoardUpdater(boardWithContent);
        
        updater.UpdateBoard();

        foreach (var content in contentOnBoard)
        {
            Assert.Equal(1, content.UpdatesCount);
        }
    }
}