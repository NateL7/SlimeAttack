using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGame2
{
    class Tiles
    {
        private float cX;
        private float cY;
        private bool cOcupied;

        public Tiles(int cX, int cY, bool cOcupied)
        {
            this.cX = cX;
            this.cY = cY;
            this.cOcupied = cOcupied;
        }

        public float x
        {
            set { cX = value; }
            get { return cX; }
        }
        public float y
        {
            set { cY = value; }
            get { return cY; }
        }
        public bool ocupied
        {
            set { cOcupied = value; }
            get { return cOcupied; }
        }
    }
}
