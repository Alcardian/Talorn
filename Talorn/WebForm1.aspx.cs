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
            //this.TextField.InnerText = Talorn_Core.test();
        }

        protected void Button_Alert_Click(object sender, EventArgs e)
        {
            this.TextField.InnerText = "";
            //string[] temp = Talorn_Core.SeperateX(Talorn_Core.getDataCategory(Talorn_Core.getRawData(Talorn_Core.PC_URL), Talorn_Core.DATA_CATEGORIES[2]));
            string[] temp = Core.SeperateX(Core.getDataCategory(Talorn_Core.getRawData(Core.PC_URL), Core.DATA_CATEGORIES[2]));
            string buffer = "";

            for (int i = 0; i < temp.Length; i++)
            {
                buffer += "Raw\n" + temp[i] + "\n\n\n";
                Alert alert = new Alert(temp[i]);
                buffer += "Extracted data\n";
                buffer += alert.printAlert() + "\n\n\n\n\n\n";
            }
            buffer = buffer.Remove(buffer.Length-6);
            this.TextField.InnerText = buffer;
        }

        protected void Button_Invasion_Click(object sender, EventArgs e)
        {
            this.TextField.InnerText = "";
            string[] temp = Talorn_Core.SeperateX(Talorn_Core.getDataCategory(Talorn_Core.getRawData(Talorn_Core.PC_URL), Talorn_Core.DATA_CATEGORIES[8]));
            string buffer = "";

            for (int i = 0; i < temp.Length; i++)
            {
                buffer += "Raw\n" + temp[i] + "\n\n";
                buffer += "Class length: " + temp[i].Length + "\n" +
                    "End of class index: " + Talorn_Core.getEndOfJSON(temp[i], 0) + "\n\n\n\n\n\n";
            }
            buffer = buffer.Remove(buffer.Length - 6);
            this.TextField.InnerText = buffer;
        }
    }
}