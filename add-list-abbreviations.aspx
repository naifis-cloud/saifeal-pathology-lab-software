﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-abbreviations.aspx.cs" Inherits="add_list_abbreviations" %>

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
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add / List Abbreviations
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Abbreviations</li>
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
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label>Test</label>
                                                <asp:DropDownList ID = "ddlTests" runat = "server" CssClass = "form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTest"
                                                ControlToValidate="ddlTests" runat="server" ErrorMessage="Test" ValidationGroup="vgTestAbbrev"></asp:RequiredFieldValidator>    
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Abbreviation</label>
                                            <asp:TextBox ID = "txtAbbrev" runat = "server" MaxLength = "20" placeholder="Abbreviation" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAbbrev"
                                            ControlToValidate="txtAbbrev" runat="server" ErrorMessage="Abbreviation" ValidationGroup="vgTestAbbrev">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-6">
                                            <label>Description</label>
                                            <asp:TextBox ID = "txtDesc" runat = "server" MaxLength = "200" placeholder="Description" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvaldesc"
                                            ControlToValidate="txtDesc" runat="server" ErrorMessage="Description" ValidationGroup="vgTestAbbrev">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                            Text = "Submit" ValidationGroup="vgTestAbbrev" OnClick="btnSubmit_Click"/>
                            <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                            ValidationGroup="vgTestAbbrev" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                        </div>
                        </form>
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
                                        <th>Test Name</th>
                                        <th>Abbreviation</th>
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
                                    <asp:Repeater ID = "rptTestAbbrevs" runat = "server" OnItemCommand = "rptTestAbbrevs_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("stTestName")%></td>
                                                <td><%#Eval("stAbbrevCode")%></td>
                                                <td><%#Eval("stAbbrevDesc")%></td>
                                                <td>
                                                    <asp:LinkButton ID = "lnkbtnEdit" runat = "server" ToolTip = "edit" CommandName = "eEdit" CommandArgument = '<%#Eval("inTestAbbrevId")%>'>
                                                        <i class="fa fa-edit"></i> <span>Edit</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID = "lnkbtnDelete" runat = "server" ToolTip = "delete" OnClientClick = "javascript:return confirm('Are you sure you want to delete?');"
                                                     CommandName = "eDelete" CommandArgument = '<%#Eval("inTestAbbrevId")%>'>
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
    <asp:HiddenField ID="hidTestAbbrevId" runat="server" />
</asp:Content>
