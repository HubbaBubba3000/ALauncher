using System;
namespace ALauncher.Console;

public class Program 
{
    public static void Main(string[] args) 
    {
        string command = "";

        if (args.Length != 0) 
            command = args[0];
        
        if (string.IsNullOrEmpty(command)) 
        {
            System.Console.WriteLine("ALauncher ver 0.2 dev");
        }
        else 
            System.Console.WriteLine($"ALauncher run with command {command}");

        System.Console.Write("program end ");
        System.Console.Read();
    }
}