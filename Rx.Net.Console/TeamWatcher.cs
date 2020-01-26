using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace Rx.Net.Console
{
    class TeamWatcher
    {
        private int interval = 500;

        private TeamMember[] teamMembers =
        {
            new TeamMember("Konrad", true),
            new TeamMember("Carq", false),
            new TeamMember("Antonio", false),
            new TeamMember("The Senior", false),
            new TeamMember("Karolina", false),
            new TeamMember("The Baby", false),
            new TeamMember("Kuba", false),
            new TeamMember("Adam", true),
            new TeamMember("Mateusz", false),
            new TeamMember("Daniel", false)
        };

        public IObservable<TeamMember> WeakGenes()
        {
            int i = 0;
            return Observable.Create<TeamMember>(
                    observer =>
                    {
                        var timer = new System.Timers.Timer();
                        timer.Interval = interval;
                        timer.Elapsed += (s, e) =>
                        {
                            if (i >= teamMembers.Length)
                            {
                                observer.OnCompleted();
                            }
                            else
                            {
                                observer.OnNext(teamMembers[i++]);
                            }
                        };

                        timer.Start();
                        return timer;
                    });
        }
    }
}
