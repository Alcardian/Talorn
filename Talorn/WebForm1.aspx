<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Talorn.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Talorn - Warframe News</title>
    <style type="text/css">
        #TextField {
            width: 800px;
        }
        .Warper{
            float:left;
            padding:0em 1em 0em;
            max-width:100%;
            white-space:nowrap;
            border-style: solid;
            border-width: 1px;
            margin-left: 1em;
        }
        .Warper2{
            clear: left;
            padding:1em 0em 0em 0em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <textarea id="TextField" name="S1" runat="server" rows="20" placeholder="Test"></textarea>
        <asp:Button ID="Button_All" runat="server" Text="All" OnClick="Button_All_Click" />
        <asp:Button ID="Button_Alert" runat="server" OnClick="Button_Alert_Click" Text="Alert" />
        <asp:Button ID="Button_Invasion" runat="server" OnClick="Button_Invasion_Click" Text="Invasion" />
        </div>
    </form>
    <div class="Warper2">
        <div id="Display_Alert" class="Warper" runat="server">
            <h2>Alerts</h2>
            <span class="alertX"></span>
        </div>
        <div id="Display_Invasion" class="Warper" runat="server">
            <h2>Invasion</h2>
            <span class="invasionX"></span>
        </div>
    </div>
    <div class="Warper2">
        <div id="Display_Void_Fissures" class="Warper" runat="server">
            <h2>Void Fissures</h2>
            <span class="voidFissurex"></span>
        </div>
    </div>

    <script>
// Update the count down every 1 second
var x = setInterval(function() {

    var alerts = document.getElementsByClassName("AlertTime");
    
    var d = new Date();
    var millisec = d.getTime() - (d.getTimezoneOffset() * 60000);
    
    var i;
    for (i = 0; i< alerts.length; i++){
    	var j = (parseInt(alerts[i].getAttribute("data-endtime")) - d.getTime())/1000;
        if(parseInt(alerts[i].getAttribute("data-starttime")) > d.getTime()){
        	alerts[i].innerHTML = "Starts in: " + toHHMMSS(Math.floor((parseInt(alerts[i].getAttribute("data-starttime")) - d.getTime())/1000));
        }
        else if(j < 0){
        	alerts[i].innerHTML = "Ends in: -" + toHHMMSS(Math.floor((j)*-1));
        }
        else{
        	alerts[i].innerHTML = "Ends in: " + toHHMMSS(Math.floor(j));
        }
    }
}, 1000);
</script>
<script>
function toHHMMSS(seconds) {
    var h, m, s, result='';
    // HOURs
    h = Math.floor(seconds/3600);
    seconds -= h*3600;
    if(h){
        result = h<10 ? '0'+h+':' : h+':';
    }
    // MINUTEs
    m = Math.floor(seconds/60);
    seconds -= m*60;
    result += m<10 ? '0'+m+':' : m+':';
    // SECONDs
    s=seconds%60;
    result += s<10 ? '0'+s : s;
    return result;
}
</script>
</body>
</html>
