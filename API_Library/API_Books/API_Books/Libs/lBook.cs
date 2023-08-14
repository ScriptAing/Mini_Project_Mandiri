using API_Books.Controllers;

namespace API_Books.Libs
{
    public class lBook
    {
        BaseController bc = new BaseController();

        public List<dynamic> getListBooks()
        {
            List<dynamic> retObject = new List<dynamic>();
            string spname = "public.get_list_data_books";
            retObject = bc.getDataToObject(spname);
            return retObject;
        }
    }
}
