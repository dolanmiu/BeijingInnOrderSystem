﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Beijing_Inn_Order_System.Helper_Classes
{
    public static class Helper
    {
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static string GetAppDataFile(string filename)
        {
            string fileDir;

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            fileDir = Path.Combine(folder, "Beijing Inn");
            if (!Directory.Exists(fileDir))
                Directory.CreateDirectory(fileDir);
            string fileLocation = fileDir + "\\" + filename;
            return fileLocation;
        }

        public static string EllipsisTruncate(this string value, int maxLength)
        {
            //return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
            if (value.Length <= maxLength && maxLength > 3) 
            {
                value.Substring(0, maxLength - 3);
                value += "...";
            }
            return value;
        }
    }
}
