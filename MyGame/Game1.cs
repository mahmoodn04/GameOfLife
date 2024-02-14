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
    private int i = -GameOfLife.GameOfLife.HGRID;
    private int j = -GameOfLife.GameOfLife.HGRID;


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

        var maxX = GameOfLife.GameOfLife.HGRID - 1;
        var maxY = GameOfLife.GameOfLife.HGRID - 1;
        i++;
        int[,] newBoardOfLives = new int[GameOfLife.GameOfLife.GRID, GameOfLife.GameOfLife.GRID];

        _board = _board.Generate(1);

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (_board.GetCells(x, y) == 1)
                {
                    Rectangle destinationRectangle = new Rectangle(x * (cellSize + padding),
                        y * (cellSize + padding) , cellSize, cellSize);

                    _spriteBatch.Draw(_cellTexture, destinationRectangle, Color.White);
                }
            }
        }

        for (int z = GameOfLife.GameOfLife.HGRID - 1; z < GameOfLife.GameOfLife.HGRID; z++)
        {
            if (_board.GetCells(GameOfLife.GameOfLife.HGRID - 1, z) == 1)
            {
                //for (int h = 0; h < 50; h++)
                //{
                //    for (int e = 0; e < 50; e++)
                //    {
                //        if (_board.BoardOfLives[h, e] == 1)
                //        {
                //
                //            newBoardOfLives[50 - h, 50 - e] = _board.BoardOfLives[h, e];
                //        }
                //    }
                _board.clear();

                _board.CellsLives(0-10, 1-10);
                _board.CellsLives(1-10, 2-10);
                _board.CellsLives(2-10, 0-10);
                _board.CellsLives(2-10, 1-10);
                _board.CellsLives(2-10, 2-10);
                break;
            }
        }


            //
            //          for (int l = 0; l < 50; l++)                        
            //          {                                                     
            //              for (int n = 0; n < 50; n++)                    
            //              {                                                 
            //                  _board.BoardOfLives[l, n] = newBoardOfLives[l, n];   
            //              }                                                 
            //          }                                                     
            //      }
            //      
             

            _spriteBatch.DrawString(_font, $"Tick Count: {i}", new Vector2(10, 10), Color.White);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }