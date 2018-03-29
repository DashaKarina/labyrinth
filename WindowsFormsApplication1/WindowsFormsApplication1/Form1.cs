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
            Read();
            string str = "", str2 = "";
            for (int i = 1; i <= Convert.ToInt32(small[0][0]); i++)
            {
                str = small[i][0].Remove(0, 1);
                str2 = small[i][1].Remove(0, 1);
                build_maze(str, str2); //Передаем два прохода в метод создания лабиринта
                descrip_maze(i, "small");
            }
            for (int i = 1; i <= Convert.ToInt32(large[0][0]); i++)
            {
                str = large[i][0].Remove(0, 1);
                str2 = large[i][1].Remove(0, 1);
                build_maze(str, str2); //Передаем два прохода в метод создания лабиринта
                descrip_maze(i, "large");
            }
            //Console.WriteLine(str);
            
        }
        string[][] small;
        string[][] large;
        Labyrinth[,] location;
        public void Read()
        {
              try
            {
                StreamReader sr = new StreamReader("small-test.in.txt");
                string[] line = sr.ReadToEnd().Split(new Char[] {'\n'});
                small = new string[line.Length][];
                for (int i = 0; i < line.Length; i++)
                {
                    small[i] = line[i].Split(new Char[] { ' ' });
                }
                sr.Close();
                sr = new StreamReader("large-test.in.txt");
                line = sr.ReadToEnd().Split(new Char[] { '\n' });
                large = new string[line.Length][];
                for (int i = 0; i < line.Length; i++)
                {
                    large[i] = line[i].Split(new Char[] { ' ' });
                }
                sr.Close();
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
            int size;
            if(str.Length > str2.Length)
            size = 1 + 2 * str.Count(z => z.Equals('W')); // Размер лабиринта
            else size = 1 + 2 * str2.Count(z => z.Equals('W'));
            location = new Labyrinth[size, size];
            for (int i = 0; i < Math.Sqrt(location.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(location.Length); j++)
                {
                    location[i, j] = new Labyrinth();
                }
            }
            object[] maze = { 'S', size / 2, 0 }; // Начальные значения 
            maze = go_maze(str, maze); //Прямой проход
            go_maze(str2, maze); //Обратный проход
        }

        public object[] go_maze(string str, object[] entry)
        {
            //Север = N Юг = S Запад = W Восток = E
            char flag = Convert.ToChar(entry[0]);
            int x = Convert.ToInt32(entry[1]);
            int y = Convert.ToInt32(entry[2]);
            
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Length == i) break;
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
                        y++; i++; continue;
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
            if (flag == 'N') { flag = 'S'; y++; }
            else if (flag == 'E') { flag = 'W'; x--; }
            else if (flag == 'S') { flag = 'N'; y--; }
            else { flag = 'E'; x++; }
            entry[0] = flag; entry[1] = x; entry[2] = y;
            return entry;
        }
        public void descrip_maze(int case_num, string name)
        {
            int flag = 0;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(name + @"-test.out.txt", true))
            {
                file.Write("Case #" + case_num + ":" + "\r\n");
                for (int j = 0; j < Math.Sqrt(location.Length); j++)
                {
                    flag = 0;
                    for (int i = 0; i < Math.Sqrt(location.Length); i++)
                    {
                        if (location[i, j].up_wall == true && location[i, j].down_wall == false && location[i, j].left_wall == false && location[i, j].right_wall == false)
                        {
                            file.Write('e');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == true && location[i, j].left_wall == false && location[i, j].right_wall == false)
                        {
                            file.Write('d'); 
                        }
                        else if (location[i, j].up_wall == true && location[i, j].down_wall == true && location[i, j].left_wall == false && location[i, j].right_wall == false)
                        {
                            file.Write('c');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == false && location[i, j].left_wall == true && location[i, j].right_wall == false)
                        {
                            file.Write('b');
                        }
                        else if (location[i, j].up_wall == true && location[i, j].down_wall == false && location[i, j].left_wall == true && location[i, j].right_wall == false)
                        {
                            file.Write('a');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == true && location[i, j].left_wall == true && location[i, j].right_wall == false)
                        {
                            file.Write('9');
                        }
                        else if (location[i, j].up_wall == true && location[i, j].down_wall == true && location[i, j].left_wall == true && location[i, j].right_wall == false)
                        {
                            file.Write('8');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == false && location[i, j].left_wall == false && location[i, j].right_wall == true)
                        {
                            file.Write('7');
                        }
                        else if (location[i, j].up_wall == true && location[i, j].down_wall == false && location[i, j].left_wall == false && location[i, j].right_wall == true)
                        {
                            file.Write('6');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == true && location[i, j].left_wall == false && location[i, j].right_wall == true)
                        {
                            file.Write('5');
                        }
                        else if (location[i, j].up_wall == true && location[i, j].down_wall == true && location[i, j].left_wall == false && location[i, j].right_wall == true)
                        {
                            file.Write('4');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == false && location[i, j].left_wall == true && location[i, j].right_wall == true)
                        {
                            file.Write('3');
                        }
                        else if (location[i, j].up_wall == true && location[i, j].down_wall == false && location[i, j].left_wall == true && location[i, j].right_wall == true)
                        {
                            file.Write('2');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == true && location[i, j].left_wall == true && location[i, j].right_wall == true)
                        {
                            file.Write('1');
                        }
                        else if (location[i, j].up_wall == false && location[i, j].down_wall == false && location[i, j].left_wall == false && location[i, j].right_wall == false)
                        {
                            flag++;
                        }
                    }
                    if (flag == Math.Sqrt(location.Length)) break;
                    else file.WriteLine();
                }
                
            }
        }
    }
}
