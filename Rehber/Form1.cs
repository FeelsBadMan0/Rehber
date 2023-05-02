using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-PNOIT9G\\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from KISILER", con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            txtAd.Text = "";
            txtID.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            mskTel.Text = "";
            txtAd.Focus();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, System.EventArgs e)
        {
            con.Open();
            SqlCommand komut = new SqlCommand("insert into KISILER (AD,SOYAD,TELEFON,MAIL) values (@p1,@p2,@p3,@p4)", con);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTel.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kişi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, System.EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, System.EventArgs e)
        {
            con.Open();
            DialogResult secSil = new DialogResult();

            secSil = MessageBox.Show("Silme İşlemini Onaylıyor Musunuz?", "Sil", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (secSil == DialogResult.Yes)
            {

                SqlCommand komut2 = new SqlCommand("delete from KISILER where ID=" + txtID.Text, con);
                komut2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kişi Başarıyla Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Onaylamadığınız İçin Kişi Silinmedi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                con.Close();
            }
        }

        private void btnGuncelle_Click(object sender, System.EventArgs e)
        {
            con.Open();
            SqlCommand komut3 = new SqlCommand("update KISILER set AD=@p1,SOYAD=@p2,TELEFON=@p3,MAIL=@p4 where ID=" + txtID.Text, con);
            komut3.Parameters.AddWithValue("@p1", txtAd.Text);
            komut3.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut3.Parameters.AddWithValue("@p3", mskTel.Text);
            komut3.Parameters.AddWithValue("@p4", txtMail.Text);
            komut3.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kişileriniz Başarıyla Güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
