using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSSFeed.Classes;

namespace RSSFeed
{
    public partial class Ayat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string ID = Request.QueryString["id"];
            //if (!string.IsNullOrEmpty(ID))
            //{
            //    string sql = "SELECT contents FROM R_General where category=@category and  ID=" + ID;
            //    Feeds feed = new Feeds();
            //    feed.setFeed(sql, Response);
            //}
            //else
            //{
                //string sql = "SELECT * FROM R_General where category=@category and id = 44";
            string sql = @"if not exists 
                    (select id from r_categories where categoryname='{cat}' 
	                    and DATEDIFF(dd,[date],SYSDATETIME())=0)
	                    BEGIN
		                    declare @lstid int
		                    select top 1 @lstid=g.id from R_General g,r_categories c 
		                    where c.categoryname = '{cat}' and g.Category ='{cat}' and g.id> c.lastcontentid

		                    update r_categories set  [date]=SYSDATETIME(), lastcontentid=@lstid	
		                    where categoryname='{cat}'

	                    END
                    select top(1) g.ID,g.Category,g.Contents,g.published  from R_General g,r_categories c 
                    where c.categoryname = '{cat}' and g.Category ='{cat}' and g.id= c.lastcontentid
                    order by g.ID asc";
            Feeds feed = new Feeds();
            feed.setFeeds(sql.Replace("{cat}", "Ayat"), Response);
            feed.setFeeds(sql, Response);
            //}
        }
    }
}