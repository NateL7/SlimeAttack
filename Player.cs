using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGame2
{
    class Player
    {
        private int cxArrayNumber;     // sets the number that the monster is in the array
        private int cyArrayNumber;
        private float cxGridLocation;    // where the monster is on the game board
        private float cyGridLocation;
        private int cAttack;
        private int chp;

        public Player(int cxArrayNumber, int cyArrayNumber, float cxGridLocation, float cyGridLocation)
        {
            this.cxArrayNumber = cxArrayNumber;
            this.cyArrayNumber = cyArrayNumber;
            this.cxGridLocation = cxGridLocation;
            this.cyGridLocation = cyGridLocation;
            cAttack = 1;
            chp = 10;
        }

        public int xArrayNumber
        {
            set { cxArrayNumber = value; }
            get { return cxArrayNumber; }
        }
        public int yArrayNumber
        {
            set { cyArrayNumber = value; }
            get { return cyArrayNumber; }
        }
        public float xGridLocation
        {
            set { cxGridLocation = value; }
            get { return cxGridLocation; }
        }
        public float yGridLocation
        {
            set { cyGridLocation = value; }
            get { return cyGridLocation; }
        }
        public int attack
        {
            set { cAttack = value; }
            get { return cAttack; }
        }
        public int hp
        {
            set { chp = value; }
            get { return chp; }
        }
    }
}

