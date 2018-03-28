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
            string str = "W";
            string str2 = "W";
            build_maze(str, str2); //Передаем два прохода в метод создания лабиринта
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

        public void build_maze(string str, string str2)
        {
            int size = 1 + 2 * str.Count(z => z.Equals('W')); // Размер лабиринта (надо уточнить какой проход использовать)
            Labyrinth[,] location = new Labyrinth[size, size];
            for (int i = 0; i < Math.Sqrt(location.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(location.Length); j++)
                {
                    location[i, j] = new Labyrinth();
                }
            }
            object[] maze = { 'S', size / 2, 0 }; // Начальные значения 
            maze = go_maze(str, location, maze); //Прямой проход
            go_maze(str2, location, maze); //Обратный проход
        }

        public object[] go_maze(string str, Labyrinth[,] location, object[] entry)
        {
            //Север = N Юг = S Запад = W Восток = E
            char flag = Convert.ToChar(entry[0]);
            int x = Convert.ToInt32(entry[1]);
            int y = Convert.ToInt32(entry[2]);
            Console.WriteLine(x + " " + y);
            for (int i = 0; i < str.Length; i++)
            {
                if (flag == 'S')
                {
                    if (str[i] == 'W') //Если мы смотрели на юг и пошли вперед, то слева стенка
                    {
                        location[x, y].right_wall = true;
                        y++;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на юг и повернули налево, то слева пусто и мы смотрим на восток
                    {
                        flag = 'E';
                        location[x, y].right_wall = false;
                        x++; i++; continue;
                    }
                    else if (str[i] == 'R') //Если мы смотрели на юг и повернули направо, то слева стенка и сверху стенка
                    {
                        flag = 'W';
                        location[x, y].right_wall = true;
                        location[x, y].down_wall = true;
                        if (str[i + 1] == 'W') { x--; i++; continue; }
                        else continue;
                    }
                }
                if (flag == 'E')
                {
                    if (str[i] == 'W') //Если мы смотрели на восток и пошли вперед, то слева стенка
                    {
                        location[x, y].up_wall = true;
                        x++;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на восток и повернули налево, то слева пусто и мы смотрим на север
                    {
                        flag = 'N';
                        location[x, y].up_wall = false;
                        y--; i++; continue;

                    }
                    else if (str[i] == 'R') //Если мы смотрели на восток и повернули направо, то слева стенка и впереди стенка
                    {
                        flag = 'S';
                        location[x, y].up_wall = true;
                        location[x, y].right_wall = true;
                        if (str[i + 1] == 'W') { y++; i++; continue; }
                        else continue;
                    }
                }
                if (flag == 'W')
                {
                    if (str[i] == 'W') //Если мы смотрели на запад и пошли вперед, то слева стенка
                    {
                        location[x, y].down_wall = true;
                        x--;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на запад и повернули налево, то слева пусто и мы смотрим на юг
                    {
                        flag = 'S';
                        location[x, y].down_wall = false;
                        y++;
                        i++; continue;
                    }
                    else if (str[i] == 'R') //Если мы смотрели на запад и повернули направо, то слева стенка и впереди стенка
                    {
                        flag = 'N';
                        location[x, y].down_wall = true;
                        location[x, y].left_wall = true;
                        if (str[i + 1] == 'W') { y--; i++; continue; }
                        else continue; 
                    }
                }
                if (flag == 'N')
                {
                    if (str[i] == 'W') //Если мы смотрели на север и пошли вперед, то слева стенка
                    {
                        location[x, y].left_wall = true;
                        y--;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на север и повернули налево, то слева пусто и мы смотрим на запад
                    {
                        flag = 'W';
                        location[x, y].left_wall = false;
                        x--;  i++; continue;
                    }
                    else if (str[i] == 'R') //Если мы смотрели на север и повернули направо, то слева стенка и впереди стенка и мы смотрим на восток
                    {
                        flag = 'E';
                        location[x, y].up_wall = true;
                        location[x, y].left_wall = true;

                        if (str[i + 1] == 'W') { x++; i++; continue; }
                        else continue; 
                    }
                }
            }
            for (int i = 0; i < Math.Sqrt(location.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(location.Length); j++)
                {
                    //Console.Write(String.Format("{0,3}", i,j));
                    Console.Write(String.Format("{0,5}", "\n x " + i + " y " + j + "\n dw = " + location[i, j].down_wall + "\n lw = " + location[i, j].left_wall + "\n rw = " + location[i, j].right_wall + "\n uw = " + location[i, j].up_wall));
                }
                Console.WriteLine();
            }
            if (flag == 'N') flag = 'S';
            else if (flag == 'E') flag = 'W';
            else if (flag == 'S') flag = 'N';
            else flag = 'E';
            Console.WriteLine(x + " " + y);
            entry[0] = flag; entry[1] = x; entry[2] = y-1;
            return entry;
        }

    }
}
