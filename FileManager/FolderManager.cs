using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class FolderManager : ItemManager
    {
        public override void Create()
        {
            try
            {               
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                else
                {
                    throw new Exception($"The folder {Path} already exists.");
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
                if (Directory.Exists(Path))
                {
                    foreach (string s in System.IO.Directory.GetFiles(Path))
                    {
                        ItemManager item = new FileManager();
                        item.Path = s;
                        item.Delete();
                    }

                    foreach (string s in System.IO.Directory.GetDirectories(Path))
                    {
                        ItemManager item = new FolderManager();
                        item.Path = s;
                        item.Delete();
                    }

                    Directory.Delete(Path);
                }
                else
                {
                    throw new Exception($"The folder {Path} doesn't exists");
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
                var targetDirectory = targetPath + "/" + itemName;
                Directory.CreateDirectory(targetDirectory);

                string[] files = System.IO.Directory.GetFiles(Path);
                string[] directories = System.IO.Directory.GetDirectories(Path);


                foreach (string sourceFile in files)
                {                    
                    var fileName = System.IO.Path.GetFileName(sourceFile);
                    var destFile = System.IO.Path.Combine(targetDirectory, fileName);
                    File.Copy(sourceFile, destFile, true);
                }

                foreach (string sourceDirectories in directories)
                {
                    ItemManager item = new FolderManager();
                    item.Path = sourceDirectories;
                    item.Paste(targetPath, sourceDirectories.Split('/').LastOrDefault(), removeItem);
                }

                if (removeItem)
                {
                    Delete();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void Read()
        {
            DirectoryInfo fileList;
            FileAttributes fileAttr;

            try
            {
                fileAttr = File.GetAttributes(Path);

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(Path);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();
                    ListViewObject.Items.Clear();
                    foreach (FileInfo file in files)
                    {
                        if (file.Extension.ToUpper() == ".TXT")
                        {
                            ListViewObject.Items.Add(file.Name, 0);
                        }
                    }
                    dirs.ToList<DirectoryInfo>().ForEach(dir => ListViewObject.Items.Add(dir.Name, 1));
                }
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
                if (Directory.Exists(Path))
                {
                    string newPath = Path.Replace(Path.Split('/').LastOrDefault(), newName);
                    Directory.Move(Path, newPath);
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
