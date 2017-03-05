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

namespace Connect4
{
    public partial class Leaderboard : Form
    {
        //List of the different names
        List<string> names;

        //List of all the scores (corresponding indexes)
        List<int> scores;

        //Initialise the leaderboard form
        public Leaderboard()
        {
            InitializeComponent();
            player1.Font = new Font("Canadra", 12);
            player2.Font = new Font("Canadra", 12);
            player3.Font = new Font("Canadra", 12);
            names = new List<string>();
            scores = new List<int>();
            readContents();
            showContents();
        }

        //read the contents of the file
        void readContents()
        {
            StreamReader highscoreStream;

            //Create a streamwriter
            if (File.Exists("highscore.txt"))
            {
                highscoreStream = new StreamReader("highscore.txt");
            }
            else
            {
                return;
            }

            //Loops through each line of the file
            while(highscoreStream.EndOfStream == false)
            {
                string currentLine = highscoreStream.ReadLine();

                //if list is empty add it to the start
                if (names.Count() == 0)
                {
                    names.Insert(0, currentLine);
                    scores.Insert(0, 1);
                }
                else
                {
                    bool added = false;
                    for (int i = 0; i < names.Count(); i++)
                    {
                        if (names[i] == currentLine)
                        {
                            scores[i]++;
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                    {
                        names.Insert(names.Count(), currentLine);
                        scores.Insert(scores.Count(), 1);
                        added=true;
                    }
                }
            }
            highscoreStream.Close();
        }

        //Updates the scoreboard
        void showContents()
        {
            //Order the lists
            sortScores();

            //Display the scores
            for (int i = 0; i < scores.Count(); i++)
            {
                if (i == 3)
                {
                    break;
                }
                if (i == 0)
                {
                    player1.Text = names[i];
                    score1.Text = Convert.ToString(scores[i]);
                }
                else if (i == 1)
                {
                    player2.Text = names[i];
                    score2.Text = Convert.ToString(scores[i]);
                }
                else if (i == 2)
                {
                    player3.Text = names[i];
                    score3.Text = Convert.ToString(scores[i]);
                }
            }
        }

        //Sorts the scores
        void sortScores()
        {
            //doesnt need sorted
            if (scores.Count() == 0 || scores.Count()==1)
            {
                return;
            }
            //needs sorted
            else
            {
                //Insert sort (sorts names too)
                for (int i = 0; i < scores.Count() - 1; i++)
                {
                    int j = i + 1;
                    int tmp = scores[j];
                    string tmpStr = names[j];
                    while (j > 0 && tmp > scores[j - 1])
                    {
                        scores[j] = scores[j - 1];
                        names[j] = names[j - 1];
                        j--;
                    }
                    scores[j] = tmp;
                    names[j] = tmpStr;
                }
            }
        }
    }
}
