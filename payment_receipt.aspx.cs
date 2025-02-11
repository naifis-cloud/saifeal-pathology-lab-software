using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class payment_receipt : System.Web.UI.Page
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
                lobjdtRptSettings = GetDefaultReceiptSettings();
                GetPatientDetails(Convert.ToInt32(Request.QueryString["pid"]));

                //Setting the left,right,top and bottom margins of the report//
                if (lobjdtRptSettings != null && lobjdtRptSettings.Rows.Count > 0)
                {
                    // Checking if logo should be visible or not
                    if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgLogoVisible"]) == "Y")
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lobjdtRptSettings.Rows[0]["stLogo"])))
                            imgLogo.Src = "~/images/receiptsettings/logo/" + Convert.ToString(lobjdtRptSettings.Rows[0]["stLogo"]);
                    }

                    //Setting the printer margins dynamically//
                    lstrPrintMargins = "@page { size: A4; margin: " + Convert.ToString(lobjdtRptSettings.Rows[0]["inTopMargin"]) + "px ";
                    lstrPrintMargins += Convert.ToString(lobjdtRptSettings.Rows[0]["inRightMargin"]) + "px ";
                    lstrPrintMargins += Convert.ToString(lobjdtRptSettings.Rows[0]["inBottomMargin"]) + "px ";
                    lstrPrintMargins += Convert.ToString(lobjdtRptSettings.Rows[0]["inLeftMargin"]) + "px";
                    lstrPrintMargins += "; } ";

                    //Adding <style> tag dynamically for printing CSS//
                    lstrCSSStyle = @"body { margin: 0;padding: 0; background-color: #FAFAFA; font: 12pt 'Tahoma';}";

                    lstrCSSStyle += "* { box-sizing: border-box;-moz-box-sizing: border-box; }";
                    lstrCSSStyle += ".page { width: 21cm;min-height: 14.7cm;padding: 1px;margin: 1cm auto;border: 1px #D3D3D3 solid;border-radius: 5px;background: white;box-shadow: 0 0 5px rgba(0, 0, 0, 0.1); }";

                    lstrCSSStyle += ".subpage { padding: 10px;border: 1px #f5f5f5 solid;height: auto;outline: 2px #f4f4f4 dotted; }";

                    lstrCSSStyle += lstrPrintMargins;

                    lstrCSSStyle += "@media print { .page { margin: 0;border: initial;border-radius: initial;width: initial;min-height: initial;box-shadow: initial;background: initial;page-break-after: always; }";
                    lstrCSSStyle += ".subpage { page-break-after: always; }";
                    lstrCSSStyle += ".no-print, .no-print * { display: none !important;height: 0px; }";
                    lstrCSSStyle += " }";

                    lstrCSSStyle += "@media screen { .no-print { margin-top: 10px; }";
                    lstrCSSStyle += "div[size='A5'] { padding: 8px; }";
                    lstrCSSStyle += " }";

                    //lstrCSSStyle += "page[size='A5'] { background: white;width: 21cm; height: 14.7cm;display: block;margin: 0 auto;margin-bottom: 0.5cm;box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);} ";
                    //lstrCSSStyle += "@media print {";
                    //lstrCSSStyle += "body, page[size='A5'] { margin: 0;box-shadow: 0;} ";
                    //lstrCSSStyle += lstrPrintMargins;
                    //lstrCSSStyle += ".no-print, .no-print *{ display: none !important;height: 0px;} ";
                    //lstrCSSStyle += "}";

                    htmlCSS.InnerHtml = lstrCSSStyle;

                    // Checking if border should be visible or not
                    if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgOuterBorder"]) == "Y")
                    {
                        dvTopPart.Style.Add("border", "1px solid #000");
                        middlebody.Style.Add("border", "1px solid #000");
                        dvlowerbody.Style.Add("border", "1px solid #000");

                        dvTopPart.Style.Add("border-bottom", "0px");
                        middlebody.Style.Add("border-bottom", "0px");
                    }
                    else
                    {
                        dvTopPart.Style.Add("border-style", "none");
                        middlebody.Style.Add("border-style", "none");
                        dvlowerbody.Style.Add("border-style", "none");
                    }

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


                    // Checking if signature should be visible or not
                    if (Convert.ToString(lobjdtRptSettings.Rows[0]["inSignVisible"]) == "Y")
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(lobjdtRptSettings.Rows[0]["stSignature"])))
                            imgSignature.Src = "~/images/receiptsettings/signatures/" + Convert.ToString(lobjdtRptSettings.Rows[0]["stSignature"]);
                    }

                    //Setting the font family and font size//
                    if (!string.IsNullOrEmpty(Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"])))
                    {
                        dvHeader.Style.Add("font-family", Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"]));
                        dvHeader.Style.Add("font-size", Convert.ToString(lobjdtRptSettings.Rows[0]["inRCFontSize"]) + "px");

                        dvMain.Style.Add("font-family", Convert.ToString(lobjdtRptSettings.Rows[0]["stFontFamily"]));
                        dvMain.Style.Add("font-size", Convert.ToString(lobjdtRptSettings.Rows[0]["inRCFontSize"]) + "px");
                    }

                    lblFooterText.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stFooterText"]);

                    //Checking if footer should be visible or not
                    //if (Convert.ToString(lobjdtRptSettings.Rows[0]["flgFooterVisible"]) == "Y")
                    //{
                    //    lblFooterText.Text = Convert.ToString(lobjdtRptSettings.Rows[0]["stFooterText"]);
                    //    dvFooter.Style.Add("display", "block");
                    //}
                    //else
                    //    dvFooter.Style.Add("display", "none");
                }
            }
        }

    }

    private void GetPatientDetails(int fiPatientId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        try
        {
            lobjdtData = bllogic.GetPatientDetails(fiPatientId);
            if (lobjdtData.Rows.Count > 0)
            {
                lblPatientName.Text = Convert.ToString(lobjdtData.Rows[0]["stPatientName"]);
                lblRefDoctor.Text = Convert.ToString(lobjdtData.Rows[0]["RefDoctorName"]);
                lblRegDoneBy.Text = Convert.ToString(lobjdtData.Rows[0]["RefDoctorName"]);
                lblAge.Text = Convert.ToString(lobjdtData.Rows[0]["inAge"]);
                lblRegnNo.Text = Convert.ToString(lobjdtData.Rows[0]["stRegNo"]);
                
                lblVisitChgs.Text = Convert.ToString(lobjdtData.Rows[0]["inVisitChgs"]);
                lblUrgentChgs.Text = Convert.ToString(lobjdtData.Rows[0]["inEmergencyChgs"]);

                lblTotalTests.Text = Convert.ToString(bllogic.GetPatientTests(fiPatientId).Rows.Count);
                lblTotalAmount.Text = Convert.ToString(lobjdtData.Rows[0]["inTotalTestChgs"]);
                lblPayType.Text = Convert.ToString(lobjdtData.Rows[0]["stPayType"]);
                lblDiscount.Text = Convert.ToString(lobjdtData.Rows[0]["inDiscountAmt"]);
                lblAdvAmount.Text = Convert.ToString(lobjdtData.Rows[0]["inAdvanceAmt"]);
                lblBalanceAmt.Text = Convert.ToString(lobjdtData.Rows[0]["inBalanceAmt"]);
                lblNetAmount.Text = Convert.ToString(lobjdtData.Rows[0]["inNetAmt"]);

                //Getting patient tests//
                //Getting the selected tests//

                lobjdtData = new DataTable();

                lobjdtData = bllogic.GetPatientTests(fiPatientId);
                if (lobjdtData.Rows.Count > 0)
                {
                    rptTests.DataSource = lobjdtData;
                    rptTests.DataBind();
                    rptTests.Visible = true;
                    trNoData.Visible = false;
                }
                else
                {
                    rptTests.Visible = false;
                    trNoData.Visible = true;
                }
            }
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

    private DataTable GetDefaultReceiptSettings()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetDefaultReceiptSetting();
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