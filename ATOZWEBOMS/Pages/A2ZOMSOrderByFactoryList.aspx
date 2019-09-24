<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZOMSOrderByFactoryList.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSOrderByFactoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 300px;
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

    <div id="DivGridViewOrder" runat="server" align="center" style="height: 576px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="left" style="color: blue; width: 1200px;">

                            
                            <asp:Label ID="Label11" runat="server" Text="Steps : " Font-Size="Large" ForeColor="Red" Width="150px"></asp:Label>
                            <asp:DropDownList ID="ddlSteps" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" TabIndex="5">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Not Ready</asp:ListItem>
                                <asp:ListItem Value="2">Ready In Factory</asp:ListItem>

                            </asp:DropDownList>
                            <script type="text/javascript" src="../Script/chosen.jquery.js"></script>


                            <br />
                            <br />

                            <asp:Label ID="Label12" runat="server" Text=" Order Nos. By Factory Party - " ForeColor="Green" Height="25px" Font-Size="Large"></asp:Label>
                            <asp:Label ID="Label13" runat="server" Text="Date : " ForeColor="Green" Height="25px" Font-Size="Large"></asp:Label>
                            &nbsp;<asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                                Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="Label15" runat="server" Text=" to " ForeColor="Green" Height="25px" Font-Size="Large"></asp:Label>
                            &nbsp;<asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                                Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Size="Large" ForeColor="#FFFFCC"
                                Height="35px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                                CssClass="button blue" OnClick="BtnSearch_Click" />

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:Button ID="BtnPrint" runat="server" Text="Print" Font-Size="Large" ForeColor="#FFFFCC"
                                Height="35px" Width="86px" Font-Bold="True" ToolTip="Print" CausesValidation="False"
                                CssClass="button green" OnClick="BtnPrint_Click" />

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                                Height="35px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                                CssClass="button red" OnClick="BtnExit_Click" />

                            <asp:Button ID="BtnBack" runat="server" Text="Back" Font-Size="Large" ForeColor="#FFFFCC"
                                Height="35px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                                CssClass="button red" OnClick="BtnBack_Click" />

                        </p>




                        <asp:GridView ID="gvOrderInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="1500px">
                            <Columns>


                                <asp:TemplateField HeaderText="Order Party" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderParty" runat="server" Text='<%# Eval("OrderParty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="350px" ItemStyle-Width="350px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderPartyName" runat="server" Text='<%# Eval("OrderPartyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 1" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryParty1" runat="server" Visible="false" Text='<%# Eval("FactoryParty1") %>'></asp:Label>
                                        <asp:Label ID="lblFactoryPartyName1" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName1") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder1" runat="server" Text='<%#Eval("FactoryNoOrder1") %>' OnClick="BtnFactoryNoOrder1_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 2" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryParty2" runat="server" Visible="false" Text='<%# Eval("FactoryParty2") %>'></asp:Label>
                                        <asp:Label ID="lblFactoryPartyName2" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName2") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder2" runat="server" Text='<%#Eval("FactoryNoOrder2") %>' OnClick="BtnFactoryNoOrder2_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 3" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName3" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName3") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder3" runat="server" Text='<%#Eval("FactoryNoOrder3") %>' OnClick="BtnFactoryNoOrder3_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 4" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName4" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName4") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder4" runat="server" Text='<%#Eval("FactoryNoOrder4") %>' OnClick="BtnFactoryNoOrder4_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 5" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName5" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName5") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder5" runat="server" Text='<%#Eval("FactoryNoOrder5") %>' OnClick="BtnFactoryNoOrder5_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Factory 6" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName6" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName6") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder6" runat="server" Text='<%#Eval("FactoryNoOrder6") %>' OnClick="BtnFactoryNoOrder6_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 7" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName7" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName7") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder7" runat="server" Text='<%#Eval("FactoryNoOrder7") %>' OnClick="BtnFactoryNoOrder7_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 8" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName8" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName8") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder8" runat="server" Text='<%#Eval("FactoryNoOrder8") %>' OnClick="BtnFactoryNoOrder8_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 9" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName9" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName9") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder9" runat="server" Text='<%#Eval("FactoryNoOrder9") %>' OnClick="BtnFactoryNoOrder9_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory 10" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactoryPartyName10" runat="server" Width="80px" Text='<%# Eval("FactoryPartyName10") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="BtnFactoryNoOrder10" runat="server" Text='<%#Eval("FactoryNoOrder10") %>' OnClick="BtnFactoryNoOrder10_Click" Font-Size="X-Large" Height="65px" Width="65px" CssClass="button green"></asp:Button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>



                        <asp:GridView ID="gvOrderInfo1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="957px">
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

                                  <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-ForeColor="DarkBlue"/>

                                <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Karat" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCarat" runat="server" Text='<%#Eval("ItemKarat") %>' ></asp:Label>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Piece" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="DarkBlue" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemPiece" runat="server" Text='<%# Eval("ItemPiece") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               

                                <asp:TemplateField HeaderText="Total Weight" HeaderStyle-Width="150px" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="right" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoldWtInput" runat="server" Text='<%#Eval("ItemTotalWeight","{0:00.0000}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>


                    </th>
                </tr>
            </thead>
        </table>
    </div>



    




    <%-- <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Verify Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Verify" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>--%>


    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFromDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTillDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlOrderParty" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFactoryParty" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

