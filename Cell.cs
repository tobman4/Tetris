using System;

using Microsoft.Xna.Framework;

namespace Tetris {

    public enum CellMode {
        Active,
        set,
        ToDelete,
        ToFall,
        Empty,
        
    }

    public struct Cell {

        public Point pos;

        public CellMode mode;
        public Color color;

        public int id; //TODO: fix

        public Cell(CellMode m) {
            color = Color.Black;
            mode = m;
            pos = new Point(-1,-1);
            Random r = new Random();
            id = r.Next();
        }

        public Cell(CellMode m, Point p) {
            color = Color.Black;
            mode = m;
            pos = p;
            Random r = new Random();
            id = r.Next();
        }
    }
}
