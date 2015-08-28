using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace no_window2
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = @"update_csd.exe"; // 実行するファイル
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない

            Process.Start(psInfo);
        }
    }
}
