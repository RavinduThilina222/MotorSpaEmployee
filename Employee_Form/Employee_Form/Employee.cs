using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;



namespace Employee_Form
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();

            
        }

        DataBase Emp = new DataBase();
        string query, gender, designation;
        
        
        private void Btn_Register_Click(object sender, EventArgs e)
        {
            
                if (rdo_Male.Checked == true)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                if(cmb_Designation.SelectedIndex == 0)
                {
                    designation = "Manager";
                }
                else
                {
                    designation = "Employee";
                }
            if (string.IsNullOrEmpty(txt_EmployeeID.Text))
            {
                error_msg.Text = "Please Enter the Employee ID";
                txt_EmployeeID.Focus();
            }
            else if (string.IsNullOrEmpty(txt_FirstName.Text))
            {
                error_msg.Text = "First Name cannot be blank";
                txt_FirstName.Focus();
            }
            else if (txt_FirstName.Text.Any(char.IsDigit))
            {
                error_msg.Text = "First Name cannot have numbers";
                txt_FirstName.Focus();
            }
            else if (string.IsNullOrEmpty(txt_LastName.Text))
            {
                error_msg.Text = "Last Name cannot be blank";
                txt_LastName.Focus();
            }
            else if (txt_LastName.Text.Any(char.IsDigit))
            {
                error_msg.Text = "Last Name cannot have numbers";
                txt_LastName.Focus();
            }
            else if (txt_Email.Text.Length == 0)
            {
                error_msg.Text = "Please Enter Email Address";
                txt_Email.Focus();
            }
            else if (!Regex.IsMatch(txt_Email.Text, @"^[a-zA-Z][\w\.-]*[a-zAZ0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                error_msg.Text = "Enter a valid email. Ex:name@gmail.com";
                txt_Email.Focus();
            }
            else if (rdo_Male.Checked == false && rdo_Female.Checked == false)
            {
                error_msg.Text = "Please select a Gender";
            }
            else if (!Regex.IsMatch(txt_PhoneNo.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
            {
                error_msg.Text = "Phone No must have 10 numbers";
                txt_PhoneNo.Focus();
            }
            else if (string.IsNullOrEmpty(txt_NIC.Text))
            {
                error_msg.Text = "NIC NO cannot be blank";
                txt_NIC.Focus();
            }
            else if (txt_NIC.Text.Length > 12)
            {
                error_msg.Text = "Please Enter a Valid NIC NO";
                txt_NIC.Focus();
            }
            else if (string.IsNullOrEmpty(txt_Address.Text))
            {
                error_msg.Text = "Address cannot be blank";
                txt_Address.Focus();
            }
            else if (txt_Password.Text.Length == 0)
            {
                error_msg.Text = "Please Enter your Password";
                txt_Password.Focus();
            }
            else if (txt_ConfirmPassword.Text.Length == 0)
            {
                error_msg.Text = "Please Enter your Confirm Password";
                txt_ConfirmPassword.Focus();
            }
            else if (txt_Password.Text != txt_ConfirmPassword.Text)
            {
                error_msg.Text = "Confirm Password must same as the Password";
                txt_ConfirmPassword.Focus();
            }
            else if (cmb_Designation.SelectedIndex <= -1)
            {
                error_msg.Text = "Designation cannot be blank";
                cmb_Designation.Focus();
            }
            else if (string.IsNullOrEmpty(txt_Salary.Text))
            {
                error_msg.Text = "Salary cannot be blank";
                txt_Salary.Focus();
            }
            else if (txt_Salary.Text.Any(char.IsLetter))
            {
                error_msg.Text = "Salary cannot have letters";
                txt_Salary.Focus();
            }

            else
            {
                try
                {
                    query = "Insert Into Employee Values('"+txt_EmployeeID.Text+"','" + txt_FirstName.Text + "','" + txt_LastName.Text + "','" + gender + "','" + txt_Email.Text + "','" + txt_PhoneNo.Text + "','" + txt_Address.Text + "','" + txt_NIC.Text + "','" + designation + "','" + txt_Salary.Text + "','" + txt_Password.Text + "')";
                    int i = Emp.Save_Update_Delete(query);
                    if (i == 1)
                    {
                        query = "Select * from Employee";
                        error_msg.Text = "";
                        MessageBox.Show("Registered Successfully !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Can not Register", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Check Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }                        
        }

        private void Btn_cls_Click(object sender, EventArgs e)
        {
            query = "Select Employee_ID from Employee where Employee_ID = (Select MAX(Employee_ID) From Employee)";
            //txt_EmployeeID.Text = (Emp.AutoIDNumber(query) + 1).ToString();
            txt_FirstName.Clear();
            txt_LastName.Clear();
            txt_Email.Clear();
            txt_PhoneNo.Clear();
            txt_NIC.Clear();
            txt_Password.Clear();
            txt_ConfirmPassword.Clear();
            txt_Address.Clear();
            txt_Salary.Clear();
            cmb_Designation.Text = "";
            rdo_Male.Checked = false;
            rdo_Female.Checked = false;

        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            query = "Select * From Employee ";

            dgv_EmployeeDetails.DataSource =  Emp.Search(query);
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Delete from Employee where Employee_ID = '" + txt_EmployeeID.Text + "'";

                if(MessageBox.Show("Do you want to fire Employee_ID : '"+txt_EmployeeID.Text+"'","Information",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int i = Emp.Save_Update_Delete(query);

                    if (i == 1)
                    {                   
                        MessageBox.Show("Deleted this Employee", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                    
                }           
            }

            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dgv_EmployeeDetails_MouseClick(object sender, MouseEventArgs e)
        {
             
        }

        private void Dgv_EmployeeDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Dgv_EmployeeDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dgv_EmployeeDetails.Rows[index];

            txt_EmployeeID.Text = selectedRow.Cells[0].Value.ToString();
            txt_FirstName.Text = selectedRow.Cells[1].Value.ToString();
            txt_LastName.Text = selectedRow.Cells[2].Value.ToString();
            if (selectedRow.Cells[3].Value.ToString() == "Male")
                rdo_Male.Checked = true;
            else
                rdo_Female.Checked = true;

            txt_Email.Text = selectedRow.Cells[4].Value.ToString();
            txt_PhoneNo.Text = selectedRow.Cells[5].Value.ToString();
            txt_Address.Text = selectedRow.Cells[6].Value.ToString();
            txt_NIC.Text = selectedRow.Cells[7].Value.ToString();
            if (selectedRow.Cells[8].Value.ToString() == "Manager")
                cmb_Designation.SelectedIndex = 0;
            else
                cmb_Designation.SelectedIndex = 1;

            txt_Salary.Text = selectedRow.Cells[9].Value.ToString();
            txt_Password.Text = txt_ConfirmPassword.Text = selectedRow.Cells[10].Value.ToString();
        }

        private void Employee_Load(object sender, EventArgs e)
        {

            query = "Select MAX(Employee_ID) From Employee";
            
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
                if (rdo_Male.Checked == true)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                if (cmb_Designation.SelectedIndex == 1)
                {
                    designation = "Manager";
                }
                else
                {
                    designation = "Employee";
                }

                query = "Update Employee set Employee_ID = '"+txt_EmployeeID.Text+"',First_Name = '" + txt_FirstName.Text + "',Last_Name = '" + txt_LastName.Text + "',Gender = '" + gender + "',Email = '" + txt_Email.Text + "',Phone_No = '" + txt_PhoneNo.Text + "',Address = '" + txt_Address.Text + "',NIC = '" + txt_NIC.Text + "',Salary = '" + txt_Salary.Text + "',Password = '" + txt_Password.Text + "' Where Employee_ID = '"+txt_EmployeeID.Text+"' ";
                
                try
                {
                    int i = Emp.Save_Update_Delete(query);
                    if (i == 1)
                    {
                        MessageBox.Show("Updated Successfully !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
                                
                
            
            


        }


    }
}
