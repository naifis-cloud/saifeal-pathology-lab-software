<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="abbrev-settings.aspx.cs" Inherits="abbrev_settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function fnCheckUnCheck(objId) {
            var grd = document.getElementById('ctl00_ContentPlaceHolder1_pnlViewContent');

            //Collect A
            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'radio') {
                    if (rdoArray[i].id != objId) {
                        rdoArray[i].checked = false;
                    }
                }
            }
        }

        function fnValidateDefaultSetting() {
            var grd = document.getElementById('ctl00_ContentPlaceHolder1_pnlViewContent');
            var rdoArray = grd.getElementsByTagName("input");
            var lbIsDefaultSelected = false;

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'radio') {
                    if (rdoArray[i].checked == true) {
                        lbIsDefaultSelected = true;
                        break;
                    }
                }
            }
            if (!lbIsDefaultSelected) {
                alert('Please select atleast 1 receipt setting.');
                return false;
            }
        }  
    </script>
    <!-- page script -->
    <script type="text/javascript">
        $(function () {
            $('#example1').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": false
            });
        });
    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>
                    Abbreviation Settings
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Abbreviations</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-6">
                        <asp:Label ID = "lblAddMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                        <asp:Panel ID = "pnlAddContent" runat = "server" Visible = "false">
                            <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Code</label>
                                                <asp:TextBox ID = "txtShortCode" runat = "server" MaxLength = "50" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtShortCode" runat="server" ErrorMessage="Code" ValidationGroup="vgRCSettings"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Abbreviation: </label>
                                            <asp:TextBox ID = "txtAbbrevName" runat = "server" MaxLength = "2" placeholder="receipt name" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                            ControlToValidate="txtAbbrevName" runat="server" ErrorMessage="Name" ValidationGroup="vgRCSettings"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgRCSettings" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgRCSettings" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-xs-6">
                        <asp:Label ID = "lblViewMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                        <asp:Panel ID = "pnlViewContent" runat = "server">
                            <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Module Code</th>
                                            <th>Abbreviation</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id = "trNoData" runat = "server">
                                            <td colspan = "5">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No module abbreviations added." style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID = "rptAbbrevSettings" runat = "server" OnItemCommand = "rptAbbrevSettings_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("stShortCode")%></td>
                                                    <td><%#Eval("stInitialCode")%></td>
                                                    <td>
                                                        <asp:Label ID = "lblRecSettingsId" runat = "server" Text = '<%#Eval("inCodeId")%>' Visible = "false"></asp:Label>
                                                        <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inCodeId")%>'>
                                                            <i class="fa fa-edit"></i> <span>Edit</span>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        </asp:Panel>
                        <!-- /.box -->
                    </div>
                    <!-- /.col -->
                </div>
            </asp:Panel>
                <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <asp:HiddenField ID="hidAbbrevId" runat="server" />
</asp:Content>
