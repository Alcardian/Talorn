using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alcardian.Talorn;

namespace Talorn
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();
        }

        protected void Button_Alert_Click(object sender, EventArgs e)
        {
            this.TextField.InnerText = "";
            this.Display_Alert.InnerHtml = "<h2>Alerts</h2>";
            //string[] temp = Talorn_Core.SeperateX(Talorn_Core.getDataCategory(Talorn_Core.getRawData(Talorn_Core.PC_URL), Talorn_Core.DATA_CATEGORIES[2]));
            string[] temp = Core.SeperateX(Core.getDataCategory(Talorn_Core.getRawData(Core.PC_URL), Core.DATA_CATEGORIES[2]));
            string buffer = "";

            for (int i = 0; i < temp.Length; i++)
            {
                buffer += "Raw\n" + temp[i] + "\n\n\n";
                Alert alert = new Alert(temp[i]);
                buffer += "Extracted data\n";
                buffer += alert.printAlert() + "\n\n\n\n\n\n";
                this.Display_Alert.InnerHtml += alert.HTML_Alert();
            }
            buffer = buffer.Remove(buffer.Length-6);
            this.TextField.InnerText = buffer;
            //this.Display_Alert.InnerHtml = "<span style=\"background-color: green;\">Test</span>";
        }

        protected void Button_Invasion_Click(object sender, EventArgs e)
        {
            this.TextField.InnerText = "";
            string[] temp = Core.SeperateX(Core.getDataCategory(Talorn_Core.getRawData(Core.PC_URL), Core.DATA_CATEGORIES[8]));
            string buffer = "";

            for (int i = 0; i < temp.Length; i++)
            {
                buffer += "Raw\n" + temp[i] + "\n\n";
                buffer += "Extracted data\n" + new Invasion(temp[i]).printInvasion() + "\n\n\n\n\n\n";
                //buffer += "Class length: " + temp[i].Length + "\n" +
                //    "End of class index: " + Core.getEndOfJSON(temp[i], 0) + "\n\n\n\n\n\n";
            }
            buffer = buffer.Remove(buffer.Length - 6);
            this.TextField.InnerText = buffer;
        }

        protected void loadData()
        {
            string rawData = Talorn_Core.getRawData(Core.PC_URL);
            //string[] temp = Core.SeperateX(Core.getDataCategory(Talorn_Core.getRawData(Core.PC_URL), Core.DATA_CATEGORIES[2]));
            this.TextField.InnerText = "";
            this.Display_Alert.InnerHtml = "<h2>Alerts</h2>";
            this.Display_Invasion.InnerHtml = "<h2>Invasions</h2>";
            this.Display_Void_Fissures.InnerHtml = "<h2>Void Fissures</h2>";

            // Alert
            string[] temp = Core.SeperateX(Core.getDataCategory(rawData, Core.DATA_CATEGORIES[2]));
            for (int i = 0; i < temp.Length; i++)
            {
                Alert alert = new Alert(temp[i]);
                this.Display_Alert.InnerHtml += alert.HTML_Alert();
            }

            // Invasion
            temp = Core.SeperateX(Core.getDataCategory(rawData, Core.DATA_CATEGORIES[8]));
            for (int i = 0; i < temp.Length; i++)
            {
                Invasion invasion = new Invasion(temp[i]);
                if (!invasion.isCompleted())
                {
                    this.Display_Invasion.InnerHtml += invasion.HTML_Invasion();
                }
            }

            // Void Fissure
            temp = Core.SeperateX(Core.getDataCategory(rawData, Core.DATA_CATEGORIES[5]));
            //temp = Core.SeperateX(Core.getDataCategory(rawData, Core.DATA_CATEGORIES[8]));
            string buffer = "";
            //buffer += temp.Length + "__:";
            for (int i = 0; i < temp.Length; i++)
            {
                //this.Display_Void_Fissures.InnerHtml += temp[i] + "\n\n";
                VoidFissure voidFissure = new VoidFissure(temp[i]);
                this.Display_Void_Fissures.InnerHtml += voidFissure.HTML_VoidFissure();
                //buffer += temp[i] + "<br><br>";
            }
            //this.Display_Void_Fissures.InnerHtml += buffer;
        }

        protected void Button_All_Click(object sender, EventArgs e)
        {
            this.TextField.InnerText = "";
            //string[] temp = Core.SeperateX(Core.getDataCategory(Talorn_Core.getRawData(Core.PC_URL), Core.DATA_CATEGORIES[2]));
            //string[] temp = Core.SeperateX(Talorn_Core.getRawData(Core.PC_URL));
            string[] temp = Core.SeperateX(Talorn_Core.getRawData(Core.PC_URL).Substring(1));
            string buffer = "";

            for (int i = 0; i < temp.Length; i++)
            {
                buffer += "Raw\n" + temp[i] + "\n\n\n\n\n\n";
            }
            buffer = buffer.Remove(buffer.Length - 6);
            this.TextField.InnerText = buffer;
        }
    }
}