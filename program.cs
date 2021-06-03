using System;

namespace Project
{
    class Program
    {
        public static int[] Maps = new int[100];
        public static int[] PlayerPos = new int[2];
        public static string[] PlayerName = new string[2];
        public static bool[] Flags = new bool[2];
        static void Main(string[] args)
        {
            GameShow();
            #region 用户输入姓名
            Console.WriteLine("输入玩家A的姓名");
            PlayerName[0] = Console.ReadLine();
            while (PlayerName[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入");
                PlayerName[0] = Console.ReadLine();
            }
            Console.WriteLine("输入玩家B的姓名");
            PlayerName[1] = Console.ReadLine();
            while (PlayerName[1] == "" || PlayerName[0] == PlayerName[1])
            {
                if (PlayerName[1] == "")
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入");
                    PlayerName[1] = Console.ReadLine();
                }
                else if (PlayerName[0] == PlayerName[1])
                {
                    Console.WriteLine("玩家A和玩家B的姓名不能相同，请重新输入");
                    PlayerName[1] = Console.ReadLine();
                }
            }
            #endregion
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的士兵用A表示", PlayerName[0]);
            Console.WriteLine("{0}的士兵用B表示", PlayerName[1]);
            InitialMap();
            DrawMap();
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了玩家{1}", PlayerName[0], PlayerName[1]);
                    break;
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}赢了玩家{1}", PlayerName[1], PlayerName[0]);
                    break;
                }
            }
        }
        /// <summary>
        /// 游戏开始前的显示
        /// </summary>
        static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************************");
        }
        /// <summary>
        /// 初始化地图数据
        /// </summary>
        static void InitialMap()
        {
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 };
            foreach (var item in luckyTurn)
            {
                Maps[item] = 1;
            }
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };
            foreach (var item in landMine)
            {
                Maps[item] = 2;
            }
            int[] pause = { 9, 27, 60, 93 };
            foreach (var item in pause)
            {
                Maps[item] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };
            foreach (var item in timeTunnel)
            {
                Maps[item] = 4;
            }
        }
        static void DrawMap()
        {
            Console.WriteLine("图例：幸运轮盘：◎   地雷：☆   暂停：▲   时空隧道：卐");
            for (int i = 0; i < 30; ++i)
            {
                Console.Write(DrawStringMap(i));
            }
            for (int i = 30; i < 35; ++i)
            {
                Console.WriteLine();
                for(int j=0;j<29;++j)
                    Console.Write("  ");
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            for (int i = 64; i >= 35; --i)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            for (int i = 65; i < 70; ++i)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            for (int i = 70; i < 100; ++i)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
        }
        static string DrawStringMap(int i)
        {
            string str = "";
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
                str = "<>";
            else if (PlayerPos[0] == i)
                str = "Ａ";
            else if (PlayerPos[1] == i)
                str = "Ｂ";
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        str = "卐";
                        break;
                }
            }
            return str;
        }
        static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("{0}按任意键开始掷骰子", PlayerName[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}掷骰子出了{1}", PlayerName[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine("{0}按任意键开始行动", PlayerName[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}行动完了", PlayerName[playerNumber]);
            Console.ReadKey(true);
            if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])
            {
                Console.WriteLine("玩家{0}踩到玩家{1}，玩家{1}退6格",PlayerName[playerNumber],PlayerName[1-playerNumber]);
                PlayerPos[1 - playerNumber] -= 6;
                Console.ReadKey(true);
            }
            else 
            {
                switch (Maps[PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到方块，安全", PlayerName[playerNumber]);
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到幸运轮盘，请选择1，交换位置，2，轰炸对方", PlayerName[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}选择与玩家{1}交换位置", PlayerName[playerNumber], PlayerName[1 - playerNumber]);
                                Console.ReadKey(true);
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1 - playerNumber];
                                PlayerPos[1 - playerNumber] = temp;
                                Console.WriteLine("交换完成，按任意键继续游戏");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1}，玩家{1}退6格", PlayerName[playerNumber], PlayerName[1 - playerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[1 - playerNumber] -= 6;
                                Console.WriteLine("玩家{0}退了6格", PlayerName[1 - playerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入1或者2，1：交换位置，2：轰炸对方");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到地雷，退6格", PlayerName[playerNumber]);
                        Console.ReadKey(true);
                        PlayerPos[playerNumber] -= 6;
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到暂停", PlayerName[playerNumber]);
                        Flags[playerNumber] = true;
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到时空隧道，前进10格", PlayerName[playerNumber]);
                        PlayerPos[playerNumber] += 10;
                        Console.ReadKey(true);
                        break;
                }
            }
            ChangePos();
            Console.Clear();
            DrawMap();
        }
        static void ChangePos()
        {
            if (PlayerPos[0] < 0)
                PlayerPos[0] = 0;
            if (PlayerPos[0] > 99)
                PlayerPos[0] = 99;
            if (PlayerPos[1] < 0)
                PlayerPos[1] = 0;
            if (PlayerPos[1] > 99)
                PlayerPos[1] = 99;
        }
    }
}
