using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alcardian.Talorn
{
    public class Core
    {
        /// <summary>
        /// URL to the Warframe API for the PC platform.
        /// </summary>
        public static string PC_URL = "http://content.warframe.com/dynamic/worldState.php";

        /// <summary>
        /// URL to the Warframe API for the X-Box 1 platform.
        /// </summary>
        public static string XB1_URL = "http://content.xb1.warframe.com/dynamic/worldState.php";

        /// <summary>
        /// URL to the Warframe API for the Playstation 4 platform.
        /// </summary>
        public static string PS4_URL = "http://content.ps4.warframe.com/dynamic/worldState.php";

        /// <summary>
        /// Text strings for the different data categories that are returned from the Warframe API.
        /// </summary>
        public static string[] DATA_CATEGORIES = {"\"Events\":[", "\"Goals\":[", "\"Alerts\":[", "\"Sorties\":[",
        "\"SyndicateMissions\":[", "\"ActiveMissions\":[", "\"GlobalUpgrades\":[", "\"FlashSales\":[",
        "\"Invasions\":[", "\"HubEvents\":[", "\"NodeOverrides\":[", "\"BadlandNodes\":[", "\"History\":[",
        "\"VoidTraders\":[", "\"PrimeVaultAvailabilities\":[", "\"DailyDeals\":[", "\"PVPChallengeInstances\":["};

        /*
        enum DataCategory { Events, Goals, Alerts, Sorties, SyndicateMissions, ActiveMissions, GlobalUpgrades,
            FlashSales, Invasions, HubEvents, NodeOverrides, BadlandNodes, History, VoidTraders, PrimeVaultAvailabilities,
            DailyDeals, PVPChallengeInstances
        };
        */

        /// <summary>
        /// Returns the Warframe API data from the location that the URL points to.
        /// </summary>
        /// <param name="url">The web URL to the Warframe API</param>
        /// <returns></returns>
        public static string getRawData(string url)
        {
            string data = "no data -_-";

            // TODO: Rewritte to work on all platforms
            /**
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    //readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    readStream = new StreamReader(receiveStream, Encoding.UTF8);
                }
                data = readStream.ReadToEnd();
                readStream.Close();
            }
            response.Close();
            **/
            return data;
        }

        /// <summary>
        /// Extracts data belonging to a single category from the data string.
        /// </summary>
        /// <param name="data">The raw data</param>
        /// <param name="dataCategory">Select a category from DATA_CATEGORIES.</param>
        /// <returns></returns>
        public static string getDataCategory(string data, string dataCategory)
        {
            string buffer = data.Substring(data.IndexOf(dataCategory));
            buffer = buffer.Substring(buffer.IndexOf('[') + 1);

            int count = 1;
            int i = 0;
            while (count != 0 && i < buffer.Length)
            {
                if (buffer[i] == '[')
                {
                    count++;
                }
                else if (buffer[i] == ']')
                {
                    count--;
                }
                i++;
            }
            return buffer.Remove(i - 1);
        }

        /// <summary>
        /// Seperate all JSON objects on the same level and return the seperated items as an array.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] SeperateX(string data)
        {
            List<string> buffer = new List<string>();

            int count = 0;
            int lastMark = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '{')
                {
                    count++;
                }
                else if (data[i] == '}')
                {
                    count--;
                    if (count == 0)
                    {
                        if (i + 1 == data.Length)
                        {
                            buffer.Add(data.Substring(lastMark));
                        }
                        else
                        {
                            buffer.Add(data.Remove(i + 1).Substring(lastMark));
                        }
                        //lastMark = i;
                        lastMark = i + 2;
                    }
                }
            }

            return buffer.ToArray();
        }

        /// <summary>
        /// Finds the index of the end of the JSON array or class.
        /// </summary>
        /// <param name="JSON_String">A text string that contains the JSON data.</param>
        /// <param name="start">Index of the location that the JSON array or class starts at.</param>
        /// <returns>Index of the end of the JSON array or class.
        /// Returns -1 if start points to a higher number than the JSON_String's length
        /// Returns -2 if the start index doesn't point to an array or class.
        /// Returns -3 if the method fails to find the end of the JSON array or class.</returns>
        public static int getEndOfJSON(string JSON_String, int start)
        {
            if (JSON_String.Length < start)
            {
                return -1;
            }

            char charStart = 'S';   // Random start value :P
            char charEnd = 'P'; // Random start value :P
            if (JSON_String[start] == '{')  // True if the start index points to a class.
            {
                charStart = '{';
                charEnd = '}';
            }
            else if (JSON_String[start] == '[') // True if the start index points to an array.
            {
                charStart = '[';
                charEnd = ']';
            }
            else // The start index doesn't point to an array or class.
            {
                return -2;
            }

            int level = 0;  //indicates how many subclasses deep it currently is. 1 means just within the class and not any subclass
            for (int i = start; i < JSON_String.Length; i++)
            {
                if (JSON_String[i] == charStart)
                {
                    level++;
                }
                else if (JSON_String[i] == charEnd)
                {
                    level--;
                }
                if (level == 0)
                {
                    return i;
                }
            }
            return -3;
        }

        /// <summary>
        /// Extracts the value of 
        /// </summary>
        /// <param name="dString"></param>
        /// <param name="data">the data type / keyword</param>
        /// <returns></returns>
        public static string getDatavalue_s(string dString, string data)
        {
            string temp = "";
            temp = dString.Substring(dString.IndexOf(data));
            temp = temp.Substring(temp.IndexOf(':') + 2);
            //temp = temp.Remove(temp.IndexOf(',') - 1);

            int a = temp.IndexOf(',');
            int b = temp.IndexOf('}');
            int c = temp.IndexOf(']');
            if ((a > -1) && ((a < b || b < 0) && (a < c || c < 0)))
            {
                temp = temp.Remove(a - 1);
            }
            else if ((b > -1) && (b < c || c < 0))
            {
                temp = temp.Remove(b - 1);
            }
            else if (c < 0)
            {
                temp = temp.Remove(c - 1);
            }
            else
            {
                temp = "Unknown";
            }
            return temp;
        }
    }
}
