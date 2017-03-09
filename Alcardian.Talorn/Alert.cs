using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcardian.Talorn
{
    /// <summary>
    /// Data and methods related to Warframe Alerts.
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// The unique ID of the alert.
        /// </summary>
        private string id;
        //AlertTime activation;
        //AlertTime expiry;

        /// <summary>
        /// The time that the alert starts at.
        /// The value is default set to -1, meaning that it is unset. The value will be set to -2 if it could not be read.
        /// </summary>
        private long activation = -1;

        /// <summary>
        /// The time that the alert ends at.
        /// The value is default set to -1, meaning that it is unset. The value will be set to -2 if it could not be read.
        /// </summary>
        private long expiry = -1;

        /// <summary>
        /// Describe which type of a mission it is.
        /// The value is set to "Unknown" if it havn't tried or failed to identify the Mission Type.
        /// </summary>
        /// <remarks>MT_TERRITORY = Interception</remarks>
        private string missionType = "Unknown";

        /// <summary>
        /// Describe which faction you will be facing on the mission.
        /// The value is set to "Unknown" if it havn't tried or failed to identify the faction.
        /// </summary>
        private string faction = "Unknown";

        /// <summary>
        /// Shows the location of the mission.
        /// The value is set to "Unknown" if it havn't tried or failed to identify the location.
        /// </summary>
        private string location = "Unknown";

        /// <summary>
        /// Describes what the default mission area for the location have been overridden as.
        /// The value is set to "Unknown" if it havn't tried or failed to identify the level override.
        /// If the value is set to "Default", then it is unchanged from the standard for that location.
        /// </summary>
        private string levelOverride = "Unknown";

        //String enemySpec; // Unimplemented variable from the Warframe API data

        /// <summary>
        /// Shows the minimum level of enemies on the mission. Note that the minimum level will increase during endless missions such as defence and survival.
        /// The value is default set to -1, meaning that it is unset. The value will be set to -2 if it could not be read.
        /// </summary>
        private int minEnemyLevel = -1;

        /// <summary>
        /// Shows the maximum level of enemies on the mission. Note that the maximum level will increase during endless missions such as defence and survival.
        /// The value is default set to -1, meaning that it is unset. The value will be set to -2 if it could not be read.
        /// </summary>
        private int maxEnemyLevel = -1;

        //double difficulty;    // Unimplemented variable from the Warframe API data
        //int seed; // Unimplemented variable from the Warframe API data

        /// <summary>
        /// Indicates if this mission requires the player to have an Archwing.
        /// </summary>
        private bool archwingRequired = false;

        /// <summary>
        /// Indicates if this mission contains underwater areas that the Archwing can be used at.
        /// </summary>
        private bool sharkwingMission = false;

        /// <summary>
        /// Indicates the number of waves that the player needs to complete to finish the mission if it's a wave based mission such as defence.
        /// The value is set to -1 if isn't a wave based mission or -2 if it failed to read the value.
        /// </summary>
        /// <remarks>
        /// Used by mission types; Defence, Mobile Defence, Inteligence (Spy)
        /// </remarks>
        private int maxWaveNum = -1;

        /// <summary>
        /// Shows how much credits are earned by completing the mission.
        /// </summary>
        private int credits = 0;

        /// <summary>
        /// Shows the item rewards of the mission. The player will be rewarded with 1 of each item in the array.
        /// The value is set to null if it's unset.
        /// </summary>
        private string[] items = null;

        /// <summary>
        /// Shows the item rewards of the mission. The player will be rewarded with as many of the items as the number indicates.
        /// </summary>
        List<Tuple<string, int>> countedItems = new List<Tuple<string, int>>();

        /// <summary>
        /// Create an alert object with the values contained in a string with json data for one alert.
        /// </summary>
        /// <param name="alertString">A string that contains the json data of one alert.</param>
        public Alert(string alertString)
        {
            string temp = "";

            // ID
            temp = alertString.Substring(alertString.IndexOf("_id"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(temp.IndexOf('"'));
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            id = temp;

            // Activation
            temp = alertString.Substring(alertString.IndexOf("Activation"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            {
                long j;
                if (Int64.TryParse(temp, out j))
                {
                    activation = j;
                }
                else
                {
                    // Could not parse if this happened
                    activation = -2;
                }
            }

            // Expiry
            temp = alertString.Substring(alertString.IndexOf("Expiry"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            {
                long j;
                if (Int64.TryParse(temp, out j))
                {
                    expiry = j;
                }
                else
                {
                    // Could not parse if this happened
                    expiry = -2;
                }
            }

            // Mission Type
            temp = alertString.Substring(alertString.IndexOf("missionType"));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf(','));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            missionType = temp;

            // Faction
            temp = alertString.Substring(alertString.IndexOf("faction"));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf(','));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            faction = temp;

            // Location
            temp = alertString.Substring(alertString.IndexOf("location"));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf(','));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            location = temp;

            // Level Override
            // Not all alerts have it
            if (alertString.IndexOf("levelOverride") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("levelOverride"));
                temp = temp.Substring(temp.IndexOf(':'));
                temp = temp.Substring(1);
                temp = temp.Remove(temp.IndexOf(','));
                temp = temp.Remove(temp.Length - 1);
                temp = temp.Substring(temp.LastIndexOf('\"') + 1);
                levelOverride = temp;
            }
            else
            {
                levelOverride = "Default";
            }

            // Min Enemy Level
            if (alertString.IndexOf("\"minEnemyLevel\":") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("\"minEnemyLevel\":"));
                temp = temp.Substring(temp.IndexOf(':') + 1);
                temp = temp.Remove(temp.IndexOf(','));
                {
                    int j;
                    if (Int32.TryParse(temp, out j))
                    {
                        minEnemyLevel = j;
                    }
                    else
                    {
                        // Could not parse if this happened
                        minEnemyLevel = -1;
                    }
                }
            }

            // Max Enemy Level
            if (alertString.IndexOf("\"maxEnemyLevel\":") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("\"maxEnemyLevel\":"));
                temp = temp.Substring(temp.IndexOf(':') + 1);
                temp = temp.Remove(temp.IndexOf(','));
                {
                    int j;
                    if (Int32.TryParse(temp, out j))
                    {
                        maxEnemyLevel = j;
                    }
                    else
                    {
                        // Could not parse if this happened
                        maxEnemyLevel = -1;
                    }
                }
            }

            // Archwing
            if (alertString.IndexOf("archwingRequired") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("archwingRequired"));
                temp = temp.Substring(temp.IndexOf(':'));
                temp = temp.Substring(1);
                temp = temp.Remove(temp.IndexOf(','));
                archwingRequired = (temp.ToLower() == "true");
            }

            // Shardwing
            if (alertString.IndexOf("isSharkwingMission") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("isSharkwingMission"));
                temp = temp.Substring(temp.IndexOf(':'));
                temp = temp.Substring(1);
                temp = temp.Remove(temp.IndexOf(','));
                sharkwingMission = (temp.ToLower() == "true");
            }

            // Maximum Wave Numbers
            // Not all alerts have it
            if (alertString.IndexOf("\"maxWaveNum\":") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("\"maxWaveNum\":"));
                temp = temp.Substring(temp.IndexOf(':') + 1);
                temp = temp.Remove(temp.IndexOf(','));
                {
                    int j;
                    if (Int32.TryParse(temp, out j))
                    {
                        maxWaveNum = j;
                    }
                    else
                    {
                        // Could not parse if this happened
                        maxWaveNum = -2;
                    }
                }
            }

            //Mission Reward
            if (alertString.IndexOf("missionReward") > -1)
            {
                // MissionReward, Credits
                if (alertString.IndexOf("\"credits\":") > -1)
                {
                    temp = alertString.Substring(alertString.IndexOf("\"credits\":"));
                    temp = temp.Substring(temp.IndexOf(':'));
                    temp = temp.Substring(1);
                    if (temp.IndexOf(',') > -1)
                    {
                        temp = temp.Remove(temp.IndexOf(','));
                    }
                    else
                    {
                        temp = temp.Remove(temp.IndexOf('}'));
                    }
                    {
                        int j;
                        if (Int32.TryParse(temp, out j))
                        {
                            credits = j;
                        }
                        else
                        {
                            // Could not parse if this happened
                            credits = -2;
                        }
                    }
                }

                // MissionReward, Items
                if (alertString.IndexOf("\"items\":") > -1)
                {
                    temp = alertString.Substring(alertString.IndexOf("\"items\":"));
                    temp = temp.Substring(temp.IndexOf(':'));
                    temp = temp.Substring(1);
                    if (temp.IndexOf(',') > -1)
                    {
                        temp = temp.Remove(temp.IndexOf(','));
                    }
                    else
                    {
                        temp = temp.Remove(temp.IndexOf('}'));
                    }

                    if (temp.Length > 0 && temp[0] == '[')
                    {
                        temp = temp.Substring(1);
                    }
                    if (temp.Length > 0 && temp[temp.Length - 1] == ']')
                    {
                        temp = temp.Remove(temp.Length - 1);
                    }
                    items = temp.Split(',');
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i].IndexOf('\"') == 0)
                        {
                            items[i] = items[i].Substring(1);
                        }
                        if (items[i].LastIndexOf('\"') == (items[i].Length - 1))
                        {
                            items[i] = items[i].Remove(items[i].Length - 1);
                        }
                    }
                }

                // MissionReward, Counted Items
                if (alertString.IndexOf("\"countedItems\":") > -1)
                {
                    temp = alertString.Substring(alertString.IndexOf("\"countedItems\":"));
                    temp = temp.Substring(temp.IndexOf('['));

                    int c;
                    if ((c = Core.getEndOfJSON(temp, 0)) > -1)
                    {
                        temp = temp.Remove(c);
                        temp += "]";

                        string name = temp.Remove(temp.IndexOf(',') - 1); // Name = name of the item.
                        name = name.Substring(name.IndexOf("\"ItemType\":"));
                        name = name.Substring(name.IndexOf(':') + 2);

                        int count;  // Count = number of the item.
                        temp = temp.Substring(temp.IndexOf("\"ItemCount\":"));
                        temp = temp.Substring(temp.IndexOf(':') + 1);
                        temp = temp.Remove(temp.IndexOf('}'));
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
                        countedItems.Add(new Tuple<string, int>(name, count));
                    }
                }
            }
        }
        //
        /// <summary>
        /// Prints the alert data, ignores to print empty values
        /// </summary>
        public string printAlert()
        {
            string tmp = "ID: " + id;
            tmp += "\nActivation: " + activation;
            tmp += "\nExpiry: " + expiry;

            // MissionType
            if (missionType != "Unknown")
            {
                tmp += "\nExpiry: " + missionType;
            }

            // Faction
            if (faction != "Unknown")
            {
                tmp += "\nFaction: " + faction;
            }

            // Location
            if (location != "Unknown")
            {
                tmp += "\nLocation: " + location;
            }

            // Level Override
            if (levelOverride != "Unknown" && levelOverride != "Default")
            {
                tmp += "\nlevelOverride: " + levelOverride;
            }
            // Minimum Enemy Level
            if (minEnemyLevel != -1)
            {
                tmp += "\nMinimum Enemy Level: " + minEnemyLevel;
            }

            // Maximum Enemy Level
            if (maxEnemyLevel != -1)
            {
                tmp += "\nMaximum Enemy Level: " + maxEnemyLevel;
            }

            // Archwing
            if (archwingRequired)
            {
                tmp += "\nArchwing Required";
            }

            // Sharkwing Mission
            if (sharkwingMission)
            {
                tmp += "\nSharkwing Mission";
            }

            // Maximum Wave Number
            if (maxWaveNum != -1)
            {
                tmp += "\nMaximum Wave Number: " + maxWaveNum;
            }

            // Credits
            if (credits != 0)
            {
                tmp += "\nCredits: " + credits;
            }

            // Items
            if (items != null)
            {
                tmp += "\nItems: ";
                foreach (string item in items)
                {
                    tmp += item + ", ";
                }
                tmp = tmp.Remove(tmp.Length - 2);
            }

            // Counted Items
            if (countedItems.Count > 0)
            {
                tmp += "\nCounted Items: ";
                foreach (Tuple<string, int> item in countedItems)
                {
                    tmp += item.Item1 + ": " + item.Item2 + ", ";
                }
                tmp = tmp.Remove(tmp.Length - 2);
            }
            return tmp;
        }

        /// <summary>
        /// Returns the alert as HTML
        /// </summary>
        /// <returns></returns>
        public string HTML_Alert()
        {
            string buffer = "<p>";

            buffer += "<span><b>" + location + "</b> Level " + minEnemyLevel + "-" + maxEnemyLevel + "</span><br>";
            buffer += "<span><b>" + missionType + " - " + faction + "</b></span><br>";
            buffer += "<span> Reward: ";

            bool reward = false;

            // Credits
            if (credits != 0)
            {
                buffer += credits + " Credits, ";
                reward = true;
            }

            // Items
            if (items != null)  //If there are items...
            {
                for (int i = 0; i < items.Length; i++)
                {
                    buffer += items[i] + ", ";
                }
                reward = true;
            }

            // Counted Items
            for (int i = 0; i < countedItems.Count; i++)
            {
                buffer += countedItems[i].Item1 + "(" + countedItems[i].Item2 + "), ";
            }
            if (countedItems.Count > 0)
            {
                reward = true;
            }

            if (reward) //remove the comma at the end
            {
                buffer = buffer.Remove(buffer.Length-3);
            }
            buffer += "</span>";

            // Archwing Warning
            if (archwingRequired)
            {
                if (sharkwingMission)   // Sharkwing
                {
                    buffer += "<br><span><b>Sharkwing Mission!</b></span>";
                }
                else   // Normal Archwing
                {
                    buffer += "<br><span><b>Archwing Mission!</b></span>";
                }
            }

            buffer += "<br><span class=\"AlertTime\" data-starttime=" + activation + " data-endtime=" + expiry + ">" + "</span>";

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
        public void setExpiry(long Expiry)
        {
            expiry = Expiry;
        }
        public string getMissionType()
        {
            return missionType;
        }
        public void setMissionType(string MissionType)
        {
            missionType = MissionType;
        }
        public string getFaction()
        {
            return faction;
        }
        public void setFaction(string Faction)
        {
            faction = Faction;
        }
        public string getLocation()
        {
            return location;
        }
        public void setLocation(string Location)
        {
            location = Location;
        }
        public string getLevelOverride()
        {
            return levelOverride;
        }
        public void setLevelOverride(string LevelOverride)
        {
            levelOverride = LevelOverride;
        }
        public int getMinEnemyLevel()
        {
            return minEnemyLevel;
        }
        public void setMinEnemyLevel(int MinEnemyLevel)
        {
            minEnemyLevel = MinEnemyLevel;
        }
        public int getMaxEnemyLevel()
        {
            return maxEnemyLevel;
        }
        public void setMaxEnemyLevel(int MaxEnemyLevel)
        {
            maxEnemyLevel = MaxEnemyLevel;
        }
        public bool isArchwingRequired()
        {
            return archwingRequired;
        }
        public void setArchwingRequired(bool ArchwingRequired)
        {
            archwingRequired = ArchwingRequired;
        }
        public bool isSharkwingMission()
        {
            return sharkwingMission;
        }
        public void setSharkwingMission(bool SharkwingMission)
        {
            sharkwingMission = SharkwingMission;
        }
        public int getMaxWaveNumber()
        {
            return maxWaveNum;
        }
        public void setMaxWaveNumber(int MaxWaveNumber)
        {
            maxWaveNum = MaxWaveNumber;
        }
        public int getCredits()
        {
            return credits;
        }
        public void setCredits(int Credits)
        {
            credits = Credits;
        }
        public string[] getItemRewards()
        {
            return items;
        }
        public void setItemRewards(string[] ItemRewards)
        {
            items = ItemRewards;
        }
        public List<Tuple<string, int>> getCountedItemRewards()
        {
            return countedItems;
        }
        public void setCountedItemRewards(List<Tuple<string, int>> CountedItemRewards)
        {
            countedItems = CountedItemRewards;
        }
    }
}
