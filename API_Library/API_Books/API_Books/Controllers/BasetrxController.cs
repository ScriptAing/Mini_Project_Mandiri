using API_Books.Libs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System.Dynamic;

namespace API_Books.Controllers
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

        public string insertBook(JObject json)
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
                if (!string.IsNullOrEmpty(json.ToString()))
                {
                    JArray JaData = JArray.Parse(json["listofbooks"].ToString());

                    if (JaData.Count > 0)
                    {
                        for (int i = 0; i < JaData.Count(); i++)
                        {
                            NpgsqlCommand cmd = new NpgsqlCommand("insert_books_data", connection, trans);
                            cmd.Parameters.AddWithValue("p_id", Convert.ToInt64(0));
                            cmd.Parameters.AddWithValue("p_title", JaData[i]["title"].ToString());
                            cmd.Parameters.AddWithValue("p_writer", JaData[i]["writer"].ToString());
                            cmd.Parameters.AddWithValue("p_isbn", JaData[i]["isbn"].ToString());
                            cmd.Parameters.AddWithValue("p_publisher", JaData[i]["publisher"].ToString());
                            cmd.Parameters.AddWithValue("p_page", Convert.ToInt32(JaData[i]["page"].ToString()));
                            cmd.Parameters.AddWithValue("p_synopsis", JaData[i]["synopsis"].ToString());
                            cmd.Parameters.AddWithValue("p_dimension", JaData[i]["dimension"].ToString());
                            cmd.Parameters.AddWithValue("p_language", JaData[i]["language"].ToString());
                            cmd.Parameters.AddWithValue("p_date_issue", JaData[i]["date_issue"].ToString());
                            cmd.Parameters.AddWithValue("p_quantity", Convert.ToInt32(JaData[i]["quantity"].ToString()));
                            cmd.Parameters.AddWithValue("p_image_path", JaData[i]["image_path"].ToString());
                            cmd.Parameters.AddWithValue("p_flag", "INSERT");
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        strout = "success";
                    }
                }

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

        public string updateBook(JObject json)
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
                if (!string.IsNullOrEmpty(json.ToString()))
                {
                    JArray JaData = JArray.Parse(json["listofbooks"].ToString());

                    if (JaData.Count > 0)
                    {
                        for (int i = 0; i < JaData.Count(); i++)
                        {
                            NpgsqlCommand cmd = new NpgsqlCommand("insert_books_data", connection, trans);
                            cmd.Parameters.AddWithValue("p_id", Convert.ToInt64(JaData[i]["id"].ToString()));
                            cmd.Parameters.AddWithValue("p_title", JaData[i]["title"].ToString());
                            cmd.Parameters.AddWithValue("p_writer", JaData[i]["writer"].ToString());
                            cmd.Parameters.AddWithValue("p_isbn", JaData[i]["isbn"].ToString());
                            cmd.Parameters.AddWithValue("p_publisher", JaData[i]["publisher"].ToString());
                            cmd.Parameters.AddWithValue("p_page", Convert.ToInt32(JaData[i]["page"].ToString()));
                            cmd.Parameters.AddWithValue("p_synopsis", JaData[i]["synopsis"].ToString());
                            cmd.Parameters.AddWithValue("p_dimension", JaData[i]["dimension"].ToString());
                            cmd.Parameters.AddWithValue("p_language", JaData[i]["language"].ToString());
                            cmd.Parameters.AddWithValue("p_date_issue", JaData[i]["date_issue"].ToString());
                            cmd.Parameters.AddWithValue("p_quantity", Convert.ToInt32(JaData[i]["quantity"].ToString()));
                            cmd.Parameters.AddWithValue("p_image_path", JaData[i]["image_path"].ToString());
                            cmd.Parameters.AddWithValue("p_flag", "UPDATE");
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        strout = "success";
                    }
                }

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

        public string deleteBook(JObject json)
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
                if (!string.IsNullOrEmpty(json.ToString()))
                {
                    JArray JaData = JArray.Parse(json["book_id"].ToString());

                    if (JaData.Count > 0)
                    {
                        for (int i = 0; i < JaData.Count(); i++)
                        {
                            NpgsqlCommand cmd = new NpgsqlCommand("insert_books_data", connection, trans);
                            cmd.Parameters.AddWithValue("p_id", Convert.ToInt64(JaData[i].ToString()));
                            cmd.Parameters.AddWithValue("p_title", "");
                            cmd.Parameters.AddWithValue("p_writer", "");
                            cmd.Parameters.AddWithValue("p_isbn", "");
                            cmd.Parameters.AddWithValue("p_publisher", "");
                            cmd.Parameters.AddWithValue("p_page", Convert.ToInt32(0));
                            cmd.Parameters.AddWithValue("p_synopsis", "");
                            cmd.Parameters.AddWithValue("p_dimension", "");
                            cmd.Parameters.AddWithValue("p_language", "");
                            cmd.Parameters.AddWithValue("p_date_issue", "");
                            cmd.Parameters.AddWithValue("p_quantity", Convert.ToInt32(0));
                            cmd.Parameters.AddWithValue("p_image_path", "");
                            cmd.Parameters.AddWithValue("p_flag", "DELETE");
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        strout = "success";
                    }
                }

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
