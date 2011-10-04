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
            isValid = true;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            //settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create(@"C:\BPConnect xml xsd validation\build.xml", settings);

            // Parse the file. 
            while (reader.Read() && isValid)
            {
                // Can add code here to process the content.
            }
            reader.Close();

            // Check whether the document is valid or invalid.
            if (isValid)
                MessageBox.Show("Document is valid");
            else
                MessageBox.Show("Document is invalid");
            
        }

        public static void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs args)
        {
           isValid = false;
           MessageBox.Show("Validation event\n" + args.Message + " | " + args.Exception.LineNumber + args.Exception.LinePosition);
        }
    }
}
