using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Android.Content;

namespace TD
{
    public static class Utility
    {

        public static void LoadData(ref object someData, string aRelativePath)
        {
                //string str = File.ReadAllText(aRelativePath);
                //string tempStr = "";
                //for (int i = 0; i < str.Length; ++i)
                //{
                //    if ((str[i] != '\0' && i < str.Length - 1) || i == str.Length - 1)
                //    {
                //        tempStr += str[i];
                //    }
                //}
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ObjectCreationHandling = ObjectCreationHandling.Auto;
                settings.TypeNameHandling = TypeNameHandling.Auto;

            // string fileData = System.IO.File.ReadAllText(aRelativePath);

               // string response;
              //  StreamReader strm = new StreamReader(Assets.Open("Levels/Level/myjson.json"));
             //   response = strm.ReadToEnd();

           // someData = JsonConvert.DeserializeObject(response, someData.GetType(), settings);
        }
        public static void LoadLevel(ref object someData, int aLevel, Context c)
        {
            //string str = File.ReadAllText(aRelativePath);
            //string tempStr = "";
            //for (int i = 0; i < str.Length; ++i)
            //{
            //    if ((str[i] != '\0' && i < str.Length - 1) || i == str.Length - 1)
            //    {
            //        tempStr += str[i];
            //    }
            //}
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ObjectCreationHandling = ObjectCreationHandling.Auto;
            settings.TypeNameHandling = TypeNameHandling.Auto;

            // string fileData = System.IO.File.ReadAllText(aRelativePath);


            string pathToFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fileName = Path.Combine("Levels", "Level" + aLevel.ToString() + ".json");
            string filePath = Path.Combine(pathToFolder, fileName);

            string response;
            StreamReader strm = new StreamReader(c.Assets.Open(fileName));
            response = strm.ReadToEnd();

            someData = JsonConvert.DeserializeObject(response, someData.GetType(), settings);
        }
        public static void SaveData(object someData, string aRelativePath)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.StringEscapeHandling = StringEscapeHandling.Default;

            settings.TypeNameHandling = TypeNameHandling.Auto;


            string str = JsonConvert.SerializeObject(someData, settings);

            System.IO.File.WriteAllText(aRelativePath, str);
        }

        public static bool LoadFile(string aTitle, ref string aFile, string aDirectory, string aFilter)
        {
            //myOpenFile.Reset();

            //myOpenFile.CheckFileExists = true;
            //myOpenFile.CheckPathExists = true;
            //myOpenFile.RestoreDirectory = false;
            //myOpenFile.Title = aTitle;
            //myOpenFile.SupportMultiDottedExtensions = true;
            //myOpenFile.Filter = aFilter;
            //myOpenFile.InitialDirectory = aDirectory;
            //myOpenFile.Multiselect = false;
            //if (myOpenFile.ShowDialog() == DialogResult.OK)
            //{
            //    aFile = myOpenFile.FileName;
            //    return true;
            //}
            //return false;

            return true;
        }

        public static bool SaveFile(string aTitle, string aRecommendedFileName, ref string aFile, string aDirectory = "")
        {
            //mySaveFile.Reset();

            //mySaveFile.CheckFileExists = false;
            //mySaveFile.CheckPathExists = true;
            //mySaveFile.Title = aTitle;
            //mySaveFile.SupportMultiDottedExtensions = false;
            //mySaveFile.DefaultExt = ".json";
            //mySaveFile.Filter = "JSON (*.json)|*.json";
            //mySaveFile.InitialDirectory = aDirectory;

            //mySaveFile.FileName = aRecommendedFileName;

            //if(mySaveFile.ShowDialog() == DialogResult.OK)
            //{
            //    aFile = mySaveFile.FileName;
            //    return true;
            //}
            //return false;

            return true;
        }

        public static bool IsSubPathOf(this string path, string baseDirPath)
        {
            return path.ToUpper().StartsWith(baseDirPath.ToUpper());
        }
    }
}
