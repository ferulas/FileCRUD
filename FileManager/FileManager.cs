using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class FileManager : ItemManager
    {
        public override void Create()
        {
            try
            {
                if (!File.Exists(Path))
                {
                    File.Create(Path);
                }
                else
                {                    
                    throw new Exception($"The file {Path.Split('/').LastOrDefault()} already exists.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete()
        {
            try
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
                else
                {
                    throw new Exception($"The file {Path.Split('/').LastOrDefault()} doesn't exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while the folder was deleted: " + ex.Message);
            }
        }

        public override void Paste(string targetPath, string itemName, bool removeItem)
        {
            try
            {
                var fileName = System.IO.Path.GetFileName(Path);
                var destFile = System.IO.Path.Combine(targetPath, fileName);
                File.Copy(Path, destFile, true);

                if (removeItem)
                {
                    Delete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Read()
        {
            string tempFilePath = "";
            FileAttributes fileAttr;

            try
            {
                tempFilePath = Path;
                fileAttr = File.GetAttributes(tempFilePath);
                Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Update(string newName)
        {
            try
            {
                if (File.Exists(Path))
                {
                    string newPath = Path.Replace(Path.Split('/').LastOrDefault(), newName + ".txt");
                    File.Move(Path, newPath);
                }
                else
                {
                    throw new Exception($"The folder {Path} doesn't exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while the folder was updated: " + ex.Message);
            }
        }
    }
}
