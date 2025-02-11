﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-rate-list-mainlab.aspx.cs" Inherits="add_rate_list_mainlab" %>

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

        function fnValidateDefaultList() {
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
                alert('Please select atleast 1 price list.');
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
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add / List Rate Lists (Main Lab)
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Rate list Main lab</li>
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
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Code</label>                                             
                                                <asp:TextBox ID = "txtCode" runat = "server" MaxLength = "20" placeholder="code" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalCode"
                                                ControlToValidate="txtCode" runat="server" ErrorMessage="Code" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label>Rate List Name</label>
                                            <asp:TextBox ID = "txtMachineName" runat = "server" MaxLength = "50" placeholder="machine name" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalName"
                                            ControlToValidate="txtMachineName" runat="server" ErrorMessage="Name" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Description</label>
                                            <asp:TextBox ID = "txtDescription" runat = "server" MaxLength = "200" placeholder="Description" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDescription"
                                            ControlToValidate="txtDescription" runat="server" ErrorMessage="Description" ValidationGroup="vgColCenter"></asp:RequiredFieldValidator>
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
                    </div>
                </asp:Panel>
                </div>
                <div class="col-xs-6">
                    <asp:Label ID = "lblViewMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
                    <asp:Panel ID = "pnlViewContent" runat = "server">
                        <div class="box">
                        <div class="box-header">
                            <asp:Button ID = "btnDefaultPrice" runat = "server" CssClass = "btn btn-primary" 
                            Text = "Set as Default Price List" OnClientClick = "return fnValidateDefaultList();" OnClick="btnDefaultPrice_Click"/>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Code</th>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id = "trNoData" runat = "server">
                                        <td colspan = "4">
                                            <center>
                                                <asp:Label ID = "lblMsg" runat = "server" Text = "No rate lists added." style="text-align:center;"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID = "rptRateLists" runat = "server" OnItemCommand = "rptRateLists_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID = "rbtnDefaultPrice" runat = "server" onclick="fnCheckUnCheck(this.id);"
                                                    Checked = '<%#Eval("btdefaultRateList")%>' />
                                                    <asp:Label ID = "lblRateListId" runat = "server" Text = '<%#Eval("inRateListId")%>' Visible = "false"></asp:Label> 
                                                </td>
                                                <td><%#Eval("stRateListCode")%></td>
                                                <td><%#Eval("stRateListName")%></td>
                                                <td><%#Eval("stRateListDesc")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inRateListId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inRateListId")%>'>
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
    <asp:HiddenField ID="hidRateListId" runat="server" />
</asp:Content>
