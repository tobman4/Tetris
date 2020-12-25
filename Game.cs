using System;
using System.Diagnostics;
using System.Collections;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tetris.Shapes;

namespace Tetris {

    public class TetrisGame : Game {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private DateTime lastCellUpdate = DateTime.Now;
        private int fallSpeed = 500; //ms

        private DateTime lastKey = DateTime.Now;
        private int keyDeley = 100; //ms

        private const int CellW = 10;
        private const int CellH = 10;
        private const int BoardW = 10;
        private const int BoardH = 20;

        private Cell[,] grid;
        private Shape shape = new Shape();


        public TetrisGame(int w = 10, int h = 20) {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Shape s = new Shape();
            s.getDrawcells();

            grid = new Cell[w, h];
            for(int i = 0; i < BoardW; i++) {
                for(int j = 0; j < BoardH; j++) {
                    grid[i, j] = new Cell(CellMode.Empty);
                }
            }

        }

        private Cell getEmptyCell() {
            return new Cell(CellMode.Empty);
        }


        protected override void Initialize() {

            graphics.PreferredBackBufferWidth = CellW * BoardW;
            graphics.PreferredBackBufferHeight = CellH * BoardH;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        private bool isKeyDown(Keys k) {
            bool keyIsDown = Keyboard.GetState().IsKeyDown(k);
            bool deleyOk = DateTime.Now.Subtract(lastKey).Milliseconds >= keyDeley;
            if(deleyOk && keyIsDown) {
                lastKey = DateTime.Now;

            }
            return deleyOk && keyIsDown;
        }

        private bool canMove() {
            return false;
        }

        protected override void Update(GameTime gameTime) {

            DateTime start = DateTime.Now;

            #region INPUT

            if(isKeyDown(Keys.Left)) {
                shape.move(-1);
            }
            if(isKeyDown(Keys.Right)) {
                shape.move(1);
            }
            if(isKeyDown(Keys.Up)) {
                shape.Rotate();
            }

            if(shape.hasNewChange) {
                bool isValid = true;
                Cell[] newCells = shape.getNewDrawcells();
                for(int i = 0; i < newCells.Length; i++) {

                    Point newPos = newCells[i].pos;

                    if(newPos.X < 0 || newPos.Y < 0 ||
                       newPos.X >= BoardW || newPos.Y >= BoardH) {
                        isValid = false;
                        Debug.WriteLine("Reject move: out of screen");
                        break;
                    }

                    if(grid[newPos.X, newPos.Y].mode != CellMode.Empty && grid[newPos.X, newPos.Y].id == newCells[i].id) {
                        isValid = false;
                        Debug.WriteLine("Reject move: not empty cell");
                        break;
                    }
                }
                shape.approveChange(isValid);
            }

            #endregion

            #region UPDATE CELLS
            if(start.Subtract(lastCellUpdate).TotalMilliseconds >= fallSpeed) {
                shape.Update(gameTime);
                for(int i = 0; i < BoardW; i++) {
                    for(int j = BoardH-1; j > 0; j--) {
                        if(grid[i,j].mode == CellMode.Active && shape.needNewDraw) {
                            grid[i, j] = getEmptyCell();
                        } else if(grid[i,j].mode == CellMode.Empty) {
                            Cell nxt = grid[i, j-1];
                            if(nxt.mode == CellMode.Active && !shape.needNewDraw || nxt.mode == CellMode.ToFall) {
                                grid[i, j] = nxt;
                                grid[i, j - 1] = getEmptyCell();

                                if(j == BoardH-1) {
                                    nxt.mode = CellMode.set;
                                    Debug.WriteLine("Cell set");
                                } else if(grid[i,j+1].mode == CellMode.set) {
                                    nxt.mode = CellMode.set;
                                    Debug.WriteLine("Cell set");
                                }
                            }

                        }
                    }
                }
                lastCellUpdate = start;
                #endregion

                if(shape.needNewDraw) {
                    Cell[] l = shape.getDrawcells();
                    for(int i = 0; i < l.Length; i++) {
                        grid[l[i].pos.X, l[i].pos.Y] = l[i];
                    }
                }

            }
            base.Update(gameTime);

            if(Keyboard.GetState().IsKeyDown(Keys.D)) {
                Debug.WriteLine($"Update time: {DateTime.Now.Subtract(start).Milliseconds}ms");
            }

        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            Texture2D cellTexture = new Texture2D(graphics.GraphicsDevice, CellW, CellH);

            Color[] data = new Color[CellW*CellH];
            for(int i = 0; i < data.Length; i++) {
                data[i] = Color.White;
            }
            cellTexture.SetData(data);

            for(int i = 0; i < BoardW; i++) {
                for(int j = 0; j < BoardH; j++) {
                    Rectangle rect = new Rectangle(i*CellW,j*CellH,CellW,CellH);
                    spriteBatch.Draw(cellTexture,rect,grid[i,j].color);
                }
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }


    }
}
