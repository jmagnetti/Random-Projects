using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace XsdValidator
{
    public partial class Form1 : Form
    {
        static bool isValid = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlTextReader r = new XmlTextReader(@"C:\BPConnect xml xsd validation\xsdgen.xml");
            XmlValidatingReader v = new XmlValidatingReader(r);
            v.ValidationType = ValidationType.Schema;
            v.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);
            while (v.Read())
            {
               // Can add code here to process the content.
            }
            v.Close();

            // Check whether the document is valid or invalid.
            if (isValid)
               MessageBox.Show("Document is valid");
            else
                MessageBox.Show("Document is invalid");
            
        }

        public static void MyValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs args)
        {
           isValid = false;
           MessageBox.Show("Validation event\n" + args.Message);
        }
    }
}
