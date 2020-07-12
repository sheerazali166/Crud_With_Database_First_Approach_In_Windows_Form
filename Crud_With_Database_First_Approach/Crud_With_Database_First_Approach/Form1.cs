using Crud_With_Database_First_Approach.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_With_Database_First_Approach
{
    public partial class Form1 : Form
    {
        int id = 0;

        Student model = new Student();
        Database_First_Approach_DbEntities db = new Database_First_Approach_DbEntities();

        public Form1()
        {
            InitializeComponent();
            BindGridView();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {

            model.Name = textBoxName.Text.Trim();
            model.Gender = comboBoxGender.SelectedItem.ToString();
            model.Age = Convert.ToInt32(textBoxAge.Text.Trim());
            model.Standard = Convert.ToInt32(numericUpDownClass.Value);

            db.Students.Add(model);
            int r = db.SaveChanges();

            if (r > 0)
            {

                MessageBox.Show("Data Successfully Inserted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else {

                MessageBox.Show("Data Insertion Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            model.Id = id;
            model.Name = textBoxName.Text.Trim();
            model.Gender = comboBoxGender.SelectedItem.ToString();
            model.Age = Convert.ToInt32(textBoxAge.Text.Trim());
            model.Standard = Convert.ToInt32(numericUpDownClass.Value);

            db.Entry(model).State = EntityState.Modified;
            int r = db.SaveChanges();

            if (r > 0)
            {

                MessageBox.Show("Data Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else
            {

                MessageBox.Show("Data Updation Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure", "Asking", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                model = db.Students.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(model).State = EntityState.Deleted;
                int r = db.SaveChanges();

                if (r > 0)
                {

                    MessageBox.Show("Data Successfully Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridView();
                    ResetControls();
                }
                else
                {

                    MessageBox.Show("Data Deletion Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else {

                MessageBox.Show("You have cancelled the deletion operation", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        public void BindGridView() {

            dataGridViewShowData.DataSource = db.Students.ToList<Student>();

        }

        public void ResetControls() {

            textBoxName.Clear();
            comboBoxGender.SelectedItem = null;
            textBoxAge.Clear();
            numericUpDownClass.Value = 1;
        }

        private void dataGridViewShowData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridViewShowData.SelectedRows[0].Cells[0].Value);
            model = db.Students.Where(x => x.Id == id).FirstOrDefault();

            textBoxName.Text = model.Name;
            comboBoxGender.SelectedItem = model.Gender;
            textBoxAge.Text = model.Age.ToString();
            numericUpDownClass.Value = model.Standard;

        }
    }
}
