using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    //Node
    //1. Maintains list of firends IDs
    //2. Maintains the machine id in which this person data exist
    //3. A linked list of wall posts or tweets (head points to the current or most recent msg).
    //4. While looking up friends, we can batch machine jumps.
    //5. Rather than randomly dividing people, try to divide them up by country, city or state
    //6. Use BFS to find if there is a connection between two persons.
    //7. During BFS, we do not want to modify/mark a person/node as visited (as there could be multiple searches going on), instead
    //   we can mimic marking by saving person ids to a hashtable
    //8. We need to implement a fail safe system, to avoid server outages/break downs. If one system fails, there should be another 
    //   system to take its place. Data is not stored on a single server farm, multiple systems are connected.

    //One particular machine model
    public class Machine
    {
        public Dictionary<long, Person> persons = new Dictionary<long, Person>();
        public long machineID;
    }
    
    //Entire system or list of machines
    public class Server
    {
        public Dictionary<long, Machine> machines = new Dictionary<long, Machine>();
    }

    //Represents a person object
    public class Person
    {
        private readonly long Pid;
        public List<long> friendIDs { get; set; }
        private long machineID { get; set; }    //Machine on which the current user exists
        public LinkedList<string> WallPosts { get; set; }
        public string info { get; set; }
        private Server server = new Server();

        public long[] getFriends()
        {
            if (friendIDs == null)
                return null;

            return friendIDs.ToArray();
        }

        public long getID()
        {
            return Pid;
        }

        public Person lookupFriend(long machineID, long fid)
        {
            if (server.machines.ContainsKey(machineID) && server.machines[machineID].persons.ContainsKey(fid))
                return server.machines[machineID].persons[fid];
            return null;
        }

        public Machine lookupMachine(long machineID)
        {
            if (server.machines.ContainsKey(machineID))
                return server.machines[machineID];
            return null;
        }
    }
    
    class Facebook
    {
    }
}
