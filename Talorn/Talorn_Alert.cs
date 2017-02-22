using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talorn
{
    public class Talorn_Alert
    {
        private string id;
        //AlertTime activation;
        //AlertTime expiry;
        private long activation = -1;
        private long expiry = -1;

        private string missionType = "Unknown";
        private string faction = "Unknown";
        private string location = "Unknown";
        private string levelOverride = "Unknown";
        //String enemySpec;
        private int minEnemyLevel = -1;
        private int maxEnemyLevel = -1;
        //double difficulty;
        //int seed;
        private bool archwingRequired = false;
        private bool sharkwingMission = false;
        private int maxWaveNum = 0;

        private int credits = 0;
        private string[] items = null;
        //List<string> items = new List<string>();
        List<Tuple<string, int>> countedItems = new List<Tuple<string, int>>();

        public Talorn_Alert(String alertString)
        {
            //List<string> buffer = new List<string>();
            string temp = "";

            //ID
            temp = alertString.Substring(alertString.IndexOf("_id"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(temp.IndexOf('"'));
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            //buffer.Add("ID: " + temp);
            id = temp;

            //Activation
            temp = alertString.Substring(alertString.IndexOf("Activation"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            //buffer.Add("Activation: " + temp);
            {
                long j;
                if (Int64.TryParse(temp, out j))
                {
                    activation = j;
                }
                else
                {
                    //Could not parse if this happened
                    activation = -2;
                }
            }

            //Expiry
            temp = alertString.Substring(alertString.IndexOf("Expiry"));
            temp = temp.Substring(temp.IndexOf('{'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf('}'));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            //buffer.Add("Expiry: " + temp);
            {
                long j;
                if (Int64.TryParse(temp, out j))
                {
                    expiry = j;
                }
                else
                {
                    //Could not parse if this happened
                    expiry = -2;
                }
            }

            //Mission Type
            temp = alertString.Substring(alertString.IndexOf("missionType"));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf(','));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            //buffer.Add("Mission Type: " + temp);
            missionType = temp;

            //faction
            temp = alertString.Substring(alertString.IndexOf("faction"));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf(','));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            //buffer.Add("Faction: " + temp);
            faction = temp;

            //location
            temp = alertString.Substring(alertString.IndexOf("location"));
            temp = temp.Substring(temp.IndexOf(':'));
            temp = temp.Substring(1);
            temp = temp.Remove(temp.IndexOf(','));
            temp = temp.Remove(temp.Length - 1);
            temp = temp.Substring(temp.LastIndexOf('\"') + 1);
            //buffer.Add("Location: " + temp);
            location = temp;

            //levelOverride, not always there
            if (alertString.IndexOf("levelOverride") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("levelOverride"));
                temp = temp.Substring(temp.IndexOf(':'));
                temp = temp.Substring(1);
                temp = temp.Remove(temp.IndexOf(','));
                temp = temp.Remove(temp.Length - 1);
                temp = temp.Substring(temp.LastIndexOf('\"') + 1);
                //buffer.Add("levelOverride: " + temp);
                levelOverride = temp;
            }

            //int minEnemyLevel
            if (alertString.IndexOf("\"minEnemyLevel\":") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("\"minEnemyLevel\":"));
                temp = temp.Substring(temp.IndexOf(':')+1);
                temp = temp.Remove(temp.IndexOf(','));
                {
                    int j;
                    if (Int32.TryParse(temp, out j))
                    {
                        minEnemyLevel = j;
                    }
                    else
                    {
                        //Could not parse if this happened
                        minEnemyLevel = -1;
                    }
                }
            }

            //int maxEnemyLevel
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
                        //Could not parse if this happened
                        maxEnemyLevel = -1;
                    }
                }
            }

            //Archwing
            if (alertString.IndexOf("archwingRequired") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("archwingRequired"));
                temp = temp.Substring(temp.IndexOf(':'));
                temp = temp.Substring(1);
                temp = temp.Remove(temp.IndexOf(','));
                //buffer.Add("archwingRequired: " + temp);
                archwingRequired = (temp.ToLower() == "true");
            }

            //shardwing
            if (alertString.IndexOf("isSharkwingMission") > -1)
            {
                temp = alertString.Substring(alertString.IndexOf("isSharkwingMission"));
                temp = temp.Substring(temp.IndexOf(':'));
                temp = temp.Substring(1);
                temp = temp.Remove(temp.IndexOf(','));
                //buffer.Add("isSharkwingMission: " + temp);
                sharkwingMission = (temp.ToLower() == "true");
            }

            //MissionReward
            //if (false)
            if (alertString.IndexOf("missionReward") > -1)
            {
                //MissionReward, credits
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
                    //buffer.Add("Credtis: " + temp);
                    {
                        int j;
                        if (Int32.TryParse(temp, out j))
                        {
                            credits = j;
                        }
                        else
                        {
                            //Could not parse if this happened
                            credits = -2;
                        }
                    }
                }

                //MissionReward, items
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
                    //buffer.Add("Items: " + temp);
                    items = temp.Split(',');
                    for (int i=0; i<items.Length; i++)
                    {
                        if (items[i].IndexOf('\"') == 0)
                        {
                            items[i] = items[i].Substring(1);
                        }
                        if(items[i].LastIndexOf('\"') == (items[i].Length - 1))
                        {
                            items[i] = items[i].Remove(items[i].Length-1);
                        }
                    }
                }

                //MissionReward, counteditems
                if (alertString.IndexOf("\"countedItems\":") > -1)
                {
                    temp = alertString.Substring(alertString.IndexOf("\"countedItems\":"));
                    temp = temp.Substring(temp.IndexOf('['));

                    int c;
                    if ((c = Talorn_Core.getEndOfJSON(temp, 0)) > -1)
                    {
                        temp = temp.Remove(c);
                        temp += "]";

                        string name = temp.Remove(temp.IndexOf(',')-1);
                        name = name.Substring(name.IndexOf("\"ItemType\":"));
                        name = name.Substring(name.IndexOf(':')+2);

                        int count;
                        temp = temp.Substring(temp.IndexOf("\"ItemCount\":"));
                        temp = temp.Substring(temp.IndexOf(':')+1);
                        temp = temp.Remove(temp.IndexOf('}'));
                        {
                            int j;
                            if (Int32.TryParse(temp, out j))
                            {
                                count = j;
                            }
                            else
                            {
                                //Could not parse if this happened
                                count = -1;
                            }
                        }

                        countedItems.Add(new Tuple<string, int>(name, count));
                    }
                }
            }
        }

        // Prints the alert data
        public string printAlert()
        {
            string tmp = "ID: " + id;
            tmp += "\nActivation: " + activation;
            tmp += "\nExpiry: " + expiry;

            //string missionType;
            if (missionType != "Unknown")
            {
                tmp += "\nExpiry: " + missionType;
            }

            //string faction;
            if (faction != "Unknown")
            {
                tmp += "\nFaction: " + faction;
            }

            //string location;
            if (location != "Unknown")
            {
                tmp += "\nLocation: " + location;
            }

            //string levelOverride;
            if (levelOverride != "Unknown")
            {
                tmp += "\nlevelOverride: " + levelOverride;
            }
            //int minEnemyLevel;
            if (minEnemyLevel != -1)
            {
                tmp += "\nMinimum Enemy Level: " + minEnemyLevel;
            }

            //int maxEnemyLevel;
            if (maxEnemyLevel != -1)
            {
                tmp += "\nMaximum Enemy Level: " + maxEnemyLevel;
            }

            //bool archwingRequired = false;
            if (archwingRequired)
            {
                tmp += "\nArchwing Required";
            }

            //bool sharkwingMission = false;
            if (sharkwingMission)
            {
                tmp += "\nSharkwing Mission";
            }
            //int maxWaveNum = 0;

            //int credits = 0;
            if (credits != 0)
            {
                tmp += "\nCredits: " + credits;
            }

            //string[] items;
            if (items != null)
            {
                tmp += "\nItems: ";
                foreach (string item in items)
                {
                    tmp += item + ", ";
                }
                tmp = tmp.Remove(tmp.Length-2);
            }

            //List<Tuple<string, int>> countedItems;
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