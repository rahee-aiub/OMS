<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ATOZWEBGMS.Pages.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />


    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
