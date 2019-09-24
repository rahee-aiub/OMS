<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZOMSViewAllOrder.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSViewAllOrder" %>

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
            height: 500px;
            width: 730px;            
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
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

   <div align="center">
       <div class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-BackColor="YellowGreen" 
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                                
                    <asp:TemplateField HeaderText="Action" HeaderStyle-ForeColor="DarkBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnDetails" runat="server" Text="Details" OnClick="BtnDetails_Click" Width="68px" CssClass="button green" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Order No" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server"  Text='<%# Eval("OrderNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyCode" runat="server"  Text='<%# Eval("PartyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                
                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblHeadCode" runat="server"  Text='<%# Eval("ItemName") %> '></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                 <asp:TemplateField HeaderText="Size" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblItemSize" runat="server"  Text='<%# Eval("ItemSize") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
             
                 <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalWeight" runat="server"  Text='<%# Eval("ItemTotalWeight") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Image">
                    <ItemTemplate>
                       <%--<asp:Image ID="ImgPicture" runat="server" Style="margin-top: 4px;" Height="150px" Width="150px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage")) %>' />--%>
                        <asp:ImageButton ID="Image1" runat="server" Style="margin-top: 4px;" Height="150px" Width="150px" ImageUrl='<%#"data:image;base64," + Convert.ToBase64String((byte [])Eval("ItemImage")) %>' OnClick="Image1_Click"/>

                    </ItemTemplate>
                </asp:TemplateField>



                

                
                
              </Columns>
          
        </asp:GridView>
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

