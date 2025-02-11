using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_abbreviations : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            BusinessLogic.FillAllTests(ddlTests);
            
            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "View");
                    if (lbUserRights)
                        GetAllTestAbbrevs();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view abbreviations.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidTestAbbrevId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add an abbreviation.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view abbreviations.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidTestAbbrevId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view abbreviations.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liTestAbbrevId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidTestAbbrevId.Value.Trim()))
                liTestAbbrevId = Convert.ToInt32(hidTestAbbrevId.Value.Trim());

            liRetVal = blLogic.InsertUpdateTestAbbrev(liTestAbbrevId, Convert.ToInt32(ddlTests.SelectedValue),
                txtAbbrev.Text.Trim(), txtDesc.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Test Abbreviation details saved successfully.", this);
                GetAllTestAbbrevs();
                ddlTests.SelectedIndex = 0;
                txtAbbrev.Text = "";
                txtDesc.Text = "";
                hidTestAbbrevId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add an abbreviation.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view abbreviations.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Test Abbreviation code already exists for selected Test.", this);
            else
                Commonfunction.showMsg("Error occurred while saving data!.", this);
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

    private void GetTestAbbrevDetails(int fiTestAbbrevId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetTestAbbrevDetails(fiTestAbbrevId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidTestAbbrevId.Value = Convert.ToString(fiTestAbbrevId);
                ddlTests.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inTestId"]);
                txtAbbrev.Text = Convert.ToString(lobjdtDetails.Rows[0]["stAbbrevCode"]);
                txtDesc.Text = Convert.ToString(lobjdtDetails.Rows[0]["stAbbrevDesc"]);
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

    private void GetAllTestAbbrevs()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllTestAbbrevs(0, 0, "TA.dtCreationDate DESC", "");
            rptTestAbbrevs.DataSource = lobjdtDetails;
            rptTestAbbrevs.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptTestAbbrevs.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptTestAbbrevs.Visible = false;
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


    protected void rptTestAbbrevs_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit an abbreviation.", this);
                    else
                    {
                        GetTestAbbrevDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {

                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 10, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete an abbreviation.", this);
                else
                {
                    liRetVal = bllogic.DeleteTestAbbrev(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Test Abbreviation deleted successfully.", this);
                        GetAllTestAbbrevs();
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
            bllogic = null;
        }
    }
}