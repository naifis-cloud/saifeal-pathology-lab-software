<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-test.aspx.cs" Inherits="add_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ValidateTestRate(source, args) {
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtTestRate').value) == 0)
                args.IsValid = false;
        }
    </script>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add new test           
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add Test</li>
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
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-1">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgTests"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "50" placeholder="Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Name" ValidationGroup="vgTests"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Routine</label>
                                                <asp:TextBox ID = "txtRoutineName" runat = "server" MaxLength = "50" placeholder="Routine Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRoutineName"
                                                ControlToValidate="txtRoutineName" runat="server" ErrorMessage="Routine Name" ValidationGroup="vgTests"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Department</label>
                                            <asp:DropDownList ID = "ddlMainDepts" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMainDepts"
                                            ControlToValidate="ddlMainDepts" runat="server" ErrorMessage="Main Department" ValidationGroup="vgTests"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Test Description</label>
                                            <asp:TextBox ID = "txtTestDesc" runat = "server" MaxLength = "200" placeholder="description (if any)" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTestDesc"
                                            ControlToValidate="txtTestDesc" runat="server" ErrorMessage="Test Description" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-1">
                                            <label>Order</label>
                                            <asp:TextBox ID = "txtOrder" runat = "server" MaxLength = "5" placeholder="Rank" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalOrder"
                                            ControlToValidate="txtOrder" runat="server" ErrorMessage="Order" ValidationGroup="vgTests"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltOrder" runat = "server" TargetControlID = "txtOrder" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-5">
                                            <label>Method</label>
                                            <asp:TextBox ID = "txtMethod" runat = "server" MaxLength = "200" placeholder="ex. Ziehl Nielsen Stain, CLIA" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMethod"
                                            ControlToValidate="txtMethod" runat="server" ErrorMessage="Method" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label>ICD10 Code</label>
                                            <asp:TextBox ID = "txtICDCode" runat = "server" MaxLength = "10" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalICDCode"
                                            ControlToValidate="txtICDCode" runat="server" ErrorMessage="ICD10 Code" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>HCPCS Code</label>
                                            <asp:TextBox ID = "txtHCPCSCode" runat = "server" MaxLength = "10" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalHCPCSCode"
                                            ControlToValidate="txtHCPCSCode" runat="server" ErrorMessage="HCPCS Code" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>CPT Code</label>
                                            <asp:TextBox ID = "txtCPTCode" runat = "server" MaxLength = "10" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCPTCode"
                                            ControlToValidate="txtCPTCode" runat="server" ErrorMessage="CPT Code" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Technology</label>
                                            <asp:TextBox ID = "txtTechnology" runat = "server" MaxLength = "200" placeholder="ex CLIA, SOlid Phase Enzyme Immunoassay" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTechnology"
                                            ControlToValidate="txtTechnology" runat="server" ErrorMessage="Technology" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Material</label>
                                            <asp:TextBox ID = "txtMaterial" runat = "server" MaxLength = "200" placeholder="" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMaterial"
                                            ControlToValidate="txtMaterial" runat="server" ErrorMessage="Material" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Remarks</label>
                                            <asp:TextBox ID = "txtRemarks" runat = "server" MaxLength = "200" placeholder="Remarks for this test (if any)" CssClass = "form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Conditions</label>
                                            <asp:TextBox ID = "txtConditions" runat = "server" MaxLength = "200" placeholder="Conditions for this test (if any)" CssClass = "form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">                                       
                                        <div class="col-xs-3">
                                            <label>Machine</label>
                                            <asp:DropDownList ID = "ddlMachines" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMachines"
                                                ControlToValidate="ddlMachines" runat="server" ErrorMessage="Machine" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                            <p class="help-block"><a href="add-list-machines.aspx">Add New Machine.</a></p>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Sample Type</label>
                                            <asp:DropDownList ID = "ddlSampleType" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSampleType"
                                            ControlToValidate="ddlSampleType" runat="server" ErrorMessage="Sample Type" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                            <p class="help-block"><a href="add-list-samples.aspx">Add New Sample.</a></p>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Formula</label>
                                            <asp:TextBox ID = "txtFormula" runat = "server" MaxLength = "50" placeholder="Formula" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalFormula"
                                            ControlToValidate="txtFormula" runat="server" ErrorMessage="Formula" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-1">
                                            <label>Days</label>
                                            <asp:TextBox ID = "txtDays" runat = "server" MaxLength = "5" placeholder="Days" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDays"
                                            ControlToValidate="txtDays" runat="server" ErrorMessage="Days" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                            <ajax:FilteredTextBoxExtender ID = "fltDays" runat = "server" TargetControlID = "txtDays" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label>Short Form</label>
                                            <asp:TextBox ID = "txtShortForm" runat = "server" MaxLength = "30" placeholder="Short Form" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalShortForm"
                                            ControlToValidate="txtShortForm" runat="server" ErrorMessage="Short Form" ValidationGroup="vgTests"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Any Specific Instructions</label>
                                            <asp:TextBox ID = "txtSpecInstructions" runat = "server" MaxLength = "200" placeholder="Instructions for preparation" CssClass = "form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>For Sex</label>
                                            <asp:DropDownList ID = "ddlSex" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-2" style="display:none;">
                                            <label>Test Rate</label>
                                            <asp:TextBox ID = "txtTestRate" runat = "server" MaxLength = "5" placeholder="Rate" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRate"
                                            ControlToValidate="txtTestRate" runat="server" ErrorMessage="Test Rate" ValidationGroup="vgTests" Enabled = "false"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltTestRate" runat = "server" TargetControlID = "txtTestRate" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                            <asp:CustomValidator ID = "cvalTestRate" runat = "server" SetFocusOnError="true" Display="None"
                                                ClientValidationFunction = "ValidateTestRate" ValidationGroup="vgTests" ErrorMessage = "Test Rate cannot be 0." Enabled = "false">
                                            </asp:CustomValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Attach Graph</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnAttachGraphYes" runat = "server" GroupName = "Graph" Checked = "true" />
                                                    Yes                  
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnAttachGraphNo" runat = "server" GroupName = "Graph" />
                                                    No                  
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label>Is Active</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnActive" runat = "server" GroupName = "ActiveInactive" Checked = "true" />
                                                    Yes                  
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnInactive" runat = "server" GroupName = "ActiveInactive" />
                                                    No                  
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Parameters</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnParamsSingle" runat = "server" GroupName = "Params" Checked = "true" />
                                                    Single            
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnParamsMultiple" runat = "server" GroupName = "Params" />
                                                    Multiple              
                                                </label>
                                            </div>
                                        </div>
                                         <div class="col-xs-3">
                                            <label>Type</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnDesc" runat = "server" GroupName = "Type" Checked = "true" />
                                                    Descriptive
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnNonDesc" runat = "server" GroupName = "Type"/>
                                                    Non Descriptive           
                                                </label>
                                            </div>
                                        </div>
                                         <div class="col-xs-3">
                                            <label>Add Reference Range</label>
                                            <div class="checkbox">
                                                <label style="padding-left:5px;">
                                                    <asp:RadioButton ID = "rbtnAddRefRangeYes" runat = "server" GroupName = "RefRange" Checked = "true" />
                                                    Yes
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnAddRefRangeNo" runat = "server" GroupName = "RefRange"/>
                                                    No
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgTests" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgTests" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            </form>
                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (left) -->
                    <!-- right column -->
                    <!--/.col (right) -->
                </div>
            </asp:Panel>
                <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
