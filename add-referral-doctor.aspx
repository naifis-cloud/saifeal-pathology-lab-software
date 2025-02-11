<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-referral-doctor.aspx.cs" Inherits="add_referral_doctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <style>
        .MyCalendar .ajax__calendar_container
        {
            border: 1px solid #CCCCCC;
            background-color: #ffffff;
            color: #3c8dbc;
        }
    </style>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add new referal doctor
           
          </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add referal doctor</li>
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
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "30" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Name</label>
                                                <asp:TextBox ID = "txtName" runat = "server" MaxLength = "200" placeholder="Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                                ControlToValidate="txtName" runat="server" ErrorMessage="Name" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID = "updAffiliateType" runat = "server">
                                            <ContentTemplate>
                                                <div class="col-xs-3">
                                                    <label>Affiliated Type</label>
                                                    <asp:DropDownList ID = "ddlAffiliateType" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                        OnSelectedIndexChanged="ddlAffiliateType_SelectedIndexChanged">
                                                        <asp:ListItem Text = "Main Lab" Value = "M"></asp:ListItem>
                                                        <asp:ListItem Text = "Collection Center" Value = "C"></asp:ListItem>
                                                        <asp:ListItem Text = "Sub Collection Center" Value = "SC"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <asp:UpdatePanel ID = "updAffiliateTo" runat = "server">
                                            <ContentTemplate>
                                                <div class="col-xs-4">
                                                    <label>Affiliated To</label>
                                                    <asp:DropDownList ID = "ddlCollCenter" runat = "server" CssClass = "form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAffiliateOf"
                                                        ControlToValidate="ddlCollCenter" InitialValue = "" runat="server" ErrorMessage="Affiliated To" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-xs-8">
                                            <div class="form-group">
                                                <label>Address</label>
                                                <asp:TextBox ID = "txtAddress" Rows = "3" TextMode = "MultiLine" runat = "server" placeholder="Enter Address..." 
                                                CssClass = "form-control" MaxLength = "500"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                                ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            </div>
                                        </div> 
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
                                                    ControlToValidate="ddlState" runat="server" ErrorMessage="State" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-4">
                                                    <label>City</label>
                                                    <asp:DropDownList ID = "ddlCity" runat = "server" CssClass = "form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCity"
                                                    ControlToValidate="ddlCity" runat="server" ErrorMessage="City" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-xs-3">
                                            <label>Pin Code</label>
                                            <asp:TextBox ID = "txtPinCode" runat = "server" MaxLength = "10" placeholder="pin code" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPinCode"
                                            ControlToValidate="txtPinCode" runat="server" ErrorMessage="Pincode" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Email</label>
                                            <asp:TextBox ID = "txtEmail" runat = "server" MaxLength = "50" placeholder="email id" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                                            ControlToValidate="txtEmail" runat="server" ErrorMessage="Email Address" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Valid Email Address"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgRefDoctor"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Mobile</label>
                                            <asp:TextBox ID = "txtMobile" runat = "server" MaxLength = "10" placeholder="mobile" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMobile"
                                            ControlToValidate="txtMobile" runat="server" ErrorMessage="Mobile" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltMobile" runat = "server" TargetControlID = "txtMobile" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Phone</label>
                                            <asp:TextBox ID = "txtPhone" runat = "server" MaxLength = "10" placeholder="phone" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalphone"
                                            ControlToValidate="txtPhone" runat="server" ErrorMessage="Phone" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltPhone" runat = "server" TargetControlID = "txtPhone" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                           <label>Sex</label>
                                            <asp:DropDownList ID = "ddlSex" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSex"
                                            ControlToValidate="ddlSex" runat="server" ErrorMessage="Sex" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                           <label>DOB</label>
                                            <asp:TextBox ID = "txtDOB" runat = "server" MaxLength = "20" placeholder="Date Of Birth" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDOB"
                                            ControlToValidate="txtDOB" runat="server" ErrorMessage="DOB" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                            <ajax:CalendarExtender ID = "calExtDOB" runat = "server" TargetControlID = "txtDOB" Format = "dd/MM/yyyy"
                                             Animated = "true" OnClientShown="calendarShown" CssClass = "MyCalendar">
                                            </ajax:CalendarExtender>
                                        </div>
                                        <div class="col-xs-4">
                                           <label>Qualification</label>
                                            <asp:TextBox ID = "txtQualification" runat = "server" MaxLength = "20" placeholder="Qualification" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalQualification"
                                            ControlToValidate="txtQualification" runat="server" ErrorMessage="Qualification" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                        </div>
                                        
                                        <div class="col-xs-2">
                                            <label>Referral</label>
                                            <asp:DropDownList ID = "ddlReferral" runat = "server" CssClass = "form-control">
                                                <asp:ListItem Text = "5%" Value = "5"></asp:ListItem>
                                                <asp:ListItem Text = "10%" Value = "10"></asp:ListItem>
                                                <asp:ListItem Text = "20%" Value = "20"></asp:ListItem>
                                                <asp:ListItem Text = "30%" Value = "30"></asp:ListItem>
                                                <asp:ListItem Text = "40%" Value = "40"></asp:ListItem>
                                                <asp:ListItem Text = "50%" Value = "50"></asp:ListItem>
                                                <asp:ListItem Text = "60%" Value = "60"></asp:ListItem>
                                                <asp:ListItem Text = "70%" Value = "70"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xs-6">
                                            <label>Clinic Name</label>
                                            <asp:TextBox ID = "txtClinicName" runat = "server" MaxLength = "20" placeholder="Clinic / Hospital Name" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalClinicName"
                                            ControlToValidate="txtClinicName" runat="server" ErrorMessage="Clinic Name" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Username</label>
                                           <asp:TextBox ID = "txtUserName" runat = "server" MaxLength = "20" placeholder="username" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvaluserName"
                                            ControlToValidate="txtUserName" runat="server" ErrorMessage="Username" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Password</label>
                                            <asp:TextBox ID = "txtPassword" runat = "server" MaxLength = "20" TextMode = "Password" 
                                            placeholder="password" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPassword"
                                            ControlToValidate="txtPassword" runat="server" ErrorMessage="Password" ValidationGroup="vgRefDoctor"></asp:RequiredFieldValidator>
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
                            Text = "Submit" ValidationGroup="vgRefDoctor" OnClick="btnSubmit_Click"/>
                            <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                             ValidationGroup="vgRefDoctor" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                        </div>
                </div>
            </div>
        </div>
            </asp:Panel>
    </section>
    </div>
</asp:Content>
