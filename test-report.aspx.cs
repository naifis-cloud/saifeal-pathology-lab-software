using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class test_report : System.Web.UI.Page
{
    public DataTable lobjdtRptSettings = new DataTable();
    private string lstrPrintMargins = "", lstrCSSStyle = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                lobjdtRptSettings = GetDefaultReportSettings();
                GetPatientTestData(Convert.ToInt32(Request.QueryString["pid"]));
            }
        }
    }

    private void GetPatientTestData(int fiPatientId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        try
        {
            lobjdtData = bllogic.GetPatientTestReportData(fiPatientId);
            if (lobjdtData.Rows.Count > 0)
            {
                rptTests.DataSource = lobjdtData;
                rptTests.DataBind();
                rptTests.Visible = true;
            }
            else
                rptTests.Visible = false;
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            bllogic = null;
            lobjdtData = null;
        }
    }

    protected void rptTests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable lobjTestParams = new DataTable();
        BusinessLogic blLogic = new BusinessLogic();
        StringBuilder lstrHTML = new StringBuilder();
        DataTable lobjdtDetails = new DataTable();
        string lstrTextStyle = "", lstrLineBreaks = "";
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblTestId = (Label)e.Item.FindControl("lblTestId");
                Literal litTestParamsData = (Literal)e.Item.FindControl("litTestParamsData");
                Literal litTestDescData = (Literal)e.Item.FindControl("litTestDescData");
                HtmlImage imgLogo = (HtmlImage)e.Item.FindControl("imgLogo");
                HtmlImage imgSignature = (HtmlImage)e.Item.FindControl("imgSignature");
                HtmlTableCell tdTestParams = (HtmlTableCell)e.Item.FindControl("tdTestParams");
                HtmlTableCell tdTestDescription = (HtmlTableCell)e.Item.FindControl("tdTestDescription");
                HtmlTableCell tdNoTestParams = (HtmlTableCell)e.Item.FindControl("tdNoTestParams");
                //HtmlGenericControl dvTopPart = (HtmlGenericControl)e.Item.FindControl("dvTopPart");
                HtmlGenericControl middlebody = (HtmlGenericControl)e.Item.FindControl("middlebody");
                //HtmlGenericControl dvlowerbody = (HtmlGenericControl)e.Item.FindControl("dvlowerbody");
                //HtmlGenericControl dvDocDetails = (HtmlGenericControl)e.Item.FindControl("dvDocDetails");

                //for default report settings //
                //HtmlGenericControl dvHeader = (HtmlGenericControl)e.Item.FindControl("dvHeader");
                HtmlTableRow dvHeader = (HtmlTableRow)e.Item.FindControl("dvHeader");
                Label lblLabName = (Label)e.Item.FindControl("lblLabName");
                Label lblLabAddress = (Label)e.Item.FindControl("lblLabAddress");
                Label lblLabPhone = (Label)e.Item.FindControl("lblLabPhone");
                Label lblReportHeader = (Label)e.Item.FindControl("lblReportHeader");
                Label lblFooterText = (Label)e.Item.FindControl("lblFooterText");
                HtmlGenericControl dvFooter = (HtmlGenericControl)e.Item.FindControl("dvFooter");

                HtmlGenericControl dvPage = (HtmlGenericControl)e.Item.FindControl("dvPage");

                if (lblTestId != null && litTestParamsData != null && tdTestParams != null && tdNoTestParams != null
                    && middlebody != null && imgSignature != null && tdTestDescription != null && litTestDescData != null)
                {
                    if (lobjdtRptSettings != null && lobjdtRptSettings.Rows.Count > 0)
                    {
                        // Checking if logo should be visible or not
                        if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgLogoVisible"]) == "Y")
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(lobjdtRptSettings.Rows[0]["stLogo"])))
                                imgLogo.Src = "~/images/reportsettings/logo/" + Convert.ToString(lobjdtRptSettings.Rows[0]["stLogo"]);
                        }

                        // Checking if border should be visible or not
                        if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgOuterBorder"]) == "Y")
                        {
                            //dvTopPart.Style.Add("border", "1px solid #000");
                            //middlebody.Style.Add("border", "1px solid #000");
                            //dvlowerbody.Style.Add("border", "1px solid #000");
                            //dvDocDetails.Style.Add("border", "1px solid #000");

                            //dvTopPart.Style.Add("border-bottom", "0px");
                            // middlebody.Style.Add("border-bottom", "0px");
                            //dvlowerbody.Style.Add("border-bottom", "0px");
                        }
                        else
                        {
                            //dvTopPart.Style.Add("border-style", "none");
                            //middlebody.Style.Add("border-style", "none");
                            //dvlowerbody.Style.Add("border-style", "none");
                            //dvDocDetails.Style.Add("border-style", "none");
                        }

                        // Checking if signature should be visible or not
                        if (Convert.ToString(lobjdtRptSettings.Rows[0]["inSignVisible"]) == "Y")
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(lobjdtRptSettings.Rows[0]["stSignature"])))
                                imgSignature.Src = "~/images/reportsettings/signatures/" + Convert.ToString(lobjdtRptSettings.Rows[0]["stSignature"]);
                        }
                    }

                    //Checking if the test department is X-Ray/Sonography
                    lobjdtDetails = blLogic.GetTestDetails(Convert.ToInt32(lblTestId.Text.Trim()));

                    if (lobjdtDetails.Rows.Count > 0)
                    {
                        // if the test department is not of type XRay/Sonography
                        if (!Convert.ToString(ConfigurationManager.AppSettings["MainDeptId"]).Contains(Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"])))
                        {
                            lobjTestParams = blLogic.GetPatientTestParamsForReport(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(lblTestId.Text.Trim()));

                            if (lobjTestParams.Rows.Count > 0)
                            {
                                tdTestParams.Style["display"] = "block";
                                tdNoTestParams.Style["display"] = "none";

                                foreach (DataRow lobjDrParamValues in lobjTestParams.Rows)
                                {
                                    lstrLineBreaks = "";

                                    lstrHTML.Append("<tr>");

                                    if (Convert.ToBoolean(lobjDrParamValues["btBold"]))
                                        lstrTextStyle = "font-weight:bold;";

                                    if (Convert.ToBoolean(lobjDrParamValues["btUnderLine"]))
                                        lstrTextStyle += "text-decoration:underline;";

                                    if (!string.IsNullOrEmpty(Convert.ToString(lobjDrParamValues["stColor"])))
                                        lstrTextStyle += "color:" + Convert.ToString(lobjDrParamValues["stColor"]) + ";";

                                    lstrTextStyle += "line-height: 16px; padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5;";

                                    lstrHTML.Append("<td width='45%' colspan='5' valign='top' align='left' valign='top' style='" + lstrTextStyle + "'>");
                                    lstrHTML.Append(Convert.ToString(lobjDrParamValues["stName"]));
                                    lstrHTML.Append("</td>");

                                    lstrHTML.Append("<td width='10%' align='center' style='color: #404041; font-size: 12px; line-height: 16px;");
                                    lstrHTML.Append("padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>");

                                    if (!string.IsNullOrEmpty(Convert.ToString(lobjDrParamValues["stParamValue"])))
                                        lstrHTML.Append(Convert.ToString(lobjDrParamValues["stParamValue"]));
                                    else
                                        lstrHTML.Append("-");

                                    lstrHTML.Append("</td>");

                                    lstrHTML.Append("<td width='27%' align='right' style='color: #404041; font-size: 12px; line-height: 16px;");
                                    lstrHTML.Append("padding: 5px 10px 3px 5px; border-bottom: solid 1px #e5e5e5'>");

                                    if (!string.IsNullOrEmpty(Convert.ToString(lobjDrParamValues["inLower"])) && !string.IsNullOrEmpty(Convert.ToString(lobjDrParamValues["inUpper"])))
                                        lstrHTML.Append(Convert.ToString(lobjDrParamValues["inLower"]) + " - " + Convert.ToString(lobjDrParamValues["inUpper"]));
                                    else
                                        lstrHTML.Append("-");

                                    lstrHTML.Append("</td>");

                                    lstrHTML.Append("</tr>");

                                    if (Convert.ToInt32(lobjDrParamValues["inLines"]) > 0)
                                    {
                                        for (int liCounter = 1; liCounter <= Convert.ToInt32(lobjDrParamValues["inLines"]); liCounter++)
                                        {
                                            lstrLineBreaks += "<br/>";
                                        }
                                        lstrHTML.Append("<tr>");
                                        lstrHTML.Append("<td colspan = '3'>" + lstrLineBreaks + "");
                                        lstrHTML.Append("</tr>");
                                    }
                                }

                                litTestParamsData.Text = Convert.ToString(lstrHTML);
                                tdTestDescription.Style["display"] = "none";
                            }
                            else
                            {
                                tdTestParams.Style["display"] = "none";
                                tdNoTestParams.Style["display"] = "block";
                                tdTestDescription.Style["display"] = "none";
                            }
                        }
                        else
                        {
                            tdTestDescription.Style["display"] = "block";
                            tdTestParams.Style["display"] = "none";
                            tdNoTestParams.Style["display"] = "none";

                            //Getting the saved test description//
                            lobjdtDetails = blLogic.GetPatientTestDesc(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(lblTestId.Text.Trim()));
                            if (lobjdtDetails.Rows.Count > 0)
                            {
                                lstrHTML.Append("<tr>");
                                lstrHTML.Append("<td style='color: #404041; font-size: 12px;line-height: 16px; padding: 5px 10px 3px 5px;'>");
                                lstrHTML.Append(Server.HtmlDecode(Convert.ToString(lobjdtDetails.Rows[0]["stDescription"])));
                                lstrHTML.Append("</td>");
                                lstrHTML.Append("</tr>");

                                litTestDescData.Text = Convert.ToString(lstrHTML);
                            }
                        }
                    }

                    dvPage.Style.Add("font-family", Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"]));
                    dvPage.Style.Add("font-size", Convert.ToString(lobjdtRptSettings.Rows[0]["inRptFontSize"]) + "px");

                    lstrPrintMargins = "@page { size: A4; margin: " + Convert.ToString(lobjdtRptSettings.Rows[0]["inTopMargin"]) + "px ";
                    lstrPrintMargins += Convert.ToString(lobjdtRptSettings.Rows[0]["inRightMargin"]) + "px ";
                    lstrPrintMargins += Convert.ToString(lobjdtRptSettings.Rows[0]["inBottomMargin"]) + "px ";
                    lstrPrintMargins += Convert.ToString(lobjdtRptSettings.Rows[0]["inLeftMargin"]) + "px";
                    lstrPrintMargins += "; } ";

                    //Adding <style> tag dynamically for printing CSS//
                    lstrCSSStyle = @"body { margin: 0; padding: 0; background-color: #FAFAFA; font: 12pt 'Tahoma';}";
                    lstrCSSStyle += "* { box-sizing: border-box;-moz-box-sizing: border-box; }";

                    lstrCSSStyle += ".page { width: 21cm;min-height: 29.7cm;padding: 1px;margin: 1cm auto;border: 1px #D3D3D3 solid;border-radius: 5px;background: white;box-shadow: 0 0 5px rgba(0, 0, 0, 0.1); }";
                    lstrCSSStyle += ".subpage { padding: 10px;border: 1px #f5f5f5 solid;height: auto;outline: 2px #f4f4f4 dotted; }";

                    lstrCSSStyle += lstrPrintMargins;

                    lstrCSSStyle += "@media print { table { page-break-after: auto; }";
                    lstrCSSStyle += "tr { page-break-inside: auto;page-break-after: auto; }";
                    lstrCSSStyle += "td { page-break-inside: auto;page-break-after: auto; }";
                    lstrCSSStyle += "thead { display: table-header-group; }";
                    lstrCSSStyle += "tfoot { display: table-footer-group; }";
                    lstrCSSStyle += ".page { margin: 0;border: initial;border-radius: initial;width: initial;min-height: initial;box-shadow: initial;background: initial;page-break-after: always; }";
                    lstrCSSStyle += ".subpage { page-break-after: always; }";
                    lstrCSSStyle += ".no-print, .no-print * { display: none !important;height: 0px; }";
                    lstrCSSStyle += " }";

                    lstrCSSStyle += "@media screen { .no-print { margin-top: 10px; }";
                    lstrCSSStyle += "div[size='A4'] { padding: 8px; }";
                    lstrCSSStyle += " }";

                    //lstrCSSStyle += "page[size='A4'] { background: white;width: 21cm; overflow: hidden;height: auto;display: block;margin: 0 auto;margin-bottom: 0.5cm;box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);} ";
                    //lstrCSSStyle += "@media print {";
                    //lstrCSSStyle += "body, page[size='A4'] { box-shadow: 0;page-break-after:always;position: relative;margin: 0;display: block;} ";
                    //lstrCSSStyle += lstrPrintMargins;
                    //lstrCSSStyle += ".no-print, .no-print *{ display: none !important;height: 0px;} ";
                    //lstrCSSStyle += "}";

                    htmlCSS.InnerHtml = lstrCSSStyle;
                }
                //Getting the default report settings data //

                //Setting the left,right,top and bottom margins of the report//
                if (lobjdtRptSettings != null && lobjdtRptSettings.Rows.Count > 0)
                {
                    if (dvHeader != null && lblLabName != null && lblLabAddress != null && lblLabPhone != null
                        && lblReportHeader != null && lblFooterText != null && dvFooter != null)
                    {
                        //dvMain.Style.Add("margin-left", Convert.ToString(lobjdtRptSettings.Rows[0]["inLeftMargin"]) + "px");
                        //dvMain.Style.Add("margin-right", Convert.ToString(lobjdtRptSettings.Rows[0]["inRightMargin"]) + "px");
                        //dvMain.Style.Add("margin-top", Convert.ToString(lobjdtRptSettings.Rows[0]["inTopMargin"]) + "px");
                        //dvMain.Style.Add("margin-bottom", Convert.ToString(lobjdtRptSettings.Rows[0]["inBottomMargin"]) + "px");

                        // Checking if header should be visible or not
                        if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgHeaderVisible"]) == "Y")
                        {
                            lblLabName.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stLabName"]);
                            lblLabAddress.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stAddress"]);
                            lblLabPhone.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stPhone"]);
                            lblReportHeader.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stTextOnReport"]);
                            dvHeader.Style.Add("display", "");
                        }
                        else
                            dvHeader.Style.Add("display", "none");

                        // Checking if footer should be visible or not
                        if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgFooterVisible"]) == "Y")
                        {
                            lblFooterText.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stFooterText"]);
                            dvFooter.Style.Add("display", "block");
                        }
                        else
                            dvFooter.Style.Add("display", "none");

                        //Setting the font family and font size//
                        if (!string.IsNullOrEmpty(Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"])))
                        {
                            dvHeader.Style.Add("font-family", Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"]));
                            dvHeader.Style.Add("font-size", Convert.ToString(lobjdtRptSettings.Rows[0]["inRptFontSize"]) + "px");

                            //dvMain.Style.Add("font-family", Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"]));
                            //dvMain.Style.Add("font-size", Convert.ToString(lobjdtRptSettings.Rows[0]["inRptFontSize"]) + "px");

                            dvFooter.Style.Add("font-family", Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"]));
                            dvFooter.Style.Add("font-size", Convert.ToString(lobjdtRptSettings.Rows[0]["inRptFontSize"]) + "px");
                        }
                    }
                }
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            blLogic = null;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        blLogic.UpdateTestPrintCount(Convert.ToInt32(Request.QueryString["pid"]));
        Page.ClientScript.RegisterStartupScript(this.GetType(), "key1", "window.print();", true);
        blLogic = null;
    }

    private DataTable GetDefaultReportSettings()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetDefaultReportSetting();
            return lobjdtDetails;
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            bllogic = null;
        }
    }
}