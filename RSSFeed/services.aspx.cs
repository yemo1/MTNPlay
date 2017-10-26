using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSSFeed.Classes;

namespace RSSFeed
{
    public partial class services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string query = "SELECT TOP(3) M.ID, M.TITLE,M.LINK,M.IMAGE,M.DESCRIPTION,M.PUBLISHED,S.CATEGORY FROM MTN_DataService M JOIN MTN_DataService_CAT S ON M.CATEGORYID = S.ID WHERE S.CATEGORY=@category AND DATEDIFF (DAY,[date],GETDATE())=0";
            //if (string.IsNullOrEmpty(Request.QueryString["category"]))
            //{
            //    Response.Redirect("~/bible.aspx");
            //}
            //service serve = new service();
            //if (!string.IsNullOrEmpty(Request.QueryString["category"]))
            //{
            //    service.getFeeds("FetchDataContents", Response);  //For Data Alerts
            //}

            string category = base.Request.QueryString["category"];
            if (string.IsNullOrEmpty(category))
            {
                Response.Redirect("~/bible.aspx");
            }
            if (category.Equals("newsalert", StringComparison.OrdinalIgnoreCase) || category.Equals("soccernews", StringComparison.OrdinalIgnoreCase) || category.Equals("popgossip", StringComparison.OrdinalIgnoreCase))
            {
                //service service = new service();
                General.getFeeds("GetAlerts", Response);
            }
            else
            {
                //service service = new service();
                General.getFeeds("FetchContents",Response);
            }
        }
    }
}