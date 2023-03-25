using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace YuZongZhiWindowsERSFormsApp1
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;
        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            InitalizeListControl();
        }

        private void PopulateTreeView()
        {
            throw new NotImplementedException();
        }

        private void EmployeeRecordsForm_Load(object sender, EventArgs e)
        {

        }

        private void statusBar1_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {

        }
        private void PopulateTreeView(TreeNode tvRootNode)
        {
            statusBarPanel1.Tag = "Refreshing Employee Code. Please wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);


            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            xmlTextReader reader = new xmlTextReader("F:\\86184\\source\\repos\\YuZongZhiWindowsERSFormsApp1\\YuZongZhiWindowsERSFormsApp1\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAtttibute("Id");
                        string strval = reader.Value;

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();
                        }
                        //create a child node
                        TreeNode EcodeNode = new TreeNode(strval);
                        //add the node
                        nodeCollection.Add(EcodeNode);
                    }
                }
                statusBarPanel1.Text = "Click on an employee code to see their record.";
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//end
        protected void InitalizeListControl()
        {
            listView1.Clear();
            listView1.Columns.Add("Employee Name", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Gread", 105, HorizontalAlignment.Left);
            listView1.Columns.Add("Salary", 105, HorizontalAlignment.Left);
        }
        protected void PopulateListview(TreeNode crrNode)
        {
            InitalizeListControl();
            xmlTextReader reader = new xmlTextReader("F:\\86184\\source\\repos\\YuZongZhiWindowsERSFormsApp1\\YuZongZhiWindowsERSFormsApp1\\EmpRec.xml");
            listRead.MoveToElement();

            while (listRead.Read())
            {
                string strNodeName;
                string strNodePath;
                string name;
                string gread;
                string doj;
                string sal;
                string[] strItenArr = new string[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FirstNode.FullPath.Remove(0, 17);
                if (strNodeName == strNodePath)
                {
                    ListViewItem lvi;

                    listRead.MoveToAttribute();
                    name = listRead.Value;
                    lvi = listView1.Items.Add(listRead.Value);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToAttribute();
                    doj = listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToAttribute();
                    gread = listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToAttribute();
                    sal = listRead.Value;
                    lvi.SubItems.Add(sal);

                    listRead.MoveToFirstAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();

                }

            }

        }//end PopulateTreeView
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                InitalizeListControl();
                statusBarPanel1.Text = "Double Click the Employee Records";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an Employee code to view individual record";
            }
            PopulateTreeView(tvRootNode);
        }

    }
}
