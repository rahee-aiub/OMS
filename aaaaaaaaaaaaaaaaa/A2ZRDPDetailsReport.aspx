<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZOrderPositionList.aspx.cs" Inherits="ATOZWEBOMS.Pages.A2ZOrderPositionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

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

    <%-- <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtPartyName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("GMSWebService.asmx/GetPartyName") %>',
                         data: "{ 'prefix': '" + request.term + "'}",
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return {
                                     label: item.split('-')[0],
                                     val: item.split('-')[1]
                                 }
                             }))
                         },
                         error: function (response) {
                             alert(response.responseText);
                         },
                         failure: function (response) {
                             alert(response.responseText);
                         }
                     });
                 },
                select: function (e, i) {
                    $("#<%=hPartCode.ClientID %>").val(i.item.val);
                     $("#<%=txtPartyCode.ClientID %>").val(i.item.val);
                 },
                minLength: 1,
            });
        });


    </script>--%>

    <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>





    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }
    </style>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 250px;
            /*width: 520px;*/
            width: 520px;
            margin: 0 auto;
        }

        .grid1_scroll {
            overflow: auto;
            height: 300px;
            width: 1050px;
            margin: 0 auto;
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

        .auto-style1 {
            width: 225px;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">
                        <asp:Label ID="lblStatementFunc" runat="server" Text="Label"></asp:Label>

                    </th>
                </tr>

            </thead>


            <tr>
                <td>
                    <asp:CheckBox ID="chkAllOrderNo" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllOrderNo_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                    &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Order No. " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="ddlOrderNo" runat="server" Enabled="false" CssClass="cls text" Width="200px" Height="35px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="5">
                    </asp:DropDownList>
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>


                </td>


            </tr>

            <tr>
                
                    <td colspan="8">
                      <asp:RadioButton ID="rbtOptDetails" runat="server" GroupName="GLRptGrpPrm" Font-Size="Large" Text="Details" Checked="true" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOptDetails_CheckedChanged" />
                        &nbsp;&nbsp; &nbsp;&nbsp;  
                           <asp:RadioButton ID="rbtOptSummary" runat="server" GroupName="GLRptGrpPrm" Font-Size="Large" Text="Summary" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOptSummary_CheckedChanged" />
                    </td>


                </tr>

            <tr>

                <td class="auto-style1">
                    <asp:Label ID="lblfdate" runat="server" Text="Order From Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td class="auto-style1">

                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>




                </td>



            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltdate" runat="server" Text="Order To Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>


                <td>
                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>

            </tr>



            <tr>
                <td colspan=" 4"></td>

            </tr>
            <tr>

                <td></td>

                <td>


                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="150px" CssClass="button green" OnClick="BtnView_Click" />&nbsp;
                      &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" Height="27px" Width="100px" CssClass="button red" ToolTip="Exit Page" CausesValidation="False"
                        OnClick="BtnExit_Click" />



                </td>
            </tr>

        </table>
    </div>




    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBegFinYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnCode" runat="server" Text="" Visible="false"></asp:Label>



    <asp:Label ID="lblBranchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlBranchNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblUserLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUnitFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>

