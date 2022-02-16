using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public abstract class ItemManager
    {
        public string Path { get; set; }
        public ListView ListViewObject { get; set;}
        public abstract void Create();
        public abstract void Read();
        public abstract void Update(string newName);
        public abstract void Delete();
        public abstract void Paste(string targetPath, string itemName, bool removeItem);

    }
}
