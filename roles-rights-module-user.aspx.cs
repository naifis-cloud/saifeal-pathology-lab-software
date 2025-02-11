using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class roles_rights_module_user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;

            BusinessLogic.FillAllRoles(ddlRoles);

            if (ddlRoles.Items.FindByText("Collection Center") != null || ddlRoles.Items.FindByText("CollectionCenter") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Collection Center"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("CollectionCenter"));
            }
            if (ddlRoles.Items.FindByText("SubCollection Center") != null || ddlRoles.Items.FindByText("SubCollectionCenter") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("SubCollection Center"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("SubCollectionCenter"));
            }
            if (ddlRoles.Items.FindByText("Referral Doctor") != null || ddlRoles.Items.FindByText("Referral Doctor") != null
                || ddlRoles.Items.FindByText("Doctor") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Referral Doctor"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("ReferralDoctor"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Doctor"));
            }

            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 38, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view access rights.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllModules(0);
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
            {
                lblMessage.Text = "You are not authorised to view access rights.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllModules(int fiRoleId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllModules(fiRoleId);
            rptModules.DataSource = lobjdtDetails;
            rptModules.DataBind();

            hidRowsCount.Value = Convert.ToString(lobjdtDetails.Rows.Count);
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

    protected void btnAssignRights_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 38, "Add"))
                Commonfunction.showMsg("You are not authorised to assign access rights.", this);
            
            else
            {
                //Deleting the role rights if already exists//
                blLogic.DeleteRoleRights(Convert.ToInt32(ddlRoles.SelectedValue));

                foreach (RepeaterItem rptItem in rptModules.Items)
                {
                    Label lblModuleId = (Label)rptItem.FindControl("lblModuleId");
                    CheckBox chkView = (CheckBox)rptItem.FindControl("chkView");
                    CheckBox chkAdd = (CheckBox)rptItem.FindControl("chkAdd");
                    CheckBox chkEdit = (CheckBox)rptItem.FindControl("chkEdit");
                    CheckBox chkDelete = (CheckBox)rptItem.FindControl("chkDelete");

                    if (lblModuleId != null && chkView != null && chkAdd != null && chkEdit != null && chkDelete != null)
                    {
                        blLogic.InsertRoleRights(Convert.ToInt32(ddlRoles.SelectedValue), Convert.ToInt32(lblModuleId.Text.Trim()),
                            chkView.Checked, chkAdd.Checked, chkEdit.Checked, chkDelete.Checked);
                    }
                }
                Commonfunction.showMsg("Rights assigned successfully for the selected role.", this);
                ddlRoles.SelectedIndex = 0;
                GetAllModules(0);
            }
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

    //protected void btnCheck_Click(object sender, EventArgs e)
    //{
    //}
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoles.SelectedIndex > 0)
            GetAllModules(Convert.ToInt32(ddlRoles.SelectedValue));
        else
            GetAllModules(0);
    }

    protected void rptModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblModuleId = (Label)e.Item.FindControl("lblModuleId");
            CheckBox chkView = (CheckBox)e.Item.FindControl("chkView");
            CheckBox chkAdd = (CheckBox)e.Item.FindControl("chkAdd");
            CheckBox chkEdit = (CheckBox)e.Item.FindControl("chkEdit");
            CheckBox chkDelete = (CheckBox)e.Item.FindControl("chkDelete");
            if (lblModuleId != null && chkView != null && chkAdd != null && chkEdit != null && chkDelete != null)
            {
                if (lblModuleId.Text.Trim() == "23")
                    chkDelete.Enabled = false;

                else if (lblModuleId.Text.Trim() == "25" || lblModuleId.Text.Trim() == "26" || lblModuleId.Text.Trim() == "27" || lblModuleId.Text.Trim() == "28"
                    || lblModuleId.Text.Trim() == "30" || lblModuleId.Text.Trim() == "38")
                {
                    chkEdit.Enabled = false;
                    chkDelete.Enabled = false;
                }
                else if (lblModuleId.Text.Trim() == "29" || lblModuleId.Text.Trim() == "31" || lblModuleId.Text.Trim() == "32"
                    || lblModuleId.Text.Trim() == "33" || lblModuleId.Text.Trim() == "34" || lblModuleId.Text.Trim() == "41")
                {
                    chkAdd.Enabled = false;
                    chkEdit.Enabled = false;
                    chkDelete.Enabled = false;
                }
                else if (lblModuleId.Text.Trim() == "45")
                {
                    chkAdd.Enabled = false;
                    chkDelete.Enabled = false;
                }
            }
        }
    }
}