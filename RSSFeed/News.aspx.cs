using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Web.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSSFeed.Classes;


namespace RSSFeed
{

    public partial class News : System.Web.UI.Page
    {
   

        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["id"];
            if(!string.IsNullOrEmpty(ID))
            {
                string sql = "SELECT contents FROM R_General where ID=" + ID;
                Feeds feed = new Feeds();
                feed.setFeed(sql, Response);
            }
            else
            {
                string sql = "SELECT * FROM R_General";
                Feeds feed = new Feeds();
                feed.setFeeds(sql, Response);
            }
        }
        
    }
}