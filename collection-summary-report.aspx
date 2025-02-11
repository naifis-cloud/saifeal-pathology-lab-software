<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="collection-summary-report.aspx.cs" Inherits="collection_summary_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function clearDateValue(objDateId) {
            document.getElementById(objDateId).value = "";
        }

        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10001;
        }
        function onCalendarHidden(sender, args) {
            sender._popupBehavior._element.style.zIndex = 0;
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
                <h1>Collection Summary Report
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Collection Summary Report</li>
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
                            <!-- form start -->
                            <form role="form">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Date From:&nbsp;&nbsp;
                                                <a href="javascript:void(0);" id = "anchClearFromDt" title="edit" style="font-weight:normal;" 
                                                 onclick = "clearDateValue('ctl00_ContentPlaceHolder1_txtFromdate');"><span>Clear</span></a>
                                                </label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <img id = "imgFromDate" src = "images/date.png" title = "Select date" />
                                                    </div>
                                                    <asp:TextBox ID = "txtFromdate" CssClass = "form-control pull-right" runat = "server" MaxLength = "10" 
                                                        ReadOnly = "true"></asp:TextBox>
                                                    <ajax:CalendarExtender ID = "calExtFromDate" runat = "server" TargetControlID = "txtFromdate"
                                                    PopupButtonID = "imgFromDate" Format = "dd-MM-yyyy" Animated = "true"
                                                    OnClientShown="onCalendarShown"  OnClientHidden="onCalendarHidden"></ajax:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label>Date To:
                                                &nbsp;&nbsp;
                                                <a href="javascript:void(0);" id = "anchClearToDt" title="edit" style="font-weight:normal;"
                                                onclick = "clearDateValue('ctl00_ContentPlaceHolder1_txtTodate');"><span>Clear</span></a>
                                                </label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <img id = "imgToDate" src = "images/date.png" title = "Select date" />
                                                    </div>
                                                    <asp:TextBox ID = "txtTodate" CssClass = "form-control pull-right" runat = "server" MaxLength = "10"
                                                        ReadOnly = "true"></asp:TextBox>
                                                    <ajax:CalendarExtender ID = "calExtTodate" runat = "server" TargetControlID = "txtTodate"
                                                    PopupButtonID = "imgToDate" Format = "dd-MM-yyyy" Animated = "true" OnClientShown="onCalendarShown"  
                                                    OnClientHidden="onCalendarHidden"></ajax:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>By Days</label>
                                            <asp:DropDownList ID = "ddlNoOfDays" runat = "server" CssClass = "form-control">
                                            <asp:ListItem Text = "Select" Value = "-1"></asp:ListItem>
                                            <asp:ListItem Text = "Today" Value = "0"></asp:ListItem>
                                            <asp:ListItem Text = "Last 7 Days" Value = "7"></asp:ListItem>
                                            <asp:ListItem Text = "Last 30 Days" Value = "30"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" Text = "Submit" OnClick="btnSubmit_Click"/>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table>
                                    <tr>
                                        <td>
                                            <span class="active">Download options:</span>&nbsp;
                                            <asp:LinkButton ID = "lnkbtnExcel" runat = "server" onclick="lnkbtnExcel_Click">
                                                <img src = "images/excel.png" title = "Export to excel" />
                                            </asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID = "lnkbtnPDF" runat = "server" onclick="lnkbtnPDF_Click">
                                                <img src= "images/pdf.png" title = "Export to PDF" />
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table><br />
                                <asp:Panel ID = "pnlSummaryReport" runat = "server">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Reg #</th>
                                            <th>Patient Name</th>
                                            <th>Ref Doctor</th>
                                            <th>Visit Chgs</th>
                                            <th>Urgent Chgs</th>
                                            <th>Total Amt</th>
                                            <th>Disc Amt</th>
                                            <th>Net Amt</th>
                                            <th>Advance Amt</th>
                                            <th>Balance Amt</th>
                                            <th>Doc Referal Amt</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id = "trNoData" runat = "server">
                                            <td colspan = "11">
                                                <center>
                                                    <asp:Label ID = "lblMsg" runat = "server" Text = "No data found!" style="text-align:center;"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                         <asp:Repeater ID = "rptCollection" runat = "server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#String.Format("{0:dd-MM-yyyy}",Eval("dtCreationDate"))%></td>
                                                    <td><%#Eval("stRegNo")%></td>
                                                    <td><%#Eval("stPatientName")%></td>
                                                    <td><%#Eval("RefDoctorName")%></td>
                                                    <td><%#Eval("inVisitChgs")%></td>
                                                    <td><%#Eval("inEmergencyChgs")%></td>
                                                    <td><%#Eval("inTotalTestChgs")%></td>
                                                    <td><%#Eval("inDiscountAmt")%></td>
                                                    <td><%#Eval("inNetAmt")%></td>
                                                    <td><%#Eval("inAdvanceAmt")%></td>
                                                    <td><%#Eval("inBalanceAmt")%></td>
                                                    <td><%#Eval("DoctorRefAmt")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                </asp:Panel>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
