<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZOMSOrderCancelFactory.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSOrderCancelFactory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlFactoryParty.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlFactoryParty.ClientID %>").chosen();

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

        function ApproveValidation() {
            return confirm('Do you want to Approve Data?');
        }
        function SelectValidation() {
            return confirm('Do you want to Select Data?');
        }
        function RejectValidation() {
            return confirm('Do you want to Reject Data?');
        }
    </script>

    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>

    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 850px;
        }

        .grid1_scroll {
            overflow: auto;
            height: 550px;
            width: 750px;
            margin: 0 auto;
        }

        .grid_scroll2 {
            overflow: auto;
            height: 300px;
            width: 1200px;
            margin: 0 auto;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <br />
    <br />



    <div id="DivGridViewCancle" runat="server" align="center" style="height: 260px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="center" style="color: blue">
                            Order Cancel From Factory  - Spooler&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                                Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                                CssClass="button red" OnClick="BtnExit_Click" />


                        </p>
                        <asp:GridView ID="gvOrderInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="957px">
                            <Columns>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnSelect" runat="server" Text="Select" OnClick="BtnSelect_Click" Width="68px" CssClass="button green" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Order No." ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-ForeColor="DarkBlue" />

                                <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Karat" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCarat" runat="server" Text='<%#Eval("ItemKarat") %>'></asp:Label>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Piece" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemPiece" runat="server" Text='<%# Eval("ItemPiece") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Total Weight" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoldWtInput" runat="server" Text='<%#Eval("ItemTotalWeight","{0:00.0000}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Party" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderParty" runat="server" Text='<%# Eval("PartyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderStatusDesc" runat="server" Text='<%# Eval("OrderStatusDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                        </asp:GridView>


                    </th>
                </tr>
            </thead>
        </table>
    </div>



    <div id="dvJpgTable" align="center" runat="server">

        <table>
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Order No. : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label22" runat="server" Text="Order Party : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOrderPartyJPG" runat="server"></asp:Label>
                                &nbsp;
                        <asp:Label ID="lblOrderPartyName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Order Date : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOrderDate" runat="server" Width="170px"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Delivery Possible Date :" ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDeliverypossibleDateJPG" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Way to Order : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWayToOrderJPG" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label24" runat="server" Text="Whose Order : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWhoseOrderJPG" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label26" runat="server" Text="Phone No.: " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWhoseOrderPhoneNoJPG" runat="server"></asp:Label>
                            </td>

                            <%-- <td colspan="2">
                    <asp:Image ID="ImgPicture" runat="server" Style="margin-top: 4px; height: 300px; width: 300px" />
                </td>--%>

                            <td>
                                <asp:Label ID="Label28" runat="server" Text="Item Name : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblItemNameJPG" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label4" runat="server" Text="Karat : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblItemKaratJPG" runat="server"></asp:Label>
                            </td>

                            <td class="auto-style1">
                                <asp:Label ID="Label30" runat="server" Text="Size : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblSizeJPG" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Length : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLengthJPG" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label34" runat="server" Text="Piece : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPieceJPG" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="Wide : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWideJPG" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label38" runat="server" Text="Color : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblColorJPG" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label40" runat="server" Text="Weight Per Piece : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWeightPerPieceJPG" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label25" runat="server" Text="Total Weight : " ForeColor="Green" Font-Size="Large"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalWeightJPG" runat="server"></asp:Label>
                            </td>
                        </tr>

                    </table>

                    <table class="style1">

                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Factory Name : " ForeColor="Green" Font-Size="Large"></asp:Label>

                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlFactoryParty" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" TabIndex="5">
                                </asp:DropDownList>
                                <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Note : " ForeColor="Green" Font-Size="Large"></asp:Label>

                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtRcvOrderDesc" runat="server" CssClass="cls text" Width="693px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                </td>
                <td>


                    <asp:Image ID="ImgPicture" runat="server" Style="margin-top: 4px;" Height="254px" Width="265px" />

                </td>
            </tr>
        </table>

        <div id="Div1" runat="server" align="center">
            <table>
                <tr>
                    <td colspan="6" align="center">


                        <asp:Button ID="btnCancelOrder" runat="server" Text="Reject" Font-Size="Large" ForeColor="#FFFFCC"
                            Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                            CssClass="button red" OnClick="btnCancelOrder_Click" />
                        &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button blue" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>










    <%-- <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Verify Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Verify" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>--%>


    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

