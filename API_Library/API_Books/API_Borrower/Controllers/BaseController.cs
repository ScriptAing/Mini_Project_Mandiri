using API_Borrower.Libs;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Dynamic;
using System.Text;

namespace API_Borrower.Controllers
{
    public class BaseController : Controller
    {
        private lDbConn dbconn = new lDbConn();

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

        public List<dynamic> getDataToObject(string spname, params string[] list)
        {
            var conn = dbconn.conStringDBLibrary();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            nconn.Open();
            //NpgsqlTransaction tran = nconn.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(',');

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToBoolean(pars[1]));
                        }
                        else if (pars[2] == "bg")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt64(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[0]);
                    }
                }
            }

            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    if (nconn.State.Equals(ConnectionState.Open))
                    {
                        nconn.Close();
                    }
                    NpgsqlConnection.ClearPool(nconn);



                    return retObject;
                }

                retObject = GetDataObj(dr);

                if (nconn.State.Equals(ConnectionState.Open))
                {
                    nconn.Close();
                }
                NpgsqlConnection.ClearPool(nconn);
            }
            catch (Exception)
            {
                if (nconn.State.Equals(ConnectionState.Open))
                {
                    nconn.Close();
                }
                NpgsqlConnection.ClearPool(nconn);
                //dbconn.ErrDbLog(spname, conn, list.ToString(), ex.Message);
            }


            return retObject;
        }
    }
}
