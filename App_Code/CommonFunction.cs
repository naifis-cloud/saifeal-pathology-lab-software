/**
 * Whitelist.cs
 *
 * Description : Holds the common functionality for the entire application
 *
 * Modification History
 * Modified By		Date			Description
 * --------------	-----------	    ------------------
 * Rehan Sunasra    01-June-2008	Initial Version
 * -------------------------------------------------------------------------------------------------------------------------
 * Stored Procedures used :- usp_getCountries,usp_getStates,usp_validateDomain.
 * -------------------------------------------------------------------------------------------------------------------------
 */

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using BusinessObjectHelper;
using System.Net.Mail;

namespace CommonFunctions
{
    public class Commonfunction
    {
        public Commonfunction()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Show Message functions
        // Show alert Message to User
        public static void showMsg(String fsMsg, Page foPage)
        {
            //   foPage.RegisterClientScriptBlock("Message" + (new Random().Next()).ToString(), "<script language='javascript'> alert('" + fsMsg.Replace("'", "\\'") + "');</script>"); 
            foPage.ClientScript.RegisterStartupScript(foPage.GetType(), "Message" + (new Random().Next()).ToString(), "alert('" + fsMsg.Replace("'", "\\'") + "');", true);
        }

        // Show alert Message to User and then Redirect page with client side script

        public static void showMsg(String fsMsg, Page foPage, String fsPath)
        {
            showMsg(fsMsg, foPage);
            redirect(fsPath, foPage);
        }

        // Show alert Message to User in case of Asynchronous Postback (AJAX Update Panel)
        public static void showAsyncMsg(String fsMsg, Page foPage)
        {
            ScriptManager.RegisterStartupScript(foPage, foPage.GetType(), "AsyncMessage" + (new Random().Next()).ToString(), "alert('" + fsMsg.Replace("'", "\\'") + "');", true);
        }

        // Show alert Message to User in case of Asynchronous Postback (AJAX Update Panel) and then Redirect page with client side script
        public static void showAsyncMsg(String fsMsg, Page foPage, String fsPath)
        {
            showAsyncMsg(fsMsg, foPage);
            redirectAsync(fsPath, foPage);
        }

        // Redirect page with client side script
        public static void redirect(String fsPath, Page foPage)
        {
            foPage.ClientScript.RegisterStartupScript(foPage.GetType(), "Redirect" + (new Random().Next()).ToString(), "window.location.replace('" + fsPath.Replace("'", "\\'") + "');", true);
        }

        // Redirect page with client side script
        public static void redirectAsync(String fsPath, Page foPage)
        {
            ScriptManager.RegisterStartupScript(foPage, foPage.GetType(), "Redirect" + (new Random().Next()).ToString(), "window.location.href = '" + fsPath.Replace("'", "\\'") + "';", true);
        }
        #endregion

        //for creating the folder dynamically
        #region "For Creating the Folder Dynamic"
        public static string CreateFolder(string path)
        {
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
                return path;
            }
            else
            {
                return path;
            }
        }
        #endregion

        #region "Get Absolutte Path"

        public static String absPath()
        {
            String lsPath = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            return lsPath;
        }

        #endregion

