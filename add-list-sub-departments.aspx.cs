using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_sub_departments : System.Web.UI.Page
{
    bool lbUserRights = true;
    int liRoleFlag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            BusinessLogic.FillMainDepartments(ddlMainDepts);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "View");
                    if (lbUserRights)
                        GetAllSubDepartments();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view sub departments.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidSubDeptId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a sub department.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("SubDepts");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view sub departments.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidSubDeptId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view sub departments.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetSubDepartmentDetails(int fiSubDeptId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetSubDepartmentDetails(fiSubDeptId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidSubDeptId.Value = Convert.ToString(fiSubDeptId);
                ddlMainDepts.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"]);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stSubDeptCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stSubDeptName"]);
                txtSortOrder.Text = Convert.ToString(lobjdtDetails.Rows[0]["inSortOrder"]);
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
        int liRetVal = 0, liSubDeptId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidSubDeptId.Value.Trim()))
                liSubDeptId = Convert.ToInt32(hidSubDeptId.Value.Trim());

            liRetVal = blLogic.InsertUpdateSubDepartment(liSubDeptId, Convert.ToInt32(ddlMainDepts.SelectedValue), txtCode.Text.Trim(),
                        txtName.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), txtRemarks.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Sub Department details saved successfully.", this);
                GetAllSubDepartments();
                ddlMainDepts.SelectedIndex = 0;
                txtCode.Text = "";
                txtName.Text = "";
                txtSortOrder.Text = "";
                txtRemarks.Text = "";
                hidSubDeptId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a sub department.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view sub departments.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Sub Department Code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Sub Department Name already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Sort Order already exists.", this);
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

    private void GetAllSubDepartments()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllSubDepartments(0, 0, "SD.inSortOrder", "");
            rptSubDepts.DataSource = lobjdtDetails;
            rptSubDepts.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptSubDepts.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptSubDepts.Visible = false;
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


    protected void rptSubDepts_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit a sub department.", this);
                    else
                    {
                        GetSubDepartmentDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 7, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete a sub department.", this);
                else
                {
                    liRetVal = bllogic.DeleteSubDepartment(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Sub department deleted successfully.", this);
                        GetAllSubDepartments();
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