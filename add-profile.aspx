<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-profile.aspx.cs" Inherits="add_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateSelectedTests(source, args) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_lstSelectedTests').options.length == 0)
                args.IsValid = false;
        }
    
    </script>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add Profile          
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add Profile</li>
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
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgProfiles"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <asp:TextBox ID = "txtProfileName" runat = "server" MaxLength = "50" placeholder="Profile Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalProfileName"
                                                ControlToValidate="txtProfileName" runat="server" ErrorMessage="Profile Name" ValidationGroup="vgProfiles"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Method</label>
                                                <asp:TextBox ID = "txtMethod" runat = "server" MaxLength = "100" placeholder="Profile Method" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMethod"
                                                ControlToValidate="txtMethod" runat="server" ErrorMessage="Profile Method" ValidationGroup="vgProfiles"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Department</label>
                                            <asp:DropDownList ID = "ddlDepts" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDept"
                                             ControlToValidate="ddlDepts" runat="server" ErrorMessage="Department" ValidationGroup="vgProfiles"></asp:RequiredFieldValidator>    
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID = "updAddTest" runat = "server">
                                                    <ContentTemplate>
                                                        <div class="col-xs-3">
                                                            <asp:Button ID = "btnAddTest" runat = "server" CssClass = "btn btn-primary" 
                                                                Text = "Add Test" ValidationGroup="vgAddTest" OnClick="btnAddTest_Click"/>
                                                            <asp:ValidationSummary ID="valSummAddTest" runat="server" 
                                                            ValidationGroup="vgAddTest" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="clearfix"></div>
                                            <label>Lists Tests</label>
                                            <asp:UpdatePanel ID = "updTests" runat = "server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID = "lstTests" runat = "server" SelectionMode = "Multiple" CssClass = "form-control"></asp:ListBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalListTests"
                                                     ControlToValidate="lstTests" runat="server" ErrorMessage="Select atleast 1 Test to Add" ValidationGroup="vgAddTest"></asp:RequiredFieldValidator>    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID = "updRemoveTest" runat = "server">
                                                    <ContentTemplate>
                                                        <div class="col-xs-3">
                                                            <asp:Button ID = "btnRemoveTest" runat = "server" CssClass = "btn btn-primary" 
                                                            Text = "Remove Test" ValidationGroup="vgRemoveTest" OnClick="btnRemoveTest_Click"/>
                                                            <asp:ValidationSummary ID="valSummRemove" runat="server" 
                                                            ValidationGroup="vgRemoveTest" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="clearfix"></div>
                                            <label>Selected Tests</label>
                                            <asp:UpdatePanel ID = "updSelectedTests" runat = "server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID = "lstSelectedTests" runat = "server" SelectionMode = "Multiple" CssClass = "form-control"></asp:ListBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRemoveTest"
                                                     ControlToValidate="lstSelectedTests" runat="server" ErrorMessage="Select atleast 1 Test to Remove" ValidationGroup="vgRemoveTest"></asp:RequiredFieldValidator>    
                                                     <asp:CustomValidator SetFocusOnError="true" Display="None" ID="customSelectedTest" ClientValidationFunction = "ValidateSelectedTests"
                                                     runat="server" ErrorMessage="Select atleast 1 Test" ValidationGroup="vgProfiles"></asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        &nbsp;<br />
                                        </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Group Test Rate</label>
                                                <asp:TextBox ID = "txtGroupRate" runat = "server" MaxLength = "5" placeholder="Enter Rate" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLower"
                                                ControlToValidate="txtGroupRate" runat="server" ErrorMessage="Group Rate" ValidationGroup="vgProfiles"></asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltLower" runat = "server" TargetControlID = "txtGroupRate" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID = "updTotalPrice" runat = "server">
                                            <ContentTemplate>
                                                <div class="col-xs-6">
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Selected Test Rate (Total)</label><br />
                                                        <label for="exampleInputEmail1" style="font-size:15px;padding-top:5px;">INR &nbsp;<asp:Label ID = "lblTotalPrice" runat = "server"></asp:Label></label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <%--<div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1"></label>
                                                <button class="btn btn-block btn-primary btn-sm">Calculate Rate</button>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgProfiles" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgProfiles" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            </form>
                        </div>
                        <!-- /.box -->
                    </div>
                  
                </div>
            </asp:Panel>
                <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
