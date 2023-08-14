using API_Borrower.Libs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API_Borrower.Controllers
{
    [Route("api_borrower/[controller]")]
    public class BorrowerController : Controller
    {
        private MessageController mc = new MessageController();
        private BasetrxController bcx = new BasetrxController();
        private lBorrower lb = new lBorrower();
        private lConvert lc = new lConvert();

        [HttpGet("getList")]
        public JObject getListBorrower()
        {
            List<dynamic> retObject = new List<dynamic>();
            JObject jOut = new JObject();

            try
            {
                retObject = lb.getListBorrower();
                if (retObject.Count > 0)
                {
                    jOut = new JObject();
                    jOut.Add("status", mc.GetMessage("api_output_ok"));
                    jOut.Add("message", mc.GetMessage("process_success"));
                    jOut.Add("data", lc.convertDynamicToJArray(retObject));
                }
                else
                {
                    jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                    jOut.Add("message", mc.GetMessage("no_data"));
                }
            }
            catch (Exception ex)
            {
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }
            return jOut;

        }

        [HttpGet("getDetail/{id}")]
        public JObject getDetailBorrower(string id)
        {
            List<dynamic> retObject = new List<dynamic>();
            JObject jOut = new JObject();

            try
            {
                retObject = lb.getDetailBorrower(id);
                if (retObject.Count > 0)
                {
                    jOut = new JObject();
                    jOut.Add("status", mc.GetMessage("api_output_ok"));
                    jOut.Add("message", mc.GetMessage("process_success"));

                    JObject jData = new JObject();
                    jData.Add("id", retObject[0].id);
                    jData.Add("name", retObject[0].name);
                    jData.Add("nim", retObject[0].nim);
                    jData.Add("phone_number", retObject[0].phone_number);
                    jData.Add("address", retObject[0].address);
                    jData.Add("faculty", retObject[0].faculty);
                    jData.Add("major", retObject[0].major);
                    jData.Add("borrow_date", retObject[0].borrow_date);
                    jData.Add("return_date", retObject[0].return_date);
                    jData.Add("penalty_amount", retObject[0].penalty_amount);
                    jData.Add("created_date", retObject[0].created_date);
                    jData.Add("updated_date", retObject[0].updated_date);

                    JArray JaData = new JArray();
                    for (int i = 0; i < retObject.Count; i++)
                    {
                        JObject JObj = new JObject();
                        JObj.Add("title", retObject[i].title);
                        JObj.Add("writer", retObject[i].writer);
                        JObj.Add("isbn", retObject[i].isbn);
                        JObj.Add("publisher", retObject[i].publisher);
                        JObj.Add("page", retObject[i].page);
                        JObj.Add("synopsis", retObject[i].synopsis);
                        JObj.Add("dimension", retObject[i].dimension);
                        JObj.Add("language", retObject[i].language);
                        JObj.Add("date_issue", retObject[i].date_issue);
                        JObj.Add("image_path", retObject[i].image_path);

                        JaData.Add(JObj);
                    }

                    jData.Add("listofbooks", JaData);

                    jOut.Add("data", jData);
                }
                else
                {
                    jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                    jOut.Add("message", mc.GetMessage("no_data"));
                }
            }
            catch (Exception ex)
            {
                jOut = new JObject();
                jOut.Add("status", mc.GetMessage("api_output_not_ok"));
                jOut.Add("message", ex.Message);
            }
            return jOut;

        }

        [HttpPost("insert")]
        public JObject insertBorrowerData([FromBody] JObject json)
        {
            JObject data = new JObject();

            try
            {
                string strout = bcx.insertBorrower(json);
                if (strout == "success")
                {
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("save_success"));
                }
                else
                {
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", strout);
                }
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpPost("update")]
        public JObject updateBorrowerData([FromBody] JObject json)
        {
            JObject data = new JObject();

            try
            {
                string strout = bcx.updateBorrower(json);
                if (strout == "success")
                {
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("save_success"));
                }
                else
                {
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", strout);
                }
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        [HttpPost("delete")]
        public JObject deleteBorrowerData([FromBody] JObject json)
        {
            JObject data = new JObject();

            try
            {
                string strout = bcx.deleteBorrower(json);
                if (strout == "success")
                {
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("save_success"));
                }
                else
                {
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", strout);
                }
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }
    }
}
