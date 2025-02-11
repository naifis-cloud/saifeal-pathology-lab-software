using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class vendor_test_rate : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            BusinessLogic.FillVendorLabs(ddlVendorLabs);
            BusinessLogic.FillAllTests(ddlTests);
            
            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "View");
                    if (lbUserRights)
                        GetAllVendorTestRates();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view vendor test rates.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidVendorRateId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a vendor test rate.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view vendor test rates.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidVendorRateId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view vendor test rates.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetVendorTestRateDetails(int fiVendorTestRateId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetVendorTestRateDetails(fiVendorTestRateId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidVendorRateId.Value = Convert.ToString(fiVendorTestRateId);
                ddlVendorLabs.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inVendorLabId"]);
                ddlTests.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inTestId"]);
                txtRate.Text = Convert.ToString(lobjdtDetails.Rows[0]["inRate"]);
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
        int liRetVal = 0, liVendorRateId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidVendorRateId.Value.Trim()))
                liVendorRateId = Convert.ToInt32(hidVendorRateId.Value.Trim());

            liRetVal = blLogic.InsertUpdateVendorTestRate(liVendorRateId, Convert.ToInt32(ddlVendorLabs.SelectedValue), 
                Convert.ToInt32(ddlTests.SelectedValue),Convert.ToInt32(txtRate.Text.Trim()));

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Vendor Test Rate details saved successfully.", this);
                GetAllVendorTestRates();
                ddlVendorLabs.SelectedIndex = 0;
                ddlTests.SelectedIndex = 0;
                txtRate.Text = "";
                hidVendorRateId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a vendor path lab.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view vendor path labs.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Rate already exists for selected Vendor Lab and selected Test.", this);
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

    private void GetAllVendorTestRates()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllVendorTestRates(0, 0, "VTR.dtCreationDate DESC", "");
            rptVendorRates.DataSource = lobjdtDetails;
            rptVendorRates.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptVendorRates.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptVendorRates.Visible = false;
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


    protected void rptVendorRates_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit a vendor test rate.", this);
                    else
                    {
                        GetVendorTestRateDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 9, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete a vendor test rate.", this);
                else
                {
                    liRetVal = bllogic.DeleteVendorTestRate(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Vendor Test Rate deleted successfully.", this);
                        GetAllVendorTestRates();
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