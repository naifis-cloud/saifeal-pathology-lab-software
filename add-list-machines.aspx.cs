using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_machines : System.Web.UI.Page
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
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "View");
                    if (lbUserRights)
                        GetAllMachines();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view machines.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidMachineId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a machine.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("Machines");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view machines.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidMachineId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view machines.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetMachineDetails(int fiRateListId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetMachineDetails(fiRateListId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidMachineId.Value = Convert.ToString(fiRateListId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMachineCode"]);
                txtMachineName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMachineName"]);
                txtDescription.Text = Convert.ToString(lobjdtDetails.Rows[0]["stMachineDesc"]);
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
        int liRetVal = 0, liMachineId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidMachineId.Value.Trim()))
                liMachineId = Convert.ToInt32(hidMachineId.Value.Trim());

            liRetVal = blLogic.InsertUpdateMachine(liMachineId, txtCode.Text.Trim(), txtMachineName.Text.Trim(), txtDescription.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Machine details saved successfully.", this);
                GetAllMachines();
                txtCode.Text = "";
                txtMachineName.Text = "";
                txtDescription.Text = "";
                hidMachineId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a machine.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view machines.";
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

    private void GetAllMachines()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllMachines(0, 0, "dtCreationDate DESC", "");
            rptMachines.DataSource = lobjdtDetails;
            rptMachines.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptMachines.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptMachines.Visible = false;
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


    protected void rptMachines_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Edit"))
                    Commonfunction.showMsg("You are not authorised to edit a machine.", this);
                else
                {
                    GetMachineDetails(Convert.ToInt32(e.CommandArgument));
                    lblAddMessage.Text = "";
                    pnlAddContent.Visible = true;
                    lblAddMessage.Visible = false;
                }
            }
            else if (e.CommandName == "eDelete")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 12, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a machine.", this);
                else
                {
                    liRetVal = bllogic.DeleteMachine(Convert.ToInt32(e.CommandArgument));
                    txtCode.Text = "";
                    txtMachineName.Text = "";
                    txtDescription.Text = "";
                    hidMachineId.Value = "";
                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Machine deleted successfully.", this);
                        GetAllMachines();
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