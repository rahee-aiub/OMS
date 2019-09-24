<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZOMSOrderDelivery.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSOrderDelivery" Title="Order Delivery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
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
            /*width: 850px;*/
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
            $("#<%= ddlDeliveryParty.ClientID %>").chosen();
            $("#<%= ddlOrderNo.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlDeliveryParty.ClientID %>").chosen();
                $("#<%= ddlOrderNo.ClientID %>").chosen();

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
    <br />

    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Order Delivery/Sale
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Delivery Party : " Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:DropDownList ID="ddlDeliveryParty" runat="server" CssClass="cls text" Width="400px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="5">
                    </asp:DropDownList>
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                </td>
            </tr>
        </table>



        <br />


        <%-- <asp:Panel ID="pnlProperty" runat="server" Height="320px">--%>
        <table class="style1" width="300px">
            <tr>

                <td>
                    <h6>
                        <asp:Label ID="Label1" runat="server" Text="Order No." Font-Size="Large" Width="260px" ForeColor="Red"></asp:Label></h6>
                    <asp:DropDownList ID="ddlOrderNo" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="550px" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged">
                    </asp:DropDownList>
                     <script type="text/javascript" src="../Script/chosen.jquery.js"></script>

                </td>

                <td>
                    <h6>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label3" runat="server" Text="Gross Wt" Font-Size="Large" Width="260px" ForeColor="Red"></asp:Label></h6>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtGrossWt" runat="server" TabIndex="2" Width="150px" Height="25px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large" onkeypress="return IsDecimalKey(event)"
                                        onFocus="javascript:this.select()" AutoPostBack="true" OnTextChanged="txtGrossWt_TextChanged"></asp:TextBox>
                </td>


            </tr>


            <tr>

                <td colspan="2">
                    <div align="center" class="grid_scroll" style="height: 250px;">
                        <asp:GridView ID="gvDetails" runat="server" HeaderStyle-BackColor="DarkBlue"
                            AutoGenerateColumns="False" AlternatingRowStyle-BackColor="DarkBlue" RowStyle-Height="10px" EnableModelValidation="True" OnRowDeleting="gvItemDetails_RowDeleting" Width="543px">
                            <HeaderStyle BackColor="DarkBlue" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No." HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnKeyNo" runat="server" Text='<%# Eval("RefAccNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="GrossWt" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnAmount" runat="server" Text='<%# Eval("TrnAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="70px" HeaderText="Action" ItemStyle-Width="70px">
                                    <ControlStyle Font-Bold="True" ForeColor="#FF3300" />
                                </asp:CommandField>

                            </Columns>


                        </asp:GridView>
                    </div>
                </td>
            </tr>

        </table>
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Whom Receive : " ForeColor="Red" Font-Size="Large"></asp:Label>


                    <asp:TextBox ID="txtDeliveryWhomReceive" runat="server" CssClass="cls text" MaxLength="43" Width="293px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Note : " ForeColor="Red" Font-Size="Large"></asp:Label>


                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


                    <asp:TextBox ID="txtTrnNote" runat="server" CssClass="cls text" MaxLength="43" Width="493px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>

                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

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









    <asp:Label ID="lblLastLPartyNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblProcessDate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblNewLPartyNo" runat="server" Visible="False"></asp:Label>
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

    <asp:Label ID="lblAccBalance" runat="server" Visible="False"></asp:Label>

    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
