using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_roles_user : System.Web.UI.Page
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
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "View");
                    if (lbUserRights)
                        GetAllRoles();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view user roles.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidRoleId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a user role.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("Roles");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view user roles.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidRoleId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view user roles.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetRoleDetails(int fiRoleId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetRoleDetails(fiRoleId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidRoleId.Value = Convert.ToString(fiRoleId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stCode"]);
                txtRole.Text = Convert.ToString(lobjdtDetails.Rows[0]["stRoleName"]);
                txtSortOrderNo.Text = Convert.ToString(lobjdtDetails.Rows[0]["inSortOrder"]);
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
        int liRetVal = 0, liRoleId = 0;

        try
        {
            if (!string.IsNullOrEmpty(hidRoleId.Value.Trim()))
                liRoleId = Convert.ToInt32(hidRoleId.Value.Trim());

            liRetVal = blLogic.InsertUpdateRole(liRoleId, txtCode.Text.Trim(), txtRole.Text.Trim(), Convert.ToInt32(txtSortOrderNo.Text.Trim()), txtRemarks.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Role details saved successfully.", this);
                GetAllRoles();
                txtCode.Text = "";
                txtRole.Text = "";
                txtSortOrderNo.Text = "";
                txtRemarks.Text = "";
                hidRoleId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a user role.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view user roles.";
                    pnlViewContent.Visible = false;
                    lblViewMessage.Visible = true;
                }
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Code already exists.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Name already exists.", this);
            else if (liRetVal == -3)
                Commonfunction.showMsg("Sort order no already exists.", this);
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

    private void GetAllRoles()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllRoles(0, 0, "inSortOrder ASC", "");
            rptRoles.DataSource = lobjdtDetails;
            rptRoles.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptRoles.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptRoles.Visible = false;
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


    protected void rptRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Edit"))
                    Commonfunction.showMsg("You are not authorised to edit a user role.", this);
                else
                {
                    GetRoleDetails(Convert.ToInt32(e.CommandArgument));
                    lblAddMessage.Text = "";
                    pnlAddContent.Visible = true;
                    lblAddMessage.Visible = false;
                }

            }
            else if (e.CommandName == "eDelete")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 37, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a user role.", this);
                else
                {
                    liRetVal = bllogic.DeleteRole(Convert.ToInt32(e.CommandArgument));
                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Role deleted successfully.", this);
                        GetAllRoles();
                        txtCode.Text = "";
                        txtRole.Text = "";
                        txtSortOrderNo.Text = "";
                        txtRemarks.Text = "";
                        hidRoleId.Value = "";
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