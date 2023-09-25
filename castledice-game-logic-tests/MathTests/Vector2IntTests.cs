using castledice_game_logic.Math;

namespace castledice_game_logic_tests.MathTests;

public class Vector2IntTests
{
    [Fact]
    public void XYProperties_ShouldReturnValues_GivenInConstructor()
    {
        int x = 2;
        int y = 3;
        var vector = new Vector2Int(x, y);
        
        Assert.Equal(x, vector.X);
        Assert.Equal(y, vector.Y);
    }
    
    [Fact]
    public void Equals_ShouldReturnTrue_IfVectorsAreEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 3);
        
        Assert.True(firstVector.Equals(secondVector));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_IfVectorsAreNotEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 4);
        
        Assert.False(firstVector.Equals(secondVector));
    }

    [Fact]
    public void EqualityOperator_ShouldReturnTrue_IfVectorsAreEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 3);
        
        Assert.True(firstVector == secondVector);
    }
    
    [Fact]
    public void EqualityOperator_ShouldReturnFalse_IfVectorsAreNotEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 4);
        
        Assert.False(firstVector == secondVector);
    }

    [Fact]
    public void InequalityOperator_ShouldReturnTrue_IfVectorsAreNotEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 4);
        
        Assert.True(firstVector != secondVector);
    }
    
    [Fact]
    public void InequalityOperator_ShouldReturnFalse_IfVectorsAreEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 3);
        
        Assert.False(firstVector != secondVector);
    }
    
    [Fact]
    public void GetHashCode_ShouldReturnSameValue_IfVectorsAreEqual()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 3);
        
        Assert.Equal(firstVector.GetHashCode(), secondVector.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentValues_IfVectorsAreDifferent()
    {
        var firstVector = new Vector2Int(1, 3);
        var secondVector = new Vector2Int(1, 4);
        
        Assert.NotEqual(firstVector.GetHashCode(), secondVector.GetHashCode());
    }

    [Fact]
    public void TupleConversionOperator_ShouldReturnVector_WithGivenValues()
    {
        int x = 2;
        int y = 3;
        Vector2Int vector = (x, y);
        
        Assert.Equal(x, vector.X);
        Assert.Equal(y, vector.Y);
    }
}