using System.Collections;
using Xunit;

namespace GameOfLifeTests;

public class GameOfLifeTests
{
    [Fact]
    public void ShouldDeadIfOnlyOne()
    {
        var board = new GameOfLife.GameOfLife();
        board.CellsLives(1, 1);
        // When
        board.Tick(25, 25);

        // Then
        Assert.Equal(0, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldReturnDeadWhenLessThanTwoLivesNeighbors()
    {
        // Given

        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(1, 1);
        board = board.CellsLives(1, 2);
        // When
        board.Tick(25, 25);
        // Then
        Assert.Equal(1, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldReturnAnotherDeadWhenLessThanTwoLivesNeighbors()
    {
        // Given

        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(1, 1);
        board = board.CellsLives(1, 0);
        // When
        board.Tick(25, 25);
        // Then
        Assert.Equal(1, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldReturnDeadWhenMoreThanThreeLivesNeighbors()
    {
        // Given

        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(1, 1);
        board = board.CellsLives(1, 2);
        board = board.CellsLives(1, 0);
        board = board.CellsLives(0, 1);
        board = board.CellsLives(0, 2);
        // When
         board.Tick(25, 25);
        // Then
        Assert.Equal(4, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldStaysLifeWhenItHasTwonieghbors()
    {
        // Given

        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(1, 1);
        board = board.CellsLives(1, 0);
        board = board.CellsLives(0, 2);
        board = board.CellsLives(1, 2);

        // When
        board.Tick(25, 25);
        // Then
        Assert.Equal(3, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldReturnLivesWhenItHasThreeNeighbors()
    {
        // Given
        var boardOfDeads = new int[3, 3] { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };

        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(1, 0);
        board = board.CellsLives(0, 2);
        board = board.CellsLives(1, 2);

        // When
        board.Tick(25, 25);
        // Then

        Assert.Equal(3, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldReturnNewNeighborsOfOldCellsInBoard()
    {
        // Given


        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(0, 1);
        board = board.CellsLives(0, 2);
        board = board.CellsLives(1, 1);

        // When
        board = board.Generate(1);
        // Then

        Assert.Equal(1, board.GetCells(0, 1));
        Assert.Equal(1, board.GetCells(0, 2));
        Assert.Equal(1, board.GetCells(1, 1));
    }

    [Fact]
    public void ShouldReturnCounterOfCellsNeighbors()
    {
        // Given
        var boardOfDeads = new int[50, 50];


        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(0, 1);
        board = board.CellsLives(0, 2);
        board = board.CellsLives(1, 1);

        // When
        board = board.Generate(1);
        // Then


        Assert.Equal(3, board.Counter(25, 25));
    }

    [Fact]
    public void ShouldTranslateOnXAbices()
    {
        // Given


        var board = new GameOfLife.GameOfLife();
        board = board.CellsLives(0, 1);
        board = board.CellsLives(0, 2);
        board = board.CellsLives(1, 1);

        // When
        board = board.Generate(2);
        // Then


        Assert.Equal(3, board.Counter(25, 26));
    }

    [Fact]
    public void ShouldReturnBoardAfterTwoGenerate()
    {
        // Given
        var boardOfDeads = new int[50, 50];


        var board = new GameOfLife.GameOfLife();
        var newboard = new GameOfLife.GameOfLife();
        board.CellsLives(0, 0);
        board.CellsLives(1, 2);
        board.CellsLives(2, 1);

        // When
        board = board.Generate(2);

        // Then


        Assert.Equal(0, board.Counter(25, 25));
    }
    [Fact]
    public void ShouldReturnMaxAndMinOfYX()
    {
        // Given
        var board = new GameOfLife.GameOfLife();
        board.CellsLives(0, 0);
        board.CellsLives(1, 2);
        board.CellsLives(2, 1);
        // When
        // Then

        var r = new ArrayList(); 
            r.Add(24);
            r.Add(26);
            r.Add(24);
            r.Add(26);
        Assert.Equal(r, board.MaxGrid());
    }
    [Fact]
    public void ShouldReturnCreateOscillator()
    {
        // Given
        var board = new GameOfLife.GameOfLife();
        board.CellsLives(1, 0);
        board.CellsLives(1, 1);
        board.CellsLives(1, 2);
        // When
        board.Generate(1);
        // Then
        Assert.Equal(1 , board.GetCells(1, 1));
        Assert.Equal(1 , board.GetCells(0, 1));
        Assert.Equal(1 , board.GetCells(2, 1));


    }
  // [Fact]
  // public void ShouldPassBoarderToTheOtherSide()
  // {
  //     // Given
  //     var board = new GameOfLife.GameOfLife();
  //     board.CellsLives(23, 24);
  //     board.CellsLives(24, 25);
  //     board.CellsLives(25, 23);
  //     board.CellsLives(25, 24);
  //     board.CellsLives(25, 25);
  //     // When
  //     board.Generate(1);
  //     
  //     // Then
  //     Assert.Equal(1 , board.GetCells(0, 0));
  //    
  // 
  // 
  // }
}