using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(TelefonTipi)));
        }

        BindingList<Kisi> kisiListesi = new BindingList<Kisi>();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAd.Text.Trim()) || string.IsNullOrEmpty(maskedTextBox1.Text.Trim()) || comboBox1.SelectedIndex == -1 || (rdbBayi.Checked == false && rdbCalisan.Checked == false && rdbMüsteri.Checked == false))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz..");
            }
            else
            {
                if (duzenlenen == null)
                {
                    Kisi k = new Kisi();
                    k.Adi = txtAd.Text;
                    k.Rol = rdbBayi.Checked ? "Bayi" : rdbCalisan.Checked ? "Çalışan" : "Müşteri";
                    k.Telefon = maskedTextBox1.Text;
                    k.TelefonTipi = (TelefonTipi)Enum.Parse(typeof(TelefonTipi), comboBox1.SelectedItem.ToString());

                    try
                    {
                        kisiListesi.Add(k);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    duzenlenen.Adi = txtAd.Text;
                    duzenlenen.Telefon = maskedTextBox1.Text;
                    duzenlenen.Rol = rdbBayi.Checked ? "Bayi" : rdbCalisan.Checked ? "Çalışan" : "Müşteri";
                    duzenlenen.TelefonTipi = (TelefonTipi)Enum.Parse(typeof(TelefonTipi), comboBox1.SelectedItem.ToString());

                    kisiListesi.ResetBindings();

                    duzenlenen = null;
                }
                dataGridView1.DataSource = kisiListesi;

                txtAd.Text = maskedTextBox1.Text = "";
                comboBox1.SelectedIndex = -1;
                rdbBayi.Checked = rdbCalisan.Checked = rdbMüsteri.Checked = false;
            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {

            if (txtAra.Text == "")
            {
                dataGridView1.DataSource = kisiListesi;
            }
            else
            {
                BindingList<Kisi> aramaSonucu = new BindingList<Kisi>();


                foreach (var item in kisiListesi)
                {
                    if (item.Adi.ToLower().Contains(txtAra.Text.Trim().ToLower()) || item.Rol.ToLower().Contains(txtAra.Text.Trim().ToLower()) || item.Telefon.ToLower().Contains(txtAra.Text.Trim().ToLower()))
                        aramaSonucu.Add(item);
                }
                dataGridView1.DataSource = aramaSonucu;
            }
        }

        Kisi duzenlenen;
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            duzenlenen = (Kisi)dataGridView1.SelectedRows[0].DataBoundItem;

            txtAd.Text = duzenlenen.Adi;
            maskedTextBox1.Text = duzenlenen.Telefon;
            rdbBayi.Checked = duzenlenen.Rol == "Bayi" ? true : false;
            rdbCalisan.Checked = duzenlenen.Rol == "Çalışan" ? true : false;
            rdbMüsteri.Checked = duzenlenen.Rol == "Müsteri" ? true : false;

            comboBox1.SelectedIndex = (int)duzenlenen.TelefonTipi;

            //multıselect ı false yaparsak control tusu ıle bırden fazla secım yapamıyoruz bır tane secım yapabılıyoruz
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;   //herhangı bırı secılı degılse dön

            Kisi silinen = (Kisi)dataGridView1.SelectedRows[0].DataBoundItem;

            kisiListesi.Remove(silinen);
        }
    }
}
