using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGame2
{
    class Monsters
    {
        private int cxArrayNumber;     // sets the number that the monster is in the array
        private int cyArrayNumber;
        private int cxGridLocation;    // where the monster is on the game board
        private int cyGridLocation;
        private int chp;
        private int cAttack;
        
        public Monsters(int cxArrayNumber, int cyArrayNumber, int cxGridLocation, int cyGridLocation)
        {
            this.cxArrayNumber = cxArrayNumber;
            this.cyArrayNumber = cyArrayNumber;
            this.cxGridLocation = cxGridLocation;
            this.cyGridLocation = cyGridLocation;
            chp = 4;
            cAttack = 1;
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
        public int xGridLocation
        {
            set { cxGridLocation = value; }
            get { return cxGridLocation; }
        }
        public int yGridLocation
        {
            set { cyGridLocation = value; }
            get { return cyGridLocation; }
        }
        public int hp
        {
            set { chp = value; }
            get { return chp; }
        }
        public int attack
        {
            set { cAttack = value; }
            get { return cAttack; }
        }

        public static Monsters monsterMove(Monsters monster, Player player,Tiles[,] tile, int xOutOfRange, int yOutOfRange)
        {
            if (monster.xGridLocation * 50 > player.xGridLocation + 50)
            {
                if (tile[monster.xGridLocation - 1, monster.yGridLocation].ocupied == false)
                {
                    monster.xGridLocation = monster.xGridLocation - 1; // move left
                }
            }
            if (monster.xGridLocation * 50 < player.xGridLocation + 50)
            {
                if (tile[monster.xGridLocation + 1, monster.yGridLocation].ocupied == false)
                {
                    monster.xGridLocation = monster.xGridLocation + 1; // move right
                }
            }
            if (monster.yGridLocation * 50 > player.yGridLocation + 50)
            {
                if (tile[monster.xGridLocation, monster.yGridLocation - 1].ocupied == false)
                {
                    monster.yGridLocation = monster.yGridLocation - 1;// move up
                }
            }
            if (monster.yGridLocation * 50 < player.yGridLocation - 50)
            {
                if (tile[monster.xGridLocation, monster.yGridLocation + 1].ocupied == false)
                {
                    monster.yGridLocation = monster.yGridLocation + 1;// move down
                }
            }           
            return monster;
        }
        // ++++++++++++++ calculates the lenght of the monsters health bar +++++++++++++++++++++++++++++++ //
        public static int calcMonsterHealthBarLength(int hp)
        {
            // will add code here if I make the slime able to lvl up.
            return hp * 5;
        }  

        public static int calcHp(int hp, int damage)
        {
            return hp - damage;
        }

        public static bool calcRange(Monsters monster, Player player)
        {
            if (monster.xGridLocation -1 <= player.xArrayNumber && player.xArrayNumber <= monster.xGridLocation  +1)
            {
                if (monster.yGridLocation -1 <= player.yArrayNumber && player.yArrayNumber <= monster.yGridLocation + 1)
                {
                    return true;
                }
            }
            return false;
        }
       
    }
}
