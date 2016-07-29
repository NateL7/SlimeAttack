using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace VideoGame2
{
    public partial class Form1 : Form
    {
        // ++++++++++++++++++++++++++++ Global Variables +++++++++++++++++++++++++++ //             
        bool task1Running = false;     // controls the different Taskes.  makes sure they do not get called if they
        bool task2Running = false;     // are already running.
        static int gameBoardWidth = 12;     // controls the number of tiles that run the width of the game board (each tile is 50pxl)
        static int gameBoardHeight = 10;    // controls the number of ties that run the height of the game board
        Tiles[,] tile = new Tiles[gameBoardWidth, gameBoardHeight];
        static int monsterCountWidth = 3;
        static int monsterCountHeight = 3;
        Monsters[,] monsters = new Monsters[monsterCountWidth, monsterCountHeight];
        Player player = new Player(4, 4, 200, 200);
        static int timer = 0;                      // Ginaric timer for the overall game timer
        bool loadGame = false;                     // Controls things that should only happen on origninal game load.
        bool playerWantsToMove = false;            // this variable keeps the player from moveing to fast
        //+++++++++++++++++++++++++ End of Global Variables ++++++++++++++++++++++++ //

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            drawPlayerHps();
            buildGameTiles();
            drawGameTiles();
            buildMonsters();
            drawPlayerPicOne();
            loadGame = true;
        }
        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            drawPlayerHps();
            buildGameTiles();
            drawGameTiles();
            buildMonsters();
            drawPlayerPicOne();
            loadGame = true;         
        }

        private void drawPlayerHps()
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush black = new SolidBrush(Color.Black);
            SolidBrush red = new SolidBrush(Color.Red);
            g.FillRectangle(black, 400, 20, 80, 20);
            g.FillRectangle(red, 400, 20, player.hp * 8, 20);

        }
        private void drawPlayerPicOne()
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            Bitmap playerAvitar = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/WisardAttack.png");
            g.DrawImage(playerAvitar, player.xGridLocation, player.yGridLocation);
        }
        private void drawPlayerPicTwo()
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            Bitmap playerAvitar = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/WisardAttack1.png");
            g.DrawImage(playerAvitar, player.xGridLocation, player.yGridLocation);
        }
        private void drawMonsters(int picChange)
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush blue = new SolidBrush(Color.Blue);
            SolidBrush gray = new SolidBrush(Color.Gray);
            Bitmap greenSlime1 = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/GreenSlime_1.png");
            Bitmap greenSlime2 = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/GreenSlime_2.png");
            for (int x = 0; x < monsterCountWidth; x++)
            {
                for (int y = 0; y < monsterCountHeight; y++)
                {
                    if (picChange == 1) g.DrawImage(greenSlime1, monsters[x, y].xGridLocation * 50, monsters[x, y].yGridLocation * 50, 40, 40);
                    if (picChange == 2) g.DrawImage(greenSlime2, monsters[x, y].xGridLocation * 50, monsters[x, y].yGridLocation * 50, 40, 40);
                    g.FillRectangle(gray, monsters[x, y].xGridLocation * 50 + 10, monsters[x, y].yGridLocation * 50 - 4, 20, 2);
                    g.FillRectangle(blue, monsters[x, y].xGridLocation * 50 + 10, monsters[x, y].yGridLocation * 50 - 4, Monsters.calcMonsterHealthBarLength(monsters[x,y].hp) , 2);         // slime health bars
                    tile[monsters[x, y].xGridLocation, monsters[x, y].yGridLocation].ocupied = true;
                }
            }

        }
        private void drawGameTiles()
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            for (int x = 0; x < gameBoardWidth; x++)
            {
                for (int y = 0; y < gameBoardHeight; y++)
                {
                    g.FillRectangle(gray, tile[x, y].x * 50, tile[x, y].y * 50, 50, 50);
                }
            }
        }
        private void drawExplotion(float xLocation, float yLocation)
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            Bitmap explode1 = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/explode1.png");
            Bitmap explode2 = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/explode2.png");
            int counter = 0;
            while (counter < 5)
            {
                g.DrawImage(explode1, xLocation, yLocation, 30, 30);
                Thread.Sleep(100);
                g.DrawImage(explode2, xLocation, yLocation, 30, 30);
                Thread.Sleep(100);
                counter++;
            }
            g.FillRectangle(gray, xLocation, yLocation, 30, 30);
        }

        private void buildGameTiles()
        {
            for (int i = 0; i < gameBoardWidth; i++)
            {
                for (int j = 0; j < gameBoardHeight; j++)
                {
                    tile[i, j] = new Tiles(i, j, false);
                }
            }
        }
        private void buildMonsters()
        {          
            Random rand = new Random();                           
            for (int i = 0; i < monsterCountWidth; i++)
            {
                for (int j = 0; j < monsterCountHeight; j++)
                {
                    int rnx = rand.Next(0, 7);
                    int rny = rand.Next(0, 7);
                    monsters[i, j] = new Monsters(i, j, rnx, rny);           
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Bitmap playerAvitar = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/WisardAttack.png");
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            if (e.KeyCode == Keys.S && player.yArrayNumber + 1 < gameBoardHeight)          // moves down
            {
                if (movePlayer(player.xArrayNumber, player.yArrayNumber, "down") == true && playerWantsToMove == true)        // calls a function moveplayer to see if the tile i ocupied
                {
                    g.FillRectangle(gray, player.xGridLocation, player.yGridLocation, 50, 50);
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = false;
                    player.yArrayNumber = player.yArrayNumber + 1;
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = true;
                    player.yGridLocation = player.yGridLocation + 50;
                    g.DrawImage(playerAvitar, player.xGridLocation, player.yGridLocation);
                    playerWantsToMove = false;
                } 
            }
            else if (e.KeyCode == Keys.W && player.yArrayNumber > 0)   // moves up
            {
                if (movePlayer(player.xArrayNumber, player.yArrayNumber, "up") == true && playerWantsToMove == true)
                {
                    g.FillRectangle(gray, player.xGridLocation, player.yGridLocation, 50, 50);
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = false;
                    player.yArrayNumber = player.yArrayNumber - 1;
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = true;
                    player.yGridLocation = player.yGridLocation - 50;
                    g.DrawImage(playerAvitar, player.xGridLocation, player.yGridLocation);
                    playerWantsToMove = false;
                }
            }
            else if (e.KeyCode == Keys.A && player.xArrayNumber > 0)   // moves Left
            {
                if (movePlayer(player.xArrayNumber, player.yArrayNumber, "left") == true && playerWantsToMove == true)
                {
                    g.FillRectangle(gray, player.xGridLocation, player.yGridLocation, 50, 50);
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = false;
                    player.xArrayNumber = player.xArrayNumber - 1;
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = true;
                    player.xGridLocation = player.xGridLocation - 50;
                    g.DrawImage(playerAvitar, player.xGridLocation, player.yGridLocation);
                    playerWantsToMove = false;
                }
            }
            else if (e.KeyCode == Keys.D && player.xArrayNumber + 1 < gameBoardWidth)   // moves right
            {
                if (movePlayer(player.xArrayNumber, player.yArrayNumber, "right") == true && playerWantsToMove == true)
                {
                    g.FillRectangle(gray, player.xGridLocation, player.yGridLocation, 50, 50);
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = false;
                    player.xArrayNumber = player.xArrayNumber + 1;
                    tile[player.xArrayNumber, player.yArrayNumber].ocupied = true;
                    player.xGridLocation = player.xGridLocation + 50;
                    g.DrawImage(playerAvitar, player.xGridLocation, player.yGridLocation);
                    playerWantsToMove = false;
                }
            }           
        }

        private bool movePlayer(int x, int y, string direction)
        {
            if (direction == "down")
            {
                if (tile[x, y + 1].ocupied == true)
                {
                    return false;
                }
            }
            else if (direction == "up")
            {
                if (tile[x, y - 1].ocupied == true)
                {
                    return false;
                }
            }
            else if (direction == "left")
            {
                if (tile[x - 1, y].ocupied == true)
                {
                    return false;
                }
            }
            else if (direction == "right")
            {
                if (tile[x + 1, y].ocupied == true)
                {
                    return false;
                }
            }
            return true;
        }

        private void pnlGameBoard_Click(object sender, EventArgs e)
        {
            Bitmap cWisard = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/WisardAttack.png");
            Bitmap cFireBall = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/FireBall.png");
            Graphics g = pnlGameBoard.CreateGraphics();           
            Point mouseLocation;
            mouseLocation = PointToClient(Cursor.Position);
            float xMouse = mouseLocation.X - 10;
            float yMouse = mouseLocation.Y - 10;
            Task t1 = new Task(() => ShootFireBall1(player.xGridLocation, player.yGridLocation, xMouse, yMouse));
            Task t2 = new Task(() => ShootFileBaal2(player.xGridLocation, player.yGridLocation, xMouse, yMouse));
            if (task1Running == false)
            {
                task1Running = true;
                t1.Start();
            }
            else if (task2Running == false)
            {
                task2Running = true;
                t2.Start();
            }            
        }
        public void ShootFireBall1(float xPlayerLocation, float yplayerLocation, float xMouse, float yMouse)
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            Bitmap FireBall = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/FireBall.png");
            float xMoveDistance = xMouse - xPlayerLocation;
            float yMoveDistance = yMouse - yplayerLocation;
            float xMoveIncriments = 1;
            float yMoveIncriments = 1;
            if (xMoveDistance > 0 && yMoveDistance > 0)
            {
                xMoveIncriments = 1;
                yMoveIncriments = yMoveDistance / xMoveDistance;
            }
            else if (xMoveDistance < 0 && yMoveDistance > 0)
            {
                xMoveIncriments = -1;
                yMoveIncriments = yMoveDistance / -xMoveDistance;
            }
            else if (xMoveDistance > 0 && yMoveDistance < 0)
            {
                xMoveIncriments = 1;
                yMoveIncriments = yMoveDistance / xMoveDistance;
            }
            else if (xMoveDistance < 0 && yMoveDistance < 0)
            {
                xMoveIncriments = -1;
                yMoveIncriments = yMoveDistance / -xMoveDistance;
            }
            while (yMoveIncriments > 1)
            {
                yMoveIncriments = yMoveIncriments / 2;
                xMoveIncriments = xMoveIncriments / 2;
            }
            while (yMoveIncriments < -1)
            {
                yMoveIncriments = yMoveIncriments / 2;
                xMoveIncriments = xMoveIncriments / 2;
            }
            float xTotalMoved = 0;
            float yTotalMoved = 0;          
            int y = 0;
            while (y < 200)
            {
                xTotalMoved = xTotalMoved + xMoveIncriments;
                yTotalMoved = yTotalMoved + yMoveIncriments;
                g.DrawImage(FireBall, xPlayerLocation + xTotalMoved, yplayerLocation +yTotalMoved , 12, 12);
                Thread.Sleep(10);
                g.FillRectangle(gray, xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved, 12, 12);
                bool colistion = CheckFireBaalColishion(xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved);
                if (colistion == true)
                {
                    drawExplotion(xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved);
                    break;
                }
                y++;
            }
            task1Running = false;
        }
        public void ShootFileBaal2(float xPlayerLocation, float yplayerLocation, float xMouse, float yMouse)
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            Bitmap FireBall = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/FireBall.png");
            float xMoveDistance = xMouse - xPlayerLocation;
            float yMoveDistance = yMouse - yplayerLocation;
            float xMoveIncriments = 1;
            float yMoveIncriments = 1;
            if (xMoveDistance > 0 && yMoveDistance > 0)
            {
                xMoveIncriments = 1;
                yMoveIncriments = yMoveDistance / xMoveDistance;
            }
            else if (xMoveDistance < 0 && yMoveDistance > 0)
            {
                xMoveIncriments = -1;
                yMoveIncriments = yMoveDistance / -xMoveDistance;
            }
            else if (xMoveDistance > 0 && yMoveDistance < 0)
            {
                xMoveIncriments = 1;
                yMoveIncriments = yMoveDistance / xMoveDistance;
            }
            else if (xMoveDistance < 0 && yMoveDistance < 0)
            {
                xMoveIncriments = -1;
                yMoveIncriments = yMoveDistance / -xMoveDistance;
            }
            while (yMoveIncriments > 1)
            {
                yMoveIncriments = yMoveIncriments / 2;
                xMoveIncriments = xMoveIncriments / 2;
            }
            while (yMoveIncriments < -1)
            {
                yMoveIncriments = yMoveIncriments / 2;
                xMoveIncriments = xMoveIncriments / 2;
            }
            float xTotalMoved = 0;
            float yTotalMoved = 0;
            int y = 0;
            while (y < 200)
            {
                xTotalMoved = xTotalMoved + xMoveIncriments;
                yTotalMoved = yTotalMoved + yMoveIncriments;
                g.DrawImage(FireBall, xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved, 12, 12);
                Thread.Sleep(10);
                g.FillRectangle(gray, xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved, 12, 12);
                bool colistion = CheckFireBaalColishion(xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved);
                if (colistion == true)
                {
                    drawExplotion(xPlayerLocation + xTotalMoved, yplayerLocation + yTotalMoved);
                    break;
                }
                y++;
            }
            task2Running = false;
        }

        private bool CheckFireBaalColishion(float xFireBallLocation, float yFireBallLocation)
        {
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            for (int i = 0; i < monsterCountWidth; i++)
            {
                for (int j = 0; j < monsterCountHeight; j++)
                {
                    if (monsters[i, j].xGridLocation * 50 < xFireBallLocation && xFireBallLocation < monsters[i, j].xGridLocation * 50 + 30 &&
                        monsters[i, j].yGridLocation  *50 < yFireBallLocation && yFireBallLocation < monsters[i, j].yGridLocation * 50 + 30)
                    {
                        monsters[i,j].hp = Monsters.calcHp(monsters[i, j].hp, player.attack);
                        if (monsters[i, j].hp < 1)
                        {
                            g.FillRectangle(gray, monsters[i, j].xGridLocation * 50, monsters[i, j].yGridLocation * 50, 50, 50);
                            tile[monsters[i, j].xGridLocation, monsters[i, j].yGridLocation].ocupied = false;
                            monsters[i, j].xGridLocation = 11;                          
                        }
                        return true;
                    }
                }
            }
            return false;
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap greenSlime1 = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/GreenSlime_1.png");
            Graphics g = pnlGameBoard.CreateGraphics();
            SolidBrush gray = new SolidBrush(Color.Gray);
            Random rand = new Random();
            timer++;
            if (timer % 2 == 0)
            {
                drawPlayerPicOne();
                if (loadGame == true) drawMonsters(1);
                playerWantsToMove = true;
                drawPlayerHps();
            }
            if (timer % 2 == 1)
            {
                drawPlayerPicTwo();
                if (loadGame == true) drawMonsters(2);
            }
            if (timer % 1 == 0)
            {
                monsterJail(); // the MonsterJail is were all the deal slimes go.  It is just a wall of tiles that the tile.ocupied value is set to true.  All slimes that are put on the right side of it can not get back to the battle field.                 
                int x = rand.Next(0, monsterCountWidth);
                int y = rand.Next(0, monsterCountHeight);
                g.FillRectangle(gray, monsters[x, y].xGridLocation * 50, monsters[x, y].yGridLocation * 50 - 4, 50, 54);
                tile[monsters[x, y].xGridLocation, monsters[x, y].yGridLocation].ocupied = false;
                monsters[x,y] = Monsters.monsterMove(monsters[x, y], player, tile, gameBoardWidth, gameBoardHeight);
                g.DrawImage(greenSlime1, monsters[x, y].xGridLocation * 50, monsters[x, y].yGridLocation * 50, 40, 40);
            }
            if (timer % 10 == 0)
            {
                monsterAttack();
            }
           
            
        }

        private void monsterJail()
        {

            for (int y = 0; y < gameBoardHeight; y++)
            {
                tile[10, y].ocupied = true;
            }

        }

        private void monsterAttack()
        {
            bool attackPosible;
            for (int x = 0; x < monsterCountWidth; x++)
            {
                for (int y = 0; y < monsterCountHeight; y++)
                {
                    attackPosible = Monsters.calcRange(monsters[x, y], player);
                    if (attackPosible)
                    {
                        Graphics g = pnlGameBoard.CreateGraphics();
                        Bitmap slimeAttack = new Bitmap("C:/Users/Nate/Desktop/VideoGame2/Resources/GreenSlimeAttacking_21.png");
                        g.DrawImage(slimeAttack, monsters[x, y].xGridLocation * 50, monsters[x, y].yGridLocation * 50, 40, 40);
                        player.hp = player.hp - monsters[x, y].attack;
                        drawPlayerHps();
                        if(player.hp <= 0)
                        {
                            MessageBox.Show("Game Over. click OK to start a new game");
                            player.hp = 10;
                            Form1_Load(this, null);
                        }
                    }
                }
            }
        } 
    }
}
