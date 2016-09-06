using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace TDMapEditor
{
    public static class Utility
    {
        private static OpenFileDialog myOpenFile = new OpenFileDialog();
        private static SaveFileDialog mySaveFile = new SaveFileDialog();
        private static FolderBrowserDialog mySelectFolder = new FolderBrowserDialog();

        public static void SelectFolder(ref object someData, string aRelativePath)
        {
            //if(Directory.Exists(Path.GetFullPath(aRelativePath)) == true)
            //{
            //    if(mySelectFolder.ShowDialog() == DialogResult.OK)
            //    {
            //        (string)someData = mySelectFolder.SelectedPath;
            //    }
            //}
        }

        public static void LoadData(ref object someData, string aRelativePath)
        {
            if (File.Exists(aRelativePath) == true)
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

                string fileData = System.IO.File.ReadAllText(aRelativePath);
                someData = JsonConvert.DeserializeObject(fileData, someData.GetType(), settings);
            }
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
            myOpenFile.Reset();

            myOpenFile.CheckFileExists = true;
            myOpenFile.CheckPathExists = true;
            myOpenFile.RestoreDirectory = false;
            myOpenFile.Title = aTitle;
            myOpenFile.SupportMultiDottedExtensions = true;
            myOpenFile.Filter = aFilter;
            myOpenFile.InitialDirectory = aDirectory;
            myOpenFile.Multiselect = false;
            if (myOpenFile.ShowDialog() == DialogResult.OK)
            {
                aFile = myOpenFile.FileName;
                return true;
            }
            return false;
        }

        public static bool SaveFile(string aTitle, string aRecommendedFileName, ref string aFile, string aDirectory = "")
        {
            mySaveFile.Reset();

            mySaveFile.CheckFileExists = false;
            mySaveFile.CheckPathExists = true;
            mySaveFile.Title = aTitle;
            mySaveFile.SupportMultiDottedExtensions = false;
            mySaveFile.DefaultExt = ".json";
            mySaveFile.Filter = "JSON (*.json)|*.json";
            mySaveFile.InitialDirectory = aDirectory;

            mySaveFile.FileName = aRecommendedFileName;

            if (mySaveFile.ShowDialog() == DialogResult.OK)
            {
                aFile = mySaveFile.FileName;
                return true;
            }
            return false;
        }

        public static bool IsSubPathOf(this string path, string baseDirPath)
        {
            return path.ToUpper().StartsWith(baseDirPath.ToUpper());
        }
    }
}
