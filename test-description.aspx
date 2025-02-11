<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="test-description.aspx.cs" Inherits="add_list_interpretation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Description for Test - <asp:Label ID = "lblTestName" runat = "server"></asp:Label>
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Description</li>
                </ol>
        </section>
        <!-- Main content -->
        <section class="content">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-12">
                                                <div class='box-body'>
                                                    <asp:TextBox ID = "txtDescription" runat = "server" MaxLength = "50" placeholder="Place some text here" CssClass = "textarea" 
                                                    style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTestInter"
                                                    ControlToValidate="txtDescription" runat="server" ErrorMessage="Description" ValidationGroup="vgTestDescription"></asp:RequiredFieldValidator>    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                <!-- /.box-body -->
                                <div class="box-footer pad">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgTestDescription" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgTestDescription" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            </div> 
                        </div>
                    </div>
                </section>
                    <!-- /.col -->
            </div>
                <!-- /.row -->
        <!-- /.content -->
</asp:Content>
