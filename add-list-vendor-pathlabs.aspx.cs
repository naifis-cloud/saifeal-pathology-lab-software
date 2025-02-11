using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using AjaxControlToolkit;

public partial class add_list_vendor_pathlabs : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            Commonfunction.GetAllStates(ddlState);
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));


            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "View");
                    if (lbUserRights)
                        GetAllVendorLabs();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view vendor path labs.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidVendorLabId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a vendor path lab.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("RefLabs");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view vendor path labs.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidVendorLabId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view vendor path labs.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }

    }

    private void GetVendorLabDetails(int fiVendorLabId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetVendorLabDetails(fiVendorLabId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidVendorLabId.Value = Convert.ToString(fiVendorLabId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stVendorLabCode"]);
                txtMachineName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stVendorLabName"]);
                txtAddress.Text = Convert.ToString(lobjdtDetails.Rows[0]["stLabAddress"]);

                ddlState.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inStateId"]);

                Commonfunction.GetCities(ddlCity, Convert.ToInt32(ddlState.SelectedValue));

                if (ddlCity.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inCityId"])) != null)
                    ddlCity.Items.FindByValue(Convert.ToString(lobjdtDetails.Rows[0]["inCityId"])).Selected = true;


                txtPinCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stLabPinCode"]);
                txtLandLine.Text = Convert.ToString(lobjdtDetails.Rows[0]["stLandLineNo"]);
                txtMobile.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMobileNo"]);
                txtEmail.Text = Convert.ToString(lobjdtDetails.Rows[0]["stEmailAddress"]);
                txtContactPerson.Text = Convert.ToString(lobjdtDetails.Rows[0]["stContactPerson"]);
                txtWebsiteName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stWebsiteName"]);
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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
            Commonfunction.GetCities(ddlCity, Convert.ToInt32(ddlState.SelectedValue));
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("--Select--", ""));
        }
        ToolkitScriptManager tman = (ToolkitScriptManager)this.Master.FindControl("ScriptManager1");
        if (tman != null)
        {
            tman.SetFocus(ddlState);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liVendorLabId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidVendorLabId.Value.Trim()))
                liVendorLabId = Convert.ToInt32(hidVendorLabId.Value.Trim());

            liRetVal = blLogic.InsertUpdateVendorLab(liVendorLabId, txtCode.Text.Trim(), txtMachineName.Text.Trim(), txtAddress.Text.Trim(),
                        Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue), txtPinCode.Text.Trim(),
                        txtLandLine.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), txtContactPerson.Text.Trim(), txtWebsiteName.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Vendor Lab details saved successfully.", this);
                GetAllVendorLabs();
                txtCode.Text = "";
                txtMachineName.Text = "";
                txtAddress.Text = "";
                ddlState.SelectedIndex = 0;
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("--Select--", ""));
                txtPinCode.Text = "";
                txtLandLine.Text = "";
                txtMobile.Text = "";
                txtEmail.Text = "";
                txtContactPerson.Text = "";
                txtWebsiteName.Text = "";
                hidVendorLabId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a vendor path lab.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view vendor path labs.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Name already exists.", this);
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

    private void GetAllVendorLabs()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllVendorLabs(0, 0, "VL.dtCreationdate DESC", "");
            rptVendorLabs.DataSource = lobjdtDetails;
            rptVendorLabs.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptVendorLabs.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptVendorLabs.Visible = false;
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


    protected void rptVendorLabs_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit a vendor path lab.", this);
                    else
                    {
                        GetVendorLabDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 8, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete a vendor path lab.", this);
                else
                {
                    liRetVal = bllogic.DeleteVendorLab(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Vendor Lab deleted successfully.", this);
                        GetAllVendorLabs();
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