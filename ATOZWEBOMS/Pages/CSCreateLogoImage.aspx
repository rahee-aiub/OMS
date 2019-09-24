<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSCreateLogoImage.aspx.cs" Inherits="ATOZWEBOMS.Pages.CSCreateLogoImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div align="center">

        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Create Logo Image
                    </th>
                </tr>

            </thead>
            

           
           
            <tr>
                <td>
                    <asp:Label ID="lblUpload" runat="server" Text="Upload Logo Image" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" ForeColor="#FF3300" />
                    <asp:ImageButton ID="ibtnUpload" runat="server" ImageUrl="~/Images/uploadicon.jpg" Width="60px" Height="51px" OnClick="ibtnUpload_Click" />
                     <asp:ImageButton ID="BtnUpdate" runat="server" ImageUrl="~/Images/update.jpg" Width="60px" Height="51px" OnClick="BtnUpdate_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlImage" runat="server" Height="260px" Width="350px">
                        <asp:Image ID="ImgPicture" runat="server" Height="256px" ImageUrl="~/Images/index.jpg" Width="387px" />
                        <asp:ImageButton ID="ibtnDelete" runat="server" Height="26px" ImageUrl="~/Images/delete_user.png" Style="position: relative; top: 2px; left: 8px;" OnClick="ibtnDelete_Click" Width="41px" />

                    </asp:Panel>
                </td>

            </tr>
        </table>
    </div>
    <div align="center">
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
            Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
    </div>


     <asp:Label ID="lblMemType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMemNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUnitFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBranchNo" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
