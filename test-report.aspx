<%@ Page Language="C#" AutoEventWireup="true" Title="Patient Test Report" CodeFile="test-report.aspx.cs"
    Inherits="test_report" %>

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
    <center>
        <div class="no-print">
            <asp:Button ID="btnTopPrint" runat="server" CssClass="btn btn-primary no-print" Text="Print Report"
                OnClick="btnPrint_Click" />
        </div>
    </center>
    <div class="book">
        <asp:Repeater ID="rptTests" runat="server" OnItemDataBound="rptTests_ItemDataBound">
            <ItemTemplate>
                <div class="page" runat="server">
                    <div class="subpage" id="dvPage" runat="server">
                        <!--Patient report start-->
                        <div bgcolor='#e4e4e4' text='#ff6633' link='#666666' vlink='#666666' alink='#ff6633'
                            style='margin: 0; font-family: Arial,Helvetica,sans-serif; border-bottom: 1'>
                            <table background='' bgcolor='#e4e4e4' width='100%' style='padding: 20px 0 20px 0'
                                cellspacing='0' border='0' align='center' cellpadding='0'>
                                <tbody>
                                    <tr>
                                        <td>
                                            <table width='670' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF'
                                                style='border-radius: 5px;'>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table width='620' border='0' cellspacing='0' cellpadding='0' style='border-bottom: solid 0px #e5e5e5'>
                                                                <tbody>
                                                                    <!--Header start-->
                                                                    <tr id="dvHeader" runat="server">
                                                                        <td align='left' valign='top' style='padding: 0px 5px 0px 5px'>
                                                                            <table height='20px' width='100%' border='0' cellpadding='0' cellspacing='0'>
                                                                                <tbody>
                                                                                    <div>
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
                                                                                    </div>
                                                                                </tbody>
                                                                            </table>
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
                                                    <tr>
                                                        <td valign='top' style='color: #404041; line-height: 16px; padding: 15px 10px 0px 10px'>
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
                                                        <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 2px 2px 2px 2px'>
                                                            <div id="middlebody" runat="server">
                                                                <table width='100%' border='0' cellpadding='0' cellspacing='0' style='border-radius: 1px;
                                                                    border: solid 1px #e5e5e5'>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <asp:Label ID="lblTestId" runat="server" Text='<%#Eval("inTestId")%>' Visible="false"></asp:Label>
                                                                                <%#Eval("stTestName")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <%#Eval("stTestDesc")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="tdTestParams" runat="server">
                                                                                <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td width='53%' colspan='5' align='left' valign='top' style='color: #404041; font-size: 12px;
                                                                                                line-height: 16px; padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                                <strong>Test</strong>
                                                                                            </td>
                                                                                            <td width='10%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                                padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                                <strong>Value</strong>
                                                                                            </td>
                                                                                            <td width='27%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                                padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>
                                                                                                <strong>Reference</strong>
                                                                                            </td>
                                                                                            <td width='10' align='right' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                                                padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5; display: none'>
                                                                                                <strong>Unit</strong>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <asp:Literal ID="litTestParamsData" runat="server"></asp:Literal>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td id="tdTestDescription" runat="server">
                                                                                <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td align='left' valign='top' style='color: #404041; font-size: 12px;
                                                                                                line-height: 16px; padding: 5px 10px 3px 5px;'>
                                                                                                <strong>Description</strong>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <asp:Literal ID="litTestDescData" runat="server"></asp:Literal>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td id="tdNoTestParams" runat="server" style="text-align: center; display: none;">
                                                                                <span>No Test parameters found</span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr align='left'>
                                                        <td style='color: #404041; font-size: 12px; line-height: 16px; padding: 10px 16px 20px 2px'>
                                                            <table width='0' border='0' align='left' cellpadding='0' cellspacing='0'>
                                                                <tbody>
                                                                    <tr>
                                                                        <td width='100%' align='left' valign='top' style='color: #404041; font-size: 12px;
                                                                            line-height: 16px; padding: 0px 0px 3px 0px'>
                                                                            <strong>Method:</strong>
                                                                            <%#Eval("stTestMethod")%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='left' width='100%' valign='top' style='color: #404041; font-size: 12px;
                                                                            line-height: 16px; padding: 5px 0px 3px 0px;'>
                                                                            <strong>Technology:</strong>
                                                                            <%#Eval("stTechnology")%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='left' width='100%' valign='top' style='color: #404041; font-size: 12px;
                                                                            line-height: 16px; padding: 5px 0px 3px 0px;'>
                                                                            <strong>Remarks:</strong>
                                                                            <%#Eval("stRemarks")%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='left' width='100%' valign='top' style='color: #404041; font-size: 12px;
                                                                            line-height: 16px; padding: 5px 0px 3px 0px;'>
                                                                            <strong>Interpretations:</strong>
                                                                            <%#Server.HtmlDecode(Convert.ToString(Eval("stInterPretText")))%>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width='450' align="left" border='0' cellspacing='0' cellpadding='0'>
                                                                <tbody>
                                                                    <tr>
                                                                        <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                            padding: 0px 0px 3px 0px'>
                                                                            <strong>Registration done by:</strong>
                                                                            <%#Eval("RefDoctorName")%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                            padding: 0px 0px 3px 0px'>
                                                                            <strong>Data Fed by:</strong>
                                                                            <%#Eval("RefDoctorName")%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width='0' align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                            padding: 0px 0px 3px 0px'>
                                                                            <strong>Checked by:</strong>
                                                                            <%#Eval("RefDoctorName")%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style='color: #404041; font-size: 12px; line-height: 16px; padding: 15px 5px 5px 2px'>
                                                                            <p id="dvFooter" runat="server">
                                                                                <asp:Label ID="lblFooterText" runat="server"></asp:Label>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table width='0' border='0' align='right' cellpadding='0' cellspacing='0'>
                                                                <tbody>
                                                                    <tr>
                                                                        <td align='left' valign='top' style='color: #404041; font-size: 12px; line-height: 16px;
                                                                            padding: 5px 0px 3px 0px; border-bottom: solid 1px #999999'>
                                                                            <img id="imgSignature" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='left' valign='bottom' style='color: #404041; font-size: 13px; line-height: 16px;
                                                                            padding: 5px 0px 3px 0px'>
                                                                            <strong>Dr MD Pathologist</strong>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <center>
        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary no-print" Text="Print Report"
            OnClick="btnPrint_Click" />
    </center>
    </form>
</body>
</html>
