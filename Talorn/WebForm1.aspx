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
</body>
</html>
