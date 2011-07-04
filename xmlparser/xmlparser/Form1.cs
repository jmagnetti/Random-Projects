using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace xmlparser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            checkedListBox1.Items.Clear();
            List<string> attributes = new List<string>();
            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All Files (*.*)|*.*";
            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(openFileDialog1.FileName);

                while (attributes.Count == 0 && !reader.EOF)
                {
                    reader.Read();
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            Console.Write("<" + reader.Name);

                            while (reader.MoveToNextAttribute()) // Read the attributes.
                            {
                                attributes.Add(reader.Name);
                            }
                            Console.WriteLine(">");
                            break;

                    }
                    checkedListBox1.Items.AddRange(attributes.ToArray());
                }
                reader.Close();
                label1.Text = "Please select 2 items";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 2)
            {
                string att1 = "", att2 = "";
                List<KeyValuePair<string, string>> itemsToOutput = new List<KeyValuePair<string, string>>();
                System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(textBox1.Text);
                List<string> attributesToGet = new List<string>();
                StringBuilder sb = new StringBuilder();
                foreach (string item in checkedListBox1.CheckedItems)
                {
                    attributesToGet.Add(item);
                }
                while (!reader.EOF)
                {
                    reader.Read();
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            Console.Write("<" + reader.Name);
                            while (reader.MoveToNextAttribute()) // Read the attributes.
                            {
                                if (attributesToGet.Contains(reader.Name))
                                {
                                    if (att1 == "") att1 = reader.Value; else att2 = reader.Value;
                                }
                            }
                            if (att1 != "")
                            {
                                sb.AppendLine(att1 + "," + att2);
                                att1 = "";
                                att2 = "";
                            }
                            
                            Console.WriteLine(">");
                            break;

                    }
                }
                saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                DialogResult dr = saveFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                    writer.Write(sb.ToString());
                    writer.Close();
                    MessageBox.Show("File " + saveFileDialog1.FileName + " created successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select 2 attribute types.", "Error", MessageBoxButtons.OK);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count + (e.NewValue == CheckState.Checked ? 1 : -1) == 2)
            {
                label1.Text = "";
                button5.Enabled = true;
                
            }
            else
            {
                label1.Text = "Please select 2 items";
                button5.Enabled = false;
            }
        }
    }
}
