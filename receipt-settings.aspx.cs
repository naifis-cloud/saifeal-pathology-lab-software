﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonFunctions;
using System.Data;

public partial class receipt_settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Commonfunction.CheckUserLogIn();
        if (!IsPostBack)
        {
            bool lbUserRights = true;
            int liRoleFlag = 0;
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);

            Commonfunction.GetAllFonts(ddlFontFamily);

            if (liRoleFlag == 0) // if the role access is for the other users//
            {
                // if both add and view rights are not there//
                if (BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 44, "View")
                    || BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 44, "Add"))
                {
                    lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 44, "View");
                    if (lbUserRights)
                        GetAllReportSettings();
                    else
                    {
                        lblViewMessage.Text = "You are not authorised to view receipt settings.";
                        pnlViewContent.Visible = false;
                        lblViewMessage.Visible = true;
                    }

                    if (string.IsNullOrEmpty(hidRCSettingsId.Value.Trim()))
                    {
                        lbUserRights = BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 44, "Add");
                        if (!lbUserRights)
                        {
                            lblAddMessage.Text = "You are not authorised to add a receipt setting.";
                            pnlAddContent.Visible = false;
                            lblAddMessage.Visible = true;
                        }
                        else
                            txtCode.Text = Commonfunction.GenerateModuleCode("RCSettings");
                    }
                }
                else
                {
                    lblMainMessage.Text = "You are not authorised to add/view receipt settings.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
            else if (liRoleFlag == 1 || liRoleFlag == 2 || liRoleFlag == 3) // if logged in as Col Center OR SubCol center OR Referral doctor
            {
                if (string.IsNullOrEmpty(hidRCSettingsId.Value.Trim()))
                {
                    lblMainMessage.Text = "You are not authorised to add/view receipt settings.";
                    pnlMainContent.Visible = false;
                    lblMainMessage.Visible = true;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BusinessLogic blLogic = new BusinessLogic();
        int liRetVal = 0, liRptId = 0;
        string lsImageFile = "", lsLocationToSave = "", lstrLogoImage = "", lstrSignImage = "";
        int liLeftMargin = 0, liRightMargin = 0, liTopMargin = 0, liBottomMargin = 0;
        bool IsFileSizeValid = true;
        try
        {
            //Validating the  first image file size of 2 MB
            if (fupLogo.PostedFile.ContentLength > 2097152 || fupSignature.PostedFile.ContentLength > 2097152)
                IsFileSizeValid = false;

            if (IsFileSizeValid)
            {
                //Uploading of logo image
                if (fupLogo.HasFile)
                {
                    //deleting the old image if new image is uploaded//
                    if (!string.IsNullOrEmpty(hidLogoImage.Value.Trim()))
                    {
                        if (System.IO.File.Exists(Server.MapPath("images/receiptsettings/logo/" + hidLogoImage.Value.Trim())))
                            System.IO.File.Delete(Server.MapPath("images/receiptsettings/logo/" + hidLogoImage.Value.Trim()));
                    }

                    //Creating a file name for the image using GUID//
                    lsImageFile = "logo_" + Guid.NewGuid().ToString().ToLower().Substring(0, 6);

                    //For Thumbnail Image//
                    lsLocationToSave = Server.MapPath(Commonfunction.absPath() + "/images/receiptsettings/logo/");
                    lstrLogoImage = Commonfunction.uploadImageFile(fupLogo, lsLocationToSave, 100, 100, lsImageFile);
                }
                else
                {
                    if (!string.IsNullOrEmpty(hidLogoImage.Value.Trim()))
                        lstrLogoImage = hidLogoImage.Value.Trim();
                }


                //Uploading of signature image
                if (fupSignature.HasFile)
                {
                    //deleting the old image if new image is uploaded//
                    if (!string.IsNullOrEmpty(hidSignatureImage.Value.Trim()))
                    {
                        if (System.IO.File.Exists(Server.MapPath("images/receiptsettings/signatures/" + hidSignatureImage.Value.Trim())))
                            System.IO.File.Delete(Server.MapPath("images/receiptsettings/signatures/" + hidSignatureImage.Value.Trim()));
                    }

                    //Creating a file name for the image using GUID//
                    lsImageFile = "sign_" + Guid.NewGuid().ToString().ToLower().Substring(0, 6);

                    //For Thumbnail Image//
                    lsLocationToSave = Server.MapPath(Commonfunction.absPath() + "/images/receiptsettings/signatures/");
                    lstrSignImage = Commonfunction.uploadImageFile(fupSignature, lsLocationToSave, 100, 100, lsImageFile);
                }
                else
                {
                    if (!string.IsNullOrEmpty(hidSignatureImage.Value.Trim()))
                        lstrSignImage = hidSignatureImage.Value.Trim();
                }

                if (!string.IsNullOrEmpty(hidRCSettingsId.Value.Trim()))
                    liRptId = Convert.ToInt32(hidRCSettingsId.Value.Trim());

                if (!string.IsNullOrEmpty(txtLeftMargin.Text.Trim()))
                    liLeftMargin = Convert.ToInt32(txtLeftMargin.Text.Trim());

                if (!string.IsNullOrEmpty(txtRightMargin.Text.Trim()))
                    liRightMargin = Convert.ToInt32(txtRightMargin.Text.Trim());

                if (!string.IsNullOrEmpty(txtTopMargin.Text.Trim()))
                    liTopMargin = Convert.ToInt32(txtTopMargin.Text.Trim());

                if (!string.IsNullOrEmpty(txtBottomMargin.Text.Trim()))
                    liBottomMargin = Convert.ToInt32(txtBottomMargin.Text.Trim());

                liRetVal = blLogic.InsertUpdateReceiptSettings(liRptId, txtCode.Text.Trim(), txtName.Text.Trim(), txtDescription.Text.Trim(),
                        liLeftMargin, liRightMargin, liTopMargin, liBottomMargin, ddlOuterBorder.SelectedValue.Trim(), ddlHeaderVisible.SelectedValue.Trim(),
                        ddlLogoVisible.SelectedValue.Trim(), ddlReceiptNoVisible.SelectedValue.Trim(), ddlSignVisible.SelectedValue.Trim(),
                        Convert.ToInt32(ddlFontFamily.SelectedValue.Trim()), Convert.ToInt32(txtFontSize.Text.Trim()), txtReportText.Text.Trim(),
                        lstrLogoImage, lstrSignImage, txtLabName.Text.Trim(), txtAddress.Text.Trim(), txtPhone.Text.Trim(), txtFooterText.Text.Trim());

                if (liRetVal > 0)
                {
                    Commonfunction.showMsg("Receipt settings saved successfully.", this, "receipt-settings.aspx");
                    hidRCSettingsId.Value = "";
                    txtCode.Text = "";
                    txtName.Text = "";
                    txtDescription.Text = "";
                    txtLeftMargin.Text = "";
                    txtRightMargin.Text = "";
                    txtTopMargin.Text = "";
                    txtBottomMargin.Text = "";
                    ddlOuterBorder.SelectedIndex = 0;
                    ddlHeaderVisible.SelectedIndex = 0;
                    ddlLogoVisible.SelectedIndex = 0;
                    ddlReceiptNoVisible.SelectedIndex = 0;
                    ddlSignVisible.SelectedIndex = 0;
                    ddlFontFamily.SelectedIndex = 0;
                    txtFontSize.Text = "";
                    txtReportText.Text = "";
                    hidLogoImage.Value = "";
                    hidSignatureImage.Value = "";
                    txtLabName.Text = "";
                    txtAddress.Text = "";
                    txtPhone.Text = "";
                    txtFooterText.Text = "";
                    GetAllReportSettings();
                }
                else
                    Commonfunction.showMsg("Error occurred while saving receipt settings data.", this);
            }
            else
                Commonfunction.showMsg("Maximum image size should be 2 MB only.", this);
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

    private void GetAllReportSettings()
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetAllReceiptSettings(0, 0, "dtCreationDate DESC", "");
            rptRecSettings.DataSource = lobjdtDetails;
            rptRecSettings.DataBind();

            if (lobjdtDetails.Rows.Count > 0)
            {
                rptRecSettings.Visible = true;
                trNoData.Visible = false;
            }
            else
            {
                rptRecSettings.Visible = false;
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

    private void GetRecSettingDetails(int fiRptSettingsId)
    {
        BusinessLogic bllogic = new BusinessLogic();
        DataTable lobjdtDetails = new DataTable();
        try
        {
            lobjdtDetails = bllogic.GetReceiptSettingsDetails(fiRptSettingsId);

            if (lobjdtDetails.Rows.Count > 0)
            {
                hidRCSettingsId.Value = Convert.ToString(fiRptSettingsId);
                txtCode.Text = Convert.ToString(lobjdtDetails.Rows[0]["rcSettingsCode"]);
                txtName.Text = Convert.ToString(lobjdtDetails.Rows[0]["rcSettingsName"]);
                txtDescription.Text = Convert.ToString(lobjdtDetails.Rows[0]["rcSettingsDesc"]);
                txtLeftMargin.Text = Convert.ToString(lobjdtDetails.Rows[0]["inLeftMargin"]);
                txtRightMargin.Text = Convert.ToString(lobjdtDetails.Rows[0]["inRightMargin"]);
                txtTopMargin.Text = Convert.ToString(lobjdtDetails.Rows[0]["inTopMargin"]);
                txtBottomMargin.Text = Convert.ToString(lobjdtDetails.Rows[0]["inBottomMargin"]);
                ddlOuterBorder.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["flgOuterBorder"]);
                ddlHeaderVisible.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["flgHeaderVisible"]);
                ddlLogoVisible.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["flgLogoVisible"]);
                ddlReceiptNoVisible.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["flgReceiptNoVisible"]);
                ddlSignVisible.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inSignVisible"]);
                ddlFontFamily.SelectedValue = Convert.ToString(lobjdtDetails.Rows[0]["inRCFontFamilyId"]);
                txtFontSize.Text = Convert.ToString(lobjdtDetails.Rows[0]["inRCFontSize"]);
                txtReportText.Text = Convert.ToString(lobjdtDetails.Rows[0]["stTextOnReport"]);
                hidLogoImage.Value = Convert.ToString(lobjdtDetails.Rows[0]["stLogo"]);
                hidSignatureImage.Value = Convert.ToString(lobjdtDetails.Rows[0]["stSignature"]);
                txtLabName.Text = Convert.ToString(lobjdtDetails.Rows[0]["stLabName"]);
                txtAddress.Text = Convert.ToString(lobjdtDetails.Rows[0]["stAddress"]);
                txtPhone.Text = Convert.ToString(lobjdtDetails.Rows[0]["stPhone"]);
                txtFooterText.Text = Convert.ToString(lobjdtDetails.Rows[0]["stFooterText"]);
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

    protected void rptRecSettings_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        int liRetVal = 0, liRoleFlag = 0;
        try
        {
            liRoleFlag = Convert.ToInt32(Session["RoleFlag"]);
            if (e.CommandName == "eEdit")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 44, "Edit"))
                    Commonfunction.showMsg("You are not authorised to edit a receipt setting.", this);
                else
                {
                    GetRecSettingDetails(Convert.ToInt32(e.CommandArgument));
                    lblAddMessage.Text = "";
                    pnlAddContent.Visible = true;
                    lblAddMessage.Visible = false;
                }
            }
            else if (e.CommandName == "eDelete")
            {
                if (liRoleFlag == 0 && !BusinessLogic.GetUserAccessRights(Convert.ToInt32(Session["RoleId"]), 44, "Delete"))
                    Commonfunction.showMsg("You are not authorised to delete a receipt setting.", this);
                else
                {
                    liRetVal = bllogic.DeleteReportSetting(Convert.ToInt32(e.CommandArgument));

                    if (liRetVal > 0)
                    {
                        hidRCSettingsId.Value = "";
                        txtCode.Text = "";
                        txtName.Text = "";
                        txtDescription.Text = "";
                        txtLeftMargin.Text = "";
                        txtRightMargin.Text = "";
                        txtTopMargin.Text = "";
                        txtBottomMargin.Text = "";
                        ddlOuterBorder.SelectedIndex = 0;
                        ddlHeaderVisible.SelectedIndex = 0;
                        ddlLogoVisible.SelectedIndex = 0;
                        ddlReceiptNoVisible.SelectedIndex = 0;
                        ddlSignVisible.SelectedIndex = 0;
                        ddlFontFamily.SelectedIndex = 0;
                        txtFontSize.Text = "";
                        txtReportText.Text = "";
                        hidLogoImage.Value = "";
                        hidSignatureImage.Value = "";
                        txtLabName.Text = "";
                        txtAddress.Text = "";
                        txtPhone.Text = "";
                        txtFooterText.Text = "";
                        Commonfunction.showMsg("Receipt setting deleted successfully.", this);
                        GetAllReportSettings();
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

    protected void btnDefaultSetting_Click(object sender, EventArgs e)
    {
        BusinessLogic bllogic = new BusinessLogic();
        try
        {
            foreach (RepeaterItem rptItem in rptRecSettings.Items)
            {
                RadioButton rbtnDefaultSetting = (RadioButton)rptItem.FindControl("rbtnDefaultSetting");
                Label lblRecSettingsId = (Label)rptItem.FindControl("lblRecSettingsId");

                if (rbtnDefaultSetting != null && lblRecSettingsId != null)
                {
                    if (rbtnDefaultSetting.Checked)
                    {
                        bllogic.UpdateDefaultReceiptSetting(Convert.ToInt32(lblRecSettingsId.Text.Trim()));
                        break;
                    }
                }
            }
            Commonfunction.showMsg("Default receipt setting set successfully.", this);
            GetAllReportSettings();
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