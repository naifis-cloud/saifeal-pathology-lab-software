<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-normal-range.aspx.cs" Inherits="add_list_normal_range" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ValidateBounds(source, args) 
        {
            var liLowerbound = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLower').value);
            var liUpperbound = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtUpper').value);

            if (liLowerbound >= liUpperbound) 
            {
                source.errormessage = "Lower Range value should be less than Upper Range value.";
                args.IsValid = false;
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
                <h1>Add / List Normal Range
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Normal Range</li>
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
                            <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <asp:UpdatePanel ID = "updMainDept" runat = "server">
                                            <ContentTemplate>
                                                <div class="col-xs-9">
                                                    <div class="form-group">
                                                        <label>Main Department</label>
                                                        <asp:DropDownList ID = "ddlDepts" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                            OnSelectedIndexChanged="ddlDepts_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTest"
                                                        ControlToValidate="ddlDepts" runat="server" ErrorMessage="Main Department" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>    
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                        <%--<div class="col-xs-3">
                                            <label>&nbsp;</label>
                                            <button class="btn btn-block btn-primary btn-sm">List Tests</button>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Code</label>
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "200" placeholder="Enter code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-9">
                                            <asp:UpdatePanel ID = "updTests" runat = "server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label>Tests</label>
                                                        <asp:DropDownList ID = "ddlTests" runat = "server" CssClass = "form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTests"
                                                        ControlToValidate="ddlTests" runat="server" ErrorMessage="Test" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>    
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Sex</label>
                                                <asp:DropDownList ID = "ddlSex" runat = "server" CssClass = "form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSex"
                                                ControlToValidate="ddlSex" runat="server" ErrorMessage="Sex" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Lower</label>
                                                <asp:TextBox ID = "txtLower" runat = "server" MaxLength = "200" placeholder="Lower" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalLower"
                                                ControlToValidate="txtLower" runat="server" ErrorMessage="Lower Range" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltLower" runat = "server" TargetControlID = "txtLower" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Upper</label>
                                                <asp:TextBox ID = "txtUpper" runat = "server" MaxLength = "200" placeholder="Upper" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalUpper"
                                                ControlToValidate="txtUpper" runat="server" ErrorMessage="Upper Range" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltUpper" runat = "server" TargetControlID = "txtUpper" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Days</label>
                                                <asp:TextBox ID = "txtDays" runat = "server" MaxLength = "200" placeholder="days" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDays"
                                                ControlToValidate="txtDays" runat="server" ErrorMessage="Days" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                                <ajax:FilteredTextBoxExtender ID = "fltDays" runat = "server" TargetControlID = "txtDays" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Unit</label>
                                                <asp:TextBox ID = "txtUnit" runat = "server" MaxLength = "50" placeholder="Unit" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalUnit"
                                                ControlToValidate="txtUnit" runat="server" ErrorMessage="Unit" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                                <%--<ajax:FilteredTextBoxExtender ID = "fltUnit" runat = "server" TargetControlID = "txtUnit" FilterType = "Numbers">
                                                </ajax:FilteredTextBoxExtender>--%>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Remarks</label>
                                                <asp:TextBox ID = "txtRemarks" runat = "server" MaxLength = "200" placeholder="Remarks" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRemarks"
                                                ControlToValidate="txtRemarks" runat="server" ErrorMessage="Remarks" ValidationGroup="vgNormalRange"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                            Text = "Submit" ValidationGroup="vgNormalRange" OnClick="btnSubmit_Click"/>
                            <asp:CustomValidator ID = "cvalBounds" runat = "server" ClientValidationFunction = "ValidateBounds"
                            ValidationGroup="vgNormalRange"></asp:CustomValidator>
                            <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                              ValidationGroup="vgNormalRange" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
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
                                        <th>TestCode</th>
                                        <th>Test</th>
                                        <th>Remarks</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "4">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No normal ranges added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptRanges" runat = "server" OnItemCommand = "rptRanges_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stCode")%></td>
                                                <td><%#Eval("stTestName")%></td>
                                                <td><%#Eval("stRemarks")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inRangeId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inRangeId")%>'>
                                                        <i class="fa fa-fw fa-trash-o"></i><span>Delete</span>
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
    <!-- /.content-wrapper -->
    <asp:HiddenField ID="hidRangeId" runat="server" />
</asp:Content>
