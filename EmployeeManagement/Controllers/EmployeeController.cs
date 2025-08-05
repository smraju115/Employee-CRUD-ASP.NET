using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employees.Add(new Employee()
                    {
                        EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                        Name = rdr["Name"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Phone = rdr["Phone"].ToString(),
                        Address = rdr["Address"].ToString(),
                        Designation = rdr["Designation"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"])
                    });
                }
            }
            return View(employees);
        }

        //Create Employee
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                
                    SqlCommand cmd = new SqlCommand("spInsertEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                    con.Open();
                    cmd.ExecuteNonQuery();

                

            }
            // Store success message in TempData
            TempData["SuccessMessage"] = "Employee added successfully!";
            return RedirectToAction("Index");

        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            Employee emp = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    emp.EmployeeId = (int)rdr["EmployeeId"];
                    emp.Name = rdr["Name"].ToString();
                    emp.Email = rdr["Email"].ToString();
                    emp.Phone = rdr["Phone"].ToString();
                    emp.Address = rdr["Address"].ToString();
                    emp.Designation = rdr["Designation"].ToString();
                    emp.Salary = (decimal)rdr["Salary"];
                }
            }

            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                cmd.Parameters.AddWithValue("@Address", emp.Address);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Delete
        public ActionResult Delete(int id)
        {
            Employee emp = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    emp.EmployeeId = (int)rdr["EmployeeId"];
                    emp.Name = rdr["Name"].ToString();
                    emp.Email = rdr["Email"].ToString();
                    emp.Phone = rdr["Phone"].ToString();
                    emp.Address = rdr["Address"].ToString();
                    emp.Designation = rdr["Designation"].ToString();
                    emp.Salary = (decimal)rdr["Salary"];
                }
            }

            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

    }
}