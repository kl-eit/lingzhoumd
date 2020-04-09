//using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LazZiya.ImageResize;

namespace Ling.Common
{
    public class CommonHelper
    {
        // THIS IS USED WHEN CONVERTING DATA GETTING FROM DATABASE 
        public static T FromDB<T>(object value)
        {
            return value == DBNull.Value ? default(T) : (T)value;
        }

        // THIS IS USED WHEN WE HAVE PASS PERAMETER IN DB
        public static object ToDB<T>(T value)
        {
            return value == null ? (object)DBNull.Value : value;
        }

        // THIS IS USED WHEN WE HAVE CONVERT VARIABLE OR OBJECT TO SPECIFIC TYPE
        public static T ConvertTo<T>(object value)
        {
            return value == null ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        // THIS IS USED WHEN WE HAVE RESIZE IMAGE FOR SPECIFIC SIZE
        public static void ResizeImage(string sourcePath, string destinationPath, string genratedFilename, string imageVersions)
        {
            var img = Image.FromFile(sourcePath);

            var splitString = "";
            for (int i = 0; i < imageVersions.Split(',').Length; i++)
            {
                splitString = imageVersions.Split(',')[i];
                if (splitString == Constants.THUMBNAILIMAGERESIZER)
                {
                    var scaleImage = ImageResize.Scale(img, 100, 100);
                    scaleImage.SaveAs(destinationPath + Constants.THUMBNAILIMAGERESIZER + genratedFilename);
                }

                if (splitString == Constants.MEDIUMIMAGERESIZER)
                {
                    var scaleImage = ImageResize.Scale(img, 200, 200);
                    scaleImage.SaveAs(destinationPath + Constants.MEDIUMIMAGERESIZER + genratedFilename);
                }

                //if (splitString == Constants.MEDIUMROOMIMAGERESIZER)
                //{
                //    var scaleImage = ImageResize.Scale(img, 368, 226);
                //    scaleImage.SaveAs(destinationPath + Constants.MEDIUMROOMIMAGERESIZER + genratedFilename);
                //}

                //if (splitString == Constants.MEDIUMEVENTIMAGERESIZER)
                //{
                //    var scaleImage = ImageResize.Scale(img, 368, 226);
                //    scaleImage.SaveAs(destinationPath + Constants.MEDIUMEVENTIMAGERESIZER + genratedFilename);
                //}

                if (splitString == Constants.LARGEIMAGERESIZER)
                {
                    var scaleImage = ImageResize.Scale(img, 375, 500);
                    scaleImage.SaveAs(destinationPath + Constants.LARGEIMAGERESIZER + genratedFilename);
                }

                if (splitString == Constants.SMALLIMAGERESIZER)
                {
                    var scaleImage = ImageResize.Scale(img, 64, 64);
                    scaleImage.SaveAs(destinationPath + Constants.SMALLIMAGERESIZER + genratedFilename);
                }

                if (splitString == Constants.SMALLHOMESLIDERIMAGERESIZER)
                {
                    var scaleImage = ImageResize.Scale(img, 500, 175);
                    scaleImage.SaveAs(destinationPath + Constants.SMALLHOMESLIDERIMAGERESIZER + genratedFilename);
                }
            }
        }
    }
}
