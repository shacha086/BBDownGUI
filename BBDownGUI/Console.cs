using System.Diagnostics;
using System.Text;

namespace BBDownGUI
{
    internal class Console
    {
        internal delegate void OutputHandler(string str);
        internal event OutputHandler OutputRecieved;
        private readonly Process process;
        private static readonly StringBuilder sb = new();
        internal Console(OutputHandler onDataReceived)
        {
            //创建进程对象
            Process process = new();
            //创建进程时使用的一组值，如下面的属性
            ProcessStartInfo startinfo = new()
            {
                //设定需要执行的命令程序
                //以下是隐藏cmd窗口的方法
                FileName = "cmd.exe",
                //不使用系统外壳程序启动
                UseShellExecute = false,
                //不重定向输入
                RedirectStandardInput = true,
                //重定向输出，而不是默认的显示在dos控制台上
                RedirectStandardOutput = true,
                //重定向错误输出
                RedirectStandardError = true,
                //不创建窗口
                CreateNoWindow = true
            };
            process.StartInfo = startinfo;
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    Trace.WriteLine("Recieved new output data");
                    sb.Append(e.Data);
                    OutputRecieved(sb.ToString());
                }
            );
            OutputRecieved += onDataReceived;
            //开始进程
            process.Start();
            this.process = process;
        }

        internal void Run(string str)
        {
            sb.Clear();
            StreamWriter sIn = process.StandardInput;
            sIn.WriteLine(str);
        }
    }
}
