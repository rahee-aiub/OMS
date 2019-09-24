<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZERPMenuFunction.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZERPMenuFunction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }
    </style>

    <style type="text/css">
        .stbutton {
            text-align: left;
        }
    </style>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 500px;
            /*width: 520px;*/
            width: 100%;
        }

        .grid_scroll1 {
            overflow: auto;
            height: 350px;
            /*width: 520px;*/
            width: 1480px;
        }


        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 490px;*/
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
    </style>


    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div id="divMain" align="left" runat="server" class="grid_scroll">
        <table class="style1">

            <%--<asp:Repeater ID="rptAccordian" runat="server">
                <ItemTemplate>--%>
            <asp:GridView ID="gvMainInfo" runat="server" HeaderStyle-BackColor="YellowGreen"
                AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvMainInfo_RowDataBound">
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <Columns>

                 <%--   <asp:TemplateField HeaderText="HeadCode" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblHeadCode" runat="server" Visible="false" Text='<%# Eval("DetailFuncCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SubHeadCode" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubHeadCode" runat="server" Visible="false" Text='<%# Eval("FuncDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <%--     <tr>
                                <td colspan="100%">
                                    <div id="div" style="overflow: auto; display: none; position: relative; left: 15px; overflow: auto">
                                        <asp:GridView ID="gvAst" runat="server" Width="760px" AutoGenerateColumns="false">
                                            <Columns>

                                              
                                            </Columns>
                                            <HeaderStyle BackColor="#95B4CA" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>--%>



                    <asp:TemplateField HeaderText="DetailCode" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblDetailCode" runat="server" Visible="false" Text='<%# Eval("DetailFuncCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="MainInformation" runat="server" Style="text-align: left" ItemStyle-HorizontalAlign="left" Text='<%# Eval("FuncDescription") %>' Height="40" Width="100%" CssClass="button green" />
                        </ItemTemplate>
                    </asp:TemplateField>


                  <%--<asp:TemplateField HeaderText="Rec Flag" Visible="false"  HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordsFlag" runat="server" Visible="false"  Text='<%# Eval("RecordsFlag") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
          
        </table>
    </div>


    <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGrpItemType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGrpType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewOld" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblItemType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCaratPrice" runat="server" Text="" Visible="false"></asp:Label>
    <asp:TextBox ID="txtHdnNext" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHdnPrv" runat="server" Visible="false"></asp:TextBox>

    <asp:TextBox ID="lblItemNo" runat="server" Visible="false"></asp:TextBox>

    <asp:TextBox ID="lblItmStatus" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lblItemNoControl" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblLineTitle" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblLineDesc" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblFunc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVch" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDataName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPayTypeDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCredit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDebit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAmount" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGrpDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCarat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblQty" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGoldWt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblKeyNo" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
