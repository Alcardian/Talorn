using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            String[] temp = Talorn_Core.SeperateX(Talorn_Core.getDataCategory(Talorn_Core.getRawData(Talorn_Core.PC_URL), Talorn_Core.DATA_CATEGORIES[2]));
            //this.TextField.InnerText = "Objects: " + temp.Length + "   Data: " + temp[temp.Length - 1];
            //this.TextField.InnerText = "Objects: " + temp.Length + "   Data: " + temp[0];

            for (int i = 0; i < temp.Length; i++)
            {
                this.TextField.InnerText += "Raw\n" + temp[i] + "\n\n\n";

                Talorn_Alert alert = new Talorn_Alert(temp[i]);
                this.TextField.InnerText += "Extracted data\n";
                this.TextField.InnerText += alert.printAlert() + "\n\n\n\n\n\n";
            }

            

            //this.TextField.InnerText += (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
            //this.TextField.InnerText += Talorn_Core.test();
        }

        protected void Button_Invasion_Click(object sender, EventArgs e)
        {
            this.TextField.InnerText = "";
            String[] temp = Talorn_Core.SeperateX(Talorn_Core.getDataCategory(Talorn_Core.getRawData(Talorn_Core.PC_URL), Talorn_Core.DATA_CATEGORIES[8]));

            for (int i = 0; i < temp.Length; i++)
            {
                this.TextField.InnerText += "Raw\n" + temp[i] + "\n\n";
                this.TextField.InnerText += "Class length: " + temp[i].Length + "\n" +
                    "End of class index: " + Talorn_Core.getEndOfJSON(temp[i], 0) + "\n\n\n\n\n\n";
            }
        }
    }
}