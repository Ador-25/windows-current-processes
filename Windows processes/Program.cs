using System.Diagnostics;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        ProcessHelper instance = new();
    }

}
interface IBrower
{
    public void OpenTabs();
}
class ProcessHelper : IBrower
{
    public ProcessHelper()
    {
        string firefox = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        OpenProcess(firefox);
        Thread.Sleep(1600);
        OpenTabs();
    }
    public Process[] GetAllProcess()
    {
        return Process.GetProcesses();
    }
    public void DisplayAllProcesses()
    {
        Process[] processes = this.GetAllProcess();
        foreach (Process process in processes)
        {
            if (!string.IsNullOrEmpty(process.MainWindowTitle))
            {
                Console.WriteLine($"Process Name: {process.ProcessName}");
                Console.WriteLine($"Window Title: {process.MainWindowTitle}");
                Console.WriteLine($"Process ID: {process.Id}");
                Console.WriteLine();
            }
        }
    }
    public void OpenProcess(string str)
    {
        Process.Start(str);
    }
    public void OpenBrowserTab(string url)
    {
        string command = $@"start firefox.exe " + url;
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",  // The command prompt
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        // Start the process
        Process process = new Process { StartInfo = psi };
        process.Start();

        // Write the command to the command prompt and read the output
        process.StandardInput.WriteLine(command);
        process.StandardInput.WriteLine("exit");
        string result = process.StandardOutput.ReadToEnd();

        // Display the result
        Console.WriteLine("Command Output:");
        Console.WriteLine(result);

        // Wait for the process to exit
        process.WaitForExit();
        process.Close();
    }

    public void OpenTabs()
    {
        List<string> urls = new List<string>()
        {
            @"""https://www.linkedin.com/""",
            @"""https://github.com/""",
            @"""https://docs.google.com/spreadsheets/d/1WFUG7-B0OY4I_-fjL13R34flyRtopG-o0AAukDoOUeI/edit?ouid=115324290066130434891&usp=sheets_home&ths=true""",
            @"""https://www.facebook.com/""",
            @"""https://mail.google.com/mail/u/0/#inbox""",
            @"""https://mail.google.com/mail/u/1/#inbox""",
            @"""https://chat.openai.com/""",
            @"""https://northsouth.instructure.com/?login_success=1"""
        };

        foreach (string url in urls)
        {
            this.OpenBrowserTab(url);
        }
    }
}


