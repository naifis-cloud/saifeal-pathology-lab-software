using System;
using System.Collections.Generic;
using CommonFunctions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class add_rate_list_mainlab : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "View");
                    if (lbUserRights)
                        GetAllMainLabRateLists();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view main lab rate list.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidRateListId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add main lab rate list.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("RateList-ML");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view main lab rate list.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidRateListId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view main lab rate list.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetRateListDetails(int fiRateListId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetMainlabRateListDetails(fiRateListId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidRateListId.Value = Convert.ToString(fiRateListId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stRateListCode"]);
                txtMachineName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stRateListName"]);
                txtDescription.Text = Convert.ToString(lobjdtDetails.Rows[0]["stRateListDesc"]);
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
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liRatelistId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidRateListId.Value.Trim()))
                liRatelistId = Convert.ToInt32(hidRateListId.Value.Trim());

            liRetVal = blLogic.InsertUpdateMainLabRateList(liRatelistId, txtCode.Text.Trim(), txtMachineName.Text.Trim(), txtDescription.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Rate list details saved successfully.", this);
                GetAllMainLabRateLists();
                txtCode.Text = "";
                txtMachineName.Text = "";
                txtDescription.Text = "";
                hidRateListId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add main lab rate list.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view main lab rate list.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Code already exists.", this);
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

    private void GetAllMainLabRateLists()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllMainLabRateLists(0, 0, "dtCreationDate DESC", "");
            rptRateLists.DataSource = lobjdtDetails;
            rptRateLists.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptRateLists.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptRateLists.Visible = false;
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


    protected void rptRateLists_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit a main lab rate list.", this);
                    else
                    {
                        GetRateListDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 19, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete main lab rate list.", this);
                else
                {
                    //validating if the rates are assigned to the rate list. If yes, do not allow user to delete//
                    liRetVal = bllogic.ValidateMainLabRateList(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal <= 0)
                    {
                        liRetVal = bllogic.DeleteMainLabRateList(Convert.ToInt32(e.CommandArgument));

                        if (liRetVal > 0)
                        {
                            Commonfunction.showMsg("Rate list item deleted successfully.", this);
                            GetAllMainLabRateLists();
                        }
                    }
                    else
                        Commonfunction.showMsg("Cannot delete rate list as rates are already assigned.", this);
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

    protected void btnDefaultPrice_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        try
        {
            foreach (RepeaterItem rptItem in rptRateLists.Items)
            {
                RadioButton rbtnDefaultPrice = (RadioButton)rptItem.FindControl("rbtnDefaultPrice");
                Label lblRateListId = (Label)rptItem.FindControl("lblRateListId");

                if (rbtnDefaultPrice != null && lblRateListId != null)
                {
                    if (rbtnDefaultPrice.Checked)
                    {
                        bllogic.UpdateDefaultPriceListForMainLab(Convert.ToInt32(lblRateListId.Text.Trim()));
                        break;
                    }
                }
            }
            Commonfunction.showMsg("Default Price List Set Successfully.", this);
            GetAllMainLabRateLists();
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