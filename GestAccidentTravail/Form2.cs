using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestAccidentTravail
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }

        SqlCommand cmd = new SqlCommand();
        DataTable dtbl = new DataTable();
        SqlDataAdapter da;

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDAdp = new SqlDataAdapter("select * from Accident", chaineConn.Cnx);
            sqlDAdp.Fill(dtbl);
            Bs1.DataSource = dtbl;
            dgv.DataSource = Bs1;
            label2.DataBindings.Add("text", Bs1, "idAccident");

        }


        //**************Filtrer le liste des Accidents**************//
        private void textBoxValueToSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dtbl);
            dv.RowFilter = string.Format("LieuAccident LIKE '%{0}%'", textBoxValueToSearch.Text);
            dgv.DataSource = dv;
        }


        //******* redirection to Add Accident ******//
        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1("0");
            f1.ShowDialog();
        }

     
        //******* redirection to Update Accident ******//
        private void btnMdfAcc_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                
                this.Hide();
                Form1 f1 = new Form1(label2.Text);
                f1.ShowDialog();
               // btnAddAcc.Visible = false;


            }
            else
            {
                MessageBox.Show("veuillez selectionner un accident");
            }
        }


        //*******Supprimer Accident******//
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("suppression confirmé ?", "supprimer accident", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                if (label2.Text != "")
                {
                    chaineConn.Cnx.Open();
                    cmd = new SqlCommand("DELETE FROM Accident WHERE idAccident = '" + label2.Text + "'",
                        chaineConn.Cnx);

                    cmd.ExecuteNonQuery();

                    int rowIndex = dgv.CurrentCell.RowIndex;
                   


                    chaineConn.Cnx.Close();
                    MessageBox.Show("Record Deleted Successfully!");
                    dgv.Rows.RemoveAt(rowIndex);

                }
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }


    
}
