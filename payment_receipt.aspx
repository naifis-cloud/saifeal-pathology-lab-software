<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payment_receipt.aspx.cs"
    Inherits="payment_receipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Patient Report</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="font-awesome/css/font-awesome.min.css" />
    <%--<script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>--%>
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>
    <style type="text/css" runat="server" id="htmlCSS"></style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="book" id="dvMain" runat="server">
        <div class="page">
            <div class="subpage">
                <!--Patient report start-->
                <div bgcolor='#e4e4e4' text='#ff6633' link='#666666' vlink='#666666' alink='#ff6633'
                    style='margin: 0; font-family: Arial,Helvetica,sans-serif; border-bottom: 1'>
                    <table background='' bgcolor='#e4e4e4' width='100%' style='padding: 20px 0 20px 0'
                        cellspacing='0' border='0' align='center' cellpadding='0'>
                        <tbody>
                            <tr>
                                <td>
                                    <table width='620' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF'
                                        style='border-radius: 5px;'>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table width='620' border='0' cellspacing='0' cellpadding='0' style='border-bottom: solid 1px #e5e5e5'>
                                                        <tbody>
                                                            <tr>
                                                                <td align='left' valign='top' style='padding: 0px 5px 0px 5px'>
                                                                    <table height='20px' width='100%' border='0' cellpadding='0' cellspacing='0'>
                                                                        <tbody>
                                                                            <!--Header start-->
                                                                            <tr id="dvHeader" runat="server">
                                                                                <td align='left' valign='top' style='padding: 0px 5px 0px 5px'>
                                                                                    <div id="dvTopPart" runat="server">
                                                                                        <table height='20px' width='100%' border='0' cellpadding='0' cellspacing='0'>
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td height='10px' valign='top' style='color: #404041; font-size: 13px; padding: 5px 5px 0px 20px'>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign='top' style='color: #404041; font-size: 19px; padding: 5px 5px 0px 20px'>
                                                                                                        <strong>
                                                                                                            <asp:Label ID="lblLabName" runat="server"></asp:Label>
                                                                                                        </strong>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign='top' style='color: #404041; font-size: 13px; padding: 5px 5px 0px 20px'>
                                                                                                        <strong></strong>
                                                                                                        <img id="imgLogo" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                                <td align='left' valign='top' colspan="2" style='padding: 0px 5px 0px 5px'>
                                                                                    <table height='100%' width='100%' border='0' cellpadding='3' cellspacing='3' style='border-bottom: solid 1px #e5e5e5'>
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 15px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                        <strong>Lab Address:</strong>
                                                                                                        <asp:Label ID="lblLabAddress" runat="server"></asp:Label>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <strong>Tel:</strong> <a href="#" target='_blank'>
                                                                                                            <asp:Label ID="lblLabPhone" runat="server"></asp:Label>
                                                                                                        </a>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <strong>Email:</strong> <a href='mailto:info@preview.co.za'>info@preview.com</a>
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <!--Header end-->
                                                                            <!--Pateint Details start-->
                                                                            <tr>
                                                                                <td align='left' valign='top' width="40%" style='padding: 0px 5px 0px 21px'>
                                                                                    <div>
                                                                                        <table height='100%' width='100%' border='0' cellpadding='0' cellspacing='0'>
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 15px 5px 0px 5px'>
                                                                                                        <p>
                                                                                                            <strong>Patient Name:</strong>
                                                                                                            <asp:Label ID="lblPatientName" runat="server" Text='<%#Eval("stPatientName")%>'></asp:Label>
                                                                                                        </p>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 0px 5px 0px 5px'>
                                                                                                        <p>
                                                                                                            <strong>Ref By:</strong>
                                                                                                            <asp:Label ID="lblRefDoctor" runat="server" Text='<%#Eval("RefDoctorName")%>'></asp:Label>
                                                                                                        </p>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 0px 5px 0px 5px'>
                                                                                                        <p>
                                                                                                            <strong>Print Date:</strong>
                                                                                                            <%#String.Format("{0:dd/MM/yyyy}",DateTime.Now) %>
                                                                                                        </p>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                                <td align='left' valign='top' width="30%" style='padding: 0px 5px 0px 0px'>
                                                                                    <table height='100%' width='100%' border='0' cellpadding='3' cellspacing='3'>
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 15px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                        <strong>Age:</strong>
                                                                                                        <asp:Label ID="lblAge" runat="server" Text='<%#Eval("inAge")%>'></asp:Label>
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 0px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                        <strong>Job ID:</strong>
                                                                                                        <asp:Label ID="lblRegnNo" runat="server" Text='<%#Eval("stRegNo")%>'></asp:Label>
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 0px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                        <strong>Report Date:</strong>
                                                                                                        <%#String.Format("{0:dd/MM/yyyy}",DateTime.Now) %>
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                                <td align='left' valign='top' width="30%" style='padding: 0px 5px 0px 0px'>
                                                                                    <table height='100%' width='100%' border='0' cellpadding='3' cellspacing='3'>
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 15px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                        <strong>Mobile:</strong> 9867867861<br />
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 0px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height='16' valign='top' style='color: #404041; font-size: 13px; padding: 0px 5px 0px 5px'>
                                                                                                    <p>
                                                                                                    </p>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <!--Pateint Details end-->
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign='top' style='color: #404041; line-height: 16px; padding: 25px 20px 0px 20px'>
                                                    <p>
                                                        <section style='position: relative; clear: both; margin: 5px 0; height: 1px; border-top: 1px solid #cbcbcb;
                                                            margin-bottom: 25px; margin-top: 10px; text-align: center;'>
                                                                <h5 align='center' style='margin-top: -12px;background-color: #FFF;clear: both;width: 180px;margin-right: auto;margin-left: auto;padding-left: 15px;padding-right: 15px; font-family: arial,sans-serif;'>
                                                                    <span><asp:Label ID="lblReportHeader" runat="server" Font-Bold="true"></asp:Label></span>
                                                                </h5>
                                                            </section>
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign='top' style='color: #404041; font-size: 12px; line-height: 16px; padding: 5px 20px 0px 20px'>
                                                    <p>
                                                        Thank you for choosing us as your preferred healthcare partner, we would assure
                                                        you of best services
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 10px 16px 20px 18px'>
                                                    <!--start Middle Body Content of patient report-->
                                                    <div class="middlebody" id="middlebody" runat="server">
                                                        <table width='100%' border='0' cellpadding='0' cellspacing='0' style='border-radius: 5px;
                                                            border: solid 1px #e5e5e5'>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <table width='570' border='0' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td width='5%'>
                                                                                    </td>
                                                                                    <td width='15%' colspan='5' align='left' valign='top' style='color: #404041; font-size: 12px;
                                                                                        line-height: 16px; padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                        <strong>Test ID</strong>
                                                                                    </td>
                                                                                    <td width='50%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                        padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                        <strong>Name</strong>
                                                                                    </td>
                                                                                    <td width='30%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                        padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                        <strong>Amount</strong>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                            <tr id="trNoData" runat="server">
                                                                                <td colspan="5">
                                                                                    <center>
                                                                                        <asp:Label ID="lblMsg" runat="server" Text="No tests found." Style="text-align: center;"></asp:Label>
                                                                                    </center>
                                                                                </td>
                                                                            </tr>
                                                                            <asp:Repeater ID="rptTests" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td width='5%'>
                                                                                        </td>
                                                                                        <td width='15%' colspan='5' align='left' valign='top' style='color: #404041; font-size: 12px;
                                                                                            line-height: 16px; padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                            <div class="clear">
                                                                                            </div>
                                                                                            <div class=" testid">
                                                                                                <%#Eval("stTestCode")%>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td width='50%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                            padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                            <div class="clear">
                                                                                            </div>
                                                                                            <div class=" testnames">
                                                                                                <%#Eval("stTestName")%>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td width='30%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                            padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                            <div class="clear">
                                                                                            </div>
                                                                                            <div class="totamount">
                                                                                                <%#Eval("inTestRate")%>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr align='left'>
                                                <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 10px 16px 20px 18px'>
                                                    <table width='0' border='0' align='left' cellpadding='0' cellspacing='0'>
                                                        <tbody>
                                                            <tr>
                                                                <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 15px 0px 3px 0px'>
                                                                    <strong>Visit:</strong><asp:Label ID="lblVisitChgs" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='0' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 15px 5px 3px 5px'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 15px 0px 3px 0px'>
                                                                    <strong>Pay Type:</strong><asp:Label ID="lblPayType" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='0' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 15px 5px 3px 5px'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 15px 0px 3px 0px'>
                                                                    <strong>Urgent:</strong><asp:Label ID="lblUrgentChgs" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='0' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 15px 5px 3px 5px'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 5px 0px 3px 0px'>
                                                                    <strong>Total Tests:</strong><asp:Label ID="lblTotalTests" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='62' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 5px 5px 3px 5px'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 5px 0px 3px 0px;'>
                                                                    <strong>Total Amount:</strong><asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='62' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 5px 5px 3px 5px;'>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table width='0' border='0' align='right' cellpadding='0' cellspacing='0'>
                                                        <tbody>
                                                            <tr>
                                                                <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 15px 0px 3px 0px'>
                                                                    <strong>VAT</strong>
                                                                </td>
                                                                <td width='0' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 15px 5px 3px 5px'>
                                                                    -
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 15px 0px 3px 0px'>
                                                                    <strong>Sub-total:</strong> -
                                                                </td>
                                                                <td width='0' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 15px 5px 3px 5px'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 5px 0px 3px 0px'>
                                                                    <strong>Discount:</strong><asp:Label ID="lblDiscount" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='62' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 5px 5px 3px 5px'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 5px 0px 3px 0px;'>
                                                                    <strong>Advance:</strong><asp:Label ID="lblAdvAmount" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='62' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 5px 5px 3px 5px;'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                    padding: 5px 0px 3px 0px; border-bottom: solid 1px #999999'>
                                                                    <strong>Balance:</strong><asp:Label ID="lblBalanceAmt" runat="server"></asp:Label>
                                                                </td>
                                                                <td width='62' align='right' valign='top' style='color: #404041; font-size: 12px;
                                                                    line-height: 16px; padding: 5px 5px 3px 5px; border-bottom: solid 1px #999999'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='left' valign='bottom' style='color: #404041; font-size: 13px; line-height: 16px;
                                                                    padding: 5px 0px 3px 0px'>
                                                                    <strong>Grand Total:</strong>
                                                                </td>
                                                                <td width='62' align='right' valign='bottom' style='color: #339933; font-size: 13px;
                                                                    line-height: 16px; padding: 5px 5px 3px 5px'>
                                                                    <strong>
                                                                        <asp:Label ID="lblNetAmount" runat="server"></asp:Label></strong>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width='550' border='0' cellspacing='0' cellpadding='0'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 15px 5px 5px 10px'>
                                                                    Registration done by:<asp:Label ID="lblRegDoneBy" runat="server"></asp:Label>
                                                            </tr>
                                                            <tr>
                                                                <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 15px 5px 5px 10px'>
                                                                    <asp:Label ID="lblFooterText" runat="server"></asp:Label>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <table id="dvlowerbody" width='510' border='0' cellspacing='0' cellpadding='0' runat="server">
                                                            <tbody>
                                                                <tr>
                                                                    <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 5px 5px 1px 2px'>
                                                                        <img style="float: left;" id="imgSignature" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <!--Patient report end-->
            </div>
        </div>
        <!--Start Footer of patient report-->
        <div>
            <center>
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary no-print" Text="Print Report"
                    OnClientClick="javascript:window.print();" />
            </center>
        </div>
        <!--End Footer of patient report-->
    </div>
    </form>
</body>
</html>
