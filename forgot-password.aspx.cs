using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;
using System.Text;
using System.Configuration;

public partial class forgot_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BusinessLogic.FillAllRoles(ddlRoles);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            if (ddlRoles.SelectedItem.Text.Trim().StartsWith("Collection"))
                liRoleFlag = 1;     // for collection center
            else if (ddlRoles.SelectedItem.Text.Trim().StartsWith("Sub Collection") || ddlRoles.SelectedItem.Text.Trim().StartsWith("SubCollection"))
                liRoleFlag = 2;     // for subcollection center
            else if (ddlRoles.SelectedItem.Text.Trim().Contains("Referral Doctor") || ddlRoles.SelectedItem.Text.Trim().Contains("ReferralDoctor")
                || ddlRoles.SelectedItem.Text.Trim().Contains("Doctor"))
                liRoleFlag = 3;     // for referral doctor

            liRetVal = blLogic.ValidateUserEmail(txtemail.Text.Trim(), Convert.ToInt32(ddlRoles.SelectedValue), liRoleFlag);
            if (liRetVal > 0)
            {
                //Getting user's details//
                if (liRoleFlag == 0)
                    lobjdtData = blLogic.GetUserDetails(liRetVal);
                else if (liRoleFlag == 1)
                    lobjdtData = blLogic.GetCollCenterDetails(liRetVal);
                else if (liRoleFlag == 2)
                    lobjdtData = blLogic.GetSubCollCenterDetails(liRetVal);
                else if (liRoleFlag == 3)
                    lobjdtData = blLogic.GetReferalDoctorDetails(liRetVal);

                if (lobjdtData.Rows.Count > 0)
                {
                    if (liRoleFlag == 0)
                    {
                        EmailLoginDetails(Convert.ToString(lobjdtData.Rows[0]["stUserName"]), Convert.ToString(lobjdtData.Rows[0]["stEmail"]),
                            Convert.ToString(lobjdtData.Rows[0]["stPassword"]));
                    }
                    else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
                    {
                        EmailLoginDetails(Convert.ToString(lobjdtData.Rows[0]["stUserName"]), Convert.ToString(lobjdtData.Rows[0]["stEmailAddress"]),
                            Convert.ToString(lobjdtData.Rows[0]["stPassword"]));
                    }
                    Commonfunction.showMsg("Login details successfully emailed to your registered email address.", this, "default.aspx");
                }
            }
            else
                Commonfunction.showMsg("Sorry. Entered details does not match the registered credentials", this);
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

    private void EmailLoginDetails(string fstrUsername, string fstrEmail, string fstrPassword)
    {
        StringBuilder lstrBuilder = new StringBuilder();

        lstrBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head></head>");
        lstrBuilder.Append("<body><table width='100%' border='0' cellspacing='1' cellpadding='0'>");
        lstrBuilder.Append("<tr><td colspan='4' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000; padding-bottom: 10px; padding-left: 5px;'>");

        lstrBuilder.Append("Dear User ,<br /><p>Thank you for requesting your login details. Please find them below:</p></td></tr>");

        lstrBuilder.Append("<tr><td colspan='4' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000; padding-bottom: 10px; padding-left: 5px;'>");
        lstrBuilder.Append("<table width='500' border='0' align='left' cellpadding='0' cellspacing='1'>");
        lstrBuilder.Append("<tr>");
        lstrBuilder.Append("<td width='120' align='left' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000000; padding: 5px;width:100px;'>User Name:</td>");
        lstrBuilder.Append("<td width='290' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;padding: 5px;'>" + fstrUsername + "</td>");
        lstrBuilder.Append("</tr>");

        lstrBuilder.Append("<tr>");
        lstrBuilder.Append("<td width='120' align='left' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000000; padding: 5px;width:100px;'>Email address:</td>");
        lstrBuilder.Append("<td width='290' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif; color: #000000;padding: 5px;'>" + fstrEmail + "</td>");
        lstrBuilder.Append("</tr>");

        lstrBuilder.Append("<tr>");
        lstrBuilder.Append("<td align='left' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000000; padding: 5px'>Password:</td>");
        lstrBuilder.Append("<td style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000000; padding: 5px;'>" + fstrPassword + "</td>");
        lstrBuilder.Append("</tr>");

        lstrBuilder.Append("</table></td></tr>");
        lstrBuilder.Append("<tr><td colspan='4' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000; padding-left: 5px; padding-bottom: 10px;'>");


        lstrBuilder.Append("<p>Please let us know if you need anything that you cannot find or if you have any questions regarding our service. We appreciate any feedbacks that you might have for us.</p>");
        lstrBuilder.Append("Thank you for choosing our site. <br/>");


        lstrBuilder.Append("</td></tr>");

        lstrBuilder.Append("<tr><td colspan='4' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000; padding-left: 5px; padding-bottom: 10px;'>");
        lstrBuilder.Append("<p>Best Regards,<br/><a style='font-size:12px;font-family:Arial,Helvetica,sans-serif;padding-bottom:10px;' href = '" + Convert.ToString(ConfigurationManager.AppSettings["SiteUrl"]) + "'>SHC</a> Team<br /></p>");
        lstrBuilder.Append("</td></tr></table>");
        lstrBuilder.Append("</body></html>");

        Commonfunction.SendMail(fstrEmail, Convert.ToString(ConfigurationManager.AppSettings["Support"]), "SHC - Login Details", Convert.ToString(lstrBuilder));
    }
}