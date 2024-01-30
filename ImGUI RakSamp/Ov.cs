using ClickableTransparentOverlay;
using ImGuiNET;
using Memory;
using MyApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGUI_RakSamp
{
    public class Ov : Overlay
    {
        private byte[] input_buffer = new byte[256];
        protected override void Render()
        {
            Size = Program.size;
            ImGui.Begin("RakSamp | " + Program.rak.GetBotNickname() + " [" + Program.rak.GetBotId() + "]", ImGuiWindowFlags.NoScrollbar);

            ImGui.Text("Money: " + FormatNumber(Program.rak.GetBotMoney()));
            ImGui.SameLine(140);
            ImGui.Text("Health: " + Program.rak.GetBotHealth());
            ImGui.SetCursorPosY(50);
            ImGui.SetNextItemWidth(175);
            ImGui.InputText("", input_buffer, 256);
            ImGui.SameLine();
            if (ImGui.Button("Send"))
            {
                Program.rak.SendBotMessage(Encoding.UTF8.GetString(input_buffer));
            }
            ImGui.End();
        }

        internal static string FormatNumber(long num)
        {
            num = MaxThreeSignificantDigits(num);

            if (num >= 100000000)
                return (num / 1000000D).ToString("0.#M");
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##M");
            if (num >= 100000)
                return (num / 1000D).ToString("0k");
            if (num >= 100000)
                return (num / 1000D).ToString("0.#k");
            if (num >= 1000)
                return (num / 1000D).ToString("0.##k");
            return num.ToString("#,0");
        }


        internal static long MaxThreeSignificantDigits(long x)
        {
            int i = (int)Math.Log10(x);
            i = Math.Max(0, i - 2);
            i = (int)Math.Pow(10, i);
            return x / i * i;
        }
    }
}
