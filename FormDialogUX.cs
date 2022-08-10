using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// 10/08/2022
//Dialog box Example
namespace DialogUX
{
    public partial class FormDialogUX : Form
    {
        public FormDialogUX()
        {
            InitializeComponent();
        }
        static int rowSize = 8;
        static int columnSize = 3;
        string[,] employees=new string[rowSize, columnSize];
        string defaultFileName = "default.bin";

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "bin file|*.bin";
            saveFileDialog.Title = "Save A BIN File";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.DefaultExt = "bin";
            saveFileDialog.ShowDialog();
            string fileName=saveFileDialog.FileName;
            if(saveFileDialog.FileName !="")
            {
                SaveRecord(fileName);
            }
            else
            {
                SaveRecord(defaultFileName);
            }
        }
        private void SaveRecord(String saveFileName)
        {
            try 
            {
                using (Stream stream = File.Open(saveFileName,FileMode.Create))
                {
                    BinaryFormatter bin=new BinaryFormatter();
                    {
                        for(int y=0; y<columnSize; y++)
                        {
                            for(int x=0; x<rowSize; x++)
                            {
                                bin.Serialize(stream, employees[x, y]);
                            }
                        }
                    }
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonAdd_MouseClick(object sender, MouseEventArgs e)
        {
            // no error trapping
            for(int x=0;x<rowSize;x++)
            {
                if (employees[x, 0] == "~")
                {
                    employees[x, 0] = textBoxName.Text;
                    employees[x, 1] = textBoxTitle.Text;
                    employees[x, 2] = textBoxTelephone.Text;

                    var result = MessageBox.Show("Proceed with new Record?", "Add new Record",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        break;
                    else
                    {
                        employees[x, 0] = "~";  //Name
                        employees[x, 1] = "Sales";   //Job Title
                        employees[x, 2] = "1234451";   //Telephone
                        break;

                    }

                }
                else
                { 
                    MessageBox.Show("The array is full", "Array is FULL");
                }
            }
            DisplayEmployees();
        }

        private void DisplayEmployees()
        {
            listBoxOutput.Items.Clear();
            for (int x = 0; x < rowSize; x++)
            {
                listBoxOutput.Items.Add(employees[x,0]+
                    " "+employees[x,1]+
                    " "+employees[x,2]);
            }
        }
        private void InitialiseArray()
        {
            for (int x = 0; x < rowSize; x++)
            {
                employees[x, 0] = "~";  //Name
                employees[x, 1] = "";   //Job Title
                employees[x, 2] = "";   //Telephone
            }
            DisplayEmployees();
        }

        private void FormDialogUX_Load(object sender, EventArgs e)
        {
            InitialiseArray();
        }

        
    }
}
