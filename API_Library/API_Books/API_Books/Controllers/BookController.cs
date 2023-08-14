using API_Books.Libs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API_Books.Controllers
{
    [Route("api_books/[controller]")]
    public class BookController : Controller
    {
        private MessageController mc = new MessageController();
        private BasetrxController bcx = new BasetrxController();
        private lBook lb = new lBook();
        private lConvert lc = new lConvert();

        [HttpGet("getList")]
        public JObject getListBooks()
        {
            List<dynamic> retObject = new List<dynamic>();
            JObject jOut = new JObject();

            try
            {
                retObject = lb.getListBooks();
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

        [HttpPost("insert")]
        public JObject insertBooksData([FromBody] JObject json)
        {
            JObject data = new JObject();

            try
            {
                string strout = bcx.insertBook(json);
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
        public JObject updateBooksData([FromBody] JObject json)
        {
            JObject data = new JObject();

            try
            {
                string strout = bcx.updateBook(json);
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
        public JObject deleteBooksData([FromBody] JObject json)
        {
            JObject data = new JObject();

            try
            {
                string strout = bcx.deleteBook(json);
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
