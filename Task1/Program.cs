using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {

    
        static object locker=new object();
        static int[,] garden;
        delegate void Message();

        static void Main(string[] args)
        {

            Message message =Print;
            Console.Write("Укажите размер сада: ");
            int size = Convert.ToInt32(Console.ReadLine());
            garden = new int[size, size];
            DrawGarden(size);
            ThreadStart firstThreadStart = new ThreadStart(Gardner1);
            Thread firstWorker = new Thread(firstThreadStart);
            firstWorker.Start();
            Gardner2();
            message.Invoke();
            Console.ReadKey();
        }


        public static void Print() => Console.WriteLine("Сад обработан");
        static void DrawGarden(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(garden[i, j] + " ");
                }
                Console.WriteLine();
            }

        }


         
        static void Gardening(int i, int j)
        {
            lock (locker)
            {
                garden[i, j] = 1;
                Console.Clear();
                DrawGarden(garden.GetLength(1));
                Thread.Sleep(500);
            }
        }

        static void Gardner1()
        {
                int step = 1;
                for (int i = 0; i < garden.GetLength(0); i++)
                {
                    if (step == 1)
                    {
                        for (int j = 0; j < garden.GetLength(1); j += step)
                        {
                            if (garden[i, j] == 0)
                            {
                           Gardening(i, j);
                            }
                        }
                    }
                    else
                    {
                        for (int j = garden.GetLength(1) - 1; j > -1; j += step)
                        {
                            if (garden[i, j] == 0)
                            {
                            Gardening(i, j);
                            }
                        }
                    }
                    step *= -1;

                }
            
                
        }

        static void Gardner2()
        {
                int step = -1;
                for (int j = garden.GetLength(1) - 1; j > -1; j--)
                {
                    if (step == -1)
                    {
                        for (int i = garden.GetLength(0) - 1; i >  - 1; i += step)
                        {
                            if (garden[i, j] == 0)
                            {
                            Gardening(i, j);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < garden.GetLength(0); i += step)
                        {
                            if (garden[i, j] == 0)
                            {
                            Gardening(i, j);
                            }
                        }
                    }
                    step *= -1;
                }
            
        }


      
    
    }
}
