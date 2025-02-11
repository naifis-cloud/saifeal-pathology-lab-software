using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Data;
using CommonFunctions;

public partial class list_tests : System.Web.UI.Page
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
                lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 15, "View");

                if (!lbUserRights)
                {
                    lblMessage.Text = "You are not authorised to view tests.";
                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
                else
                    GetAllTests();
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3)
            {
                lblMessage.Text = "You are not authorised to view tests.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    private void GetAllTests()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjDtTests = new DataTable();
        try
        {
            lobjDtTests = bllogic.GetAllTests(0, 0, "T.dtCreationDate DESC", "");
            rptTests.DataSource = lobjDtTests;
            rptTests.DataBind();

            if (lobjDtTests.Rows.Count > 0)
            {
                rptTests.Visible = true;
                trNoData.Visible = false;
            }

            else
            {
                rptTests.Visible = false;
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

    protected void rptTests_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjData = new DataTable();
        int liRetVal = 0;
        try
        {
            if (e.CommandName == "eDelete")
            {
                if (liRoleFlag == 0) // if the role access is for the other users//
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 15, "Delete");

                if (liRoleFlag == 0 && !lbUserRights)
                    Commonfunction.showMsg("You are not authorised to delete a test.", this);
                else
                {
                    liRetVal = bllogic.DeleteTest(Convert.ToInt32(e.CommandArgument));
                    if (liRetVal > 0)
                    {
                        Commonfunction.showMsg("Test deleted successfully.", this);
                        GetAllTests();
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

    protected void rptTests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            Label lblType = (Label)e.Item.FindControl("lblType");
            Label lblTestId = (Label)e.Item.FindControl("lblTestId");
            HtmlAnchor anchDesc = (HtmlAnchor)e.Item.FindControl("anchDesc");
            //Label lblParamType = (Label)e.Item.FindControl("lblParamType");
            //HtmlAnchor anchParam = (HtmlAnchor)e.Item.FindControl("anchParam");

            if (lblType != null && anchDesc != null && lblTestId != null)
            {
                if (lblType.Text.Trim() == "Descriptive")
                {
                    anchDesc.Visible = true;
                    anchDesc.HRef = "test-description.aspx?tid=" + lblTestId.Text.Trim();
                }
            }

            //if (anchParam != null && lblParamType != null)
            //{
            //    if (lblParamType.Text.Trim() == "Multiple")
            //    {
            //        anchParam.Visible = true;
            //        anchParam.HRef = "list-param-for-test.aspx?tid=" + lblTestId.Text.Trim();
            //    }
            //}
        }
    }
}