using ImGUI_RakSamp;
using Memory;
using System;
using System.Drawing;
using Vortice.Mathematics;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public  static Mem mem = new Mem();
        public static System.Drawing.Size size = new System.Drawing.Size();
        public static RakMemory rak;
        static void Main(string[] args)
        {
            Console.Write("RakSAMP PID: ");
            int pid = -1;
            int.TryParse(Console.ReadLine(), out pid);

            System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(pid);
            if (process != null)
            {
                Process.NET.ProcessSharp processSharp = new Process.NET.ProcessSharp(process, Process.NET.Memory.MemoryType.Local);
                rak = new RakMemory(processSharp, pid);

                mem.OpenProcess(pid);

                //long money = mem.ReadLong("004D36C8");
                //int health = mem.Read2Byte("004AF980");
                //int id = mem.Read2Byte("004D36AC");
                //int skin = mem.Read2Byte("004D3758");
                //string nickname = mem.ReadString("0057A072");
                //Console.WriteLine("Name " + nickname);
                size = new Size(1680, 1050);

                Ov ov = new Ov();
                ov.Run();

                //Console.WriteLine("Money " + money);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Error Process not found");
                Console.ReadKey();
            }
        }



        static string GetPlayerName(int id, Mem mem)
        {
            if (id > 1000 || id < 0)
            {
                return "";
            }
            int addres = 0x004E88E3;
            if (id > 0)
            {
                addres = addres + (0x15E * id);
            }
            return mem.ReadString(addres.ToString("X"));
        }

        private static int MAKELPARAM(int p, int p_2)
        {
            return ((p_2 << 16) | (p & 0xFFFF));
        }

        static void PressSendButton(Process.NET.ProcessSharp processSharp)
        {
            foreach (var window in processSharp.WindowFactory.Windows.ToList())
            {
                if (window.Title == "&Send")
                {
                    window.SendMessage(0x201, 0, MAKELPARAM(0, 1));
                    window.SendMessage(0x202, 0, MAKELPARAM(0, 1));
                    Console.WriteLine("Send");

                }
                Console.WriteLine(window.Title);
            }
        }

        static string GetInput(Mem mem)
        {
            int lenght = mem.ReadInt("RakSAMP Lite.exe+0x32C4,0x38,0x54,0x4,0x18C,0x3C,0x48,0xE94");
            Console.WriteLine(lenght);
            return "";
        }
    }
}