using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CommonFunctions;
using System.Data;
using System.Text;
using System.Configuration;

public partial class list_sub_collection_centres : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0, miColCenterId = 0, miSubColCenterid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 3, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view subcollection centers.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllSubCollCenters(miColCenterId, miSubColCenterid);
            }
            //if collection center is logged in then show only his own subcollection centers//
            else if (liRoleFlag == 1)
            {
                miColCenterId = Convert.ToInt32(Session["UserId"]);
                GetAllSubCollCenters(miColCenterId, miSubColCenterid);
            }
            //if subcollection center is logged in then show only his record//
            else if (liRoleFlag == 2)
            {
                miSubColCenterid = Convert.ToInt32(Session["UserId"]);
                GetAllSubCollCenters(miColCenterId, miSubColCenterid);
            }
            else if (liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view subcollection centers.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllSubCollCenters(int fiColCenterId, int fiSubColCenterId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtSubColCenters = new DataTable();
        try
        {
            lobjdtSubColCenters = bllogic.GetAllSubCollCenters(0, 0, "CC.dtCreationDate DESC", "", fiColCenterId, fiSubColCenterId);
            rptCollCenters.DataSource = lobjdtSubColCenters;
            rptCollCenters.DataBind();

            if (lobjdtSubColCenters.Rows.Count > 0)
            {
                rptCollCenters.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptCollCenters.Visible = false;
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

    protected void rptCollCenters_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjData = new DataTable();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eDelete")
            {
                // if the role access is for the other users//
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 3, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a subcollection center.", this);
                else
                {
                    liRetVal = bllogic.DeleteSubCollectionCenter(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Sub Collection center deleted successfully.", this);
                        GetAllSubCollCenters(miColCenterId, miSubColCenterid);
                    }
                }
            }
            else if (e.CommandName == "ePassword")
            {
                lobjData = bllogic.GetSubCollCenterDetails(Convert.ToInt32(e.CommandArgument));

                if (lobjData.Rows.Count > 0)
                {
                    EmailPassword(Convert.ToString(lobjData.Rows[0]["stUserName"]), Convert.ToString(lobjData.Rows[0]["stEmailAddress"]),
                        Convert.ToString(lobjData.Rows[0]["stPassword"]));
                    Commonfunction.showMsg("Password emailed successfully.", this);
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
        }
    }

    private void EmailPassword(string fstrUsername, string fstrEmail, string fstrPassword)
    {
        StringBuilder lstrBuilder = new StringBuilder();

        lstrBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head></head>");
        lstrBuilder.Append("<body><table width='100%' border='0' cellspacing='1' cellpadding='0'>");
        lstrBuilder.Append("<tr><td colspan='4' style='font-size: 12px; font-family: Arial, Helvetica, sans-serif;color: #000; padding-bottom: 10px; padding-left: 5px;'>");

        lstrBuilder.Append("Dear User ,<br /><p>Thank you for requesting your login details. Please find the below login details:</p></td></tr>");

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

        Commonfunction.SendMail(fstrEmail, Convert.ToString(ConfigurationManager.AppSettings["Support"]), "SHC - Sub Collection Center Login Details", Convert.ToString(lstrBuilder));
    }
}