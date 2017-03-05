using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Connect4
{
    public partial class Form1 : Form
    {
        //Picture Box array
        PictureBox[,] grid = new PictureBox[6,7];

        //Array of strings to store the values
        String[,] gridValue = new String[6, 7];

        //Array of column buttons
        Button[] columnChoice = new Button[7];

        //Keeps track of whose turn it is
        string turn;

        //Keeps track of how many turns there has been to make catch a tie game
        int turnCount;

        //Stores player names
        string name1;
        string name2;

        //Initialises the grid of picture boxes
        void initialiseGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                for(int y = 0; y<7; y++)
                {
                    //Set options the grid
                    grid[i, y] = new PictureBox();
                    grid[i, y].SetBounds((51*y)+12,(51*i)+78,45,45);
                    grid[i, y].BackColor = Color.FromArgb(255, 255, 255);
                    grid[i, y].SizeMode = PictureBoxSizeMode.StretchImage;
                    gridValue[i, y] = "None";
                    Controls.Add(grid[i, y]);

                    //Make sure each grid square has the correct click handler
                    switch (y)
                    {
                        case 0:
                            grid[i, y].Click += new EventHandler(this.clickCol1);
                            break;
                        case 1:
                            grid[i, y].Click += new EventHandler(this.clickCol2);
                            break;
                        case 2:
                            grid[i, y].Click += new EventHandler(this.clickCol3);
                            break;
                        case 3:
                            grid[i, y].Click += new EventHandler(this.clickCol4);
                            break;
                        case 4:
                            grid[i, y].Click += new EventHandler(this.clickCol5);
                            break;
                        case 5:
                            grid[i, y].Click += new EventHandler(this.clickCol6);
                            break;
                        case 6:
                            grid[i, y].Click += new EventHandler(this.clickCol7);
                            break;
                    }

                }
            } 
        }

        //Set player 1s name
        public void setName1(string name)
        {
            name1 = name;
            label2.Text = name;
        }

        //Set player 2s name
        public void setName2(string name)
        {
            name2 = name;
        }

        //grid click event handler for column1
        void clickCol1(object sender, EventArgs e)
        {
            dropDisc(0);
        }

        //grid click event handler for column2
        void clickCol2(object sender, EventArgs e)
        {
            dropDisc(1);
        }

        //grid click event handler for column3
        void clickCol3(object sender, EventArgs e)
        {
            dropDisc(2);
        }

        //grid click event handler for column4
        void clickCol4(object sender, EventArgs e)
        {
            dropDisc(3);
        }

        //grid click event handler for column5
        void clickCol5(object sender, EventArgs e)
        {
            dropDisc(4);
        }

        //grid click event handler for column6
        void clickCol6(object sender, EventArgs e)
        {
            dropDisc(5);
        }

        //grid click event handler for column7
        void clickCol7(object sender, EventArgs e)
        {
            dropDisc(6);
        }

        //Initialises the data needed to run the game
        void initialiseGameData()
        {
            turn = "Yellow";
            label2.Text = name1;
            label2.ForeColor = Color.FromArgb(255, 200, 0);
            turnCount = 0;
        }

        //Initialises the row of buttons
        void initialiseButtons()
        {
            for (int i = 0; i < 7; i++)
            { 
                columnChoice[i] = new Button();
                columnChoice[i].SetBounds((51*i)+12,27,45,45);
                columnChoice[i].Text = Convert.ToString(i+1);
                columnChoice[i].Click += new EventHandler(this.columnClick);
                Controls.Add(columnChoice[i]);
            }
        }

        //Runs on startup of program to initialise the form
        public Form1()
        {
            InitializeComponent();
            name1 = "Yellow";
            name2 = "Red";
            initialiseGrid();
            initialiseButtons();
            initialiseGameData();
        }
        
        //Runs when a column is picked
        void columnClick(object sender, EventArgs e)
        {
            dropDisc(int.Parse(((Button)sender).Text)-1);
        }

        //Switches whos turn it is
        void switchTurn()
        {
            if (turn == "Red")
            {
                turn = "Yellow";
                label2.Text = name1;
                label2.ForeColor = Color.FromArgb(255,200,0);
            }
            else
            {
                turn = "Red";
                label2.Text = name2;
                label2.ForeColor = Color.FromArgb(255,0,0);
            }
            turnCount++;

            //Runs if the game is a tie
            if (turnCount == 42)
            {
                gameEnd();
            }
        }

        //Runs if no one wins the game
        void gameEnd()
        {
            DialogResult result;
            result = MessageBox.Show("No one managed to connect 4 so the game ends as a TIE!", "Tie Game!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            newGame();
        }

        //Checks to see if the given grid spaces completes a 4
        //Returns true if game has been one
        bool checkWin(int row, int column)
        {
            //HORIZONTAL
            //set counter to 1 (the piece itself)
            int connectedCounter = 1;
            
            //Loop left and count
            int tempColumn = column-1;
            while(tempColumn>=0 && gridValue[row,tempColumn]==turn){
                connectedCounter++;
                tempColumn--;
            }

            //Loop right and count
            tempColumn = column + 1;
            while(tempColumn<=6 && gridValue[row, tempColumn]==turn){
                connectedCounter++;
                tempColumn++;
            }

            //Check if there is a win
            if(connectedCounter>=4){
                gameWon();
                return true;
            }

            //VERTICAL
            //set counter to 1(the piece itself)
            connectedCounter = 1;

            //Loop up and count
            int tempRow = row - 1;
            while (tempRow >= 0 && gridValue[tempRow, column] == turn)
            {
                connectedCounter++;
                tempRow--;
            }

            //Loop down and count
            tempRow = row + 1;
            while (tempRow <= 5 && gridValue[tempRow, column] == turn)
            {
                connectedCounter++;
                tempRow++;
            }

            //Check if there is a win
            if (connectedCounter >= 4)
            {
                gameWon();
                return true;
            }

            //+ve GRADIENT DIAGONAL
            //set counter to 1(the piece itself)
            connectedCounter = 1;

            //Loop down and left
            tempRow = row + 1;
            tempColumn = column - 1;
            while (tempRow <= 5 && tempColumn >= 0 && gridValue[tempRow, tempColumn] == turn)
            {
                connectedCounter++;
                tempRow++;
                tempColumn--;
            }

            //Loop up and right
            tempRow = row - 1;
            tempColumn = column + 1;
            while(tempRow>=0 && tempColumn <= 6 && gridValue[tempRow,tempColumn] == turn)
            {
                connectedCounter++;
                tempRow--;
                tempColumn++;
            }

            //Check if there is a win
            if (connectedCounter >= 4)
            {
                gameWon();
                return true;
            }

            //-ve GRADIENT DIAGONAL
            //set counter to 1(the piece itself)
            connectedCounter = 1;

            //Loop up and left
            tempRow = row - 1;
            tempColumn = column - 1;
            while (tempRow >= 0 && tempColumn >= 0 && gridValue[tempRow, tempColumn] == turn)
            {
                connectedCounter++;
                tempRow--;
                tempColumn--;
            }

            //Loop down and right
            tempRow = row + 1;
            tempColumn = column + 1;
            while (tempRow <= 5 && tempColumn <= 6 && gridValue[tempRow, tempColumn] == turn)
            {
                connectedCounter++;
                tempRow++;
                tempColumn++;
            }

            //Check if there is a win
            if (connectedCounter >= 4)
            {
                gameWon();
                return true;
            }

            return false;
        }

        //Game Won by current player
        void gameWon()
        {
            //Displays a dialog box with the result
            DialogResult result;
            string winningName = "";
            if (turn == "Yellow")
            {
                winningName = name1;
            }
            else
            {
                winningName = name2;
            }
            result = MessageBox.Show((winningName + " has won the game! CONGRATULATIONS!"), "Game Won!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //Save the win to the highscore
            saveWin(winningName);

            //resets the game
            newGame();
        }

        //Saves who won the game to the leaderboard
        void saveWin(string winningName)
        {
            //Create the file if it doesnt exist
            StreamWriter writer = new StreamWriter("highscore.txt", true);

            //Write the name of the winner to the file
            writer.WriteLine(winningName);
            writer.Dispose();
        }

        //Flips the gridValue and the Image
        //Returns true if the game has been one
        bool flipTile(int row, int column)
        {
            gridValue[row, column] = turn;
            if(turn=="Yellow")
            {
                grid[row, column].Image = Properties.Resources.Yellow;
            }
            else
            {
                grid[row, column].Image = Properties.Resources.Red;
            }
            return checkWin(row, column);
        }

        //Drops a disc of the current color in the colum passed in
        //column should be 0 indexed
        void dropDisc(int column)
        {
            //Play drop noise
            Stream str = Properties.Resources._339361__newagesoup__drop01;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();

            //Loop down grid
            bool traversal = true;
            bool validPlay = false;
            int row = 0;
            while (traversal)
            {
                if (row == 5)
                { 
                    traversal = false;
                    validPlay = true;
                }
                else if(gridValue[row+1,column]!="None")
                {
                    if (gridValue[row,column]=="None")
                    {
                        validPlay = true;
                    }
                    traversal = false;
                }
                else
                {
                    row++;
                }
            }
            if (validPlay == true)
            {
                if (!flipTile(row, column))
                {
                    switchTurn();
                }
            }
        }

        //Starts a new game
        void newGame()
        {
        for (int i = 0; i < 6; i++)
            {
                for (int y = 0; y < 7; y++)
                {
                    grid[i, y].Image = null;
                    gridValue[i, y] = "None";
                }
            }
            initialiseGameData();
        }

        //New game menu strip
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        //About menu strip
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Connect4 in C# by Gavin Henderson (c) 2017", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //new game strip
        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGame();
        }

        //Highscores menu strip
        private void highscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Leaderboard leaderboard = new Leaderboard();
            leaderboard.Show();
        }

        //Help menu strip
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 helpForm = new Form2();
            helpForm.Show();
        }

        //exit menu strip
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
