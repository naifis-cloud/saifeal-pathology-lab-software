using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommonFunctions;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BusinessLogic.FillAllRoles(ddlRoles);

            //Removing 
            if (ddlRoles.Items.FindByText("Referral Doctor") != null || ddlRoles.Items.FindByText("ReferralDoctor") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("Referral Doctor"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("ReferralDoctor"));
            }
            if (ddlRoles.Items.FindByText("SubCollection Center") != null || ddlRoles.Items.FindByText("SubCollection Center") != null)
            {
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("SubCollection Center"));
                ddlRoles.Items.Remove(ddlRoles.Items.FindByText("SubCollectionCenter"));
            }

            GetCookieLogin();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        DataTable lobjdtData = new DataTable();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            if (ddlRoles.SelectedItem.Text.Trim().StartsWith("Collection"))
                liRoleFlag = 1;     // for collection center
            else if (ddlRoles.SelectedItem.Text.Trim().StartsWith("Sub Collection") || ddlRoles.SelectedItem.Text.Trim().StartsWith("SubCollection"))
                liRoleFlag = 2;     // for subcollection center
            else if (ddlRoles.SelectedItem.Text.Trim().Contains("Referral Doctor") || ddlRoles.SelectedItem.Text.Trim().Contains("ReferralDoctor")
                || ddlRoles.SelectedItem.Text.Trim().Contains("Doctor"))
                liRoleFlag = 3;     // for referral doctor

            liRetVal = blLogic.ValidateUserLogin(txtUserName.Text.Trim(), txtpassword.Text.Trim(), Convert.ToInt32(ddlRoles.SelectedValue), liRoleFlag);
            if (liRetVal > 0)
            {
                //Getting user's details//
                if (liRoleFlag == 0)
                    lobjdtData = blLogic.GetUserDetails(liRetVal);
                else if (liRoleFlag == 1)
                    lobjdtData = blLogic.GetCollCenterDetails(liRetVal);
                else if (liRoleFlag == 2)
                    lobjdtData = blLogic.GetSubCollCenterDetails(liRetVal);
                else if (liRoleFlag == 3)
                    lobjdtData = blLogic.GetReferalDoctorDetails(liRetVal);

                //Creating user's session with cookies//
                if (lobjdtData.Rows.Count > 0)
                {
                    if (liRoleFlag == 0)
                    {
                        Session["UserEmail"] = Convert.ToString(lobjdtData.Rows[0]["stEmail"]);
                        Session["UserName"] = Convert.ToString(lobjdtData.Rows[0]["stName"]);
                    }
                    else if (liRoleFlag == 1 || liRoleFlag == 2)
                    {
                        Session["UserEmail"] = Convert.ToString(lobjdtData.Rows[0]["stEmailAddress"]);
                        Session["UserName"] = Convert.ToString(lobjdtData.Rows[0]["stCenterName"]);
                    }
                    else if (liRoleFlag == 3)
                    {
                        Session["UserEmail"] = Convert.ToString(lobjdtData.Rows[0]["stEmailAddress"]);
                        Session["UserName"] = Convert.ToString(lobjdtData.Rows[0]["stName"]);
                    }

                    Session["UserId"] = liRetVal;
                    Session["RoleName"] = ddlRoles.SelectedItem.Text.Trim().Substring(ddlRoles.SelectedItem.Text.IndexOf("-") + 1).Trim();
                    Session["RoleId"] = ddlRoles.SelectedValue.Trim();
                    Session["RoleFlag"] = liRoleFlag;

                    // remember me functionality
                    if (chkRemember.Checked == true)
                        RememberLogin(true);
                    else
                        RememberLogin(false);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["url"]))
                    Response.Redirect(Request.QueryString["url"], true);
                else
                    Response.Redirect("lab-dashboard.aspx", true);
            }
            else
                Commonfunction.showMsg("Invalid login details entered.", this);
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

    private void RememberLogin(bool flgRemember)
    {
        HttpCookie loUserInfoCookie;
        loUserInfoCookie = new HttpCookie("userInfo");
        loUserInfoCookie.Values["stEmailAddress"] = txtUserName.Text.Trim();
        loUserInfoCookie.Values["stPassword"] = txtpassword.Text.Trim();

        //setting the cookie to expire after a month.
        if (flgRemember)
            loUserInfoCookie.Expires = System.DateTime.Now.AddDays(30);
        else
            loUserInfoCookie.Expires = System.DateTime.Now.AddDays(-1);

        Response.Cookies.Add(loUserInfoCookie);
    }

    private void GetCookieLogin()
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Request.Cookies["userInfo"])))
        {
            txtUserName.Text = Server.HtmlEncode(Request.Cookies["userInfo"]["stEmailAddress"]);
            txtpassword.Attributes.Add("value", Server.HtmlEncode(Request.Cookies["userInfo"]["stPassword"]));
            chkRemember.Checked = true;
        }
    }
}