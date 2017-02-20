using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Talorn
{
    public class Talorn_Core
    {
        public static String PC_URL = "http://content.warframe.com/dynamic/worldState.php";
        public static String XB1_URL = "http://content.xb1.warframe.com/dynamic/worldState.php";
        public static String PS4_URL = "http://content.ps4.warframe.com/dynamic/worldState.php";

        public static String[] DATA_CATEGORIES = {"\"Events\":[", "\"Goals\":[", "\"Alerts\":[", "\"Sorties\":[",
        "\"SyndicateMissions\":[", "\"ActiveMissions\":[", "\"GlobalUpgrades\":[", "\"FlashSales\":[",
        "\"Invasions\":[", "\"HubEvents\":[", "\"NodeOverrides\":[", "\"BadlandNodes\":[", "\"History\":[",
        "\"VoidTraders\":[", "\"PrimeVaultAvailabilities\":[", "\"DailyDeals\":[", "\"PVPChallengeInstances\":["};

        /*
        enum DataCategory { Events, Goals, Alerts, Sorties, SyndicateMissions, ActiveMissions, GlobalUpgrades,
            FlashSales, Invasions, HubEvents, NodeOverrides, BadlandNodes, History, VoidTraders, PrimeVaultAvailabilities,
            DailyDeals, PVPChallengeInstances
        };
        */

        public static String getRawData(String url)
        {
            string data = "no data -_-";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
            return data;
        }

        public static String getDataCategory(String data, String dataCategory)
        {
            String buffer = data.Substring(data.IndexOf(dataCategory));
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

        public static String[] SeperateX(String data)
        {
            List<String> buffer = new List<String>();

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

        public static int getEndOfJSON(String JSON_String, int start)
        {
            if (JSON_String.Length < start)
            {
                return -1;
            }

            char charStart = 'S';
            char charEnd = 'P';
            if (JSON_String[start] == '{')
            {
                charStart = '{';
                charEnd = '}';
            }
            else if (JSON_String[start] == '[')
            {
                charStart = '[';
                charEnd = ']';
            }
            else
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
    }
}