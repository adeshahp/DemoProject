using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerDemo;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDemo
{
    public class CustomerDB
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        public CustomerDB()
        {
            string conStr = "server=.;database=Day5DB;user id=sa;pwd=sa";
            con = new SqlConnection(conStr);
        }

        public int GenerateID()
        {
            string maxStr = $"select max(cid) from Customer";
            cmd = new SqlCommand(maxStr, con);
            int genId = 0;
            try
            {
                con.Open();
                object data = cmd.ExecuteScalar();
                if (data.ToString().Equals(""))
                {
                    genId = 1;
                    return genId;
                }
                else
                {
                    genId = Convert.ToInt32(data) + 1;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return genId;
        }

        public void AddCustomer(Customer c)
        {
            string intStr = $"insert into Customer values({c.CID},'{c.CNAME}','{c.CGENDER}','{c.ADDRESS}','{c.MOBILE}')";
            cmd = new SqlCommand(intStr, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("New Customer Added Successfully");
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public bool UpdateCustomer(int cid, Customer c)
        {
            string uptStr = $"update Customer set cname='{c.CNAME}',cgender='{c.CGENDER}',caddress='{c.ADDRESS}',cmobile='{c.MOBILE}' where CID={cid}";
            cmd = new SqlCommand(uptStr, con);
            try
            {
                con.Open();
                int rEffected = cmd.ExecuteNonQuery();
                if (rEffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public bool DeleteCustomer(int cid)
        {
            string delStr = $"delete from customer where cid={cid}";
            cmd = new SqlCommand(delStr, con);
            try
            {
                con.Open();
                int rEffected = cmd.ExecuteNonQuery();
                if (rEffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public Customer FindCustomer(int cid)
        {
            string finStr = $"select * from customer where cid={cid}";
            cmd = new SqlCommand(finStr, con);
            SqlDataReader dr = null;
            Customer cr = null;
            try
            {
                con.Open();
                dr=cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    cr = new Customer
                    {
                        CID = dr.GetInt32(0),
                        CNAME = dr.GetString(1),
                        CGENDER = dr.GetString(2),
                        ADDRESS = dr.GetString(3),
                        MOBILE = dr.GetString(4)
                    };
                    return cr;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public List<Customer> GetCustomers()
        {
            string finStr = $"select * from customer";
            cmd = new SqlCommand(finStr, con);
            SqlDataReader dr = null;
            List<Customer> cr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    cr = new List<Customer>();
                    while (dr.Read())
                    {

                        Customer c = new Customer
                        {
                            CID = dr.GetInt32(0),
                            CNAME = dr.GetString(1),
                            CGENDER = dr.GetString(2),
                            ADDRESS = dr.GetString(3),
                            MOBILE = dr.GetString(4)
                        };
                        cr.Add(c);
                    }
                    return cr;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}

