using API_Borrower.Libs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System.Dynamic;

namespace API_Borrower.Controllers
{
    public class BasetrxController : Controller
    {
        lDbConn dbconn = new lDbConn();

        public List<dynamic> GetDataObj(NpgsqlDataReader dr)
        {
            var retObject = new List<dynamic>();
            while (dr.Read())
            {
                var dataRow = new ExpandoObject() as IDictionary<string, object>;
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dataRow.Add(
                           dr.GetName(i),
                           dr.IsDBNull(i) ? null : dr[i] // use null instead of {}
                   );
                }
                retObject.Add((ExpandoObject)dataRow);
            }

            return retObject;
        }

        public string insertBorrower(JObject json)
        {
            string strout = "";
            JObject jo = new JObject();
            List<dynamic> retObject = new List<dynamic>();   
            var conn = dbconn.conStringDBLibrary();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert_borrower_data", connection, trans);
                cmd.Parameters.AddWithValue("p_id", Convert.ToInt64(0));
                cmd.Parameters.AddWithValue("p_name", json["name"].ToString());
                cmd.Parameters.AddWithValue("p_nim", json["nim"].ToString());
                cmd.Parameters.AddWithValue("p_phone_number", json["phone_number"].ToString());
                cmd.Parameters.AddWithValue("p_address", json["address"].ToString());
                cmd.Parameters.AddWithValue("p_faculty", json["faculty"].ToString());
                cmd.Parameters.AddWithValue("p_major", json["major"].ToString());
                cmd.Parameters.AddWithValue("p_borrow_date", json["borrow_date"].ToString());
                cmd.Parameters.AddWithValue("p_return_date", json["return_date"].ToString());
                cmd.Parameters.AddWithValue("p_penalty_amount", Convert.ToDecimal(0));
                cmd.Parameters.AddWithValue("p_flag", "INSERT");
                cmd.CommandType = CommandType.StoredProcedure;
                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    trans.Rollback();
                    connection.Close();
                }

                retObject = GetDataObj(dr);
                dr.Close();

                string br_id = retObject[0].br_id.ToString();

                JArray JaData = JArray.Parse(json["listofbooks"].ToString());

                if (JaData.Count > 0)
                {
                    for (int i = 0; i < JaData.Count(); i++)
                    {
                        cmd = new NpgsqlCommand("insert_borrower_book_trx", connection, trans);
                        cmd.Parameters.AddWithValue("p_br_id", Convert.ToInt64(br_id));
                        cmd.Parameters.AddWithValue("p_bk_id", Convert.ToInt64(JaData[i].ToString()));
                        cmd.Parameters.AddWithValue("p_flag", "INSERT");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                strout = "success";

            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            finally
            {
                if (connection.State.Equals(ConnectionState.Open))
                {
                    connection.Close();
                }
                NpgsqlConnection.ClearPool(connection);
            }
            return strout;
        }

        public string updateBorrower(JObject json)
        {
            string strout = "";
            List<dynamic> retObject = new List<dynamic>();
            var conn = dbconn.conStringDBLibrary();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert_borrower_data", connection, trans);
                cmd.Parameters.AddWithValue("p_id", Convert.ToInt64(json["id"].ToString()));
                cmd.Parameters.AddWithValue("p_name", json["name"].ToString());
                cmd.Parameters.AddWithValue("p_nim", json["nim"].ToString());
                cmd.Parameters.AddWithValue("p_phone_number", json["phone_number"].ToString());
                cmd.Parameters.AddWithValue("p_address", json["address"].ToString());
                cmd.Parameters.AddWithValue("p_faculty", json["faculty"].ToString());
                cmd.Parameters.AddWithValue("p_major", json["major"].ToString());
                cmd.Parameters.AddWithValue("p_borrow_date", json["borrow_date"].ToString());
                cmd.Parameters.AddWithValue("p_return_date", json["return_date"].ToString());
                cmd.Parameters.AddWithValue("p_penalty_amount", Convert.ToDecimal(0));
                cmd.Parameters.AddWithValue("p_flag", "UPDATE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                cmd = new NpgsqlCommand("insert_borrower_book_trx", connection, trans);
                cmd.Parameters.AddWithValue("p_br_id", Convert.ToInt64(json["id"].ToString()));
                cmd.Parameters.AddWithValue("p_bk_id", Convert.ToInt64(0));
                cmd.Parameters.AddWithValue("p_flag", "DELETE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                JArray JaData = JArray.Parse(json["listofbooks"].ToString());

                if (JaData.Count > 0)
                {
                    for (int i = 0; i < JaData.Count(); i++)
                    {
                        cmd = new NpgsqlCommand("insert_borrower_book_trx", connection, trans);
                        cmd.Parameters.AddWithValue("p_br_id", Convert.ToInt64(json["id"].ToString()));
                        cmd.Parameters.AddWithValue("p_bk_id", Convert.ToInt64(JaData[i].ToString()));
                        cmd.Parameters.AddWithValue("p_flag", "INSERT");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                strout = "success";

            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            finally
            {
                if (connection.State.Equals(ConnectionState.Open))
                {
                    connection.Close();
                }
                NpgsqlConnection.ClearPool(connection);
            }
            return strout;
        }

        public string deleteBorrower(JObject json)
        {
            string strout = "";
            JObject jo = new JObject();
            var conn = dbconn.conStringDBLibrary();
            NpgsqlTransaction trans;
            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            trans = connection.BeginTransaction();

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert_borrower_data", connection, trans);
                cmd.Parameters.AddWithValue("p_id", Convert.ToInt64(json["id"].ToString()));
                cmd.Parameters.AddWithValue("p_name", "");
                cmd.Parameters.AddWithValue("p_nim", "");
                cmd.Parameters.AddWithValue("p_phone_number", "");
                cmd.Parameters.AddWithValue("p_address", "");
                cmd.Parameters.AddWithValue("p_faculty", "");
                cmd.Parameters.AddWithValue("p_major", "");
                cmd.Parameters.AddWithValue("p_borrow_date", "");
                cmd.Parameters.AddWithValue("p_return_date", "");
                cmd.Parameters.AddWithValue("p_penalty_amount", Convert.ToDecimal(0));
                cmd.Parameters.AddWithValue("p_flag", "DELETE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                cmd = new NpgsqlCommand("insert_borrower_book_trx", connection, trans);
                cmd.Parameters.AddWithValue("p_br_id", Convert.ToInt64(json["id"].ToString()));
                cmd.Parameters.AddWithValue("p_bk_id", Convert.ToInt64(0));
                cmd.Parameters.AddWithValue("p_flag", "DELETE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                trans.Commit();
                strout = "success";

            }
            catch (Exception ex)
            {
                trans.Rollback();
                strout = ex.Message;
            }
            finally
            {
                if (connection.State.Equals(ConnectionState.Open))
                {
                    connection.Close();
                }
                NpgsqlConnection.ClearPool(connection);
            }
            return strout;
        }

    }
}
