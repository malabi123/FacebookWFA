using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    internal static class FakeFriendsGenerator
    {
        public static readonly List<FakeFacebookFriend> sr_FakeFriends = new List<FakeFacebookFriend>
        {
            new FakeFacebookFriend("Mickey Mouse", true, Properties.Resources.MickeyMouse, new DateTime(1928, 11, 18), "Toontown"),
            new FakeFacebookFriend("Bugs Bunny", false, Properties.Resources.BugsBunny, new DateTime(1940, 7, 27), "Brooklyn"),
            new FakeFacebookFriend("SpongeBob SquarePants", true, Properties.Resources.SpongeBob, new DateTime(1986, 7, 14), "Bikini Bottom"),
            new FakeFacebookFriend("Homer Simpson", false, Properties.Resources.HomerSimpson, new DateTime(1956, 5, 12), "Springfield"),
            new FakeFacebookFriend("Tom Cat", true, Properties.Resources.TomCat, new DateTime(1940, 2, 10), "London"),
            new FakeFacebookFriend("Jerry Mouse", false, Properties.Resources.JerryMouse, new DateTime(1940, 2, 10), "London"),
            new FakeFacebookFriend("Scooby-Doo", true, Properties.Resources.ScoobyDoo, new DateTime(1969, 9, 13), "Crystal Cove"),
            new FakeFacebookFriend("Pikachu", false, Properties.Resources.Picatchu, new DateTime(1996, 2, 27), "Pallet Town"),
            new FakeFacebookFriend("Donald Duck", true, Properties.Resources.DonaldDuck, new DateTime(1934, 6, 9), "Duckburg"),
            new FakeFacebookFriend("Goofy", false, Properties.Resources.Goofey, new DateTime(1932, 5, 25), "Spoonerville")
        };

        public static void GenerateEventsForFriends(List<Event> i_MyEvents)
        {
            Random random = new Random();
            foreach (FakeFacebookFriend friend in sr_FakeFriends)
            {
                friend.AttendingEvents = chooseRandomEvents(i_MyEvents, random);
            }
        }

        private static List<Event> chooseRandomEvents(List<Event> i_MyEvents, Random i_Random)
        {
            List<Event> randomEvents = new List<Event>();
            int numberOfEvents = i_Random.Next(0, i_MyEvents.Count);

            for(int i = 0; i < numberOfEvents; i++)
            {
                int eventNumber = i_Random.Next(0, i_MyEvents.Count - 1);
                randomEvents.Add(i_MyEvents[eventNumber]);
            }

            return randomEvents;
        }
    }
}
