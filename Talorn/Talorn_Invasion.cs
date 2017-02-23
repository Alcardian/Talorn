using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talorn
{
    public class Talorn_Invasion
    {
        /// <summary>
        /// The unique ID of the invsion.
        /// </summary>
        private string id = "Unknown";

        /// <summary>
        /// Describe which faction you will be facing on the mission.
        /// The value is set to "Unknown" if it havn't tried or failed to identify the faction.
        /// </summary>
        private string faction = "Unknown";

        /// <summary>
        /// Shows the node that is the location of the mission.
        /// The value is set to "Unknown" if it havn't tried or failed to identify the node.
        /// </summary>
        private string node = "Unknown";

        /// <summary>
        /// TODO: Find out how this value works
        /// </summary>
        private int count; //maybe long?

        /// <summary>
        /// TODO: Find out how this value works
        /// </summary>
        private int goal; //maybe long?


        private string locTag = "Unknown";

        /// <summary>
        /// TODO: Find out how this value works
        /// What is this? maybe remains here as true after the mission is finished until planet invasion ends?
        /// </summary>
        private bool completed;

        /// <summary>
        /// The reward that the attackers will get.
        /// </summary>
        private Tuple<string, int> attackerReward = null;

        /// <summary>
        /// The reward that the defenders will get.
        /// </summary>
        private Tuple<string, int> defenderReward = null;

        /// <summary>
        /// The faction that the attackers will face.
        /// </summary>
        private string AEF = "Unknown"; // Attacker Ememy Faction

        /// <summary>
        /// The faction that the defenders will face.
        /// </summary>
        private string DEF = "Unknown"; // Defender Ememy Faction

        /// <summary>
        /// Create an invasion object with the values contained in a string with json data for one invasion.
        /// </summary>
        /// <param name="iString">A string that contains the json data of one invasion.</param>
        public Talorn_Invasion(string iString)  //invasion string
        {

        }

        public string printInvasion()
        {
            string tmp = "";
            return tmp;
        }

        public string getID()
        {
            return id;
        }
        public void setID(string ID)
        {
            id = ID;
        }
        public string getFaction()
        {
            return faction;
        }
        public void setFaction(string Faction)
        {
            faction = Faction;
        }
        public string getNode()
        {
            return node;
        }
        public void setNode(string Node)
        {
            node = Node;
        }
        // int count
        // int goal
        // string locTag
        // bool completed
        // Tuple<string, int> attackerReward
        // Tuple<string, int> defenderReward
        // string AEF
        // string DEF
    }
}