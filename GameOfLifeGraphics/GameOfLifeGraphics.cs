using SkiaSharp;

namespace GameOfLifeGraphics;

public class GameOfLifeGraphics
{
    private static void Main()
    {
        var board = new GameOfLife.GameOfLife();
        board.CellsLives(0, 1);
        board.CellsLives(0, 2);
        board.CellsLives(1, 1);

        for (var i = 0; i < 20; i++)
        {
            board = board.Tick(i % 10 + 20, 25);
            CallCanva(board, i);
        }
    }

    public static void CallCanva(GameOfLife.GameOfLife board, int i)
    {
        SKBitmap bmp = new(640, 480);


        var cellSize = 10;
        var padding = 2;
        using SKCanvas canvas = new(bmp);
        canvas.Clear(SKColor.Parse("#003366"));

        for (var x = 0; x < 3; x++)
        for (var y = 0; y < 3; y++)
            if (board.GetCells(x, y) == 1)
            {
                SKPaint paint = new() { Color = SKColors.White.WithAlpha(100), IsAntialias = true };
                float xpos = x * (cellSize + padding) + 320;
                float ypos = y * (cellSize + padding) + 240;

                canvas.DrawRect(new SKRect(xpos, ypos, xpos + cellSize, ypos + cellSize), paint);
            }

        // Save the image to disk
        SKFileWStream fs =
            new(
                $"C:\\Users\\Mahmood\\Documents\\Projects\\GameOfLifeKata\\GameOfLife\\GameOfLifeGraphics\\cell{i}.jpg");
        bmp.Encode(fs, SKEncodedImageFormat.Jpeg, 85);
    }
}