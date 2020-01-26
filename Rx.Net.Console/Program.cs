using System;
using System.Reactive.Linq;

namespace Rx.Net.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new TeamWatcher()
                .WeakGenes()
                .Subscribe(
                x => System.Console.WriteLine($"Cześć {x.Name}, jak tam weekend?"),
                () => System.Console.WriteLine("Wszyscy już przyszli."));

            System.Console.ReadKey();
        }
    }
}
