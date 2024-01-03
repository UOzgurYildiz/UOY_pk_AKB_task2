using Task2_api.Services;
using System;

namespace Task2_api;

public class ConsoleLogger: ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine("[ConsoleLogger] - " + message);
    }
}