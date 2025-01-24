using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using System.Collections.Specialized;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        #region FieldDefinitions
            public XMLdata XMLobject;
            public JSONdata JSONobject;
        #endregion
      
        #region Constructors
        public Form1()
        {
            InitializeComponent();
            this.XMLobject = new XMLdata();
            this.JSONobject = new JSONdata();
        }

        ~Form1()
        {
            
        }
        #endregion
        
    
         #region Serialization
        private void button4_Click(object sender, EventArgs e) // SERIALIZE
        {
            StreamWriter myStream;
            XmlSerializer mySerializer;
            
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                if (saveFileDialog1.FileName != null)
                {
                    mySerializer = new XmlSerializer(XMLobject.GetType());
                    myStream = new StreamWriter(saveFileDialog1.FileName, false);
                    mySerializer.Serialize(myStream,XMLobject);
                    myStream.Close();
                
                  
                }
                
                
            }


        }

        private void button5_Click(object sender, EventArgs e)      // DESERIALIZE
        {
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            XMLdata inputData;
            XmlSerializer mySerializer;
            FileStream myStream;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName != null)
                {
                    inputData = new XMLdata();
                    mySerializer = new XmlSerializer(XMLobject.GetType());
                    myStream = new FileStream(openFileDialog1.FileName, FileMode.Open);
                    inputData = (XMLdata)mySerializer.Deserialize(myStream);

                    textBox2.Text = inputData.ToString();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)      // SERIALIZE JSON
        {
            StreamWriter myStream;
            JsonSerializerOptions options;

            saveFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != null)
                {
                    myStream = new StreamWriter(saveFileDialog1.FileName, false);
                    options = new JsonSerializerOptions();
                    options.WriteIndented = true;
                    myStream.Write(JsonSerializer.Serialize(JSONobject, options));
                    myStream.Close();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)          // DESERIALIZE JSON
        {
            openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            JSONdata inputData;
            string JSONbuffer;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName != null)
                {
                    inputData = new JSONdata();
                    JSONbuffer = File.ReadAllText(openFileDialog1.FileName);
                    inputData = JsonSerializer.Deserialize<JSONdata>(JSONbuffer);
                    textBox2.Text = inputData.ToString();
                }
            }
        }
        #endregion
    }
}
