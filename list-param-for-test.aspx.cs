using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class list_param_for_test : System.Web.UI.Page
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
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 16, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view test parameters.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                    {
                        GetTestDetails(Convert.ToInt32(Request.QueryString["tid"]));
                        GetAllTestParameters(Convert.ToInt32(Request.QueryString["tid"]));
                        btnAddParam.PostBackUrl = "add-param.aspx?tid=" + Request.QueryString["tid"];
                    }
                }
            }
            // if logged in as ColCenter OR SubCol center OR Referral doctor, do not allow to view collection center//
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view test parameters.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetTestDetails(int fiTestId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetTestDetails(fiTestId);
            if (lobjdtDetails.Rows.Count > 0)
                lblTestName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTestName"]);
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

    private void GetAllTestParameters(int fiTestId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllTestParameters(0, 0, "inOrderNo ASC", "", fiTestId, 0);
            rptParameters.DataSource = lobjdtDetails;
            rptParameters.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptParameters.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptParameters.Visible = false;
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

    protected void rptParameters_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
        try
        {
            if (e.CommandName == "eDelete")
            {
                // if the role access is for the other users//
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 16, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a test parameter.", this);
                else
                {
                    int liRetVal = bllogic.DeleteTestParameter(Convert.ToInt32(Request.QueryString["tid"]), Convert.ToInt32(e.CommandArgument));
                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Parameter deleted successfully.", this);
                        GetAllTestParameters(Convert.ToInt32(Request.QueryString["tid"]));
                    }
                    else
                        Commonfunction.showMsg("Error occurred while deleting parameter.", this);
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