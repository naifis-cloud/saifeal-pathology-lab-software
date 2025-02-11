<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="report-settings.aspx.cs" Inherits="report_settings" %>

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
                alert('Please select atleast 1 report setting.');
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
                    Report Settings
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Report Settings</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Label ID = "lblAddMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                        <asp:Panel ID = "pnlAddContent" runat = "server">
                            <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                            
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Report Settings Name: </label>
                                            <asp:TextBox ID = "txtName" runat = "server" MaxLength = "50" placeholder="report name" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                            ControlToValidate="txtName" runat="server" ErrorMessage="Name" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Report Description</label>
                                            <asp:TextBox ID = "txtDescription" runat = "server" MaxLength = "200" placeholder="Description" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDesc"
                                            ControlToValidate="txtName" runat="server" ErrorMessage="Description" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <h4>page margins (in pxs)</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Left: </label>
                                            <asp:TextBox ID = "txtLeftMargin" runat = "server" MaxLength = "3" placeholder="e.g 1" CssClass = "form-control"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID = "fltLeftMargin" runat = "server" TargetControlID = "txtLeftMargin" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLeftMargin"
                                            ControlToValidate="txtName" runat="server" ErrorMessage="Left Margin" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Right: </label>
                                            <asp:TextBox ID = "txtRightMargin" runat = "server" MaxLength = "3" placeholder="e.g 1" CssClass = "form-control"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID = "fltRightMargin" runat = "server" TargetControlID = "txtRightMargin" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRightMargin"
                                            ControlToValidate="txtName" runat="server" ErrorMessage="Right Margin" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Top: </label>
                                            <asp:TextBox ID = "txtTopMargin" runat = "server" MaxLength = "3" placeholder="e.g 1" CssClass = "form-control"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID = "fltTopMargin" runat = "server" TargetControlID = "txtTopMargin" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTopMargin"
                                            ControlToValidate="txtTopMargin" runat="server" ErrorMessage="Name" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Bottom: </label>
                                            <asp:TextBox ID = "txtBottomMargin" runat = "server" MaxLength = "3" placeholder="e.g 1" CssClass = "form-control"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID = "fltBottomMargin" runat = "server" TargetControlID = "txtBottomMargin" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalBottomMargin"
                                            ControlToValidate="txtBottomMargin" runat="server" ErrorMessage="Name" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <h4>settings</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Outer Border: </label>
                                            <asp:DropDownList ID = "ddlOuterBorder" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "Yes" Value = "Y"></asp:ListItem>
                                                <asp:ListItem Text = "No" Value = "N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Header Visible: </label>
                                            <asp:DropDownList ID = "ddlHeaderVisible" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "Yes" Value = "Y"></asp:ListItem>
                                                <asp:ListItem Text = "No" Value = "N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Logo Visible: </label>
                                            <asp:DropDownList ID = "ddlLogoVisible" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "Yes" Value = "Y"></asp:ListItem>
                                                <asp:ListItem Text = "No" Value = "N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Footer visible: </label>
                                            <asp:DropDownList ID = "ddlFooterVisible" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "Yes" Value = "Y"></asp:ListItem>
                                                <asp:ListItem Text = "No" Value = "N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Signature visible: </label>
                                            <asp:DropDownList ID = "ddlSignVisible" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "Yes" Value = "Y"></asp:ListItem>
                                                <asp:ListItem Text = "No" Value = "N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Report Font Family: </label>
                                            <asp:DropDownList ID = "ddlFontFamily" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Report Font size: </label>
                                            <asp:TextBox ID = "txtFontSize" runat = "server" MaxLength = "3" placeholder="e.g 8" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalFontSize"
                                            ControlToValidate="txtFontSize" runat="server" ErrorMessage="Font Size" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltFontSize" runat = "server" TargetControlID = "txtFontSize" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-xs-3">
                                            <label> Text on Report&nbsp;&nbsp;&nbsp;&nbsp;: </label>
                                            <asp:TextBox ID = "txtReportText" runat = "server" MaxLength = "50" placeholder="e.g 8" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalReportText"
                                            ControlToValidate="txtReportText" runat="server" ErrorMessage="Text on Report" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Upload logo: (Max file size - 2MB) </label>
                                            <asp:FileUpload ID = "fupLogo" runat = "server" CssClass = "form-control" />
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Upload signature: (Max file size - 2MB) </label>
                                            <asp:FileUpload ID = "fupSignature" runat = "server" CssClass = "form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <label>Lab Name: </label>
                                            <asp:TextBox ID = "txtLabName" runat = "server" MaxLength = "50" placeholder="e.g 8" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLabName"
                                            ControlToValidate="txtLabName" runat="server" ErrorMessage="Lab Name" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Adress: </label>
                                            <asp:TextBox ID = "txtAddress" runat = "server" MaxLength = "100" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                            ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Phone#: </label>
                                            <asp:TextBox ID = "txtPhone" runat = "server" MaxLength = "20" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="RequiredFieldValidator2"
                                            ControlToValidate="txtPhone" runat="server" ErrorMessage="Phone#" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <label>Footer text: </label>
                                            <asp:TextBox ID = "txtFooterText" runat = "server" MaxLength = "200" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="RequiredFieldValidator3"
                                            ControlToValidate="txtFooterText" runat="server" ErrorMessage="Footer text" ValidationGroup="vgRptSettings"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgRptSettings" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgRptSettings" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            
                        </div>
                        </asp:Panel>
                    </div>
                    <div class="col-xs-12">
                        <asp:Label ID = "lblViewMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                        <asp:Panel ID = "pnlViewContent" runat = "server">
                            <div class="box">
                            <div class="box-header">
                                <asp:Button ID = "btnDefaultSetting" runat = "server" CssClass = "btn btn-primary" 
                                Text = "Set as Default Setting" OnClientClick = "return fnValidateDefaultSetting();" OnClick="btnDefaultSetting_Click"/>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Code</th>
                                            <th>Report</th>
                                            <th>Description</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "5">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No report settings added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptSettings" runat = "server" OnItemCommand = "rptSettings_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td align = "center" style="width:5%;">
                                                    <asp:RadioButton ID = "rbtnDefaultSetting" runat = "server" onclick="fnCheckUnCheck(this.id);"
                                                        Checked = '<%#Eval("btDefaultSetting")%>' />
                                                    <asp:Label ID = "lblRptSettingsId" runat = "server" Text = '<%#Eval("inRtpSettingsId")%>' Visible = "false"></asp:Label>
                                                </td>
                                                <td><%#Eval("rptSettingsCode")%></td>
                                                <td><%#Eval("rptSettingsName")%></td>
                                                <td><%#Eval("rptSettingsDesc")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inRtpSettingsId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inRtpSettingsId")%>'>
                                                        <i class="fa fa-fw fa-trash-o"></i><span>Delete</span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                </div>
        </div>
    </asp:Panel>
    </section>
    </div>
    <asp:HiddenField ID="hidLogoImage" runat="server" />
    <asp:HiddenField ID="hidSignatureImage" runat="server" />
    <asp:HiddenField ID="hidRptSettingsId" runat="server" />
</asp:Content>
