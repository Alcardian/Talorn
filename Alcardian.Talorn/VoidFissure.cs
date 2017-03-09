using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcardian.Talorn
{
    public class VoidFissure
    {
        private string id = "Unknown";
        private int region = -1;
        private int seed = -1;
        private long activation = -1;
        private long expiry = -1;
        private string node = "Unknown";
        private string modifier = "Unknown";

        /// <summary>
        /// Create an invasion object with the values contained in a string with json data for one void fissure.
        /// </summary>
        /// <param name="vfString">A string that contains the json data of one void fissure.</param>
        public VoidFissure(string vfString)
        {
            // ID
            id = Core.getDataValue_s(vfString.Substring(vfString.IndexOf("_id")), "{");

            // Region
            region = Core.getDataValue_i(vfString, "\"Region\":");

            // Seed
            seed = Core.getDataValue_i(vfString, "\"Seed\":");

            // Activation
            //activation = Core.getDataValue_long(vfString.Substring(vfString.IndexOf("\"Activation\""))+13, "{");
            //Had to use this implementation as the method doesn't work for these numbers....
            string temp = vfString.Substring(vfString.IndexOf("\"Activation\""));
            temp = temp.Substring(temp.IndexOf("$numberLong"));
            temp = temp.Substring(temp.IndexOf(":")+2);
            temp = temp.Remove(temp.IndexOf("\""));
            {
                long j;
                if (Int64.TryParse(temp, out j))
                {
                    activation = j;
                }
                else
                {
                    // Could not parse if this happened
                    activation = -1;
                }
            }

            // Expiry
            //expiry = Core.getDataValue_long(vfString.Substring(vfString.IndexOf("\"Expiry\"")) + 10, "{");
            //Had to use this implementation as the method doesn't work for these numbers....
            temp = vfString.Substring(vfString.IndexOf("\"Expiry\""));
            temp = temp.Substring(temp.IndexOf("$numberLong"));
            temp = temp.Substring(temp.IndexOf(":") + 2);
            temp = temp.Remove(temp.IndexOf("\""));
            {
                long j;
                if (Int64.TryParse(temp, out j))
                {
                    expiry = j;
                }
                else
                {
                    // Could not parse if this happened
                    expiry = -1;
                }
            }

            // Node
            node = Core.getDataValue_s(vfString, "\"Node\":");

            // Modifier
            modifier = Core.getDataValue_s(vfString, "\"Modifier\":");

        }
        //public string printVoidFissure()
        public string HTML_VoidFissure()
        {
            string buffer = "<p>";
            buffer += "<span><b>" + "Node: " + node + " - Region: " + region + "</b></span>";
            buffer += "<br><span><b>" + "[MissionType] || Seed: " + seed+ "</b></span>";
            buffer += "<br><span>" + modifier + "</span>";
            buffer += "<br><span class=\"VoidFTime\" data-starttime=" + activation + " data-endtime=" + expiry + ">" + "</span>";
            buffer += "</p>";

            return buffer;
        }

        public string getID()
        {
            return id;
        }
        public void setID(string ID)
        {
            id = ID;
        }
        public int getRegion()
        {
            return region;
        }
        public void setRegion(int Region)
        {
            region = Region;
        }
        public int getSeed()
        {
            return seed;
        }
        public void setSeet(int Seed)
        {
            seed = Seed;
        }
        public long getActivation()
        {
            return activation;
        }
        public void setActivation(long Activation)
        {
            activation = Activation;
        }
        public long getExpiry()
        {
            return expiry;
        }
        public string getNode()
        {
            return node;
        }
        public void setNode(string Node)
        {
            node = Node;
        }
        public string getModifier()
        {
            return modifier;
        }
        public void setModifier(string Modifier)
        {
            modifier = Modifier;
        }
    }
}
