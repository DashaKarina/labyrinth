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
            Main();
        }

        public void Main()
        {
            //Отрезать в лабиринтах первый ход вперед
            Read();
            string str = txt[2][0];
            char flag = build_maze(str, 'S');
            string str1 = txt[2][1];
            build_maze(str1, flag);
        }
        string[][] txt;
        public void Read()
        {
              try
            {
                StreamReader sr = new StreamReader("small-test.in.txt");
                string[] line = sr.ReadToEnd().Split(new Char[] {'\n'});
                txt = new string[line.Length][];
                for (int i = 0; i < line.Length; i++)
                {
                    txt[i] = line[i].Split(new Char[] {' '});
                }
                sr.Close();
                //foreach (string[] x in txt)
                //{
                //    foreach (string y in x)
                //        Console.WriteLine(y);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
              
              
        }
 

        public  class Labyrinth // Свойства лабиринта
        {
            private Boolean val_up;
            private Boolean val_down;
            private Boolean val_left;
            private Boolean val_right;

            public Boolean up_wall
            {
                get { return val_up; }
                set { if (value == true) val_up = true; else val_up = false; }
            }
            public Boolean down_wall
            {
                get { return val_down; }
                set { if (value == true) val_down = true; else val_down = false; }
            }
            public Boolean left_wall
            {
                get { return val_left; }
                set { if (value == true) val_left = true; else val_left = false; }
            }
            public Boolean right_wall
            {
                get { return val_right; }
                set { if (value == true) val_right = true; else val_right = false; }
            }
        };

        public char build_maze(string str, char flag)
        {
            //char flag = 'S';
            //Север = N Юг = S Запад = W Восток = E
            
            Labyrinth[] location = new Labyrinth[30];
            for (int i = 0; i < location.Length; i++)
            {
                location[i] = new Labyrinth();
            }
            
            int j = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (i == str.Length) break;
                j++;
                if (flag == 'S')
                {
                    if (str[i] == 'W') //Если мы смотрели на юг и пошли вперед, то слева стенка
                    {
                        location[j].right_wall = true;                   
                    }
                    else if (str[i] == 'L') //Если мы смотрели на юг и повернули налево, то слева пусто и мы смотрим на восток
                    {
                        flag = 'E';
                        location[j].right_wall = false;
                        i++; continue;
                    }
                    else if (str[i] == 'R') //Если мы смотрели на юг и повернули направо, то слева стенка и сверху стенка
                    {
                        flag = 'W';
                        location[j].right_wall = true;
                        location[j].down_wall = true;
                        if (str[i + 1] == 'W') { i++; continue; }
                        else { j--; continue; }
                    }
                }
                if (flag == 'E')
                {
                    if (str[i] == 'W') //Если мы смотрели на восток и пошли вперед, то слева стенка
                    {
                        location[j].up_wall = true;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на восток и повернули налево, то слева пусто и мы смотрим на север
                    {
                        flag = 'N';
                        location[j].up_wall = false;
                        i++; continue;

                    }
                    else if (str[i] == 'R') //Если мы смотрели на восток и повернули направо, то слева стенка и впереди стенка
                    {
                        flag = 'S';
                        location[j].up_wall = true;
                        location[j].right_wall = true;
                        if (str[i + 1] == 'W') { i++; continue; }
                        else { j--; continue; }
                    }
                }
                if (flag == 'W')
                {
                    if (str[i] == 'W') //Если мы смотрели на запад и пошли вперед, то слева стенка
                    {
                        location[j].down_wall = true;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на запад и повернули налево, то слева пусто и мы смотрим на юг
                    {
                        flag = 'S';
                        location[j].down_wall = false;
                        i++; continue;
                    }
                    else if (str[i] == 'R') //Если мы смотрели на запад и повернули направо, то слева стенка и впереди стенка
                    {
                        flag = 'N';
                        location[j].down_wall = true;
                        location[j].left_wall = true;

                        if (str[i + 1] == 'W') { i++; continue; }
                        else { j--; continue; }
                    }
                }
                if (flag == 'N')
                {
                    if (str[i] == 'W') //Если мы смотрели на север и пошли вперед, то слева стенка
                    {
                        location[j].left_wall = true;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на север и повернули налево, то слева пусто и мы смотрим на запад
                    {
                        flag = 'W';
                        location[j].left_wall = false;
                        i++; continue;
                    }
                    else if (str[i] == 'R') //Если мы смотрели на север и повернули направо, то слева стенка и впереди стенка и мы смотрим на восток
                    {
                        flag = 'E';
                        location[j].up_wall = true;
                        location[j].left_wall = true;

                        if (str[i + 1] == 'W') { i++; continue; }
                        else { j--; continue; }
                    }
                }
            }
            for (int i = 0; i < location.Length; i++)
            {
                Console.WriteLine(i + 1 + "\n dw = " + location[i].down_wall + "\n lw = " + location[i].left_wall + "\n rw = " + location[i].right_wall + "\n uw = " + location[i].up_wall);
            }
            if (flag == 'N') flag = 'S';
            else if (flag == 'E') flag = 'W';
            else if (flag == 'S') flag = 'N';
            else flag = 'E';
            return flag;
        }

    }
}
