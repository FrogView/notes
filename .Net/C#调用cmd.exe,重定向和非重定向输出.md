### C#调用cmd.exe,重定向和非重定向输出
1. 重定向输出
``` csharp
//重定向输出
using (Process myPro = new Process())
{
    ProcessStartInfo psi = new ProcessStartInfo($@"C:\WINDOWS\system32\cmd.exe");
    string workDir = @"C:\Windows\";
    psi.WorkingDirectory = workDir;
    psi.UseShellExecute = false;
    psi.RedirectStandardInput = true;
    psi.RedirectStandardOutput = true;
    psi.RedirectStandardError = true;
    psi.CreateNoWindow = true;
    myPro.StartInfo = psi;
    myPro.Start();
    myPro.StandardInput.WriteLine("ipconfig" + "&exit");
    myPro.StandardInput.AutoFlush = true;
    StreamReader reader = myPro.StandardOutput;
    string line = reader.ReadLine();
    while (!reader.EndOfStream)
    {
        line = reader.ReadLine();
        Console.WriteLine(line);
    }
    myPro.WaitForExit();
}
```
2. 非重定向输出
``` csharp
//非重定项cmd输出，Vs debug模式下看不到效果，程序运行时可看到效果
using (Process myPro = new Process())
{
    //指定启动进程是调用的应用程序和命令行参数
    ProcessStartInfo psi = new ProcessStartInfo($@"C:\WINDOWS\system32\cmd.exe");
    string workDir = @"C:\Windows\";
    psi.WorkingDirectory = workDir;//batrun
    psi.UseShellExecute = false;    //是否使用操作系统shell启动
    psi.RedirectStandardInput = true;//接受来自调用程序的输入信息
    myPro.StartInfo = psi;
    myPro.Start();
    //命令行参数
    myPro.StandardInput.WriteLine(cmdStr);
    myPro.StandardInput.AutoFlush = true;

    myPro.WaitForExit();
}
```
