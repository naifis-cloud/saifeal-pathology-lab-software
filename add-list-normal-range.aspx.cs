using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class add_list_normal_range : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        Commonfunction.GetAllGenders(ddlSex);
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            BusinessLogic.FillMainDepartments(ddlDepts);
            ddlTests.Items.Insert(0, new ListItem("--Select--", ""));
            
            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "View");
                    if (lbUserRights)
                        GetAllRanges();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view range.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidRangeId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a range.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("NMRange");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view range.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidRangeId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view range.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    protected void ddlDepts_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTests();
    }

    private void FillTests()
    {
        ddlTests.Items.Clear();
        if (ddlDepts.SelectedIndex > 0)
            BusinessLogic.FillAllTestsByMainDepartment(ddlTests, Convert.ToInt32(ddlDepts.SelectedValue));
        else
            ddlTests.Items.Insert(0, new ListItem("--Select--", ""));
    }

    private void GetNormalRangeDetails(int fiRangeId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetNormalRangeDetails(fiRangeId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidRangeId.Value = Convert.ToString(fiRangeId);
                ddlDepts.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"]);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCode"]);
                FillTests();
                ddlTests.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inTestId"]);
                ddlSex.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["stGender"]);
                txtLower.Text = Convert.ToString(lobjdtDetails.Rows[0]["inLowerRange"]);
                txtUpper.Text = Convert.ToString(lobjdtDetails.Rows[0]["inUpperRange"]);
                txtDays.Text = Convert.ToString(lobjdtDetails.Rows[0]["inDays"]);
                txtUnit.Text = Convert.ToString(lobjdtDetails.Rows[0]["inUnit"]);
                txtRemarks.Text = Convert.ToString(lobjdtDetails.Rows[0]["stRemarks"]);
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
        int liRetVal = 0, liRangeId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidRangeId.Value.Trim()))
                liRangeId = Convert.ToInt32(hidRangeId.Value.Trim());

            liRetVal = blLogic.InsertUpdateNormalRange(liRangeId, Convert.ToInt32(ddlDepts.SelectedValue), txtCode.Text.Trim(),
              Convert.ToInt32(ddlTests.SelectedValue), ddlSex.SelectedValue, Convert.ToInt32(txtLower.Text.Trim()), Convert.ToInt32(txtUpper.Text.Trim()),
              Convert.ToInt32(txtDays.Text.Trim()), txtUnit.Text.Trim(), txtRemarks.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Normal Range details saved successfully.", this);
                GetAllRanges();
                ddlDepts.SelectedIndex = 0;
                txtCode.Text = "";
                ddlTests.SelectedIndex = 0;
                ddlSex.SelectedIndex = 0;
                txtLower.Text = "";
                txtUpper.Text = "";
                txtDays.Text = "";
                txtUnit.Text = "";
                txtRemarks.Text = "";
                hidRangeId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a range.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view range.";
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

    private void GetAllRanges()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllNormalRange(0, 0, "NR.dtCreationDate DESC", "");
            rptRanges.DataSource = lobjdtDetails;
            rptRanges.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptRanges.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptRanges.Visible = false;
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


    protected void rptRanges_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit a range.", this);
                    else
                    {
                        GetNormalRangeDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 14, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete a range.", this);
                else
                {
                    liRetVal = bllogic.DeleteNormalRange(Convert.ToInt32(e.CommandArgument));
                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Range deleted successfully.", this);
                        GetAllRanges();
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