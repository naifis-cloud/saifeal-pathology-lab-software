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


public partial class list_collection_centre : System.Web.UI.Page
{
    int miColCenterId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 1, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view collection centers.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllCollCenters(miColCenterId);
            }
            //if collection center is logged in then show only his record//
            else if (liRoleFlag == 1)
            {
                miColCenterId = Convert.ToInt32(Session["UserId"]);
                GetAllCollCenters(miColCenterId);
            }
            // if logged in as SubCol center OR Referral doctor, do not allow to view collection center//
            else if (liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view collection centers.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllCollCenters(int fiColCenterId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtColCenters = new DataTable();
        try
        {
            lobjdtColCenters = bllogic.GetAllCollCenters(0, 0, "CC.dtCreationDate DESC", "", fiColCenterId);
            rptCollCenters.DataSource = lobjdtColCenters;
            rptCollCenters.DataBind();

            if (lobjdtColCenters.Rows.Count > 0)
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
        int liRetVal = 0, liRoleFlag = 0;

        liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

        try
        {
            if (e.CommandName == "eDelete")
            {
                // if the role access is for the other users//
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 1, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a collection center.", this);
                else
                {
                    liRetVal = bllogic.DeleteCollectionCenter(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Collection center deleted successfully.", this);
                        GetAllCollCenters(miColCenterId);
                    }
                }
            }

            else if (e.CommandName == "ePassword")
            {
                lobjData = bllogic.GetCollCenterDetails(Convert.ToInt32(e.CommandArgument));

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

        Commonfunction.SendMail(fstrEmail, Convert.ToString(ConfigurationManager.AppSettings["Support"]), "SHC - Collection Center Login Details", Convert.ToString(lstrBuilder));
    }
}