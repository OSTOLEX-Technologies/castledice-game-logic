using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests;

public class ContentTests
{
    public class ContentStub : Content
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void ModifyState()
        {
            OnStateModified();
        }

        public override T Accept<T>(IContentVisitor<T> visitor)
        {
            throw new NotImplementedException();
        }
    }
    
    [Fact]
    public void StateModifiedEvent_ShouldPassItselfAsEventArgument()
    {
        var content = new ContentStub();
        var eventRaised = false;
        content.StateModified += (sender, args) =>
        {
            eventRaised = true;
            Assert.Equal(content, sender);
            Assert.Equal(content, args);
        };
        
        content.ModifyState();
        
        Assert.True(eventRaised);
    }
}