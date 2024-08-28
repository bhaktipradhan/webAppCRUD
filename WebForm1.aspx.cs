using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace webAppCRUD
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["practiceConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd; 
        SqlDataAdapter adapter;
        DataTable dt;

        public void DataLoad()
        {
            if (Page.IsPostBack)
            {
                gridView.DataBind();
            }
        }

        public void ClearAllData()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            ddlGender.SelectedValue = ddlGender.Items[0].ToString();
            txtDOB.Text = DateTime.Today.Date.ToString();
            lblMessage.Text = "";
            lblSID.Text = "";
            chkBoxAgree.Checked = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text!="" &&  txtEmail.Text!="" && chkBoxAgree.Checked)
            {
                using(con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("Insert Into Employee (Name, Email, Gender, DOB) Values (@name, @email, @gender, @dob)",con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DataLoad();
                    ClearAllData();
                }
            }
            else
            {
                lblMessage.Text = "Fill required fields";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using(con = new SqlConnection(cs))
            {
                cmd = new SqlCommand("Update Employee set Name=@name, Email=@email, Gender=@gender, DOB=@dob where Id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
                cmd.Parameters.AddWithValue("@id", lblSID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                DataLoad(); ClearAllData();
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using(con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Delete from Employee Where Id=@id",con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", lblSID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                DataLoad();
                ClearAllData();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSID.Text = gridView.SelectedRow.Cells[1].Text;
            txtName.Text = gridView.SelectedRow.Cells[2].Text;
            txtEmail.Text = gridView.SelectedRow.Cells[3].Text;
            ddlGender.Text = gridView.SelectedRow.Cells[4].Text;
            txtDOB.Text = gridView.SelectedRow.Cells[5].Text;
        }
    }
}