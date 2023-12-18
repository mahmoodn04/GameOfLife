using System.Collections;

namespace GameOfLife;

public class GameOfLife
{
    private const int GRID = 50;
    private const int HGRID = GRID / 2;
    public int[,] BoardOfLives;
    public int tickCount = 0;

    public GameOfLife()
    {
        BoardOfLives = new int[GRID, GRID];

        for (var x = 0; x < GRID; x++)
        for (var y = 0; y < GRID; y++)
            BoardOfLives[x, y] = 0;
    }

    public GameOfLife Tick(int x, int y)
    {
        var life = Counter(x, y);
        if (life == 0 && BoardOfLives[x, y] == 0)
        {
            return this;
        }

        if (life < 2 || life > 3)
        {
            BoardOfLives[x, y] = 0;
            
        }
        else if (life == 3 && BoardOfLives[x, y] == 0)
        {
            BoardOfLives[x, y] = 1;
        }

        return this;
    }

    public int Counter(int x, int y)
    {
        var counter = 0;
        for (var i = x - 1; i < x + 2 ; i++)
        {
            for (var j = y - 1; j < y + 2; j++)
            {
                if (i >= 0 && i <= 49 && j >= 0 && j <= 49)
                {
                    if (BoardOfLives[i, j] == 1 && (j != y || i != x))
                    {
                        counter++;
                    }
                }
            }
        }

        return counter;
    }

    public GameOfLife CellsLives(int x, int y)
    {
        BoardOfLives[x - 1 + HGRID, y - 1 + HGRID] = 1;
        return this;
    }

    public int GetCells(int x, int y)
    {
        return BoardOfLives[x - 1 + HGRID, y - 1 + HGRID];
    }

    public GameOfLife SetBoardOfLives(int[,] newCells)
    {
        Console.WriteLine($"{newCells}");
        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            BoardOfLives[i, j] = newCells[i, j];
        return this;
    }

    public GameOfLife Generate(int numberOfGenerations)
    {
        for (var gen = 0; gen < numberOfGenerations; gen++)
        {
            var newBoard = new int[GRID, GRID];
            for (var i = 0; i < GRID; i++)
            {
                for (var j = 0; j < GRID; j++)
                {
                    newBoard[i, j] = Tick(i, j).BoardOfLives[i, j];
                }
            }

            BoardOfLives = newBoard;
        }

        return this;
    }

    public ArrayList MaxGrid()
    {
        var resultx = new ArrayList();
        var resulty = new ArrayList();


        for (int i = 0; i < GRID; i++)
        {
            for (int j = 0; j < GRID; j++)
            {
                if (BoardOfLives[i, j] == 1)
                {
                    resultx.Add(i);
                    resulty.Add(j);
                }
            }
        }

        resultx.Sort();
        var MinX = resultx[0];
        var MaxX = resultx[resultx.Count - 1];
        resulty.Sort();
        var MinY = resulty[0];
        var MaxY = resulty[resulty.Count - 1];
        var result = new ArrayList();
        result.Add(MinX);
        result.Add(MaxX);
        result.Add(MinY);
        result.Add(MaxY);

        return result;
    }
}