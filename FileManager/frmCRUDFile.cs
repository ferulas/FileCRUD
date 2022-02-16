using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class frmCRUDFile : Form
    {
        private string filePath = "C:/";
        private bool isFile = false;
        private string currentSelectedItemName = "";
        private string fullName = "";
        private ItemManager item;
        private string targetPath = "";
        private string sourcePath = "";
        private bool removeItem = false;


        public frmCRUDFile()
        {
            InitializeComponent();
        }

        private void frmCRUDFile_Load(object sender, EventArgs e)
        {
            txtPath.Text = filePath;
            btnBack.Enabled = false;
            LoadFilesAndDirectories();
            
        }

        public void LoadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            FileAttributes fileAttr;

            try
            {
                if (isFile)
                {
                    fileAttr = File.GetAttributes(filePath);
                    fullName = new FileInfo(filePath).Name;
                    Process.Start(filePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);
                }

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();

                    lstFiles.Items.Clear();
                    foreach (FileInfo file in files)
                    {
                        if (file.Extension.ToUpper() == ".TXT")
                        {
                            lstFiles.Items.Add(file.Name, 0);
                        }
                    }
                    dirs.ToList<DirectoryInfo>().ForEach(dir => lstFiles.Items.Add(dir.Name, 1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadButtonAction()
        {
            filePath = txtPath.Text;
            btnBack.Enabled = filePath.Split('/').Length > 2;
            LoadFilesAndDirectories();
            isFile = false;
        }      

        public void GoBack()
        {
            try
            {
                string path = txtPath.Text;
                path = path.Substring(0, path.LastIndexOf("/"));
                isFile = false;
                txtPath.Text = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            LoadButtonAction();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GoBack();
            LoadButtonAction();
        }

        private void lstFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentSelectedItemName = e.Item.Text;

            FileAttributes fileAttr = File.GetAttributes($"{filePath}/{currentSelectedItemName}");

            isFile = (fileAttr & FileAttributes.Directory) != FileAttributes.Directory;
            txtPath.Text = $"{filePath}/{currentSelectedItemName}";
        }

        private void lstFiles_DoubleClick_1(object sender, EventArgs e)
        {
            LoadButtonAction();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
           CreateFolder();
        }
        
        public void CreateFolder()
        {
            item = new FolderManager();
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    item.Path = $"{filePath}/{txtName.Text}";
                    item.ListViewObject = lstFiles;
                    item.Create();
                    txtName.Text = "";
                    LoadButtonAction();
                }
                else
                {
                    throw new Exception("Introduce the folder name in Name field");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            item = new FileManager();
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    item.Path = $"{filePath}/{txtName.Text}.txt";
                    item.ListViewObject = lstFiles;
                    item.Create();
                    LoadButtonAction();
                }
                else
                {
                    throw new Exception("Introduce the file name in Name field");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            item = new FileManager();
            try
            {
                item.Path = txtPath.Text;
                item.ListViewObject = lstFiles;
                item.Delete();
                GoBack();
                LoadButtonAction();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            item = new FolderManager();
            try
            {
                item.Path = txtPath.Text;
                item.ListViewObject = lstFiles;
                item.Delete();
                GoBack();
                LoadButtonAction();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            item = new FolderManager();
            try
            {
                item.Path = txtPath.Text;
                item.ListViewObject = lstFiles;
                item.Read();
                btnBack.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            item = new FolderManager();
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    item.Path = txtPath.Text;
                    item.ListViewObject = lstFiles;
                    item.Update(txtName.Text);
                    GoBack();
                    LoadButtonAction();
                }
                else
                {
                    throw new Exception("Introduce the folder name in Name field");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            item = new FileManager();
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    item.Path = txtPath.Text;
                    item.ListViewObject = lstFiles;
                    item.Update(txtName.Text);
                    GoBack();
                    LoadButtonAction();
                }
                else
                {
                    throw new Exception("Introduce the file name in Name field");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void readToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            item = new FileManager();
            try
            {
                item.Path = txtPath.Text;
                item.ListViewObject = lstFiles;
                item.Read();
                btnBack.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!isFile)
            {
                sourcePath = txtPath.Text;
                removeItem = false;
            }
            else
            {
                MessageBox.Show("You must to select a Folder");
            }
           
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isFile)
            {
                sourcePath = txtPath.Text;
                removeItem = false;
            }
            else
            {
                MessageBox.Show("You must to select a file");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            item = new FolderManager();
            targetPath = txtPath.Text;
            try
            {
                if (!isFile)
                {
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        item.Path =sourcePath;
                        item.ListViewObject = lstFiles;
                        item.Paste(targetPath, sourcePath.Split('/').LastOrDefault(), removeItem); 
                        LoadButtonAction();
                    }
                    else
                    {
                        throw new Exception("You must to select a target path");
                    }
                }
                else
                {
                    throw new Exception("You must to select a Folder");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                removeItem = false;
            }
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isFile)
            {
                sourcePath = txtPath.Text;
                removeItem = true;
            }
            else
            {
                MessageBox.Show("You must to select a Folder");
            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            item = new FileManager();
            targetPath = txtPath.Text;
            try
            {
                {
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        item.Path = sourcePath;
                        item.ListViewObject = lstFiles;
                        item.Paste(targetPath, sourcePath.Split('/').LastOrDefault(), removeItem);
                        LoadButtonAction();
                    }
                    else
                    {
                        throw new Exception("You must to select a target path");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                removeItem = false;
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFile)
            {
                sourcePath = txtPath.Text;
                removeItem = true;
            }
            else
            {
                MessageBox.Show("You must to select a file");
            }
        }
    }
}
