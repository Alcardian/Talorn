<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Talorn.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Talorn - Warframe News</title>
    <style type="text/css">
        #TextField {
            width: 800px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <textarea id="TextField" name="S1" runat="server" rows="20" placeholder="Test"></textarea><asp:Button ID="Button_Alert" runat="server" OnClick="Button_Alert_Click" Text="Alert" />
        <asp:Button ID="Button_Invasion" runat="server" OnClick="Button_Invasion_Click" Text="Invasion" />
        </div>
    </form>
    <div style="width: 800px" id="Display_Alert" runat="server">
        <span class="alertX" data-starttime="1488801024" data-endtime="1488803399" style="background-color: green;">

        </span>
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
