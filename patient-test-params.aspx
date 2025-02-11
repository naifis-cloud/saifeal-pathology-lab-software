<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="patient-test-params.aspx.cs"
    Inherits="patient_test_params" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>SBS LIS | Pathology lab automation software</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport'>
    <link href="~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.7.2.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ValidateParamData(source, args) 
        {
            var liTestCounter = 0;
            var liParamCounter = 0;
            var lstrerrormsg = "";

            var TestCount = parseInt(document.getElementById('hidTestCount').value);

            //Looping through test 
            for (liTestCounter = 0; liTestCounter < TestCount; liTestCounter++) 
            {
                var ParamCountObj = document.getElementById('rptPatientTests_ctl0' + liTestCounter + '_hidTestParamCount');
                var TestNameObj = document.getElementById('rptPatientTests_ctl0' + liTestCounter + '_lblTestName');

                if (ParamCountObj != null) 
                {
                    //Looping through test params//
                    for (liParamCounter = 0; liParamCounter < parseInt(ParamCountObj.value); liParamCounter++) 
                    {
                        var ParamTextBoxObj = document.getElementById('rptPatientTests_ctl0' + liTestCounter + '_rptTestsParams_ctl0' + liParamCounter + '_txtParamValue');
                        var ParamNameObj = document.getElementById('rptPatientTests_ctl0' + liTestCounter + '_rptTestsParams_ctl0' + liParamCounter + '_lblParamName');

                        if (ParamTextBoxObj != null && ParamNameObj != null) 
                        {
                            if ($.trim(ParamTextBoxObj.value) == "") 
                            {
                                //lstrerrormsg += "@Enter all parameter values for " + ParamNameObj.innerText + " for " + TestNameObj.innerText + ".";
                                lstrerrormsg += "@Enter all parameter values for " + TestNameObj.innerText + ".";
                                break;
                            }
                        }
                    }
                }
            }

            if (lstrerrormsg != "") 
            {
                lstrerrormsg = lstrerrormsg.substring(1);
                source.errormessage = lstrerrormsg.replace(/@/g, '\n');
                args.IsValid = false;
            }
        }
    
    </script>

    <script language="javascript" type="text/javascript">
        $('#rptPatientTests_ctl00_rptTestDesc_ctl00_txtTestDesc').wysihtml5();
    </script>

</head>
<body class="skin-blue sidebar-mini">
    <form id="form1" runat="server">
    <div>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <asp:Panel ID = "pnlHeader" runat = "server">
                <h4>Parameters for  - <asp:Label ID = "lblPatientName" runat = "server"></asp:Label></h4>
            </asp:Panel>
        </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <asp:Repeater ID = "rptPatientTests" runat = "server" OnItemDataBound="rptPatientTests_ItemDataBound">
                                <ItemTemplate>
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <th>
                                                <asp:Label ID = "lblTestName" runat = "server" Text = '<%#Eval("stTestName")%>' style="vertical-align:middle;"></asp:Label>
                                                <asp:Label ID = "lblTestId" runat = "server" Text = '<%#Eval("inTestId")%>' Visible = "false"></asp:Label>
                                                <asp:HiddenField ID = "hidTestParamCount" runat = "server" />
                                            </th>
                                        </tr>
                                    </table>
                                    <asp:Panel id="tblTestParams" runat = "server">
                                        <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>Sr #</th>
                                                        <th>Parameter</th>
                                                        <th>Value</th>
                                                    </tr>
                                                </thead>
                                                    <tbody>
                                                    <tr id = "trNoData" runat = "server">
                                                        <td colspan = "3">
                                                            <center>
                                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No test parameters found." style="text-align:center;"></asp:Label>
                                                            </center>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID = "rptTestsParams" runat = "server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#(Container.ItemIndex+1)%>
                                                                    <asp:Label ID = "lblParamId" runat = "server" Text = '<%#Eval("inParamId")%>' Visible = "false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID = "lblParamName" runat = "server" Text = '<%#Eval("stCode") + " - " + Eval("stName")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID = "txtParamValue" runat = "server" MaxLength = "50" placeholder="param value" CssClass = "form-control"
                                                                    Text = '<%#Eval("stParamValue")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel id="tblTestDescription" runat = "server">
                                        <table class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>Description</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID = "rptTestDesc" runat = "server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td valign = "top">
                                                                <asp:TextBox ID = "txtTestDesc" runat = "server" CssClass = "textarea"  
                                                                style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"
                                                                placeholder="description here" Text = '<%#Server.HtmlDecode(Convert.ToString(Eval("stDescription")))%>'></asp:TextBox>
                                                            </td>
                                                     </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                        <!-- /.box-body -->
                    </div>
                <div>
                    <asp:CustomValidator ID = "cValParamData" runat = "server" ClientValidationFunction = "ValidateParamData"
                        ValidationGroup="vgTestParams"></asp:CustomValidator>
                    <asp:ValidationSummary ID="valColCenter" runat="server" ValidationGroup="vgTestParams" ShowMessageBox="true" 
                        DisplayMode="List" ShowSummary="false" />
                    <asp:Button ID = "btnSave" runat = "server" CssClass = "btn btn-primary" Text = "Save Details" OnClick="btnSave_Click"
                        ValidationGroup="vgTestParams"/>
                </div>
                    <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
        </asp:Panel>
    </section>
        <!-- /.content -->
    </div>
    <asp:HiddenField ID="hidTestCount" runat="server" />
    <asp:HiddenField ID="hidNoParamsForAllTests" runat="server" />
    </form>
</body>
</html>
