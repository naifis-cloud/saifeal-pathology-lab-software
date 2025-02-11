<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="add-list-interpretation.aspx.cs" Inherits="add_list_interpretation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add / List interpretation
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Interpretation</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMainMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group pad">
                                                <label>Tests</label>
                                                <asp:DropDownList ID = "ddlTests" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                    OnSelectedIndexChanged="ddlTests_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTest"
                                                ControlToValidate="ddlTests" runat="server" ErrorMessage="Test" ValidationGroup="vgTestInter"></asp:RequiredFieldValidator>    
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            
                                            <div>
                                                <!-- /.box-header -->
                                                <div class='box-body'>
                                                    <asp:TextBox ID = "txtInterpretation" runat = "server" MaxLength = "50" placeholder="Place some text here" CssClass = "textarea" 
                                                    style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalTestInter"
                                                    ControlToValidate="txtInterpretation" runat="server" ErrorMessage="Interpretation" ValidationGroup="vgTestInter"></asp:RequiredFieldValidator>    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer pad">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Submit" ValidationGroup="vgTestInter" OnClick="btnSubmit_Click"/>
                                    <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                    ValidationGroup="vgTestInter" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>
                            </div>
                    </div>

                    <!-- /.col -->
                </div>
            </asp:Panel>
                <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <asp:HiddenField ID="hidTestInterId" runat="server" />
</asp:Content>
