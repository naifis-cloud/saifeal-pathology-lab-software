<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-vendor-pathlabs.aspx.cs" Inherits="add_list_vendor_pathlabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add / List High End Referral Labs
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">High End Referral Labs</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                <asp:Panel ID = "pnlMainContent" runat = "server">
                    <div class="row">
                        <div class="col-xs-6">
                            <asp:Label ID = "lblAddMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                            <asp:Panel ID = "pnlAddContent" runat = "server">
                                                                                                                                                                                                                                                                                                                                                                                                                                                        <div class="box box-primary">
                               <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Code</label>                                             
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>High End Ref Lab</label>
                                            <asp:TextBox ID = "txtMachineName" runat = "server" MaxLength = "50" placeholder="High end ref lab" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                            ControlToValidate="txtMachineName" runat="server" ErrorMessage="High end ref lab" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Address</label>
                                            <asp:TextBox ID = "txtAddress" runat = "server" MaxLength = "200" placeholder="Address" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDescription"
                                            ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
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
                                                    ControlToValidate="ddlState" runat="server" ErrorMessage="State" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xs-4">
                                                    <label>City</label>
                                                    <asp:DropDownList ID = "ddlCity" runat = "server" CssClass = "form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCity"
                                                    ControlToValidate="ddlCity" runat="server" ErrorMessage="City" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Pin Code</label>                                             
                                                <asp:TextBox ID = "txtPinCode" runat = "server" MaxLength = "10" placeholder="pin code" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPinCode"
                                                ControlToValidate="txtPinCode" runat="server" ErrorMessage="Pincode" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <label>Landline</label>
                                                <asp:TextBox ID = "txtLandLine" runat = "server" MaxLength = "10" placeholder="landline" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLandLine"
                                                ControlToValidate="txtLandLine" runat="server" ErrorMessage="Landline #" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltLandLine" runat = "server" TargetControlID = "txtLandLine" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-xs-3">
                                                <label>Mobile</label>
                                                <asp:TextBox ID = "txtMobile" runat = "server" MaxLength = "10" placeholder="mobile" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalmobile"
                                                ControlToValidate="txtMobile" runat="server" ErrorMessage="Mobile #" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltMobile" runat = "server" TargetControlID = "txtMobile" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label>Email</label>                                             
                                                    <asp:TextBox ID = "txtEmail" runat = "server" MaxLength = "50" placeholder="email" CssClass = "form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                                                    ControlToValidate="txtEmail" runat="server" ErrorMessage="Email" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                                                    runat="server" ControlToValidate="txtEmail" ErrorMessage="Valid Email Address."
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgVendorLabs"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label>Contact Person</label>
                                            <asp:TextBox ID = "txtContactPerson" runat = "server" MaxLength = "50" placeholder="contact person" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalContactPerson"
                                            ControlToValidate="txtContactPerson" runat="server" ErrorMessage="Contact Person" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Website</label>
                                            <asp:TextBox ID = "txtWebsiteName" runat = "server" MaxLength = "50" placeholder="website" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalWebsiteName"
                                            ControlToValidate="txtWebsiteName" runat="server" ErrorMessage="Website" ValidationGroup="vgVendorLabs"></asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regExpURL"
                                            runat="server" ControlToValidate="txtWebsiteName" ErrorMessage="Valid Website URL."
                                            ValidationExpression="(((ht|f)tp(s)?://)|www.){1}([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" ValidationGroup="vgVendorLabs"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                
                        
                        <div class="box-footer">
                            <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                            Text = "Submit" ValidationGroup="vgVendorLabs" OnClick="btnSubmit_Click"/>
                            <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                             ValidationGroup="vgVendorLabs" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                        </div>
                        
                    </div>
                            </asp:Panel>
                        </div>
                        <div class="col-xs-6">
                            <asp:Label ID = "lblViewMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                            <asp:Panel ID = "pnlViewContent" runat = "server">
                              <div class="box">
                                <div class="box-header"></div>
                                <div class="box-body">
                                    <table id="example1" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>Code</th>
                                                <th>Name</th>
                                                <th>City</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id = "trNoData" runat = "server">
                                                <td colspan = "4">
                                                    <center>
                                                        <asp:Label ID = "lblMsg" runat = "server" Text = "No vendor labs added." style="text-align:center;"></asp:Label>
                                                    </center>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID = "rptVendorLabs" runat = "server" OnItemCommand = "rptVendorLabs_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("stVendorLabCode")%></td>
                                                        <td><%#Eval("stVendorLabName")%></td>
                                                        <td><%#Eval("stCity")%></td>
                                                        <td>
                                                            <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inVendorLabId")%>'>
                                                                <i class="fa fa-edit"></i> <span>Edit</span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                             CommandName = "eDelete" CommandArgument = '<%#Eval("inVendorLabId")%>'>
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
                            <!-- /.box -->
                    <!-- /.col -->
                        </div>
                    </div>
                </asp:Panel>
        <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <asp:HiddenField ID="hidVendorLabId" runat="server" />
</asp:Content>
