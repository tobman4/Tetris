using System;
//using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;

using Microsoft.Xna.Framework;


namespace Tetris.Shapes {
    [Obsolete]
    public class Shape {

        public bool needNewDraw = true;
        public bool hasNewChange = false;

        private Point pos = new Point(5,5);
        private Point change = new Point(0,0);


        private int shapeIndex = 0;

        private int rot = 0;
        private int rotChange = 0;

        //private Cell[,] cells;

        private Color cellColor;

        public Shape() {
            cellColor = Color.Pink;
        }

        [Obsolete]
        private Cell getCell() => new Cell(CellMode.Active);

        public void Update(GameTime gameTime) {
            pos.Y++;
        }

        public void move(int x,int y = 0) {
            change.X += x;
            change.Y += y;
            hasNewChange = true;
        }

        public void approveChange(bool isValid) {
            if(isValid) {
                pos.X += change.X;
                pos.Y += change.Y;
                rot = rotChange;
                needNewDraw = true;
            }
            hasNewChange = false;
            change = new Point(0,0);
            rotChange = rot;
        }

        public void Rotate(bool back = false) {
            rotChange++;
            if(rotChange >= 4) {
                rotChange = 0;
            } else if(rot < 0) {
                rotChange = 3;
            }
            Debug.WriteLine($"Rot set to {rotChange}");
            hasNewChange = true;
        }

        public Cell[] getNewDrawcells() {
            List<Cell> o = new List<Cell>();

            for(int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    bool ToDraw = ShapePreMaed.Shape[shapeIndex, rot, i, j];
                    if(ToDraw) {
                        Cell c = new Cell(CellMode.Active);
                        c.color = cellColor;
                        c.pos = new Point(
                            pos.X + i + change.X,
                            pos.Y + j + change.Y
                        );
                        o.Add(c);
                    }
                }
            }
            return o.ToArray();
        }

        public Cell[] getDrawcells() {

            needNewDraw = false;

            List<Cell> o = new List<Cell>();

            for(int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    bool ToDraw = ShapePreMaed.Shape[shapeIndex, rot, i, j];
                    if(ToDraw) {
                        Cell c = new Cell(CellMode.Active);
                        c.color = cellColor;
                        c.pos = new Point(pos.X+i,pos.Y+j);
                        o.Add(c);
                    }
                }
            }
            return o.ToArray();
        }

    }
}
