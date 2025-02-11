using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class abbrev_settings : System.Web.UI.Page
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
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 45, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 45, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 45, "View");
                    if (lbUserRights)
                        GetAllAbbreviations();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view abbreviation settings.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidAbbrevId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 45, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add abbreviation setting.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view abbreviation settings.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidAbbrevId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view abbreviation settings.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liAbbrevId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidAbbrevId.Value.Trim()))
                liAbbrevId = Convert.ToInt32(hidAbbrevId.Value.Trim());

            liRetVal = blLogic.UpdateModuleAbbreviation(liAbbrevId, txtShortCode.Text.Trim(), txtAbbrevName.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Abbreviation settings saved successfully.", this, "abbrev-settings.aspx");
                hidAbbrevId.Value = "";
                txtAbbrevName.Text = "";
                GetAllAbbreviations();
            }
            else
                Commonfunction.showMsg("Error occurred while saving abbreviation data.", this);


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

    private void GetAllAbbreviations()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllAbbreviations(0, 0, "dtCreationDate DESC", "");
            rptAbbrevSettings.DataSource = lobjdtDetails;
            rptAbbrevSettings.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptAbbrevSettings.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptAbbrevSettings.Visible = false;
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

    private void GetAbbrevDetails(int fiAbbrevId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAbbreviationDetails(fiAbbrevId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidAbbrevId.Value = Convert.ToString(fiAbbrevId);
                txtShortCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stShortCode"]);
                txtAbbrevName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stInitialCode"]);
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

    protected void rptAbbrevSettings_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 45, "Edit"))
                    Commonfunction.showMsg("You are not authorised to edit abbreviation codes.", this);
                else
                {
                    GetAbbrevDetails(Convert.ToInt32(e.CommandArgument));
                    lblAddMessage.Text = "";
                    pnlAddContent.Visible = true;
                    lblAddMessage.Visible = false;
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