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

    }
}
