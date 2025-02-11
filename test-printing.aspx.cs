using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;
using System.Text;
using System.Configuration;

public partial class test_printing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            Commonfunction.GetAllTestStatus(ddlTestStatus);
            BusinessLogic.FillMainDepartments(ddlDepts);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 29, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view test printing page.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllPatients(0);
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view test printing page.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllPatients(int fiPrintCountFlag)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        string lstrFromDate = "", lstrToDate = "", lstrPatientName = "", lstrRegNo = "";
        int liDeptId = 0, liTestStatusId = 0;
        try
        {
            if (!string.IsNullOrEmpty(Request.Form.Get(txtFromdate.UniqueID)))
                lstrFromDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.Form.Get(txtFromdate.UniqueID), new System.Globalization.CultureInfo("en-GB")));

            if (!string.IsNullOrEmpty(Request.Form.Get(txtTodate.UniqueID)))
                lstrToDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(Request.Form.Get(txtTodate.UniqueID), new System.Globalization.CultureInfo("en-GB")));

            if (!string.IsNullOrEmpty(txtPatientName.Text.Trim()))
                lstrPatientName = txtPatientName.Text.Trim();

            if (!string.IsNullOrEmpty(txtRegNo.Text.Trim()))
                lstrRegNo = txtRegNo.Text.Trim();

            if (ddlDepts.SelectedIndex > 0)
                liDeptId = Convert.ToInt32(ddlDepts.SelectedValue);

            if (ddlTestStatus.SelectedIndex > 0)
                liTestStatusId = Convert.ToInt32(ddlTestStatus.SelectedValue);

            lobjdtDetails = bllogic.GetAllPatients(lstrFromDate, lstrToDate, liDeptId, liTestStatusId, lstrPatientName, lstrRegNo, "", 0, fiPrintCountFlag);
            rptPatients.DataSource = lobjdtDetails;
            rptPatients.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptPatients.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptPatients.Visible = false;
                trNoData.Visible = true;
            }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetAllPatients(0);
    }

    public string ShowHideReportLink(int fiTestStatusId)
    {
        string lstrDisplayStyle = "";
        try
        {
            if (fiTestStatusId == 6 || fiTestStatusId == 7 || fiTestStatusId == 8)
                lstrDisplayStyle = "display:block;";
            else
                lstrDisplayStyle = "display:none;";
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }

        return lstrDisplayStyle;
    }

    public bool ShowHideEmailLink(int fiTestStatusId)
    {
        bool lbDisplayStyle = true;
        try
        {
            if (fiTestStatusId == 6 || fiTestStatusId == 7 || fiTestStatusId == 8)
                lbDisplayStyle = true;
            else
                lbDisplayStyle = false;
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }

        return lbDisplayStyle;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GetAllPatients(1);
    }

    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        GetAllPatients(0);
    }

    private void EmailTestReport(DataTable lobjDtPatientDetails, int fiFlag)
    {
        StringBuilder lstrBuilder = new StringBuilder();
        DataTable lobjdtReportData = new DataTable();
        DataTable lobjTestParams;
        BusinessLogic bllogic = new BusinessLogic();
        string lstrTextStyle = "", lstrLineBreaks = "";

        lstrBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head></head>");
        lstrBuilder.Append("<body>");

        if (fiFlag == 0)
            lstrBuilder.Append("Dear " + Convert.ToString(lobjDtPatientDetails.Rows[0]["RefDoctorName"]) + ",<br /><p>Thank you for requesting the report for the patient - " + Convert.ToString(lobjDtPatientDetails.Rows[0]["stPatientName"]) + ". Below are the report details:</p>");
        else
            lstrBuilder.Append("Dear " + Convert.ToString(lobjDtPatientDetails.Rows[0]["stPatientName"]) + ",<br /><p>Thank you for requesting your test report. Below are the report details:</p>");

        lobjdtReportData = bllogic.GetPatientTestReportData(Convert.ToInt32(lobjDtPatientDetails.Rows[0]["inPatientId"]));
        if (lobjdtReportData.Rows.Count > 0)
        {
            foreach (DataRow loDtRow in lobjdtReportData.Rows)
            {
                lstrBuilder.Append("<table cellpadding='3' cellspacing='3' width='100%'>");
                lstrBuilder.Append("<thead><tr><th align='left'><b style='font-weight:bold;'>Test Name</b></th></tr></thead>");
                lstrBuilder.Append("<tbody>");
                lstrBuilder.Append("<tr><td align='left' valign='top'>" + Convert.ToString(loDtRow["stTestName"]) + "</td><tr>");
                lstrBuilder.Append("<tr><td align='left' valign='top'>" + Convert.ToString(loDtRow["stTestDesc"]) + "</td><tr>");
                lstrBuilder.Append("</tbody>");
                lstrBuilder.Append("</table><br />");

                //Test Parameters//
                lstrBuilder.Append("<table cellpadding='3' cellspacing='3' width='70%' style='border:1px solid #ECECEC;'>");
                lstrBuilder.Append("<tbody>");
                lstrBuilder.Append("<tr>");
                lstrBuilder.Append("<th align='left' nowrap='nowrap'><b style='font-weight:bold;'>Parameters</b></th>");
                lstrBuilder.Append("<th align='left' nowrap='nowrap'><b style='font-weight:bold;'>Test Values</b></th>");
                lstrBuilder.Append("<th align='left' nowrap='nowrap'><b style='font-weight:bold;'>Reference Range</b></th>");
                lstrBuilder.Append("</tr>");

                lobjTestParams = new DataTable();
                lobjTestParams = bllogic.GetPatientTestParamsForReport(Convert.ToInt32(lobjDtPatientDetails.Rows[0]["inPatientId"]), Convert.ToInt32(loDtRow["inTestId"]));

                if (lobjTestParams.Rows.Count > 0)
                {
                    foreach (DataRow lobjDrParamValues in lobjTestParams.Rows)
                    {
                        lstrLineBreaks = "";
                        lstrBuilder.Append("<tr>");

                        lstrBuilder.Append("<td align='left' valign='top'>");

                        if (Convert.ToBoolean(lobjDrParamValues["btBold"]))
                            lstrTextStyle = "font-weight:bold;";

                        if (Convert.ToBoolean(lobjDrParamValues["btUnderLine"]))
                            lstrTextStyle += "text-decoration:underline;";

                        if (!string.IsNullOrEmpty(Convert.ToString(lobjDrParamValues["stColor"])))
                            lstrTextStyle += "color:" + Convert.ToString(lobjDrParamValues["stColor"]) + ";";

                        lstrBuilder.Append("<div style='" + lstrTextStyle + "'>" + Convert.ToString(lobjDrParamValues["stName"]) + "</div>");
                        lstrBuilder.Append("</td>");

                        lstrBuilder.Append("<td align='left' valign='top'>");
                        lstrBuilder.Append("<div>" + Convert.ToString(lobjDrParamValues["stParamValue"]) + "</div>");
                        lstrBuilder.Append("</td>");

                        lstrBuilder.Append("<td align='left' valign='top'>");
                        lstrBuilder.Append("<div>" + Convert.ToString(lobjDrParamValues["inLower"]) + " - " + Convert.ToString(lobjDrParamValues["inUpper"]) + "</div>");
                        lstrBuilder.Append("</td>");

                        lstrBuilder.Append("</tr>");

                        if (Convert.ToInt32(lobjDrParamValues["inLines"]) > 0)
                        {
                            for (int liCounter = 1; liCounter <= Convert.ToInt32(lobjDrParamValues["inLines"]); liCounter++)
                            {
                                lstrLineBreaks += "<br/>";
                            }
                            lstrBuilder.Append("<tr>");
                            lstrBuilder.Append("<td colspan = '3'>" + lstrLineBreaks + "");
                            lstrBuilder.Append("</tr>");
                        }
                    }
                }
                else
                {
                    lstrBuilder.Append("<tr>");
                    lstrBuilder.Append("<td colspan = '3' align='center' valign='top'>");
                    lstrBuilder.Append("<div>No parameters found</div>");
                    lstrBuilder.Append("</td>");
                    lstrBuilder.Append("</tr>");
                }

                lstrBuilder.Append("</tbody></table><br /><br />");

                lstrBuilder.Append("<table cellpadding='3' cellspacing='3' border='0' width='100%'>");
                lstrBuilder.Append("<tbody>");
                lstrBuilder.Append("<tr><td align='left' valign='top'>Method: " + Convert.ToString(loDtRow["stTestMethod"]) + "</td><tr>");
                lstrBuilder.Append("<tr><td align='left' valign='top'>Technology: " + Convert.ToString(loDtRow["stTechnology"]) + "</td><tr>");
                lstrBuilder.Append("<tr><td align='left' valign='top'>Remarks: " + Convert.ToString(loDtRow["stRemarks"]) + "</td><tr>");
                lstrBuilder.Append("<tr><td align='left' valign='top'>Interpretation: " + Server.HtmlDecode(Convert.ToString(loDtRow["stTechnology"])) + "<br /></td><tr>");
                lstrBuilder.Append("</tbody>");
                lstrBuilder.Append("</table><br />");

                //lstrBuilder.Append("<p style='color:#ECECEC;'>-----------------------------------------------------------------------------------------------------------</p>");
                lstrBuilder.Append("<hr style='color:#ECECEC;' />");
            }
        }

        lstrBuilder.Append("<p>Please let us know if you need anything that you cannot find or if you have any questions regarding our service. We appreciate any feedbacks that you might have for us.</p>");
        lstrBuilder.Append("<p>Best Regards,<br/><a style='font-size:12px;font-family:Arial,Helvetica,sans-serif;padding-bottom:10px;' href = '" + Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"]) + "'>SHC</a> Team<br /></p>");

        lstrBuilder.Append("</body></html>");

        Commonfunction.SendMail(Convert.ToString(lobjDtPatientDetails.Rows[0]["stEmail"]), Convert.ToString(ConfigurationManager.AppSettings["Support"]), "SHC - Patient Test Report", Convert.ToString(lstrBuilder));

        if (fiFlag == 0)
            Commonfunction.showMsg("Test report successfully emailed to the doctor.", this);
        else
            Commonfunction.showMsg("Test report successfully emailed to the patient.", this);
    }

    protected void rptPatients_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjDtPatientDetails = new DataTable();
        try
        {
            lobjDtPatientDetails = bllogic.GetPatientDetails(Convert.ToInt32(e.CommandArgument));
            if (lobjDtPatientDetails.Rows.Count > 0)
            {
                if (e.CommandName == "eEmailDoc")
                    EmailTestReport(lobjDtPatientDetails, 0);
                else
                    EmailTestReport(lobjDtPatientDetails, 1);
            }
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