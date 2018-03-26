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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Read();
            Tester();
        }
        string[] txt;
        String line;
        public void Read()
        {
              try
            {
                StreamReader sr = new StreamReader("D://Даша/3/2/Жолобов/small-test.in.txt");

                line = sr.ReadToEnd();
                txt = line.Split(' ');

                //while (line != null)
                //{
                //    Console.WriteLine(line);
                //    line = sr.ReadLine();
                //    txt = line.Split(' ');
                //}
                //Console.WriteLine(line);
                //string[] txt = line.Split('\n'); 
                //foreach (string t in txt)
                //{
                //    Console.WriteLine(t);
                //}
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
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
            Console.WriteLine(txt[1]);
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
