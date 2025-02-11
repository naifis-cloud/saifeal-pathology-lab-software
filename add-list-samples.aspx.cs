using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class add_list_samples : System.Web.UI.Page
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
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "View");
                    if (lbUserRights)
                        GetAllSamples();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view samples.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidSampleId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a sample.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view samples.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidSampleId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view samples.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    private void GetMachineDetails(int fiSampleId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetSampleDetails(fiSampleId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidSampleId.Value = Convert.ToString(fiSampleId);
                txtSample.Text = Convert.ToString(lobjdtDetails.Rows[0]["stSampleName"]);
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
        int liRetVal = 0, liSampleId = 0;
        try
        {
            if (!string.IsNullOrEmpty(hidSampleId.Value.Trim()))
                liSampleId = Convert.ToInt32(hidSampleId.Value.Trim());

            liRetVal = blLogic.InsertUpdateSample(liSampleId, txtSample.Text.Trim());

            if (liRetVal > 0)
            {
                Commonfunction.showMsg("Sample saved successfully.", this);
                GetAllSamples();
                txtSample.Text = "";
                hidSampleId.Value = "";

                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Edit")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Add"))
                {
                    lblAddMessage.Text = "You are not authorised to add a sample.";
                    pnlAddContent.Visible = false;
                    lblAddMessage.Visible = true;
                }

                else if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Add")
                    && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "View"))
                {
                    lblViewMessage.Text = "You are not authorised to view samples.";
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

    private void GetAllSamples()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllSamples(0, 0, "dtCreationDate DESC", "");
            rptSamples.DataSource = lobjdtDetails;
            rptSamples.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptSamples.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptSamples.Visible = false;
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


    protected void rptSamples_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0)
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Edit");
                    if (!lbUserRights)
                        Commonfunction.showMsg("You are not authorised to edit a sample.", this);
                    else
                    {
                        GetMachineDetails(Convert.ToInt32(e.CommandArgument));
                        lblAddMessage.Text = "";
                        pnlAddContent.Visible = true;
                        lblAddMessage.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "eDelete")
            {
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 13, "Delete");
                if (!lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete a sample.", this);
                else
                {
                    liRetVal = bllogic.DeleteSample(Convert.ToInt32(e.CommandArgument));
                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Sample deleted successfully.", this);
                        GetAllSamples();
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