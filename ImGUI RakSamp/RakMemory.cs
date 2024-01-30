using Memory;
using Process.NET;
using Process.NET.Native.Types;
using System.Runtime.InteropServices;
using TextCopy;

namespace ImGUI_RakSamp
{
    public class RakMemory
    {
        public Process.NET.ProcessSharp process { get; private set; }
        private Mem mem = new Mem();
        public RakMemory(Process.NET.ProcessSharp process2, int nativeProcess)
        {
            
            
            this.process = process2;
            bool opened = this.mem.OpenProcess(nativeProcess);
            if (opened)
            {
                Console.WriteLine("Detected");
            }
            else
            {
                Console.WriteLine("Not detected");
            }
        }

        public long GetBotMoney()
        {
            return mem.ReadLong("004D36C8");
        }
        public long GetBotId()
        {
            //return process.Memory.Read<int>(0x004D36AC);
            return mem.Read2Byte("004D36AC");
        }

        public string GetBotNickname()
        {
            return mem.ReadString("0057A072");
        }

        public int GetBotHealth()
        {
            return mem.Read2Byte("004AF980");
        }

        public string GetNicknameByID(int id)
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

        [DllImport("user32.dll")] static extern short VkKeyScan(char ch);

        public void SendBotMessage(string message)
        {
            foreach (var window in process.WindowFactory.Windows.ToList())
            {
                Console.WriteLine(window.ClassName);
                if (window.ClassName == "Edit")
                {
                    var text = ClipboardService.GetText();
                    ClipboardService.SetText(message);
                    window.SendMessage(0x0302, 0, 0);
                    if (text != null)
                    {
                        ClipboardService.SetText(text);
                    }
                }

                if (window.Title == "&Send")
                {
                    window.SendMessage(0x201, 0, MAKELPARAM(0, 1));
                    window.SendMessage(0x202, 0, MAKELPARAM(0, 1));
                }
            }
        }

        private static int MAKELPARAM(int p, int p_2)
        {
            return ((p_2 << 16) | (p & 0xFFFF));
        }
    }
}
