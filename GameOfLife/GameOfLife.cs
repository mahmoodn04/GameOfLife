using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace GameOfLife;

public class GameOfLife
{
    private const int GRID = 50;
    private const int HGRID = GRID / 2;
    public int[,] BoardOfLives;
    int[,] newBoardOfLives;


    public GameOfLife()
    {
        BoardOfLives = new int[GRID, GRID];
        newBoardOfLives = new int[GRID, GRID];
        for (var x = 0; x < GRID; x++)
        for (var y = 0; y < GRID; y++)
            BoardOfLives[x, y] = 0;
    }

    public void Tick(int x, int y)
    {
        var life = Counter(x, y);

        if (x >= GRID)
        {
            y = GRID - y;
            x = 0;
            
        }

        if (y >= GRID)
        {
            
            x = GRID - x;
            y = 0;
        }
        if (life == 0 && BoardOfLives[x, y] == 0)
        {
            newBoardOfLives[x, y] = BoardOfLives[x, y];
        }

        else if (life < 2 || life > 3)
        {
            newBoardOfLives[x, y] = 0;
        }
        else if (life == 3 && BoardOfLives[x, y] == 0)
        {
            newBoardOfLives[x, y] = 1;
        }
        else
        {
            newBoardOfLives[x, y] = BoardOfLives[x, y];
        }
        
        
    }

    public int Counter(int x, int y)
    {
        var counter = 0;
        var frontier = MaxGrid();

        for (var i = Math.Max(0, x - 1); i <= Math.Min(GRID - 1, x + 1); i++)
        {
            for (var j = Math.Max(0, y - 1); j <= Math.Min(GRID - 1, y + 1); j++)
            {
                if ((i != x || j != y) && BoardOfLives[i, j] == 1)
                {
                    counter++;
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
        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            BoardOfLives[i, j] = newCells[i, j];
        return this;
    }

    public GameOfLife Generate(int numberOfGenerations)
    {
        for (var gen = 0; gen < numberOfGenerations; gen++)
        {
            var frontier = MaxGrid();

            newBoardOfLives = new int[GRID, GRID];
            for (var i = Math.Max(0, (int)frontier[0] - 1); i <= Math.Min(GRID - 1, (int)frontier[1] + 1); i++)
            {
                for (var j = Math.Max(0, (int)frontier[2] - 1); j <= Math.Min(GRID - 1, (int)frontier[3] + 1); j++)
                {
                    Tick(i, j);
                    
                }
            }
            

            BoardOfLives = new int[GRID, GRID];
            for (int i = 0; i < GRID; i++)
            {
                for (int j = 0; j < GRID; j++)
                {
                    BoardOfLives[i, j] = newBoardOfLives[i, j];
                }
            }
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

        var result = new ArrayList();
        if (resultx.Count == 0)
        {
            result.Add(0);
            result.Add(0);
            result.Add(0);
            result.Add(0);
        }
        else
        {
            resultx.Sort();
            var MinX = resultx[0];
            var MaxX = resultx[resultx.Count - 1];
            resulty.Sort();
            var MinY = resulty[0];
            var MaxY = resulty[resulty.Count - 1];
            result.Add(MinX);
            result.Add(MaxX);
            result.Add(MinY);
            result.Add(MaxY);
        }

        return result;
    }
}