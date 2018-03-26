using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public  class Labyrinth // Свойства лабиринта
        {
            private Boolean val_wall;
            public Boolean up_wall
            {
                get { return val_wall; }
                set {if (value == true) val_wall = true;  else val_wall = false;}
            }
            public Boolean down_wall
            {
                get { return val_wall; }
                set { if (value == true) val_wall = true; else val_wall = false; }
            }
            public Boolean left_wall
            {
                get { return val_wall; }
                set { if (value == true) val_wall = true; else val_wall = false; }
            }
            public Boolean right_wall
            {
                get { return val_wall; }
                set { if (value == true) val_wall = true; else val_wall = false; }
            }
        };

        public void Tester()
        {
            Labyrinth test = new Labyrinth();
            test.up_wall = true;
        }

        public void build_maze()
        {
            Labyrinth[] location = new Labyrinth[15];
            string str = "WRWWLWWLWWLWLWRRWRWWWRWWRWLW";
            str.ToCharArray();
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == 'W')
                {
                    location[i - 1].down_wall = false; 
                }
                else if (str[i] == 'L')
                {
                    location[i - 1].down_wall = true;
                    if (str[i+1] == 'W')
                    {
                        location[i - 1].down_wall = false;
                    }
                    else
                    {
                        location[i - 1].left_wall = true;
                    }
                }
                else if (str[i] == 'R')
                {
                    location[i - 1].down_wall = true;
                    if (str[i + 1] == 'W')
                    {
                        location[i - 1].down_wall = false;
                    }
                    else
                    {
                        location[i - 1].left_wall = true;
                        location[i - 1].right_wall = true;
                    }
                }
            }
        }

    }
}
