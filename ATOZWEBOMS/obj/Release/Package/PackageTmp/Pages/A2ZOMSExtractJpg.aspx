<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A2ZOMSExtractJpg.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOMSExtractJpg" %>

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
                        <th colspan="4">Extract Image - Details
                        </th>
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
                                            
                   <asp:RadioButton ID="rbtWithParty" runat="server" GroupName="GLRpt30OptPrm" Text="With Party Name" style="font-weight: 700" Font-Italic="True" Checked="True" AutoPostBack="true" OnCheckedChanged="rbtWithParty_CheckedChanged"  />

                    &nbsp;&nbsp;<asp:RadioButton ID="rbtWithoutParty" runat="server" GroupName="GLRpt30OptPrm" Text="Without Party Name" style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtWithoutParty_CheckedChanged"  />   
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
                            <asp:Label ID="lblOrderPartyName" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Order No. :" Font-Size="Large"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblOrderNo" runat="server" BorderColor="Black" BorderStyle="Solid"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Pendig Days :" Font-Size="Large"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblPendingDays" runat="server" BorderColor="Black" BorderStyle="Solid"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Order Date :" Font-Size="Large"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblOrderDate" runat="server" BorderStyle="Solid"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="D/L Date :" Font-Size="Large"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblDeliverypossibleDateJPG" runat="server" BorderStyle="Solid"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>

                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Size" Font-Size="Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSizeJPG" runat="server" BorderStyle="Solid"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Pcs" Font-Size="Large"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblPieceJPG" runat="server" BorderStyle="Solid"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" Text="Item Name:" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblItemNameJPG" runat="server" BorderStyle="Solid"></asp:Label>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Size="Large" Text="wide"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWideJPG" runat="server" BorderStyle="Solid"> </asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" Font-Size="Large" Text="Color : "></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblColorJPG" runat="server" BorderStyle="Solid"></asp:Label>

                                    </td>
                                </tr>

                            </table>


                        </td>
                        <td>
                            <asp:Image ID="ImgPicture" runat="server" Style="margin-top: 4px; height: 300px; width: 300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Customer Comment Box : " Font-Size="Large"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomerCommentBox" runat="server" Width="207px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="Order Way Details : " Font-Size="Large"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWayToOrderJPG" runat="server"></asp:Label>
                            /
                            <asp:Label ID="lblWayToOrderPhoneJPG" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Factory Name : " Font-Size="Large" BorderStyle="Solid"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFactoryName" runat="server" BorderStyle="Solid"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Size="Large" Text="Factory Receive Status"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblFactoryReceiveStatus" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFactoryReceiveStatusDate" runat="server" BorderStyle="Solid"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblFactoryReceiveStatusMode" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label>

                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Font-Size="Large" Text="Factory Ready Status"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblFactoryStatus" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblFactoryStatusDate" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label>

                                    </td>

                                    <td>
                                        <asp:Label ID="lblFactoryStatusMode" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Font-Size="Large" Text="Transit Status"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTransitStatus" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTransitStatusDate" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTransitStatusMode" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label></td>
                                </tr>
                                <tr>


                                    <td>
                                        <asp:Label ID="Label12" runat="server" Font-Size="Large" Text="Receive Status"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblReceiveStatus" runat="server" Text="Receive Status : " Font-Size="Large" BorderStyle="Solid"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReceiveStatusDate" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblReceiveStatusMode" runat="server" Font-Size="Large" BorderStyle="Solid"></asp:Label></td>
                                </tr>
                               
                            </table>



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
