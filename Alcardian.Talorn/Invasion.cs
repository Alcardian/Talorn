using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcardian.Talorn
{
    public class Invasion
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
        private int count = 0; //maybe long?

        /// <summary>
        /// TODO: Find out how this value works
        /// </summary>
        private int goal = 0; //maybe long?


        private string locTag = "Unknown";

        /// <summary>
        /// TODO: Find out how this value works
        /// What is this? maybe remains here as true after the mission is finished until planet invasion ends?
        /// </summary>
        private bool completed = false;

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
        public Invasion(string iString)  //invasion string
        {
            string temp = "";

            // ID
            temp = iString.Substring(iString.IndexOf("_id"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(temp.IndexOf(':')+2);
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            id = temp;

            // Faction
            temp = iString.Substring(iString.IndexOf("Faction"));
            temp = temp.Substring(temp.IndexOf(':')+2);
            temp = temp.Remove(temp.IndexOf(',')-1);
            faction = temp;

            // Node
            temp = iString.Substring(iString.IndexOf("Node"));
            temp = temp.Substring(temp.IndexOf(':') + 2);
            temp = temp.Remove(temp.IndexOf(',') - 1);
            node = temp;

            // Count
            temp = iString.Substring(iString.IndexOf("Count"));
            temp = temp.Substring(temp.IndexOf(':') + 1);
            temp = temp.Remove(temp.IndexOf(','));
            {
                int j;
                if (Int32.TryParse(temp, out j))
                {
                    count = j;
                }
                else
                {
                    // Could not parse if this happened
                    count = -1;
                }
            }

            // Goal
            temp = iString.Substring(iString.IndexOf("Goal"));
            temp = temp.Substring(temp.IndexOf(':') + 1);
            temp = temp.Remove(temp.IndexOf(','));
            {
                int j;
                if (Int32.TryParse(temp, out j))
                {
                    goal = j;
                }
                else
                {
                    // Could not parse if this happened
                    goal = -1;
                }
            }

            // LocTag
            temp = iString.Substring(iString.IndexOf("LocTag"));
            temp = temp.Substring(temp.IndexOf(':') + 2);
            temp = temp.Remove(temp.IndexOf(',') - 1);
            locTag = temp;

            // Completed
            temp = iString.Substring(iString.IndexOf("Completed"));
            temp = temp.Substring(temp.IndexOf(':')+1);
            temp = temp.Remove(temp.IndexOf(','));
            completed = (temp.ToLower() == "true");

            // Tuple<string, int> attackerReward
            // Tuple<string, int> defenderReward

            // Attacker Ememy Faction
            temp = iString.Substring(iString.IndexOf("AttackerMissionInfo"));
            temp = temp.Substring(temp.IndexOf("faction"));
            temp = temp.Substring(temp.IndexOf(':') + 2);
            temp = temp.Remove(temp.IndexOf('}') - 1);
            AEF = temp;

            // Defender Ememy Faction
            temp = iString.Substring(iString.IndexOf("DefenderMissionInfo"));
            temp = temp.Substring(temp.IndexOf("faction"));
            temp = temp.Substring(temp.IndexOf(':') + 2);
            // Fix as invasions against infested controled areas contains missionReward inside DefenderMissionInfo while areas controled by the other groups don't.
            if (temp.IndexOf(',') > temp.IndexOf('}'))
            {
                temp = temp.Remove(temp.IndexOf('}') - 1);
            }
            else
            {
                temp = temp.Remove(temp.IndexOf(',') - 1);
            }
            DEF = temp;
        }

        public string printInvasion()
        {
            string tmp = "";

            // ID
            if (id != "Unknown")
            {
                tmp += "\nID: " + id;
            }

            // Faction
            if (faction != "Unknown")
            {
                tmp += "\nFaction: " + faction;
            }

            // Node
            if (node != "Unknown")
            {
                tmp += "\nNode: " + node;
            }

            // Count
            tmp += "\nCount: " + count;

            // Goal
            tmp += "\nGoal: " + goal;

            // locTag
            if (locTag != "Unknown")
            {
                tmp += "\nLocation Tag: " + locTag;
            }

            // bool completed
            tmp += "\nCompleted: " + completed;

            // Attacker Ememy Faction
            if (AEF != "Unknown")
            {
                tmp += "\nAttacker's Enemy Faction: " + AEF;
            }

            // Tuple<string, int> attackerReward
            if (attackerReward != null)
            {
                tmp += "\nAttacker's Reward: " + attackerReward.Item1 + " (" + attackerReward.Item2 + ")";
            }

            // Defender Ememy Faction
            if (DEF != "Unknown")
            {
                tmp += "\nDefender's Enemy Faction: " + DEF;
            }

            // Tuple<string, int> defenderReward
            if (defenderReward != null)
            {
                tmp += "\nDefender's Reward: " + defenderReward.Item1 + " (" + defenderReward.Item2 + ")";
            }

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
