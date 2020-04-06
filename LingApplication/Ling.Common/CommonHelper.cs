using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public static T ConvertTo<T>(object value)
        {
            return value == null ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        public static void ImageResize(string sourcePath, string destinationPath, string genratedFilename, string imageVersions)
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the versions to generate
            versions.Add(Constants.THUMBNAILIMAGERESIZER, "maxwidth=100&maxheight=100&crop=auto&format=jpg"); //Crop to square thumbnail
            versions.Add(Constants.LARGEIMAGERESIZER, "maxwidth=375&maxheight=500&format=jpg&mode=max&quality=50"); //Fit inside 1900x1200 area
            versions.Add(Constants.MEDIUMIMAGERESIZER, "width=200&height=200&crop=auto&format=jpg");
            versions.Add(Constants.MEDIUMROOMIMAGERESIZER, "width=368&height=226&crop=auto&format=jpg"); //Fit inside 368x226 area
            versions.Add(Constants.MEDIUMEVENTIMAGERESIZER, "width=368&height=226&crop=auto&format=jpg"); //Fit inside 368x226 area
            versions.Add(Constants.SMALLIMAGERESIZER, "width=64&height=64&crop=auto&format=jpg"); //Fit inside 1900x1200 area
            versions.Add(Constants.SMALLHOMESLIDERIMAGERESIZER, "width=500&height=175&format=jpg"); //Fit inside 500x175 area

            var splitString = "";
            for (int i = 0; i < imageVersions.Split(',').Length; i++)
            {
                splitString = imageVersions.Split(',')[i];
                if (splitString == Constants.THUMBNAILIMAGERESIZER)
                {
                    FileInfo fiThumbnail = new FileInfo(sourcePath);
                    FileStream fsThumbnail = fiThumbnail.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsThumbnail, destinationPath + Constants.THUMBNAILIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.THUMBNAILIMAGERESIZER]));
                }

                if (splitString == Constants.MEDIUMIMAGERESIZER)
                {
                    FileInfo fiThumbnail = new FileInfo(sourcePath);
                    FileStream fsThumbnail = fiThumbnail.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsThumbnail, destinationPath + Constants.MEDIUMIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.MEDIUMIMAGERESIZER]));
                }

                if (splitString == Constants.MEDIUMROOMIMAGERESIZER)
                {
                    FileInfo fiThumbnail = new FileInfo(sourcePath);
                    FileStream fsThumbnail = fiThumbnail.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsThumbnail, destinationPath + Constants.MEDIUMROOMIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.MEDIUMROOMIMAGERESIZER]));
                }

                if (splitString == Constants.MEDIUMEVENTIMAGERESIZER)
                {
                    FileInfo fiThumbnail = new FileInfo(sourcePath);
                    FileStream fsThumbnail = fiThumbnail.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsThumbnail, destinationPath + Constants.MEDIUMEVENTIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.MEDIUMEVENTIMAGERESIZER]));
                }

                if (splitString == Constants.LARGEIMAGERESIZER)
                {
                    FileInfo fiLarge = new FileInfo(sourcePath);
                    FileStream fsLarge = fiLarge.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsLarge, destinationPath + "/" + Constants.LARGEIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.LARGEIMAGERESIZER]));
                }

                if (splitString == Constants.SMALLIMAGERESIZER)
                {
                    FileInfo fiSmall = new FileInfo(sourcePath);
                    FileStream fsSmall = fiSmall.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsSmall, destinationPath + Constants.SMALLIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.SMALLIMAGERESIZER]));
                }

                if (splitString == Constants.SMALLHOMESLIDERIMAGERESIZER)
                {
                    FileInfo fiSmall = new FileInfo(sourcePath);
                    FileStream fsSmall = fiSmall.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    //ImageBuilder.Current.Build(fsSmall, destinationPath + "/" + Constants.SMALLHOMESLIDERIMAGERESIZER + genratedFilename, new ResizeSettings(versions[Constants.SMALLHOMESLIDERIMAGERESIZER]));
                }
            }
        }
    }
}
