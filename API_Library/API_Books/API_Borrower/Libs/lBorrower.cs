using API_Borrower.Controllers;

namespace API_Borrower.Libs
{
    public class lBorrower
    {
        BaseController bc = new BaseController();

        public List<dynamic> getListBorrower()
        {
            List<dynamic> retObject = new List<dynamic>();
            string spname = "public.get_list_data_borrower";
            retObject = bc.getDataToObject(spname);
            return retObject;
        }

        public List<dynamic> getDetailBorrower(string id)
        {
            string split = ",";
            List<dynamic> retObject = new List<dynamic>();
            string spname = "public.get_detail_borrower";
            string p1 = "p_borrower_id" + split + id + split + "bg";
            retObject = bc.getDataToObject(spname, p1);
            return retObject;
        }

    }
}
