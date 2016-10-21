﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoFill.aspx.cs" Inherits="FillWS_ASPNET.DemoFill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body bgcolor="#E6E6FA">
    <form id="DemoFill" runat="server">
        <div style="border:groove;border-color:#336600;width:482px;">            

            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <th colspan="2">FILL</th>
                </tr>
                <tr>
                    <td>Key
                    </td>
                    <td>
                        <asp:TextBox ID="txtKey" Autocomplete="off" Width="400px" runat="server" Text="Specificare una chiave per il servizio FILL"  />
                    </td>


                </tr>
                <tr>
                    <td>Provincia</td>
                    <td>
                        <asp:TextBox ID="txtProv" Autocomplete="off" Width="30px" runat="server" />
                    </td>

                </tr>
                <tr>
                    <td>Comune
                    </td>
                    <td>
                        <asp:TextBox ID="txtComune" autocomplete="off" Width="400px" runat="server" />
                    </td>

                </tr>
                <tr>
                    <td>Cap</td>
                    <td>
                        <asp:TextBox ID="txtCap" autocomplete="off" Width="50px" runat="server" />
                    </td>

                </tr>
                <tr>
                    <td>Frazione
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrazione" autocomplete="off" Width="400px" runat="server" />
                    </td>

                </tr>
                <tr>
                    <td>Indirizzo</td>
                    <td>
                        <asp:TextBox ID="txtIndirizzo" autocomplete="off" Width="400px" runat="server" />
                    </td>

                </tr>
            </table>

        </div>
        <p>
            <asp:Button ID="btnCallFill" runat="server" OnClick="btnCallVerify_Click" Text="Call FILL" />
        </p>
      
            <div id="outArea" runat="server" style="width:482px;"/>
    </form>
</body>
</html>