        #region UploadImageMethod
        public static string uploadImageFile(FileUpload foFileUpload, string fsLocation, int fiWidth, int fiHeight, string fsOriginalFielName)
        {
            ImageSettings foImgSet = new ImageSettings();
            string fsFileToUpload, fsExt;
            //System.IO.FileInfo file;
            //int fiCount, fiMin, fiMax;
            System.Drawing.Bitmap bmpOut;
            string fsFilename = foFileUpload.PostedFile.FileName;

            Commonfunction.CreateFolder(fsLocation);

            if (String.IsNullOrEmpty(fsOriginalFielName))
                fsOriginalFielName = foFileUpload.FileName.Substring(0, foFileUpload.FileName.IndexOf('.'));

            try
            {
                fsExt = System.IO.Path.GetExtension(fsFilename);
                fsFileToUpload = fsOriginalFielName + fsExt.ToString();

                foFileUpload.PostedFile.SaveAs(fsLocation + "Dummy_" + fsFileToUpload);
                System.IO.FileInfo fileD;
                fileD = new System.IO.FileInfo(fsLocation + "Dummy_" + fsFileToUpload);
                bmpOut = foImgSet.CreateThumbnail(fsLocation + "Dummy_" + fsFileToUpload, fiWidth, fiHeight);
                bmpOut.Save(fsLocation + fsFileToUpload, GetImageFormat(fsExt.ToString()));
                bmpOut.Dispose();
                if (fileD.Exists) { fileD.Delete(); }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return fsFileToUpload;
        }

        //return image format for the uploaded images in which they will be converted.
        private static ImageFormat GetImageFormat(string fsExtension)
        {
            ImageFormat loImgFormat;
            fsExtension = fsExtension.ToLower();
            if (fsExtension.Contains("."))
            {
                fsExtension = fsExtension.Replace(".", "");
            }
            switch (fsExtension)
            {
                case "gif":
                    loImgFormat = ImageFormat.Jpeg;
                    break;
                case "png":
                    loImgFormat = ImageFormat.Png;
                    break;
                case "jpeg":
                    loImgFormat = ImageFormat.Jpeg;
                    break;
                case "jpg":
                    loImgFormat = ImageFormat.Jpeg;
                    break;
                case "bmp":
                    loImgFormat = ImageFormat.Bmp;
                    break;
                default:
                    loImgFormat = ImageFormat.Jpeg;
                    break;
            }
            return loImgFormat;
        }
        #endregion

        //for User login
        #region "For User login"
        public static void CheckUserLogIn()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserId"]))
                || string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["RoleId"])))
            {
                object qs = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.RawUrl);
                HttpContext.Current.Response.Redirect("~/Default.aspx?url=" + qs);
            }
        }
        #endregion

        // Send mail function 
        #region "Send Email"
        public static void SendMail(String sTo, String sFrom, String sSubject, String sBody)
        {
            SmtpClient loSmtpClient;
            MailMessage loMailMessage;
            try
            {
                String lsSmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
                String lsMailUser = ConfigurationManager.AppSettings["MailUser"];
                String lsMailUserPassword = ConfigurationManager.AppSettings["MailUserPassword"];

                if (string.IsNullOrEmpty(sFrom))
                    sFrom = ConfigurationManager.AppSettings["Info"];

                loSmtpClient = new System.Net.Mail.SmtpClient(lsSmtpServer, 25);
                loMailMessage = new System.Net.Mail.MailMessage(sFrom, sTo, sSubject, sBody);
                loMailMessage.IsBodyHtml = true;
                //loMailMessage.Priority = MailPriority.High;
                loSmtpClient.Credentials = new System.Net.NetworkCredential(lsMailUser, lsMailUserPassword);
                loSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //loSmtpClient.EnableSsl = true;
                loMailMessage.Bcc.Add("rsunasra@gmail.com");
                loSmtpClient.Send(loMailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                loSmtpClient = null;
                loMailMessage = null;
            }
        }
        #endregion

        //Image resising Function
        #region "Image Resize"
        public Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);

                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;

                int lnNewWidth = 0;

                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                {
                    return loBMP;
                }
                if (loBMP.Width > loBMP.Height)
                {

                    lnRatio = (decimal)lnWidth / loBMP.Width;

                    lnNewWidth = lnWidth;

                    decimal lnTemp = loBMP.Height * lnRatio;

                    lnNewHeight = (int)lnTemp;

                }
                else
                {

                    lnRatio = (decimal)lnHeight / loBMP.Height;

                    lnNewHeight = lnHeight;

                    decimal lnTemp = loBMP.Width * lnRatio;

                    lnNewWidth = (int)lnTemp;
                }
                // System.Drawing.Image imgOut =

                //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,

                //                              null,IntPtr.Zero);



                // *** This code creates cleaner (though bigger) thumbnails and properly

                // *** and handles GIF files better by generating a white background for

                // *** transparent images (as opposed to black)

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);

                Graphics g = Graphics.FromImage(bmpOut);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;


                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);

                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;

        }
        #endregion

        public static string emailSignature()
        {
            return "<p>Best Regards,<br/><a style='font-size:12px;font-family:Arial,Helvetica,sans-serif;padding-bottom:10px;' href = '" + ConfigurationManager.AppSettings["SiteUrl"] + "'>Sierra Exports </a> Team<br /></p>";
        }

        /// <summary>
        /// Connection String for the database
        /// </summary>
        public static string getConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"].ToString();
        }

        /// <summary>
        /// To fill all the states of India
        /// </summary>
        public static void GetAllStates(DropDownList foDropDownList)
        {
            DataTable lodtCountries = new DataTable();

            lodtCountries = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetStates").Tables[0];
            foDropDownList.Items.Clear();
            foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtCountries.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stState"]), Convert.ToString(lodtRow["inStateId"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtCountries = null;
            }
        }

        /// <summary>
        /// To fill all the cities of a particular state ///
        /// </summary>
        public static void GetCities(DropDownList foDropDownList, int fiStateId)
        {
            DataTable lodtStates = new DataTable();

            lodtStates = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetCities", fiStateId).Tables[0];
            foDropDownList.Items.Clear();
            foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtStates.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stCity"]), Convert.ToString(lodtRow["inCityId"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtStates = null;
            }
        }


        /// To fill all the states of India
        /// </summary>
        public static void GetAllTestStatus(DropDownList foDropDownList)
        {
            DataTable lodtStatus = new DataTable();

            lodtStatus = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetAllTestStatus").Tables[0];
            foDropDownList.Items.Clear();
            foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtStatus.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stStatus"]), Convert.ToString(lodtRow["inTestStatusId"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtStatus = null;
            }
        }

        /// <summary>
        /// To fill all the genders from the lookup table///
        /// </summary>
        public static void GetAllGenders(DropDownList foDropDownList)
        {
            DataTable lodtGender = new DataTable();

            lodtGender = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetAllGenders").Tables[0];
            foDropDownList.Items.Clear();
            foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtGender.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stGender"]), Convert.ToString(lodtRow["stGenderCode"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtGender = null;
            }
        }

        /// <summary>
        /// To fill all the payment types from the lookup table///
        /// </summary>
        public static void GetAllPaymentTypes(DropDownList foDropDownList)
        {
            DataTable lodtGender = new DataTable();

            lodtGender = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetAllPaymentTypes").Tables[0];
            foDropDownList.Items.Clear();
            foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtGender.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stPaymentName"]), Convert.ToString(lodtRow["stPaymentCode"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtGender = null;
            }
        }

        /// <summary>
        /// To fill all Titles from the lookup table///
        /// </summary>
        public static void GetAllTitles(DropDownList foDropDownList)
        {
            DataTable lodtGender = new DataTable();

            lodtGender = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetAllTitles").Tables[0];
            foDropDownList.Items.Clear();
            //foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtGender.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stTitle"]), Convert.ToString(lodtRow["stTitle"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtGender = null;
            }
        }

        /// <summary>
        /// To Generate the Module Code///
        /// </summary>
        public static string GenerateModuleCode(string fstrShortCode)
        {
            string lstrModuleCode = "";
            try
            {
                lstrModuleCode = Convert.ToString(SqlHelper.ExecuteScalar(getConnectionString(), "usp_GenerateModuleCode", fstrShortCode));    
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            return lstrModuleCode;
        }

        /// <summary>
        /// To fill all font families from the lookup table///
        /// </summary>
        public static void GetAllFonts(DropDownList foDropDownList)
        {
            DataTable lodtGender = new DataTable();

            lodtGender = SqlHelper.ExecuteDataset(getConnectionString(), "usp_GetAllFonts").Tables[0];
            foDropDownList.Items.Clear();
            //foDropDownList.Items.Insert(0, new ListItem("--Select--", ""));
            try
            {
                foreach (DataRow lodtRow in lodtGender.Rows)
                {
                    foDropDownList.Items.Add(new ListItem(Convert.ToString(lodtRow["stFontFamily"]), Convert.ToString(lodtRow["inFontFamilyId"])));
                }
            }
            catch (Exception foexception)
            {
                throw new Exception(foexception.Message);
            }
            finally
            {
                lodtGender = null;
            }
        }
    }
}
