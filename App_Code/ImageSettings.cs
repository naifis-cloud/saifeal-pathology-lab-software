/*
=============================================================================================
Purpose			: for resize the image
Created By		: Vinod Naidu
Created On		: 23/12/2008
Modified By		:
Modified On		:
============================================================================================
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
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;


namespace CommonFunctions
{
    public class ImageSettings
    {
        public ImageSettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {

            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                //ImageFormat loFormat = loBMP.RawFormat;
                
                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;
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
                Size loNewSize = new Size(lnNewWidth, lnNewHeight);
                // System.Drawing.Image imgOut =
                //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
                //                              null,IntPtr.Zero);
                // *** This code creates cleaner (though bigger) thumbnails and properly
                // *** and handles GIF files better by generating a white background for
                // *** transparent images (as opposed to black)

                PixelFormat loFormat = loBMP.PixelFormat;
                if (loFormat.ToString().Contains("Indexed"))
                    loFormat = PixelFormat.Format24bppRgb;
                
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight, loFormat);
                Graphics g = Graphics.FromImage(bmpOut);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.Clear(Color.White);
                //g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                //g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                g.DrawImage(loBMP, new Rectangle(new Point(0, 0), loNewSize));
                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }
        public Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight, bool fbIsForBigger)
        {

            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                //ImageFormat loFormat = loBMP.RawFormat;
                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (!fbIsForBigger)
                {
                    if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
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
                Size loNewSize = new Size(lnNewWidth, lnNewHeight);
                // System.Drawing.Image imgOut =
                //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
                //                              null,IntPtr.Zero);
                // *** This code creates cleaner (though bigger) thumbnails and properly
                // *** and handles GIF files better by generating a white background for
                // *** transparent images (as opposed to black)

                PixelFormat loFormat = loBMP.PixelFormat;
                if (loFormat.ToString().Contains("Indexed"))
                    loFormat = PixelFormat.Format24bppRgb;

                bmpOut = new Bitmap(lnNewWidth-1, lnNewHeight-1);
                Graphics g = Graphics.FromImage(bmpOut);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                //g.FillRectangle(Brushes.White, 0, 0, lnNewWidth-1, lnNewHeight-1);
                //g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, new Rectangle(new Point(0, 0), loNewSize));
                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }
    }
}