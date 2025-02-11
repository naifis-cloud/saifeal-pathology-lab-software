<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-users.aspx.cs" Inherits="add_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add new user
           
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add new user</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-8">
                        <!-- general form elements -->
                        <div class="box box-primary">

                            <!-- /.box-header -->
                            <!-- form start -->
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "30" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "50" placeholder="Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Name" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Role</label>
                                            <asp:DropDownList ID = "ddlRoles" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRole"
                                            ControlToValidate="ddlRoles" runat="server" ErrorMessage="Select Role" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID = "txtAddress" Rows = "3" TextMode = "MultiLine" runat = "server" placeholder="Enter Address..." 
                                        CssClass = "form-control" MaxLength = "200"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                        ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel ID = "updStateCity" runat = "server">
                                            <ContentTemplate>
                                                <div class="col-xs-4">
                                                    <label>State</label>
                                                    <asp:DropDownList ID = "ddlState" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalState"
                                                    ControlToValidate="ddlState" runat="server" ErrorMessage="State" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-4">
                                                    <label>City</label>
                                                    <asp:DropDownList ID = "ddlCity" runat = "server" CssClass = "form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCity"
                                                    ControlToValidate="ddlCity" runat="server" ErrorMessage="City" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-xs-4">
                                            <label>Pincode</label>
                                            <asp:TextBox ID = "txtPinCode" runat = "server" MaxLength = "10" placeholder="pin code" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPinCode"
                                            ControlToValidate="txtPinCode" runat="server" ErrorMessage="Pincode" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Email</label>
                                            <asp:TextBox ID = "txtEmail" runat = "server" MaxLength = "50" placeholder="email id" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                                            ControlToValidate="txtEmail" runat="server" ErrorMessage="Email Address" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Valid Email Address"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgUsers"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Contact # 1</label>
                                            <asp:TextBox ID = "txtMobile" runat = "server" MaxLength = "20" placeholder="mobile" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMobile"
                                            ControlToValidate="txtMobile" runat="server" ErrorMessage="Contact # 1" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Contact # 2</label>
                                            <asp:TextBox ID = "txtPhone" runat = "server" MaxLength = "20" placeholder="phone" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalphone"
                                            ControlToValidate="txtPhone" runat="server" ErrorMessage="phone" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Sex</label>
                                            <asp:DropDownList ID = "ddlSex" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSex"
                                            ControlToValidate="ddlSex" runat="server" ErrorMessage="Sex" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Age</label>
                                            <asp:TextBox ID = "txtAge" runat = "server" MaxLength = "3" placeholder="age" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAge"
                                            ControlToValidate="txtAge" runat="server" ErrorMessage="Age" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Qualification</label>
                                            <asp:TextBox ID = "txtQualification" runat = "server" MaxLength = "100" placeholder="Qualification" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalQual"
                                            ControlToValidate="txtQualification" runat="server" ErrorMessage="Qualification" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>


                                    </div>
                                    <div class="row">

                                        <div class="col-xs-3">
                                            <label>Username</label>
                                            <asp:TextBox ID = "txtUsername" runat = "server" MaxLength = "50" placeholder="username" CssClass = "form-control"></asp:TextBox>
                                           <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalusername"
                                            ControlToValidate="txtUsername" runat="server" ErrorMessage="Username" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Password</label>
                                            <asp:TextBox ID = "txtpassword" runat = "server" TextMode = "Password" MaxLength = "50" placeholder="password" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalpassword"
                                            ControlToValidate="txtpassword" runat="server" ErrorMessage="Password" ValidationGroup="vgUsers"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Status</label>
                                            <div class="checkbox">
                                                <label>
                                                    <asp:RadioButton ID = "rbtnActive" runat = "server" GroupName = "status" Checked = "true" />
                                                    Active                  
                                                </label>
                                                <label>
                                                    <asp:RadioButton ID = "rbtnInactive" runat = "server" GroupName = "status" />
                                                    In Active                  
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                        Text = "Submit" ValidationGroup="vgUsers" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgUsers" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            </form>
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
