<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="roles-rights-module-user.aspx.cs" Inherits="roles_rights_module_user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language = "javascript" type = "text/javascript">
        function funSelectAll(objChecked) 
        {
            var inputs = document.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) 
            {
                if (inputs[i].type == "checkbox" && inputs[i].disabled == false) 
                    inputs[i].checked = objChecked;
            }
        }

        function funSelectModule(objChecked, objFlagVal) 
        {
            var liRowsCount = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_hidRowsCount').value);
            var chkObj;
            for (var liCounter = 0; liCounter < liRowsCount; liCounter++) 
            {
                if (objFlagVal == "View") 
                {
                    if (liCounter <= 9)
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl0' + liCounter + '_chkView');
                    else
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl' + liCounter + '_chkView');

                    if (chkObj != null && chkObj.disabled == false)
                        chkObj.checked = objChecked;
                }
                else if (objFlagVal == "Add") 
                {
                    if (liCounter <= 9)
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl0' + liCounter + '_chkAdd');
                    else
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl' + liCounter + '_chkAdd');

                    if (chkObj != null && chkObj.disabled == false)
                        chkObj.checked = objChecked;
                }
                else if (objFlagVal == "Edit") 
                {
                    if (liCounter <= 9)
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl0' + liCounter + '_chkEdit');
                    else
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl' + liCounter + '_chkEdit');

                    if (chkObj != null && chkObj.disabled == false)
                        chkObj.checked = objChecked;
                }
                else 
                {
                    if (liCounter <= 9)
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl0' + liCounter + '_chkDelete');
                    else
                        chkObj = document.getElementById('ctl00_ContentPlaceHolder1_rptModules_ctl' + liCounter + '_chkDelete');

                    if (chkObj != null && chkObj.disabled == false)
                        chkObj.checked = objChecked;
                }
            }    
        }

        //ctl00_ContentPlaceHolder1_rptModules_ctl08_chkView
        //ctl00_ContentPlaceHolder1_rptModules_ctl14_chkView

    </script>

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Roles Rights Assign
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Roles Rights Assign</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary">
                            <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Role</label>
                                            <asp:DropDownList ID = "ddlRoles" runat = "server" CssClass = "form-control" AutoPostBack = "true"
                                                OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRole"
                                            ControlToValidate="ddlRoles" runat="server" ErrorMessage="Select Role." ValidationGroup="vgCheckRole"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <%--<div class="box-footer">
                                    <asp:Button ID = "btnCheck" runat = "server" CssClass = "btn btn-primary" 
                                    Text = "Check" ValidationGroup="vgCheckRole" OnClick="btnCheck_Click"/>
                                    <asp:ValidationSummary ID="valCheckRoles" runat="server" 
                                    ValidationGroup="vgCheckRole" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                </div>--%>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div style="padding-bottom:5px;">
                                    <asp:CheckBox ID = "chkSelectAll" runat = "server" onclick = "funSelectAll(this.checked);"/>
                                    &nbsp;&nbsp;<span style="vertical-align:top;">Select All</span>
                                </div>
                                    
                                  <table id="example1" class="table table-bordered table-striped" style="width:75%">
                                    <thead>
                                      <tr>
                                        <th style="width:20%;">Module</th>
                                        <th style="width:5%;text-align:center;">
                                        <asp:CheckBox ID = "chkViewAll" runat = "server" onclick = "funSelectModule(this.checked,'View');"/>&nbsp;&nbsp;<span style="vertical-align:top;">View</span></th>
                                        <th style="width:5%;text-align:center;">
                                        <asp:CheckBox ID = "chkAddAll" runat = "server" onclick = "funSelectModule(this.checked,'Add');"/>&nbsp;&nbsp;<span style="vertical-align:top;">Add</span></th>
                                        <th style="width:5%;text-align:center;">
                                        <asp:CheckBox ID = "chkEditAll" runat = "server" onclick = "funSelectModule(this.checked,'Edit');"/>&nbsp;&nbsp;<span style="vertical-align:top;">Edit</span></th>
                                        <th style="width:5%;text-align:center;">
                                        <asp:CheckBox ID = "chkDeleteAll" runat = "server" onclick = "funSelectModule(this.checked,'Delete');"/>&nbsp;&nbsp;<span style="vertical-align:top;">Delete</span></th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                         <asp:Repeater ID = "rptModules" runat = "server" OnItemDataBound="rptModules_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID = "lblModuleId" runat = "server" Text = '<%#Eval("inModuleId")%>' Visible = "false"></asp:Label>
                                                        <%#Eval("stModuleName")%>
                                                    </td>
                                                    <td align = "center">
                                                        <asp:CheckBox ID = "chkView" runat = "server"   Checked = '<%#Convert.ToBoolean(Eval("btView"))%>' />
                                                    </td>
				                                    <td align = "center">
                                                        <asp:CheckBox ID = "chkAdd" runat = "server" Checked = '<%#Convert.ToBoolean(Eval("btAdd"))%>' />
                                                    </td>
				                                    <td align = "center">
                                                        <asp:CheckBox ID = "chkEdit" runat = "server" Checked = '<%#Convert.ToBoolean(Eval("btEdit"))%>' />
                                                    </td>
					                                <td align = "center">
                                                        <asp:CheckBox ID = "chkDelete" runat = "server" Checked = '<%#Convert.ToBoolean(Eval("btDelete"))%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                  </table>
                    </div>
                <div class="box-footer">
                    <asp:Button ID = "btnAssignRights" runat = "server" CssClass = "btn btn-primary" 
                     Text = "Assign Rights" ValidationGroup="vgCheckRole" OnClick="btnAssignRights_Click"/>
                    <asp:ValidationSummary ID="valCheckRoles" runat="server" 
                     ValidationGroup="vgCheckRole" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box -->
    </div>
            </asp:Panel>
        <!-- /.col -->
        <!-- /.row -->
    </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <asp:HiddenField ID = "hidRowsCount" runat = "server" />
</asp:Content>
