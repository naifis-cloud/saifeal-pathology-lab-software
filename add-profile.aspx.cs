using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CommonFunctions;
using System.Data;

public partial class add_profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            BusinessLogic.FillMainDepartments(ddlDepts);
            BusinessLogic.FillAllTests(lstTests);
            GetTotalPrice();

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                if (!string.IsNullOrEmpty(Request.QueryString["prfid"]))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 17, "Edit");
                    if (lbUserRights)
                        GetProfileDetails(Convert.ToInt32(Request.QueryString["prfid"]));
                }
                else
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 17, "Add");
                    if(lbUserRights)
                        txtCode.Text = Commonfunction.GenerateModuleCode("Profile");
                }

                if (!lbUserRights)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                        lblMessage.Text = "You are not authorised to edit a profile.";
                    else
                        lblMessage.Text = "You are not authorised to add a profile.";

                    pnlMainContent.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Colcenter OR SubCol center OR Referral doctor
            {
                lblMessage.Text = "You are not authorised to add a profile.";
                pnlMainContent.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liProfileId = 0;
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["prfid"]))
                liProfileId = Convert.ToInt32(Request.QueryString["prfid"]);

            liRetVal = blLogic.InsertUpdateProfile(liProfileId, txtCode.Text.Trim(), txtProfileName.Text.Trim(), txtMethod.Text.Trim(),
                Convert.ToInt32(ddlDepts.SelectedValue), Convert.ToInt32(txtGroupRate.Text.Trim()));

            if (liRetVal > 0)
            {
                //Deleting and adding the profile tests//
                blLogic.DeleteProfileTests(liRetVal);

                foreach (ListItem lstItem in lstSelectedTests.Items)
                {
                    blLogic.InsertUpdateProfileTest(liRetVal, Convert.ToInt32(lstItem.Value));
                }

                Commonfunction.showMsg("Profile details saved successfully.", this, "list-profiles.aspx");
            }
            else if (liRetVal == -1)
                Commonfunction.showMsg("Profile code already exists for selected department.", this);
            else if (liRetVal == -2)
                Commonfunction.showMsg("Profile name already exists for selected department.", this);
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

    private void GetProfileDetails(int fiProfileId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();

        try
        {
            lobjdtDetails = bllogic.GetProfileDetails(fiProfileId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["stProfileCode"]);
                txtProfileName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stProfileName"]);
                txtMethod.Text = Convert.ToString(lobjdtDetails.Rows[0]["stProfileMethod"]);
                ddlDepts.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inMainDeptId"]);
                txtGroupRate.Text = Convert.ToString(lobjdtDetails.Rows[0]["inGroupRate"]);

                //Getting the selected tests//
                lobjdtDetails = bllogic.GetAllProfileTests(fiProfileId);
                if (lobjdtDetails.Rows.Count > 0)
                {
                    //Adding the items in second listbox from an array//
                    foreach (DataRow lodtRow in lobjdtDetails.Rows)
                    {
                        ListItem lstItem = new ListItem();
                        lstItem.Value = Convert.ToString(lodtRow["inTestId"]);
                        lstItem.Text = Convert.ToString(lodtRow["stTestCode"]) + " - " + Convert.ToString(lodtRow["stTestName"]);
                        lstSelectedTests.Items.Add(lstItem);

                        lstTests.Items.Remove(lstItem);
                    }
                    lstSelectedTests.SelectedIndex = -1;
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

    protected void btnAddTest_Click(object sender, EventArgs e)
    {
        ArrayList arraylist1 = new ArrayList();

        try
        {
            if (lstTests.SelectedIndex >= 0)
            {
                //Adding the selected items in an array//
                for (int i = 0; i < lstTests.Items.Count; i++)
                {
                    if (lstTests.Items[i].Selected)
                    {
                        if (!arraylist1.Contains(lstTests.Items[i]))
                            arraylist1.Add(lstTests.Items[i]);
                    }
                }

                //Adding the items in second listbox from an array//
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    if (!lstSelectedTests.Items.Contains(((ListItem)arraylist1[i])))
                        lstSelectedTests.Items.Add(((ListItem)arraylist1[i]));

                    lstTests.Items.Remove(((ListItem)arraylist1[i]));
                }
                lstSelectedTests.SelectedIndex = -1;
                GetTotalPrice();
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
    }

    protected void btnRemoveTest_Click(object sender, EventArgs e)
    {
        ArrayList arraylist2 = new ArrayList();
        try
        {
            if (lstSelectedTests.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstSelectedTests.Items.Count; i++)
                {
                    if (lstSelectedTests.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(lstSelectedTests.Items[i]))
                        {
                            arraylist2.Add(lstSelectedTests.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!lstTests.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        lstTests.Items.Add(((ListItem)arraylist2[i]));
                    }
                    lstSelectedTests.Items.Remove(((ListItem)arraylist2[i]));
                }
                lstTests.SelectedIndex = -1;
                GetTotalPrice();
            }
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
    }

    private void GetTotalPrice()
    {
        int liTotalPrice = 0;
        BusinessLogic blLogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        try
        {
            foreach (ListItem lstItem in lstSelectedTests.Items)
            {
                lobjdtData = blLogic.GetTestDetails(Convert.ToInt32(lstItem.Value));
                if (lobjdtData.Rows.Count > 0)
                    liTotalPrice += Convert.ToInt32(lobjdtData.Rows[0]["inTestRate"]);
            }
            lblTotalPrice.Text = Convert.ToString(liTotalPrice);
        }
        catch (Exception foException)
        {
            throw new Exception(foException.Message);
        }
        finally
        {
            blLogic = null;
            lobjdtData = null;
        }
    }
}