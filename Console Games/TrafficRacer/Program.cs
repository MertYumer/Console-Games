namespace TrafficRacer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    struct Object
    {
        public int x;
        public int y;
        public char symbol;
        public ConsoleColor color;
    }

    class Program
    {
        static void PrintOnPosition(int x, int y, char symbol,
            ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }

        static void PrintStringOnPosition(int x, int y, string str,
            ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        static void Main()
        {
            double speed = 100.0;
            double acceleration = 0.5;
            int playfieldWidth = 5;
            int livesCount = 5;
            Console.BufferHeight = Console.WindowHeight = 20;
            Console.BufferWidth = Console.WindowWidth = 30;
            Object userCar = new Object();
            userCar.x = 2;
            userCar.y = Console.WindowHeight - 1;
            userCar.symbol = '@';
            userCar.color = ConsoleColor.Green;
            Random randomGenerator = new Random();
            var objects = new List<Object>();

            while (true)
            {
                speed += acceleration;

                if (speed > 400)
                {
                    speed = 400;
                }

                bool hitted = false;

                int chance = randomGenerator.Next(0, 100);

                if (chance < 5)
                {
                    var newObject = new Object
                    {
                        x = randomGenerator.Next(0, playfieldWidth),
                        y = 0,
                        symbol = '$',
                        color = ConsoleColor.Cyan,
                    };

                    objects.Add(newObject);
                }

                else if (chance < 20)
                {
                    var newObject = new Object
                    {
                        x = randomGenerator.Next(0, playfieldWidth),
                        y = 0,
                        symbol = '*',
                        color = ConsoleColor.Cyan,
                    };

                    objects.Add(newObject);
                }

                else
                {
                    Object newCar = new Object
                    {
                        x = randomGenerator.Next(0, playfieldWidth),
                        y = 0,
                        symbol = '#',
                        color = ConsoleColor.Yellow
                    };

                    objects.Add(newCar);
                }

                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        if (userCar.x - 1 >= 0)
                        {
                            userCar.x--;
                        }
                    }

                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (userCar.x + 1 < playfieldWidth)
                        {
                            userCar.x++;
                        }
                    }
                }

                var newList = new List<Object>();

                for (int i = 0; i < objects.Count; i++)
                {
                    Object oldCar = objects[i];

                    Object currentObject = new Object
                    {
                        x = oldCar.x,
                        y = oldCar.y + 1,
                        symbol = oldCar.symbol,
                        color = oldCar.color
                    };

                    if (currentObject.symbol == '$' && currentObject.y == userCar.y &&
                        currentObject.x == userCar.x)
                    {
                        livesCount++;
                    }

                    if (currentObject.symbol == '*' && currentObject.y == userCar.y &&
                        currentObject.x == userCar.x)
                    {
                        speed -= 20;
                    }

                    if (currentObject.symbol == '#' && currentObject.y == userCar.y &&
                    currentObject.x == userCar.x)
                    {
                        hitted = true;
                        livesCount--;
                        speed += 50;

                        if (speed > 400)
                        {
                            speed = 400;
                        }

                        if (livesCount <= 0)
                        {
                            PrintStringOnPosition(8, 10, $"GAME OVER!!!", ConsoleColor.Red);
                            PrintStringOnPosition(8, 12, $"Press [enter] to exit", ConsoleColor.Red);
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }

                    if (currentObject.y < Console.BufferHeight)
                    {
                        newList.Add(currentObject);
                    }
                }

                objects = newList;

                Console.Clear();

                if (hitted)
                {
                    objects.Clear();
                    PrintOnPosition(userCar.x, userCar.y, 'X', ConsoleColor.Red);
                }

                else
                {
                    PrintOnPosition(userCar.x, userCar.y, userCar.symbol, userCar.color);
                }

                foreach (Object car in objects)
                {
                    PrintOnPosition(car.x, car.y, car.symbol, car.color);
                }

                PrintStringOnPosition(8, 4, $"Lives: {livesCount}", ConsoleColor.White);
                PrintStringOnPosition(8, 5, $"Speed: {speed}", ConsoleColor.White);
                PrintStringOnPosition(8, 6, $"Acceleration: {acceleration}", ConsoleColor.White);
                Thread.Sleep((int)(600 - speed));
            }
        }
    }
}
