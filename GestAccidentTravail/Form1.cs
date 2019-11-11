using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GestAccidentTravail
{
    public partial class Form1 : Form
    {
        private DataGridViewRow Row;
        public Form1(string value )
        {
            InitializeComponent();
            label25.Text = value;

        }


        public Form1(DataGridViewRow row)
        {
            this.Row = row;
            InitializeComponent();
            this.btnAjout.Visible = false;

            //dateTimeDateAcc.Text = row.Cells[1].Value.ToString();
            //textBox_Occ.Text = row.Cells[20].Value.ToString();
            //textBox_LieuAcc.Text = row.Cells[3].Value.ToString();
            //comboBox_EtabProdAcc.Text = row.Cells[4].Value.ToString();
            //textBox_NbrTrav.Text = row.Cells[5].Value.ToString();
            //textBox12_Circons.Text = row.Cells[6].Value.ToString();
            //textBox8_AgMatAcc.Text = row.Cells[7].Value.ToString();
            //textBox_FrmAcc.Text= row.Cells[8].Value.ToString();
            //textBoxSgLes.Text= row.Cells[9].Value.ToString();
            //textBox_NatLes.Text = row.Cells[10].Value.ToString();
            //textBox4_LieuTranspVict.Text = row.Cells[11].Value.ToString();
            //textBoxHeureTransp.Text = row.Cells[12].Value.ToString();
            //comboBox_Cnsq.Text= row.Cells[13].Value.ToString();

            //dateTimePicker_DteArrTrav.Text = row.Cells[14].Value.ToString();
            //dateTimePicker_DteRepMed.Text = row.Cells[15].Value.ToString();
            //dateTimePicker_DteRepMedTrav.Text = row.Cells[16].Value.ToString();

            //comboBox_SalMaint.Text = row.Cells[17].Value.ToString();
            //textBox_DurMaintSal.Text = row.Cells[22].Value.ToString();
            //textBox_MontSal.Text = row.Cells[18].Value.ToString();
            //comboBox_UniteTmpsSal.Text = row.Cells[19].Value.ToString();

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //**************Ajout d'un Accident**************//
        private void btnAjout_Click(object sender, EventArgs e)
        { 
            
            try
            {
                chaineConn.Cnx.Open();
                string sqlreq = @"insert into Accident (DateAccident,OccMmtAcc,DateOcc,LieuAccident,EtabAcc,NbreTravMmtAcc,AgMatProvAcc,FormeAcc,SiegeLesion,NatureLesion,LieuTranspVictime,Consequence,DateArretMed,DateRepriseMed,DateRepriseMedTrav,MaintSalaire,DureeMaintSal,MontantSalMaint,UniteTempsMontSalMaint,CirconstanceAcc,Perso_Matricule) 
                                              

                                              VALUES(@DateAccident,@OccMmtAcc,@DateOcc,@LieuAccident,@EtabAcc,@NbreTravMmtAcc,@AgMatProvAcc,@FormeAcc,@SiegeLesion,@NatureLesion,@LieuTranspVictime,@Consequence,@DateArretMed,@DateRepriseMed,@DateRepriseMedTrav,@MaintSalaire,@DureeMaintSal,@MontantSalMaint,@UniteTempsMontSalMaint,@CirconstanceAcc,@Perso_Matricule)";

                SqlCommand cmd = new SqlCommand(sqlreq, chaineConn.Cnx);

                cmd.Parameters.AddWithValue("@DateAccident", dateTimeDateAcc.Value);
                cmd.Parameters.AddWithValue("@OccMmtAcc", textBox_LieuAcc.Text);
                cmd.Parameters.AddWithValue("@DateOcc", dateTimeOcc.Value);
                cmd.Parameters.AddWithValue( "@LieuAccident", textBox_LieuAcc.Text );
                cmd.Parameters.AddWithValue("@EtabAcc", comboBox_EtabProdAcc.Text);
                cmd.Parameters.AddWithValue("@NbreTravMmtAcc", textBox_NbrTrav.Text);
                cmd.Parameters.AddWithValue("@AgMatProvAcc", textBox8_AgMatAcc.Text);
                cmd.Parameters.AddWithValue("@FormeAcc", textBox_FrmAcc.Text);
                cmd.Parameters.AddWithValue("@SiegeLesion", textBoxSgLes.Text);
                cmd.Parameters.AddWithValue("@NatureLesion", textBox_NatLes.Text);
                cmd.Parameters.AddWithValue("@LieuTranspVictime", textBox4_LieuTranspVict.Text);
                cmd.Parameters.AddWithValue("@Consequence", comboBox_Cnsq.Text);
                cmd.Parameters.AddWithValue("@DateArretMed", dateTimePicker_DteArrTrav.Value);
                cmd.Parameters.AddWithValue("@DateRepriseMed", dateTimePicker_DteRepMed.Value);
                cmd.Parameters.AddWithValue("@DateRepriseMedTrav", dateTimePicker_DteRepMedTrav.Value);
                cmd.Parameters.AddWithValue("@MaintSalaire", comboBox_SalMaint.Text);
                cmd.Parameters.AddWithValue("@DureeMaintSal", textBox_DurMaintSal.Text);
                cmd.Parameters.AddWithValue("@MontantSalMaint", textBox_MontSal.Text);
                cmd.Parameters.AddWithValue("@UniteTempsMontSalMaint", comboBox_UniteTmpsSal.Text);
                cmd.Parameters.AddWithValue("@CirconstanceAcc", textBox12_Circons.Text);
                cmd.Parameters.AddWithValue("@Perso_Matricule", textBoxMatr.Text);


                SqlDataReader rdr = cmd.ExecuteReader();
                MessageBox.Show(("Inseré"));
                chaineConn.Cnx.Close();
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                System.Windows.Forms.MessageBox.Show(sqlException.Message);
            }

        }

   
        //**************Upload Image**************//
        private void button1_Click(object sender, EventArgs e)
        {


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "selectionnez les images";
            ofd.Multiselect = true;
            ofd.Filter = "JPG|*.jpg|JPEG|*.jpeg|GIF|*.gif|PNG|*.png";

            DialogResult dr = ofd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = ofd.FileNames;
                int x = 20;
                int y = 20;
                int maxheight = -1;

                foreach (string img in files)
                {
                    PictureBox pic = new PictureBox();
                    pic.Image = Image.FromFile(img);
                    pic.Location = new Point(x, y);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;

                    x += pic.Width + 10;
                    maxheight = Math.Max(pic.Height, maxheight);
                    if (x > this.ClientSize.Width - 100)
                    {
                        x = 20;
                        y += maxheight + 10;
                    }

                    this.panelImages.Controls.Add(pic);
                    
                    
            
                MessageBox.Show("Accident Modifié");
                }
                chaineConn.Cnx.Close();
            }

        }


        //**************List Accident**************//
        private void btnListAcc_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2(); 
           
            f2.ShowDialog();

        }

     
        //**************recuperation du nom et prenom de la table Perso dans le form Accident**************//
        private void btnRecVict_Click(object sender, EventArgs e)
        {
            string sqlreq = "select Perso_Nom,Perso_Prenom  From Perso INNER JOIN Accident ON Perso.Perso_Matricule = Accident.IdAccident where Perso.Perso_Matricule=" + Convert.ToInt32(textBoxMatr.Text)+"";
            SqlDataReader MyReader;
            SqlCommand cmd = new SqlCommand(sqlreq, chaineConn.Cnx);

            chaineConn.Cnx.Open();
            try
            {

           

            MyReader = cmd.ExecuteReader();
            if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                       
                        string nom = MyReader.GetString(0);
                      string prenom = MyReader.GetString(1);
                        textBoxNom.Text = nom;
                      textBoxPrenom.Text = prenom;


                    }
            }
            MyReader.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
          
            chaineConn.Cnx.Close();
  
        }


        //**************Update Accident**************//
        private void btnModif_Click(object sender, EventArgs e)
        {
            chaineConn.Cnx.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Accident SET " +

                                         //   "DateAccident'" + dateTimeDateAcc.Text+

                                            "OccMmtAcc='" + textBox_Occ.Text+

                                            "',LieuAccident='" + textBox_LieuAcc.Text + 

                                            "',EtabAcc='"+ comboBox_EtabProdAcc.Text +

                                            "',NbreTravMmtAcc='" + textBox_NbrTrav.Text +

                                            "',CirconstanceAcc='"+ textBox12_Circons.Text+

                                            "',AgMatProvAcc='"+ textBox8_AgMatAcc.Text +

                                            "',FormeAcc='"+ textBox_FrmAcc.Text + "'" +

                                            ",SiegeLesion='"+ textBoxSgLes.Text + 

                                            "',NatureLesion='" + textBox_NatLes.Text +

                                            "',LieuTranspVictime='" + textBox4_LieuTranspVict.Text + 

                                            "',HeureTranspVictime='" + textBoxHeureTransp.Text +

                                           "',Consequence='" + comboBox_Cnsq.Text +

                                         //  "',DateArretMed='" + dateTimePicker_DteArrTrav.Value +

                                         //    "',DateRepriseMed='" + dateTimePicker_DteRepMed.Value +

                                          //  "',DateRepriseMedTrav='" + dateTimePicker_DteRepMedTrav.Value + 

                                            "',MaintSalaire='"+ comboBox_SalMaint.Text +

                                            "',DureeMaintSal='"+ textBox_DurMaintSal.Text +

                                            "',MontantSalMaint='"+ textBox_MontSal.Text +

                                            "',UniteTempsMontSalMaint='"+ comboBox_UniteTmpsSal.Text + "' where idAccident ='"+label25.Text+"'",chaineConn.Cnx);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Accident Modifié");

            chaineConn.Cnx.Close();
        }


        DataTable table =new DataTable();
        SqlDataAdapter da=new SqlDataAdapter();
        DataTable table1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter();




        private void Form1_Load(object sender, EventArgs e)
        {

            SqlCommand cmd =new SqlCommand("select * from Accident where idAccident='" + label25.Text + "'", chaineConn.Cnx);
            da.SelectCommand = cmd;
            da.Fill(table);
            if (table.Rows.Count == 0)
            {
                vider();
                //MessageBox.Show("ajouter acc");
                label25.Visible = false;
                return;
            }
            textBoxMatr.Text = table.Rows[0]["Perso_Matricule"].ToString();
            dateTimeDateAcc.Text = table.Rows[0]["DateAccident"].ToString();
            textBox_Occ.Text = table.Rows[0]["OccMmtAcc"].ToString();
            textBox_LieuAcc.Text = table.Rows[0]["LieuAccident"].ToString();
            comboBox_EtabProdAcc.Text = table.Rows[0]["EtabAcc"].ToString();
            textBox_NbrTrav.Text = table.Rows[0]["NbreTravMmtAcc"].ToString();
            textBox12_Circons.Text = table.Rows[0]["CirconstanceAcc"].ToString();
            textBox8_AgMatAcc.Text = table.Rows[0]["AgMatProvAcc"].ToString();
            textBox_FrmAcc.Text = table.Rows[0]["FormeAcc"].ToString();
            textBoxSgLes.Text = table.Rows[0]["SiegeLesion"].ToString();
            textBox_NatLes.Text = table.Rows[0]["NatureLesion"].ToString();
            textBox4_LieuTranspVict.Text = table.Rows[0]["LieuTranspVictime"].ToString();
            textBoxHeureTransp.Text = table.Rows[0]["HeureTranspVictime"].ToString();
            comboBox_Cnsq.Text = table.Rows[0]["Consequence"].ToString();
            dateTimePicker_DteArrTrav.Text = table.Rows[0]["DateArretMed"].ToString();
            dateTimePicker_DteRepMed.Text = table.Rows[0]["DateRepriseMed"].ToString();
            dateTimePicker_DteRepMedTrav.Text = table.Rows[0]["DateRepriseMedTrav"].ToString();
            comboBox_SalMaint.Text = table.Rows[0]["MaintSalaire"].ToString();
            textBox_DurMaintSal.Text = table.Rows[0]["DureeMaintSal"].ToString();
            textBox_MontSal.Text = table.Rows[0]["MontantSalMaint"].ToString();
            comboBox_UniteTmpsSal.Text = table.Rows[0]["UniteTempsMontSalMaint"].ToString();





            SqlCommand cmd1 = new SqlCommand("select * from Perso where Perso_Matricule='" + textBoxMatr.Text + "'", chaineConn.Cnx);

            da1.SelectCommand = cmd1;

            da1.Fill(table1);
            
         
          textBoxNom.Text = table1.Rows[0]["Perso_Nom"].ToString();
         textBoxPrenom.Text = table1.Rows[0]["Perso_Prenom"].ToString();

        }

        private void vider()
        {
            textBoxMatr.Text = "";
            dateTimeDateAcc.Text = "";
            textBox_Occ.Text = "";
            //textBox_LieuAcc.Text = "";
            //comboBox_EtabProdAcc.Text = table.Rows[0]["EtabAcc"].ToString();
            //textBox_NbrTrav.Text = table.Rows[0]["NbreTravMmtAcc"].ToString();
            //textBox12_Circons.Text = table.Rows[0]["CirconstanceAcc"].ToString();
            //textBox8_AgMatAcc.Text = table.Rows[0]["AgMatProvAcc"].ToString();
            //textBox_FrmAcc.Text = table.Rows[0]["FormeAcc"].ToString();
            //textBoxSgLes.Text = table.Rows[0]["SiegeLesion"].ToString();
            //textBox_NatLes.Text = table.Rows[0]["NatureLesion"].ToString();
            //textBox4_LieuTranspVict.Text = table.Rows[0]["LieuTranspVictime"].ToString();
            //textBoxHeureTransp.Text = table.Rows[0]["HeureTranspVictime"].ToString();
            //comboBox_Cnsq.Text = table.Rows[0]["Consequence"].ToString();
            //dateTimePicker_DteArrTrav.Text = table.Rows[0]["DateArretMed"].ToString();
            //dateTimePicker_DteRepMed.Text = table.Rows[0]["DateRepriseMed"].ToString();
            //dateTimePicker_DteRepMedTrav.Text = table.Rows[0]["DateRepriseMedTrav"].ToString();
            //comboBox_SalMaint.Text = table.Rows[0]["MaintSalaire"].ToString();
            //textBox_DurMaintSal.Text = table.Rows[0]["DureeMaintSal"].ToString();
            //textBox_MontSal.Text = table.Rows[0]["MontantSalMaint"].ToString();
            //comboBox_UniteTmpsSal.Text = table.Rows[0]["UniteTempsMontSalMaint"].ToString();
        }



        /***********Enregistrer les documents importés************/
        private void button2_Click(object sender, EventArgs e)
        {
          
            chaineConn.Cnx.Open();
            SqlCommand cmd = new SqlCommand("  INSERT INTO Documents (nomDocument,imgDocument,idAccident) VALUES('10','1','1')", chaineConn.Cnx);
            cmd.ExecuteNonQuery();

        }


        private void textBoxMatr_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        { table1.Clear();
            SqlCommand cmd1 = new SqlCommand("select * from Perso where Perso_Matricule='" + textBoxMatr.Text + "'", chaineConn.Cnx);

            da1.SelectCommand = cmd1;

            da1.Fill(table1);

            if (table1.Rows.Count == 0)
            {
                MessageBox.Show("merci d'ajouter une victime trouvé");
                return;

            }
            textBoxNom.Text = table1.Rows[0]["Perso_Nom"].ToString();
            textBoxPrenom.Text = table1.Rows[0]["Perso_Prenom"].ToString();
        }

        
    }


}
