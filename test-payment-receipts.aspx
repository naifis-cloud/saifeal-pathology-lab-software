<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="test-payment-receipts.aspx.cs" Inherits="test_payment_receipts" %>

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
                <h1>Test Payment Receipts
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Tables</a></li>
                    <li class="active">Payment Receipts</li>
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
                                            <label>Department</label>
                                            <asp:DropDownList ID = "ddlDepts" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>Patient Name</label>
                                            <asp:TextBox ID = "txtPatientName" placeholder="patient name" CssClass = "form-control" runat = "server" MaxLength = "50"></asp:TextBox>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Reg no</label>
                                            <asp:TextBox ID = "txtRegNo" placeholder="Reg no" CssClass = "form-control" runat = "server" MaxLength = "20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <button type="submit" class="btn btn-primary">Submit</button>
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
                                            <asp:Button ID = "btnUnpaid" runat = "server" CssClass = "btn bg-red btn-flat margin" 
                                            Text = "Unpaid" CausesValidation = "false" OnClick="btnUnpaid_Click"/>
                                        </td>
                                        <td>
                                            <asp:Button ID = "btnPartialPaid" runat = "server" CssClass = "btn bg-orange btn-flat margin" 
                                            Text = "Partial Paid" CausesValidation = "false" OnClick="btnPartialPaid_Click"/>
                                        </td>
                                        <td>
                                            <asp:Button ID = "btnFullyPaid" runat = "server" CssClass = "btn bg-green btn-flat margin" 
                                            Text = "Fully Paid" CausesValidation = "false" OnClick="btnFullyPaid_Click"/>
                                        </td>
                                        <td>
                                            <asp:Button ID = "btnShowAll" runat = "server" CssClass = "btn bg-blue btn-flat margin" 
                                            Text = "Show All" CausesValidation = "false" OnClick="btnShowAll_Click"/>
                                        </td>
                                    </tr>
                                </table>
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th nowrap = "nowrap">Reg No</th>
                                            <th>Patient Name</th>
                                            <th>Sex</th>
                                            <%--<th>Age</th>--%>
                                            <th>Ref Doc</th>
                                            <th>Test Name</th>
                                            <th>Bill Status</th>
                                            <th nowrap = "nowrap">Total Amt</th>
                                            <th>Advance</th>
                                            <th>Balance</th>
                                            <th>Amount</th>
                                            <th>Action</th>
                                            <th>Print</th>
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
                                        <asp:Repeater ID = "rptPatients" runat = "server" OnItemCommand="rptPatients_ItemCommand" 
                                            OnItemDataBound="rptPatients_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("stRegNo")%></td>
                                                    <td>
                                                        <asp:Label ID = "lblPatientName" runat = "server" Text = '<%#Eval("stPatientName")%>'></asp:Label>
                                                        <asp:Label ID = "lblBillStatusId" runat = "server" Text = '<%#Eval("BillStatusId")%>' Visible = "false"></asp:Label>
                                                    </td>
                                                    <td><%#Eval("stGender")%></td>
                                                    <%--<td><%#Eval("inAge")%></td>--%>
                                                    <td><%#Eval("RefDoctorName")%></td>
                                                    <td><%#Eval("TestNames")%></td>
                                                    <td>
                                                        <span class="<%#GetPaymentStatusCSS(Convert.ToInt32(Eval("BillStatusId")))%>"><%#Eval("BillStatus")%></span>
                                                    </td>
                                                    <td><%#Eval("inNetAmt")%></td>
                                                    <td><%#Eval("inAdvanceAmt")%></td>
                                                    <td><asp:Label ID = "lblBalanceAmt" runat = "server" Text = '<%#Eval("inBalanceAmt")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID = "txtAmount" placeholder="Amount" CssClass = "form-control" runat = "server" MaxLength = "8"
                                                        Width = "50px">
                                                        </asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID = "fltAmount" runat = "server" TargetControlID = "txtAmount" FilterType = "Numbers">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID = "btnUpdate" runat = "server" CssClass = "btn btn-primary" 
                                                          Text = "Update" CommandName = "eUpdate" CommandArgument = '<%#Eval("inPatientId")%>'/>
                                                    </td>
                                                    <td>
                                                        <a href = "payment_receipt.aspx?pid=<%#Eval("inPatientId")%>" target = "_blank" title="Print receipt" ><i class="fa fa-edit"></i><span>Receipt</span></a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </section>
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
