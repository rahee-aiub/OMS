<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZOMSExtractJpgSummary.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSExtractJpgSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/html2canvas.js"></script>
    <script src="../scripts/amount.js" type="text/javascript"></script>
    <script src="../scripts/validation.js" type="text/javascript"></script>
    <script src="../scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>

    <link href="../Styles/TableStyle1.css" rel="stylesheet" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" />
    <link href="../Styles/style.css" rel="stylesheet" />
    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" />
    <link href="../mydesign/design.css" rel="stylesheet" />


    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlOrderNo.ClientID %>").chosen();


            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
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


    <script type="text/javascript">
        function ConvertToImage(btnSaveImage) {

            html2canvas(document.getElementById("dvJPG"), {

                onrendered: function (canvas) {
                    var base64 = canvas.toDataURL();
                    $("[id*=hfImageData]").val(base64);
                    __doPostBack(btnSaveImage.name, "");

                }
            });
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">




            <table class="style1">
                <thead>
                    <tr>
                        <th colspan="4">Extract Image - Summary</th>
                    </tr>
                </thead>

                <tr>

                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Order No :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>

                        <asp:DropDownList ID="ddlOrderNo" runat="server" CssClass="cls text" Width="170px" Height="35px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>

                        <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                    </td>
                </tr>



                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSaveImage" runat="server" Text="Download" UseSubmitBehavior="false"
                            Font-Size="Large" ForeColor="White"
                            Font-Bold="True" CssClass="button green"
                            OnClientClick="return ConvertToImage(this)" Height="27px" OnClick="btnSaveImage_Click" />


                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


                        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                            Font-Bold="True" CausesValidation="False"
                            CssClass="button red" OnClick="BtnExit_Click" Height="27px" />
                    </td>
                </tr>

            </table>

            <div id="dvJpgTable" runat="server">
                <table id="dvJPG">
                    <tr>
                        <td colspan="2">
                            <asp:Image ID="ImgPicture" runat="server" Style="margin-top: 4px;" Width="400px" />
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="Label22" runat="server" Text="SIZE - " Font-Size="Large"></asp:Label>
                        </td>

                        <td>
                            <asp:Label ID="lblSizeJPG" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="Label2" runat="server" Text="PER PCS WEIGHT - " Font-Size="Large"></asp:Label>
                        </td>

                        <td>
                            <asp:Label ID="lblPerPcsWeight" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>

                            <asp:Label ID="Label4" runat="server" Text="TOTAL WEIGHT - " Font-Size="Large"></asp:Label>
                        </td>

                        <td>
                            <asp:Label ID="lblTotalWeight" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label38" runat="server" Font-Size="Large" Text="COLOR - "></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lblColorJPG" runat="server"></asp:Label>

                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="ORDER NO - " Font-Size="Large" Style="text-align: right"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblOrderNo" runat="server" BorderColor="Black"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="ORDER DATE - " Font-Size="Large"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="COMMENT - " Font-Size="Large"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomerCommentBox" runat="server" Width="207px" TextMode="MultiLine"></asp:TextBox>

                        </td>
                    </tr>

                </table>

            </div>

            <br />
        </div>
        <asp:HiddenField ID="hfImageData" runat="server" />
        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblOrderPartyJPG" runat="server" Visible="False"></asp:Label>

    </form>
</body>
</html>
