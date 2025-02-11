using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class authorise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["pname"]))
            {
                if (liRoleFlag == 0) // if the role access is for the other users//
                {
                    if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 26, "Add"))
                    {
                        lblMessage.Text = "You are not authorised to enter test results.";
                        pnlMainContent.Visible = false;
                        pnlHeader.Visible = false;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        GetPatientTests(Convert.ToInt32(Request.QueryString["pid"]));
                        lblPatientName.Text = Request.QueryString["pname"].Trim();
                    }
                }
                else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Referral doctor
                {
                    lblMessage.Text = "You are not authorised to enter test results.";
                    pnlMainContent.Visible = false;
                    pnlHeader.Visible = false;
                    lblMessage.Visible = true;
                }
            }
        }
    }

    private void GetPatientTests(int fiPatientId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetPatientTests(fiPatientId);
            rptPatientTests.DataSource = lobjdtDetails;
            rptPatientTests.DataBind();
            hidTestCount.Value = Convert.ToString(rptPatientTests.Items.Count);
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

    protected void rptPatientTests_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtTestData = new DataTable();
        try
        {
            int liCountCheck = 0, liRoleFlag = 0,
            liAuthoriseStatus = 0, liTestAuthCount = 0, liTotalTestCount = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            if (e.CommandName == "eAuthorise")
            {
                if (!BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 27, "Add"))
                    Commonfunction.showMsg("You are not authorised to authorise a test.", this, "test-authorise.aspx");
                else
                {
                    //updating the test status to 'partial authorise'
                    liAuthoriseStatus = bllogic.UpdateAuthoriseStatus(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(e.CommandArgument));
                    if (liAuthoriseStatus > 0)
                    {
                        //Validating if all the tests are authorised, if yes, then set  the status to - 'Full Authorized' else 'Partial Authorize'
                        lobjdtTestData = bllogic.GetTestAuthoriseCount(Convert.ToInt32(Request.QueryString["pid"]));
                        if (lobjdtTestData.Rows.Count > 0)
                        {
                            liTestAuthCount = Convert.ToInt32(lobjdtTestData.Rows[0]["TestAuthCount"]);
                            liTotalTestCount = Convert.ToInt32(lobjdtTestData.Rows[0]["TotalTestCount"]);

                            if (Convert.ToInt32(lobjdtTestData.Rows[0]["TotalTestCount"]) > 0)
                            {
                                if (liTestAuthCount == liTotalTestCount) // if all the tests are authorised//
                                    liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(Request.QueryString["pid"]), 7);
                                else
                                    liCountCheck = bllogic.UpdateTestStatus(Convert.ToInt32(Request.QueryString["pid"]), 5);

                                if (liCountCheck > 0)
                                    Commonfunction.showMsg("Test authorised successfully.", this, "test-authorise.aspx");
                            }
                        }
                    }
                    else
                        Commonfunction.showMsg("Test authorisation failed.", this, "test-authorise.aspx");
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

    protected void rptPatientTests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BusinessLogic bllogic = new BusinessLogic();
            try
            {
                Label lblTestId = (Label)e.Item.FindControl("lblTestId");
                Button btnAuthorise = (Button)e.Item.FindControl("btnAuthorise");

                if (lblTestId != null && btnAuthorise != null)
                {
                    if (bllogic.GetAuthoriseStatus(Convert.ToInt32(Request.QueryString["pid"]), Convert.ToInt32(lblTestId.Text.Trim())))
                        btnAuthorise.Visible = false;
                    else
                        btnAuthorise.Visible = true;
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
}