<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-collection-centre.aspx.cs" Inherits="add_collection_centre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add new collection centre
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add collection centre</li>
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
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-9">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "200" placeholder="Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Name" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-5">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID = "txtAddress" Rows = "3" TextMode = "MultiLine" runat = "server" placeholder="Enter Address..." 
                                        CssClass = "form-control" MaxLength = "500"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                        ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel ID = "updStateCity" runat = "server" UpdateMode = "Conditional">
                                            <ContentTemplate>
                                                <div class="col-xs-4">
                                                    <label>State</label>
                                                    <asp:DropDownList ID = "ddlState" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalState"
                                                    ControlToValidate="ddlState" runat="server" ErrorMessage="State" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-4">
                                                    <label>City</label>
                                                    <asp:DropDownList ID = "ddlCity" runat = "server" CssClass = "form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCity"
                                                    ControlToValidate="ddlCity" runat="server" ErrorMessage="City" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-xs-4">
                                            <label>Pincode</label>
                                            <asp:TextBox ID = "txtPinCode" runat = "server" MaxLength = "10" placeholder="pin code" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPinCode"
                                            ControlToValidate="txtPinCode" runat="server" ErrorMessage="Pincode" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Email</label>
                                            <asp:TextBox ID = "txtEmail" runat = "server" MaxLength = "50" placeholder="email id" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                                            ControlToValidate="txtEmail" runat="server" ErrorMessage="Email Address" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Valid Email Address"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgColCenter"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Mobile</label>
                                            <asp:TextBox ID = "txtMobile" runat = "server" MaxLength = "10" placeholder="mobile" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMobile"
                                            ControlToValidate="txtMobile" runat="server" ErrorMessage="mobile" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltMobile" runat = "server" TargetControlID = "txtMobile" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Phone</label>
                                            <asp:TextBox ID = "txtPhone" runat = "server" MaxLength = "10" placeholder="phone" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalphone"
                                            ControlToValidate="txtPhone" runat="server" ErrorMessage="phone" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltPhone" runat = "server" TargetControlID = "txtPhone" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Doctor's Name</label>
                                            <asp:TextBox ID = "txtDoctorName" runat = "server" MaxLength = "50" placeholder="Doctor's Name" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvaldoctorname"
                                            ControlToValidate="txtDoctorName" runat="server" ErrorMessage="Doctor's Name" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Lab Unit</label>
                                            <asp:TextBox ID = "txtLabUnit" runat = "server" MaxLength = "50" placeholder="Lab Unit" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLabunit"
                                            ControlToValidate="txtLabUnit" runat="server" ErrorMessage="Lab Unit" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Rate type</label>
                                            <asp:DropDownList ID = "ddlRateType" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRateType"
                                            ControlToValidate="ddlRateType" runat="server" ErrorMessage="Rate type" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xs-6">
                                            <label>Deposit Amount</label>
                                            <asp:TextBox ID = "txtDepAmt" runat = "server" MaxLength = "8" placeholder="Deposit Amount" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvaldepamt"
                                            ControlToValidate="txtDepAmt" runat="server" ErrorMessage="Deposit Amount" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltDepAmt" runat = "server" TargetControlID = "txtDepAmt" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Username</label>
                                           <asp:TextBox ID = "txtUsername" runat = "server" MaxLength = "30" placeholder="username" CssClass = "form-control"></asp:TextBox>
                                           <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalusername"
                                            ControlToValidate="txtUsername" runat="server" ErrorMessage="Username" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Password</label>
                                            <asp:TextBox ID = "txtpassword" runat = "server" TextMode = "Password" MaxLength = "30" placeholder="password" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalpassword"
                                            ControlToValidate="txtpassword" runat="server" ErrorMessage="Password" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
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
                                        Text = "Submit" ValidationGroup="vgColCenter" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgColCenter" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
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
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
