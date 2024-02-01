using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MyGame;

public class Game1 : Game
{
    Texture2D _cellTexture;

    private SpriteBatch _spriteBatch;
    GameOfLife.GameOfLife _board;
    SpriteFont _font;
    private int i = -25;
    private int j = -25;


    public Game1()
    {
        new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _board = new GameOfLife.GameOfLife();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _board.CellsLives(0, 1);
        _board.CellsLives(1, 2);
        _board.CellsLives(2, 0);
        _board.CellsLives(2, 1);
        _board.CellsLives(2, 2);


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _cellTexture = new Texture2D(GraphicsDevice, 1, 1);
        _cellTexture.SetData(new[] { Color.White });
        _font = Content.Load<SpriteFont>("Arial");
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
        
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        int cellSize = 10;
        int padding = 2;

        var maxX = 24;
        var maxY = 24;
        i++;
        
        

        _board = _board.Generate(1);

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (_board.GetCells(x, y) == 1)
                {
                    Rectangle destinationRectangle = new Rectangle(x * (cellSize + padding) + 320,
                        y * (cellSize + padding) + 240, cellSize, cellSize);

                    _spriteBatch.Draw(_cellTexture, destinationRectangle, Color.White);
                }
            }
        }

        _spriteBatch.DrawString(_font, $"Tick Count: {i} {j}", new Vector2(10, 10), Color.White);


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}