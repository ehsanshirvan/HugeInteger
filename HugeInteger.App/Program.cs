using HugeInteger.App;
using HugeInteger.Library;
using System.Diagnostics;

do
{
    Console.Write("num1 = ");
    string num1str = ConsoleWorker.GetInput();

    Console.Write("num2 = ");
    var num2str = ConsoleWorker.GetInput();

    ListNode num1 = Worker.GetNum(num1str);
    ListNode num2 = Worker.GetNum(num2str);

    ListNode num11 = num1.DeepClone();
    ListNode num22 = num2.DeepClone();


    string existOperatorsList = Worker.GetExistOperators();

    Console.Write($"select operation you want from list " +
                  $"{existOperatorsList} : ");

    var op = Console.ReadKey().KeyChar;
    if (!Worker.Operations.ContainsKey(op))
    {
        Console.WriteLine(
            new NotImplementedException("\nIt's not still Implemented please select allowed operation\n-------------------------\n").Message);
        continue;
    }

    var timer = new Stopwatch();
    timer.Start();
    var res = Worker.DoOperate(num11, num22, op);
    timer.Stop();
    var takenTimeToOperate = timer.ElapsedMilliseconds / 1000;
    Console.WriteLine($"\n{num1} {op} {num2} = {res}");


    Console.WriteLine($"Time taken to operate : {takenTimeToOperate:##,#} seconds");
    Console.WriteLine("-------------------------------\n");

}
while (Console.ReadKey().Key != ConsoleKey.Escape);
