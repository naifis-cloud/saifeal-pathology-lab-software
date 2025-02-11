<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="patient-registration.aspx.cs" Inherits="patient_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            CalculateNetAmount();
        }

        function ValidateSelectedTests(source, args) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_lstSelectedTests').options.length == 0)
                args.IsValid = false;
        }


        function EnableDisableRefDoctor(objRefDocVal) {
            if (objRefDocVal != "Other") {
                document.getElementById('ctl00_ContentPlaceHolder1_txtRefDoctorName').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalDoctors').enabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalOtherRefDoctor').enabled = false;
            }
            else if (objRefDocVal == "Other") {
                document.getElementById('ctl00_ContentPlaceHolder1_txtRefDoctorName').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalDoctors').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_reqvalOtherRefDoctor').enabled = true;
            }
        }

        function EnableDisableDiscountField(objDiscPerc) {
            if (parseInt(objDiscPerc) == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').readOnly = false;
                document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').value = "";
            }
            else {
                var DiscountPercentage = parseInt(objDiscPerc);
                var DiscountAmt = 0;
                var TotalTestAmount = 0;

                document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').readOnly = true;

                //calculating the discount amount if percentage is selected//
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value != "")
                    TotalTestAmount = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value);

                //adding the visiting charges
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtVisit').value != "")
                    TotalTestAmount += parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtVisit').value);

                //adding the emergency charges
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtEmergency').value != "")
                    TotalTestAmount += parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtEmergency').value);

                //adding other charges
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtOtherChgs').value != "")
                    TotalTestAmount += parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtOtherChgs').value);

                DiscountAmt = parseInt(parseInt(TotalTestAmount) * DiscountPercentage / 100);
                document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').value = DiscountAmt;
            }
            CalculateNetAmount();
        }

        function CalculateNetAmount() {
            var DiscountAmt = 0;
            var NetAmount = 0;
            var BalanceAmt = 0;
            var AdvanceAmt = 0;
            var TotalTestAmount = 0;

            //var DiscountPercentage = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_ddlDiscount').value);
            var DiscountPercentage = 0;

            if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value != "")
                TotalTestAmount = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value);

            //adding the visiting charges
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtVisit').value != "")
                TotalTestAmount += parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtVisit').value);

            //adding the emergency charges
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtEmergency').value != "")
                TotalTestAmount += parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtEmergency').value);

            //adding other charges
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtOtherChgs').value != "")
                TotalTestAmount += parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtOtherChgs').value);


            if (document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').value != "") {
                DiscountAmt = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').value);

                //Setting the discount percentage based on discount amount entered//
                DiscountPercentage = Math.round((DiscountAmt * 100) / TotalTestAmount);
                document.getElementById('ctl00_ContentPlaceHolder1_ddlDiscount').value = DiscountPercentage;
            }
            else {
                DiscountPercentage = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_ddlDiscount').value);
                DiscountAmt = parseInt(parseInt(TotalTestAmount) * DiscountPercentage / 100);
            }

            document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').value = DiscountAmt;

            if (DiscountAmt > TotalTestAmount) {
                alert('Discount amount cannot be greater than Total amount.');
                document.getElementById('ctl00_ContentPlaceHolder1_ddlDiscount').selectedIndex = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_txtDiscAmount').value = "";
                DiscountAmt = 0;
            }

            NetAmount = parseInt(TotalTestAmount) - parseInt(DiscountAmt);
            document.getElementById('ctl00_ContentPlaceHolder1_txtNetAmount').value = NetAmount;

            if (document.getElementById('ctl00_ContentPlaceHolder1_txtAdvanceAmt').value != "")
                AdvanceAmt = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtAdvanceAmt').value);

            if (AdvanceAmt > NetAmount) {
                alert('Advance amount cannot be greater than Net amount.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtAdvanceAmt').value = "";
            }
            else {
                BalanceAmt = parseInt(NetAmount) - parseInt(AdvanceAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_txtBalanceAmt').value = BalanceAmt;
            }
        }

        function ValidateAmounts(source, args) 
        {
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtTotal').value) == 0
            || parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNetAmount').value) == 0)
                args.IsValid = false;
        }

    </script>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
                <h1>Add New Patient Test           
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Masters</a></li>
                    <li class="active">Add Test</li>
                </ol>
            </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID = "lblMessage" runat = "server" Visible = "false" ForeColor = "Red"></asp:Label>
            <asp:Panel ID = "pnlMainContent" runat = "server">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <!-- /.box-header -->
                            <!-- form start -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Reg #</label>
                                                <asp:TextBox ID = "txtRegNo" runat = "server" MaxLength = "50" placeholder="reg no" CssClass = "form-control"
                                                Enabled = "false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Type</label>
                                                <asp:DropDownList ID = "ddlType" runat = "server" CssClass = "form-control">
                                                    <asp:ListItem Text = "OPD" Value = "OPD"></asp:ListItem>
                                                    <asp:ListItem Text = "IPD" Value = "IPD"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputPassword1">Email</label>
                                                <asp:TextBox ID = "txtEmail" runat = "server" MaxLength = "50" placeholder="email id" CssClass = "form-control"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalEmail"
                                                ControlToValidate="txtEmail" runat="server" ErrorMessage="Email Address" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" Display="None" ID="regexpEmail"
                                                runat="server" ControlToValidate="txtEmail" ErrorMessage="Valid Email Address"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgPatientRegn"></asp:RegularExpressionValidator>
                                                <%--<i class="fa fa-fw fa-refresh"></i>--%>
                                            </div>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Title</label>
                                            <asp:DropDownList ID = "ddlTitle" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Patient Name</label>
                                                <asp:TextBox ID = "txtPatientName" runat = "server" MaxLength = "100" placeholder="Patient Name" CssClass = "form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPatientName"
                                                ControlToValidate="txtPatientName" runat="server" ErrorMessage="Patient Name" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label>Gender</label>
                                            <asp:DropDownList ID = "ddlGender" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalGender"
                                            ControlToValidate="ddlGender" runat="server" ErrorMessage="Gender" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-1">
                                            <label>Age</label>
                                            <asp:TextBox ID = "txtAge" runat = "server" MaxLength = "3" placeholder="Age" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAge"
                                             ControlToValidate="txtAge" runat="server" ErrorMessage="Age" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                             <ajax:FilteredTextBoxExtender ID = "fltAge" runat = "server" TargetControlID = "txtAge" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Mobile</label>
                                            <asp:TextBox ID = "txtMobile" runat = "server" MaxLength = "10" placeholder="Mobile" CssClass = "form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalMobile"
                                            ControlToValidate="txtMobile" runat="server" ErrorMessage="Mobile" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>
                                            <ajax:FilteredTextBoxExtender ID = "fltMobile" runat = "server" TargetControlID = "txtMobile" FilterType = "Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Sample Time</label>
                                            <asp:TextBox ID = "txtSampleTime" runat = "server" MaxLength = "50" placeholder="Sample Time" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalSampleTime"
                                            ControlToValidate="txtSampleTime" runat="server" ErrorMessage="Sample Time" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-5">
                                            <label>Ref by Partner</label>
                                            <asp:TextBox ID = "txtPartner" runat = "server" MaxLength = "50" placeholder="Partner name" CssClass = "form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Address</label>
                                            <asp:TextBox ID = "txtAddress" runat = "server" MaxLength = "100" placeholder="address" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalAddress"
                                            ControlToValidate="txtAddress" runat="server" ErrorMessage="Address" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Clinical History</label>
                                            <asp:TextBox ID = "txtHistory" runat = "server" MaxLength = "100" placeholder="Clinical History" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalHistory"
                                            ControlToValidate="txtHistory" runat="server" ErrorMessage="Clinical History" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Remarks</label>
                                            <asp:TextBox ID = "txtRemarks" runat = "server" MaxLength = "100" placeholder="Remarks" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRemarks"
                                            ControlToValidate="txtRemarks" runat="server" ErrorMessage="Remarks" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-xs-2">
                                            <label>Ref by Doctor</label>
                                            <asp:DropDownList ID = "ddlRefDoctors" runat = "server" CssClass = "form-control" onchange = "EnableDisableRefDoctor(this.value);">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDoctors"
                                             ControlToValidate="ddlRefDoctors" runat="server" ErrorMessage="Ref Doctor Name" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>    --%>
                                        </div>
                                        <div class="col-xs-3">
                                            <label>&nbsp;</label>
                                            <asp:TextBox ID = "txtRefDoctorName" runat = "server" MaxLength = "100" CssClass = "form-control" Enabled = "false"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalOtherRefDoctor"
                                              ControlToValidate="txtRefDoctorName" runat="server" ErrorMessage="Ref Doctor Name" ValidationGroup="vgPatientRegn" Enabled = "false">
                                             </asp:RequiredFieldValidator>    --%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>Select Department</label>
                                            <asp:DropDownList ID = "ddlDepts" runat = "server" CssClass = "form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalDept"
                                             ControlToValidate="ddlDepts" runat="server" ErrorMessage="Department" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>    
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                        &nbsp;<br />
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID = "updAddTest" runat = "server">
                                                    <ContentTemplate>
                                                        <div class="col-xs-3" style="padding-left:0px;">
                                                            <asp:Button ID = "btnAddTest" runat = "server" CssClass = "btn btn-primary" 
                                                                Text = "Add Test" ValidationGroup="vgAddTest" OnClick="btnAddTest_Click"/>
                                                            <asp:ValidationSummary ID="valSummAddTest" runat="server" 
                                                            ValidationGroup="vgAddTest" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="clearfix"></div><br />
                                            <label>Lists Tests (<span>Type in the list box to search..</span>)</label><br /><br />
                                            <asp:UpdatePanel ID = "updTests" runat = "server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID = "lstTests" runat = "server" SelectionMode = "Multiple" CssClass = "form-control"></asp:ListBox>
                                                    <ajax:ListSearchExtender ID = "lstSearch" runat = "server" TargetControlID = "lstTests"
                                                    QueryTimeout = "0"  PromptCssClass="PromptCSSClass" PromptPosition = "Top" QueryPattern = "Contains" IsSorted = "true"></ajax:ListSearchExtender> 
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalListTests"
                                                     ControlToValidate="lstTests" runat="server" ErrorMessage="Select atleast 1 Test to Add" ValidationGroup="vgAddTest"></asp:RequiredFieldValidator>    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:UpdatePanel ID = "updRemoveTest" runat = "server">
                                                    <ContentTemplate>
                                                        <div class="col-xs-3" style="padding-left:0px;">
                                                            <asp:Button ID = "btnRemoveTest" runat = "server" CssClass = "btn btn-primary" 
                                                            Text = "Remove Test" ValidationGroup="vgRemoveTest" OnClick="btnRemoveTest_Click"/>
                                                            <asp:ValidationSummary ID="valSummRemove" runat="server" 
                                                            ValidationGroup="vgRemoveTest" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="clearfix"></div><br />
                                            <label>Selected Tests</label><br /><br />
                                            <asp:UpdatePanel ID = "updSelectedTests" runat = "server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID = "lstSelectedTests" runat = "server" SelectionMode = "Multiple" CssClass = "form-control"></asp:ListBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalRemoveTest"
                                                     ControlToValidate="lstSelectedTests" runat="server" ErrorMessage="Select atleast 1 Test to Remove" ValidationGroup="vgRemoveTest"></asp:RequiredFieldValidator>    
                                                     <asp:CustomValidator SetFocusOnError="true" Display="None" ID="customSelectedTest" ClientValidationFunction = "ValidateSelectedTests"
                                                     runat="server" ErrorMessage="Select atleast 1 Test" ValidationGroup="vgPatientRegn"></asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                       </div>
                                    </div>
                                    <div class="clearfix">
                                        &nbsp;<br /></div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label>VialID</label>
                                            <asp:TextBox ID = "txtVIALId" runat = "server" MaxLength = "50" placeholder="VialID" CssClass = "form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalVialID"
                                            ControlToValidate="txtVIALId" runat="server" ErrorMessage="VialID" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                   <div class="clearfix">
                                        &nbsp;<br /></div>
                                        <div class="row">
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1">Visit Charges</label>
                                                    <asp:TextBox ID = "txtVisit" runat = "server" MaxLength = "50" placeholder="Visit" CssClass = "form-control"
                                                    onblur = "CalculateNetAmount()"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID = "fltVisit" runat = "server" TargetControlID = "txtVisit" FilterType = "Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label for="exampleInputPassword1">Emergency Charges</label>
                                                    <asp:TextBox ID = "txtEmergency" runat = "server" MaxLength = "50" placeholder="Emergency" CssClass = "form-control"
                                                    onblur = "CalculateNetAmount()"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID = "fltEmergency" runat = "server" TargetControlID = "txtEmergency" FilterType = "Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label for="exampleInputPassword1">Other Charges</label>
                                                    <asp:TextBox ID = "txtOtherChgs" runat = "server" MaxLength = "50" placeholder="Other" CssClass = "form-control"
                                                    onblur = "CalculateNetAmount()"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID = "flOther" runat = "server" TargetControlID = "txtOtherChgs" FilterType = "Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <asp:UpdatePanel ID = "updTotalTests" runat = "server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <label for="exampleInputPassword1">Total Tests (INR)</label>
                                                            <asp:TextBox ID = "txtTotal" runat = "server" MaxLength = "6" placeholder="Total Tests" CssClass = "form-control" ReadOnly = "true"
                                                            onchange = "CalculateNetAmount()"></asp:TextBox>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-xs-2">
                                                <label>Pay Type</label>
                                                <asp:DropDownList ID = "ddlPayType" runat = "server" CssClass = "form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" Display="None" ID="reqvalPayType"
                                                     ControlToValidate="ddlPayType" runat="server" ErrorMessage="Pay Type" ValidationGroup="vgPatientRegn"></asp:RequiredFieldValidator>    
                                            </div>
                                            <div class="col-xs-2">
                                                <label>Patient Disc %</label>
                                                <asp:DropDownList ID = "ddlDiscount" runat = "server" CssClass = "form-control" onchange = "EnableDisableDiscountField(this.value);">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label for="exampleInputPassword1">Patient Discount Amt</label>
                                                    <asp:TextBox ID = "txtDiscAmount" runat = "server" MaxLength = "6" placeholder="Discount amount" CssClass = "form-control"
                                                    onblur = "CalculateNetAmount()"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID = "fltDiscount" runat = "server" TargetControlID = "txtDiscAmount" FilterType = "Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </div>
                                                </div>
                                                <div class="col-xs-2">
                                                    <div class="form-group">
                                                        <label for="exampleInputPassword1">Net Amt</label>
                                                        <asp:TextBox ID = "txtNetAmount" runat = "server" MaxLength = "6" placeholder="Net amount" CssClass = "form-control" ReadOnly = "true"
                                                        onchange = "CalculateNetAmount()"></asp:TextBox>
                                                    </div>
                                                    </div>
                                                    <div class="col-xs-2">
                                                        <div class="form-group">
                                                            <label for="exampleInputPassword1">Advance</label>
                                                            <asp:TextBox ID = "txtAdvanceAmt" runat = "server" MaxLength = "6" placeholder="Advance amount" CssClass = "form-control"
                                                            onblur = "CalculateNetAmount()"></asp:TextBox>
                                                            <ajax:FilteredTextBoxExtender ID = "fltAdvanceAmt" runat = "server" TargetControlID = "txtAdvanceAmt" FilterType = "Numbers">
                                                            </ajax:FilteredTextBoxExtender>
                                                        </div>

                                                    </div>
                                                    <div class="col-xs-2">
                                                        <div class="form-group">
                                                            <label for="exampleInputPassword1">Balance</label>
                                                            <asp:TextBox ID = "txtBalanceAmt" runat = "server" MaxLength = "6" placeholder="Balance amount" CssClass = "form-control" ReadOnly = "true"
                                                            onchange = "CalculateNetAmount()"></asp:TextBox>
                                                        </div>
                                                    </div>
                                            <div class="col-xs-3">
                                                <label>Test Status</label>
                                                <asp:DropDownList ID = "ddlTestStatus" runat = "server" CssClass = "form-control" Enabled = "false">
                                                </asp:DropDownList>
                                            </div>
                                            <%-- <div class="col-xs-2">
                                                <label>&nbsp;<br />&nbsp;</label>
                                                <button type="submit" class="btn btn-primary">Calculate</button>
                                            </div>--%>
                                        </div>
                                    <!-- /.box-body -->
                                    <div class="box-footer">
                                        <asp:Button ID = "btnSubmit" runat = "server" CssClass = "btn btn-primary" 
                                        Text = "Submit" ValidationGroup="vgPatientRegn" OnClick="btnSubmit_Click"/>
                                        <asp:CustomValidator SetFocusOnError="true" Display="None" ID="cvalValidateAmounts" ClientValidationFunction = "ValidateAmounts" 
                                        runat="server" ErrorMessage="Net Amount/Total Tests Amount cannot be 0" ValidationGroup="vgPatientRegn"></asp:CustomValidator>
                                        <asp:ValidationSummary ID="valColCenter" runat="server" HeaderText="Please Provide..."
                                        ValidationGroup="vgPatientRegn" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                                    </div>
                        </div>
                        <!-- /.box -->
                    </div>
                    <!--/.col (left) -->
                    <!-- right column -->
                    <!--/.col (right) -->
                </div>
                <!-- /.row -->
                </div>
            </asp:Panel>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>
