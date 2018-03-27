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
            //Read();
            //Tester();
            build_maze();
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

        public void Tester()
        {
            Labyrinth test = new Labyrinth();
            test.up_wall = true;
            Console.WriteLine(txt[1]);
        }

        public void build_maze()
        {
            char flag = 'S';
            //Север = N Юг = S Запад = W Восток = E
            
            Labyrinth[] location = new Labyrinth[20];
            for (int i = 0; i < location.Length; i++)
            {
                location[i] = new Labyrinth();
            }
            string str = "RWWLWWLWWLWLWRRWRWWWRWWRWLW";
            str.ToCharArray();
            int j = -1;
            for (int i = 0; i < str.Length; i++)
            {
                j++;
                if (flag == 'S')
                {
                    if (str[i] == 'W') //Если мы смотрели на юг и пошли вперед, то слева стенка
                    {
                        location[j].right_wall = true;                   
                    }
                    else if (str[i] == 'L') //Если мы смотрели на юг и повернули налево, то слева пусто и мы смотрим на 
                    {
                        flag = 'E';
                        if (str[i + 1] == 'W') //Если мы смотрели на восток и пошли вперед, то слева стенка
                        {
                            location[j + 1].up_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                    else if (str[i] == 'R') //Если мы смотрели на юг и повернули направо, то слева стенка и сверху стенка
                    {
                        flag = 'W';
                        location[j].right_wall = true;
                        location[j].down_wall = true;

                        if (str[i + 1] == 'W') //Если мы смотрели на запад и пошли вперед, то слева стенка
                        {
                            location[j + 1].down_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
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
                        if (str[i + 1] == 'W') //Если мы смотрели на север и пошли вперед, то слева стенка
                        {
                            location[j + 1].right_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                    else if (str[i] == 'R') //Если мы смотрели на восток и повернули направо, то слева стенка и впереди стенка
                    {
                        flag = 'S';
                        location[j].up_wall = true;
                        location[j].right_wall = true;

                        if (str[i + 1] == 'W') //Если мы смотрели на юг и пошли вперед, то слева стенка
                        {
                            location[j + 1].right_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                }
                if (flag == 'W')
                {
                    if (str[i] == 'W') //Если мы смотрели на запад и пошли вперед, то слева стенка
                    {
                        location[j].down_wall = true;
                        Console.WriteLine(j + 1);
                    }
                    else if (str[i] == 'L') //Если мы смотрели на запад и повернули налево, то слева пусто и мы смотрим на юг
                    {
                        flag = 'S';
                        if (str[i + 1] == 'W') //Если мы смотрели на юг и пошли вперед, то слева стенка
                        {
                            location[j + 1].right_wall = true;
                            i++;  continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                    else if (str[i] == 'R') //Если мы смотрели на запад и повернули направо, то слева стенка и впереди стенка
                    {
                        flag = 'N';
                        location[j].down_wall = true;
                        location[j].left_wall = true;

                        if (str[i + 1] == 'W') //Если мы смотрели на север и пошли вперед, то слева стенка
                        {
                            location[j + 1].left_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                }
                if (flag == 'N')
                {
                    if (str[i] == 'W') //Если мы смотрели на север и пошли вперед, то слева стенка
                    {
                        location[j].up_wall = true;
                    }
                    else if (str[i] == 'L') //Если мы смотрели на север и повернули налево, то слева пусто и мы смотрим на запад
                    {
                        flag = 'W';
                        if (str[i + 1] == 'W') //Если мы смотрели на запад и пошли вперед, то слева стенка
                        {
                            location[j + 1].down_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                    else if (str[i] == 'R') //Если мы смотрели на север и повернули направо, то слева стенка и впереди стенка и мы смотрим на восток
                    {
                        flag = 'E';
                        location[j].up_wall = true;
                        location[j].left_wall = true;

                        if (str[i + 1] == 'W') //Если мы смотрели на восток и пошли вперед, то слева стенка
                        {
                            location[j + 1].up_wall = true;
                            i++; continue;
                        }
                        else
                        {
                            j--; continue;
                        }
                    }
                }
            }

            for (int i = 0; i < location.Length; i++)
            {
                Console.WriteLine(i+1 + "\n dw = " + location[i].down_wall + "\n lw = " + location[i].left_wall + "\n rw = " + location[i].right_wall + "\n uw = " + location[i].up_wall);
            }
            //foreach (Labyrinth x in location)
            //{
            //    Console.WriteLine("dw = " + x.down_wall + "\n lw = " + x.left_wall + "\n rw = " + x.right_wall + "\n uw = " + x.up_wall);
            //}
            
        }

    }
}
