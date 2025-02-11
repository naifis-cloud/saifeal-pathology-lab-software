<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-param.aspx.cs" Inherits="add_param" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function EnableDisableOptions(objVal) {

            if (objVal == "GH") {
                document.getElementById('ctl00_ContentPlaceHolder1_txtParamDesc').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtParamDesc').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalDesc').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtTechnology').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTechnology').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalTechnology').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtMaterial').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtMaterial').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalMaterial').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtMethod').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtMethod').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalMethod').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtShortForm').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtShortForm').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalShortForm').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_ddlMachines').selectedIndex = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_ddlMachines').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalMachines').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_ddlSampleType').selectedIndex = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_ddlSampleType').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalSampleType').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtDefaultValue').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtDefaultValue').disabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtUpper').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtUpper').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalUpper').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtLower').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtLower').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalLower').enabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtUnit').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtUnit').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalUnit').enabled = false;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_txtParamDesc').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalDesc').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtTechnology').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalTechnology').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtMaterial').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalMaterial').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtMethod').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalMethod').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtShortForm').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalShortForm').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_ddlMachines').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalMachines').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_ddlSampleType').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalSampleType').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtDefaultValue').disabled = false;

                document.getElementById('ctl00_ContentPlaceHolder1_txtUpper').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalUpper').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtLower').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalLower').enabled = true;

                document.getElementById('ctl00_ContentPlaceHolder1_txtUnit').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalUnit').enabled = true;
            }
        }
    </script>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add new Param for Test: AEC           
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add Param</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Type</label>
                                                <asp:DropDownList ID = "ddlType" runat = "server" CssClass = "form-control" onchange = "EnableDisableOptions(this.value);">
                                                    <asp:ListItem Text = "Parameter" Value = "P"></asp:ListItem>
                                                    <asp:ListItem Text = "Group Header" Value = "GH"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="Enter code" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "50" placeholder="Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Name" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Order No</label>
                                            <asp:TextBox ID = "txtOrderNo" runat = "server" MaxLength = "5" placeholder="Order No" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="rqvalOrderNo"
                                                ControlToValidate="txtOrderNo" runat="server" ErrorMessage="Order No" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltOrderNo" runat = "server" TargetControlID = "txtOrderNo" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Param Description</label>
                                            <asp:TextBox ID = "txtParamDesc" runat = "server" MaxLength = "200" placeholder="description (if any)" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDesc"
                                                ControlToValidate="txtParamDesc" runat="server" ErrorMessage="Param Description" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Technology</label>
                                            <asp:TextBox ID = "txtTechnology" runat = "server" MaxLength = "200" placeholder="ex CLIA, SOlid Phase Enzyme Immunoassay" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTechnology"
                                            ControlToValidate="txtTechnology" runat="server" ErrorMessage="Technology" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Material</label>
                                            <asp:TextBox ID = "txtMaterial" runat = "server" MaxLength = "200" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMaterial"
                                            ControlToValidate="txtMaterial" runat="server" ErrorMessage="Material" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Method</label>
                                            <asp:TextBox ID = "txtMethod" runat = "server" MaxLength = "200" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMethod"
                                            ControlToValidate="txtMethod" runat="server" ErrorMessage="Method" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Short Form</label>
                                            <asp:TextBox ID = "txtShortForm" runat = "server" MaxLength = "50" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalShortForm"
                                            ControlToValidate="txtShortForm" runat="server" ErrorMessage="Short Form" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Machine</label>
                                            <asp:DropDownList ID = "ddlMachines" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMachines"
                                            ControlToValidate="ddlMachines" runat="server" ErrorMessage="Machine" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                            <p class="help-block"><a href="add-list-machines.aspx">Add New Machine.</a></p>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Sample Type</label>
                                            <asp:DropDownList ID = "ddlSampleType" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSampleType"
                                            ControlToValidate="ddlSampleType" runat="server" ErrorMessage="Sample Type" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                            <p class="help-block"><a href="add-list-samples.aspx">Add New Sample.</a></p>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Default Value</label>
                                            <asp:TextBox ID = "txtDefaultValue" runat = "server" MaxLength = "50" CssClass = "form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Lower Bound</label>
                                            <asp:TextBox ID = "txtLower" runat = "server" MaxLength = "20" placeholder="Lower Bound" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLower"
                                            ControlToValidate="txtLower" runat="server" ErrorMessage="Lower Bound" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="reqexpLower"
                                                runat="server" ControlToValidate="txtLower" ErrorMessage="Valid Lower Bound value"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$" ValidationGroup="vgParams"></asp:RegularExpressionValidator>--%>
                                            <%--<ajax:FilteredTextBoxExtender ID = "fltLower" runat = "server" TargetControlID = "txtLower" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Upper Bound</label>
                                            <asp:TextBox ID = "txtUpper" runat = "server" MaxLength = "20" placeholder="Upper Bound" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalUpper"
                                            ControlToValidate="txtUpper" runat="server" ErrorMessage="Upper Bound" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpUpper"
                                                runat="server" ControlToValidate="txtUpper" ErrorMessage="Valid Upper Bound value"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$" ValidationGroup="vgParams"></asp:RegularExpressionValidator>--%>
                                            <%--<ajax:FilteredTextBoxExtender ID = "fltUpper" runat = "server" TargetControlID = "txtUpper" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Unit</label>
                                            <asp:TextBox ID = "txtUnit" runat = "server" MaxLength = "50" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalUnit"
                                            ControlToValidate="txtUnit" runat="server" ErrorMessage="Unit" ValidationGroup="vgParams"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Bold</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnBold" runat = "server" GroupName = "Bold" />
                                                    Yes
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnNoBold" runat = "server" GroupName = "Bold" Checked = "true"  />
                                                    No
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Underline</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnUnderLine" runat = "server" GroupName = "Underline"/>
                                                    Yes
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnNoLine" runat = "server" GroupName = "Underline" Checked = "true"/>
                                                    No
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Color</label>
                                            <asp:DropDownList ID = "ddlColor" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "--Select--" Value = ""></asp:ListItem>
                                                <asp:ListItem Text = "Red" Value = "Red"></asp:ListItem>
                                                <asp:ListItem Text = "Green" Value = "Green"></asp:ListItem>
                                                <asp:ListItem Text = "Blue" Value = "Blue"></asp:ListItem>
                                                <asp:ListItem Text = "Yellow" Value = "Yellow"></asp:ListItem>
                                                <asp:ListItem Text = "Black" Value = "Black" Selected = "True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalColor"
                                            ControlToValidate="ddlColor" runat="server" ErrorMessage="Color" ValidationGroup="vgParams"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Leave Lines</label>
                                           <asp:TextBox ID = "txtLines" runat = "server" MaxLength = "3" CssClass = "form-control" Text = "0"></asp:TextBox>
                                           <ajax:FilteredTextBoxExtender ID = "fltLines" runat = "server" TargetControlID = "txtLines" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="row">
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgParams" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgParams" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (left) -->
                    <!-- right column -->
                    <!--/.col (right) -->
                </div>
                <!-- /.row -->
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
