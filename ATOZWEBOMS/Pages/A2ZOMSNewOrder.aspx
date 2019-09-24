﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZOMSNewOrder.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSNewOrder" Title="New Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>


    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are You Sure You Want to Update Information?');
        }

        
    </script>



    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlOrderParty.ClientID %>").chosen();
            $("#<%= ddlItemName.ClientID %>").chosen();
            $("#<%= ddlKarat.ClientID %>").chosen();
            $("#<%= ddlWayToOrder.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlOrderParty.ClientID %>").chosen();
                $("#<%= ddlItemName.ClientID %>").chosen();
                $("#<%= ddlKarat.ClientID %>").chosen();
                $("#<%= ddlWayToOrder.ClientID %>").chosen();

            });

        });

    </script>

    <script language="javascript" type="text/javascript">
        $(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $(".youpii").chosen();
            });
        });


    </script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtDeliveryPossibleDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtDeliveryPossibleDate.ClientID %>").datepicker();

                });

        });
    </script>
    <script language="javascript" type="text/javascript">
        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;


        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />

    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">New Order
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Order Party :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:DropDownList ID="ddlOrderParty" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="5">
                    </asp:DropDownList>
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Image" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />

                    <%--<asp:ImageButton ID="ibtnUpload" runat="server" ImageUrl="~/Images/uploadicon.jpg" Width="29px" Height="26px" OnClick="ibtnUpload_Click" />--%>

                    <asp:ImageButton ID="ibtnUpload" runat="server" Text="Upload" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True"
                        CssClass="button blue" Height="27px" ImageUrl="~/Images/uploadicon.jpg" OnClick="ibtnUpload_Click" />

                </td>

            </tr>
            <tr>

                <td>
                    <asp:Label ID="Label7" runat="server" Text="Whose Order :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWhoseOrder" runat="server" CssClass="cls text" Width="290px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ></asp:TextBox>
                    &nbsp; &nbsp; &nbsp; 
                </td>

               
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Way to Order :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:DropDownList ID="ddlWayToOrder" runat="server" CssClass="cls text" Width="170px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="5">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Whatsapp</asp:ListItem>
                        <asp:ListItem Value="2">Email</asp:ListItem>
                    </asp:DropDownList>
                     <script type="text/javascript" src="../Script/chosen.jquery.js"></script>

                    <asp:TextBox ID="txtWayToOrderNo" runat="server" CssClass="cls text" Width="250px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>


                </td>
                

            </tr>


            

            <tr>

                <td>
                    <asp:Label ID="Label21" runat="server" Text="Phone No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="cls text" Width="290px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ></asp:TextBox>
                    &nbsp; &nbsp; &nbsp; 
                </td>

                 <td>
                    <asp:Label ID="Label1" runat="server" Text="Delivery Possible Date:" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDeliveryPossibleDate" runat="server" CssClass="cls text" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>


                </td>

                


                

            </tr>

        </table>
        <br />
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Item Description
                    </th>
                </tr>
            </thead>
            <tr>
                <td style="vertical-align: top">
                    <table class="style1">

                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Item Name :" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="cls text" TabIndex="1" Width="170px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" >

                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <%--<asp:ListItem Value="1">Bala</asp:ListItem>
                                    <asp:ListItem Value="2">Churi</asp:ListItem>
                                    <asp:ListItem Value="3">Neclace</asp:ListItem>
                                    <asp:ListItem Value="4">Diamond</asp:ListItem>--%>
                                </asp:DropDownList>

                                <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Karat" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlKarat" runat="server" CssClass="cls text" TabIndex="2" Width="170px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="24">24k</asp:ListItem>
                                    <asp:ListItem Value="22">22k</asp:ListItem>
                                    <asp:ListItem Value="21">21k</asp:ListItem>
                                    <asp:ListItem Value="18">18k</asp:ListItem>
                                </asp:DropDownList>
                                <script type="text/javascript" src="../Script/chosen.jquery.js"></script>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Size" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSize" runat="server" CssClass="cls text" TabIndex="3" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Length" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLength" runat="server" CssClass="cls text" TabIndex="4" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Piece" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPiece" runat="server" CssClass="cls text" TabIndex="5" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="true" OnTextChanged="txtPiece_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Wide" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWide" runat="server" CssClass="cls text" TabIndex="6" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Color" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtColor" runat="server" CssClass="cls text" TabIndex="7" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Weight Per Piece" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWeight" runat="server" CssClass="cls text" TabIndex="8" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="true" OnTextChanged="txtWeight_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Total Weight" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalWeight" runat="server" CssClass="cls text" TabIndex="9" Width="165px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </td>


                <td>
                    <div id="divImage" style="height: 300px; width: 300px">
                        <%--<asp:Image ID="ImgPicture" runat="server" ImageUrl="~/Images/index.jpg" Style="margin-top: 4px; height: 300px; width: 300px" />--%>
                        <asp:Image ID="ImgPicture" runat="server" Style="margin-top: 4px; height: 300px; width: 300px" />
                    </div>

                </td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <%-- <asp:Button ID="btnDeleteImage" runat="server" Text="Delete" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" Height="27px" OnClick="btnDeleteImage_Click" />--%>

                    <asp:ImageButton ID="btnDeleteImage" runat="server" Text="Upload" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True"
                        CssClass="button blue" Height="27px" ImageUrl="~/Images/delete_user.png" OnClick="btnDeleteImage_Click" />
                </td>
            </tr>

            <tr>

                <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Button ID="btnUpdate" runat="server" Text="Update"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" Height="27px" OnClick="btnUpdate_Click" />
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


    <div id="DivUpdateMSG" runat="server">
        <table style="width: 340px; height: 130px; background-color: #e9e9e9;">

            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblMsg1" runat="server" Font-Bold="true"></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblMsg2" runat="server" Font-Bold="true"></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:Button ID="btnUpdMsg" runat="server" Text="OK"
                        Height="27" Width="96px" OnClick="btnUpdMsg_Click" />

                </td>
            </tr>
        </table>
    </div>



    <asp:Label ID="lblLastLPartyNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblProcessDate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblNewOrderNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnNewAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="ctrlNewAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPartyAccType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPartyAccno" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlVoucherNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlProcDate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCurrencyCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblMsgFlag" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblCtrlOrderNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label15" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label16" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label17" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label18" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label19" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label20" runat="server" Visible="False"></asp:Label>

    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
