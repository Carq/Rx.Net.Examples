using System;

namespace Rx.Net.Console
{
    public class TeamMember
    {
        public TeamMember(string name, bool homeOffice = false)
        {
            Name = name;
            HomeOffice = homeOffice;
        }

        public string Name { get; }

        public bool HomeOffice { get; }

        public bool WasOnCoffee { get; set; }

        public TeamMember LetsDrinkCoffee()
        {
            return new TeamMember(Name, HomeOffice)
            {
                WasOnCoffee = true
            };
        }
    }
}
