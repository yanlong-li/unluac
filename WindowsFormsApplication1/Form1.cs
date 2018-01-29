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
using unluac;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (sourcePatch.TextLength > 0)
            {
                forFolder(sourcePatch.Text);
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // string foldPath = dialog.SelectedPath;
                    this.sourcePatch.Text = dialog.SelectedPath;
                    forFolder(sourcePatch.Text);
                    //MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void forFolder(string folderFullName )
        {
            DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
            FileSystemInfo[] fsinfos = TheFolder.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                if (fsinfo is DirectoryInfo)     //判断是否为文件夹
                {
                    listBox1.Items.Add(fsinfo.Name);
                    forFolder(fsinfo.FullName);//递归调用
                }
                else
                {
                    listBox2.Items.Add(fsinfo.Name);
                    //输出文件的全部路径
                    if (fsinfo.Extension == ".alb") {
                        unlua(fsinfo.FullName, "out\\"+fsinfo.FullName.Remove(0, sourcePatch.TextLength+1).Replace(fsinfo.Name,"")+"\\",fsinfo.Name.Replace(".alb",""));
                        
                    }
                }
            }
        


                //
        }
        private void unlua(string fileName,string outFolder,string name)
        {


            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);
            try
            {
                Main.decompile(fileName, outFolder + name + ".alb");
            }
            catch
            {


            }
            //Main.decompile(fileName, outFolder+name+".lua");
        }
    }
}
