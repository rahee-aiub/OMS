<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZEditPartyCode.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZEditPartyCode" Title="Edit Party Open" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Edit Party Code
                    </th>
                </tr>

            </thead>
        
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Group :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="cls text" Width="316px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="11">Factory Party</asp:ListItem>
                        <asp:ListItem Value="12">Transit Party</asp:ListItem>
                        <asp:ListItem Value="13">Delivery Party</asp:ListItem>
                        
                        

                    </asp:DropDownList>
                </td>
                

            </tr>
             
             <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Party Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="100px" Height="25px" MaxLength="6" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtPartyCode_TextChanged" autocomplete="off"></asp:TextBox>

                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" >
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Party Name:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPartyName" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" autocomplete="off"></asp:TextBox>
                </td>
            </tr>

              <tr>
                <td>
                    <asp:Label ID="lblAdd1" runat="server" Text="Address Line 1 :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtPartyAddressL1" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" autocomplete="off"></asp:TextBox>
                </td>
            </tr>

              <tr>
                <td>
                    <asp:Label ID="lblAdd2" runat="server" Text="Address Line 2 :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtPartyAddressL2" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" autocomplete="off"></asp:TextBox>
                </td>
            </tr>

              <tr>
                <td>
                    <asp:Label ID="lblAdd3" runat="server" Text="Address Line 3 :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtPartyAddressL3" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" autocomplete="off"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblMobile" runat="server" Text="Mobile No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtMobileNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                       onkeypress="return IsDecimalKey(event)"  Font-Size="Large" onkeydown="return (event.keyCode !=13);" autocomplete="off"></asp:TextBox>

                </td>
               
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="E-mail :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPartyEmail" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" autocomplete="off"></asp:TextBox>
                    &nbsp;</td>
            </tr>

           
          
            <tr>
               <td>

               </td>
                <td>
                  
                    
                    <asp:Button ID="btnSubmit" runat="server" Text="Update"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" Height="27px" OnClick="btnSubmit_Click" />
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClick="btnCancel_Click" Height="27px" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" Height="27px" />
                    <br />

                </td>
            </tr>
        </table>
    </div>

    


    <div align="center">
        <table>
            <thead>
                <tr>
                    <th>
                        <h2 style="position: absolute; bottom: 30px; left: 35%;">Developed By AtoZ Computer Services - Version 1.0<br />
                            Last Update: June, 2018.</h2>
                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <asp:Label ID="lblLastLPartyNo" runat="server" Visible="False"></asp:Label>
  

     <asp:Label ID="hdnNewAccNo" runat="server" Visible="False"></asp:Label>
    
     <asp:Label ID="CrlMsgFlag" runat="server" Visible="False"></asp:Label>

</asp:Content>
