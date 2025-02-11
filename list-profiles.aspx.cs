using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class list_profiles : System.Web.UI.Page
{
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
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 17, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view profiles.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllProfiles();
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view profiles.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllProfiles()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllProfiles(0, 0, "PR.dtCreationDate DESC", "");
            rptProfiles.DataSource = lobjdtDetails;
            rptProfiles.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptProfiles.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptProfiles.Visible = false;
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

    protected void rptProfiles_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eDelete")
            {
                // if the role access is for the other users//
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 17, "Delete")) 
                    Commonfunction.showMsg("You are not authorised to delete a profile.", this);
                else
                {
                    bllogic.DeleteProfile(Convert.ToInt32(e.CommandArgument));
                    Commonfunction.showMsg("Profile deleted successfully.", this);
                    GetAllProfiles();
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
}