using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClicked;
        }

        private void dataGridView1_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid cell is clicked and not the header row
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Load data from the selected row into TextBoxes
                nameBox.Text = selectedRow.Cells[0].Value.ToString();
                addressBox.Text = selectedRow.Cells[1].Value.ToString();
                userBox.Text = selectedRow.Cells[2].Value.ToString();
                passBox.Text = selectedRow.Cells[3].Value.ToString();
            }
        }

        public static int GeneratedID(int num)
        {
            Random rand = new Random();
            int id = rand.Next(0, num);
            return id;
        }
        private void insBttn_Click(object sender, EventArgs e)
        {
            int generatedID = GeneratedID(101);

            dataGridView1.Rows.Add(generatedID, nameBox.Text, addressBox.Text, userBox.Text, passBox.Text);

            nameBox.Text = "";
            addressBox.Text = "";
            userBox.Text = "";
            passBox.Text = "";
        }

        private void chkBttn_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Create an object to hold the data from the selected row
                var rowData = new
                {
                    ID = selectedRow.Cells[0].Value.ToString(), // Assuming ID is in the first column
                    Name = selectedRow.Cells[1].Value.ToString(), // Assuming Name is in the second column
                    Address = selectedRow.Cells[2].Value.ToString(), // Assuming Address is in the third column
                    Username = selectedRow.Cells[3].Value.ToString(), // Assuming Username is in the fourth column
                    Password = selectedRow.Cells[4].Value.ToString() // Assuming Password is in the fifth column
                                                                     // Add more properties as needed for other columns
                };

                // Convert the data to JSON format
                string jsonData = JsonConvert.SerializeObject(rowData, Formatting.Indented);

                // Display the JSON data
                MessageBox.Show(jsonData, "JSON Format");
            }
            else
            {
                MessageBox.Show("No row selected.", "Error");
            }
        }

        private void retBttn_Click(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.Trim(); // Get the text from the TextBox and remove leading/trailing whitespace

            bool found = false;

            // Iterate through the rows of the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Iterate through the cells of the current row
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // Check if the cell value matches the search text
                    if (cell.Value != null && cell.Value.ToString().Equals(searchText))
                    {
                        // If a match is found, select the row and set found to true
                        row.Selected = true;
                        found = true;
                        break; // Exit the inner loop since we found a match
                    }
                }

                if (found)
                {
                    // If a match is found, exit the outer loop
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Data not found in the DataGridView.", "Not Found");
            }
        }
    }
}
