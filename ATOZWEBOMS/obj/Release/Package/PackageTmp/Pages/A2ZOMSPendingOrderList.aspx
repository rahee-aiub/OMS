<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZOMSPendingOrderList.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSPendingOrderList" %>

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


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlSteps.ClientID %>").chosen();
            $("#<%= ddlFactoryParty.ClientID %>").chosen();
            $("#<%= ddlOrderParty.ClientID %>").chosen();
            $("#<%= ddlTransitParty.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlSteps.ClientID %>").chosen();
                $("#<%= ddlFactoryParty.ClientID %>").chosen();
                $("#<%= ddlOrderParty.ClientID %>").chosen();
                $("#<%= ddlTransitParty.ClientID %>").chosen();

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
            $("#<%= txtfdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtfdate.ClientID %>").datepicker();

            });

        });

        $(function () {
            $("#<%= txttdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txttdate.ClientID %>").datepicker();

            });

        });

    </script>

    
    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/chosen.css" rel="stylesheet" />
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

    <div id="DivButton" runat="server" align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th>
                        <p align="left" style="color: blue; width: 1242px;">

                            <asp:CheckBox ID="chkAllFactoryParty" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllFactoryParty_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                            &nbsp;
                            <asp:Label ID="Label4" runat="server" Text="Factory Party : " Font-Size="Large" ForeColor="Red" Width="150px"></asp:Label>
                            <asp:DropDownList ID="ddlFactoryParty" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" TabIndex="5">
                                
                            </asp:DropDownList>
                            <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            
                           
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            
                           <asp:CheckBox ID="chkAllOrderParty" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllOrderParty_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                            &nbsp;
                            <asp:Label ID="Label5" runat="server" Text="Order Party : " Font-Size="Large" ForeColor="Red" Width="150px"></asp:Label>
                            <asp:DropDownList ID="ddlOrderParty" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" TabIndex="5">
                                
                            </asp:DropDownList>
                            <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            
                            <br />
                            <asp:CheckBox ID="chkAllTransitParty" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllTransitParty_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                            &nbsp;
                            <asp:Label ID="Label6" runat="server" Text="Transit Party : " Font-Size="Large" ForeColor="Red" Width="150px"></asp:Label>
                            <asp:DropDownList ID="ddlTransitParty" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" TabIndex="5">
                                
                            </asp:DropDownList>
                            <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            
                           
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            
                           <asp:CheckBox ID="chkAllSteps" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllSteps_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                            &nbsp;
                            <asp:Label ID="Label10" runat="server" Text="Steps : " Font-Size="Large" ForeColor="Red" Width="150px"></asp:Label>
                            <asp:DropDownList ID="ddlSteps" runat="server" CssClass="cls text" Width="300px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" TabIndex="5">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">Receive Order</asp:ListItem>
                                <asp:ListItem Value="2">Ready In Factory</asp:ListItem>
                                <asp:ListItem Value="3">Transit</asp:ListItem>
                                <asp:ListItem Value="4">Deliver</asp:ListItem>
                            </asp:DropDownList>
                            <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            
                            <br />
                             <br />

                            <asp:Label ID="Label3" runat="server" Text=" Pending Order List - " ForeColor="Green" Height="25px" Font-Size="Large"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="Date : " ForeColor="Green" Height="25px" Font-Size="Large"></asp:Label>
                            &nbsp;<asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                                Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" Text=" to " ForeColor="Green" Height="25px" Font-Size="Large"></asp:Label>
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


                        </p>

                    </th>
                </tr>
            </thead>
        </table>




        <%--
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>--%>
    </div>

    <div align="center">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Image1" runat="server" Style="margin-top: 4px;" Height="250px" Width="250px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage1")) %>' OnClick="Image1_Click" />
                        <br />
                        <asp:Label ID="lblOrderPartyName1" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("OrderPartyName1") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemName1" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("ItemName1") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemInfo1" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("ItemInfo1") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderNo1" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("OrderNo1") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderModeName1" runat="server" Font-Size="X-Large" Font-Bold="true" Width="250px" BackColor="Yellow" ForeColor="DarkBlue" Text='<%# Eval("OrderModeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Image2" runat="server" Style="margin-top: 4px;" Height="250px" Width="250px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage2")) %>' OnClick="Image2_Click" />
                        <br />
                        <asp:Label ID="lblOrderPartyName2" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("OrderPartyName2") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemName2" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("ItemName2") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemInfo2" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("ItemInfo2") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderNo2" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("OrderNo2") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderModeName2" runat="server" Font-Size="X-Large" Font-Bold="true" Width="250px" BackColor="Yellow" ForeColor="DarkBlue" Text='<%# Eval("OrderModeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Image3" runat="server" Style="margin-top: 4px;" Height="250px" Width="250px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage3")) %>' OnClick="Image3_Click" />
                        <br />
                        <asp:Label ID="lblOrderPartyName3" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("OrderPartyName3") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemName3" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("ItemName3") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemInfo3" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("ItemInfo3") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderNo3" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("OrderNo3") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderModeName3" runat="server" Font-Size="X-Large" Font-Bold="true" Width="250px" BackColor="Yellow" ForeColor="DarkBlue" Text='<%# Eval("OrderModeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Image4" runat="server" Style="margin-top: 4px;" Height="250px" Width="250px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage4")) %>' OnClick="Image4_Click" />
                        <br />
                        <asp:Label ID="lblOrderPartyName4" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("OrderPartyName4") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemName4" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("ItemName4") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemInfo4" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("ItemInfo4") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderNo4" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("OrderNo4") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderModeName4" runat="server" Font-Size="X-Large" Font-Bold="true" Width="250px" BackColor="Yellow" ForeColor="DarkBlue" Text='<%# Eval("OrderModeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Image5" runat="server" Style="margin-top: 4px;" Height="250px" Width="250px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage5")) %>' OnClick="Image5_Click" />
                        <br />
                        <asp:Label ID="lblOrderPartyName5" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("OrderPartyName5") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemName5" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("ItemName5") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblItemInfo5" runat="server" Font-Bold="true" ForeColor="Red" Text='<%# Eval("ItemInfo5") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderNo5" runat="server" Font-Bold="true" ForeColor="Black" Text='<%# Eval("OrderNo5") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblOrderModeName5" runat="server" Font-Size="X-Large" Font-Bold="true" Width="250px" BackColor="Yellow" ForeColor="DarkBlue" Text='<%# Eval("OrderModeName") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>



    <%-- <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Verify Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Verify" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>--%>


    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

