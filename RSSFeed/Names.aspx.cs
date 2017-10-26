using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSSFeed.Classes;

namespace RSSFeed
{
    public partial class Names : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //NameClass name = new NameClass();
            //string ID = Request.QueryString["id"];
            //if (!string.IsNullOrEmpty(ID))
            //{
            //    string sql = "SELECT name,arabic,meaning FROM R_names where ID=" + ID;
            //    NameClass name = new NameClass();
            //    name.setFeed(sql, Response);
            //}
            //else
            //{
                //string sql = "SELECT top(1) * FROM R_names";
                //NameClass name = new NameClass();
                //name.setFeeds(sql, Response);
            //}
            string sql = @"if not exists 
                    (select id from r_categories where categoryname='{cat}' 
	                    and DATEDIFF(dd,[date],SYSDATETIME())=0)
	                    BEGIN
		                    declare @lstid int
		                    select top 1 @lstid=g.id from R_Names g ,r_categories c 
		                    where c.categoryname = '{cat}' and g.id > c.lastcontentid

		                    update r_categories set  [date]=SYSDATETIME(), lastcontentid=@lstid	
		                    where categoryname='{cat}'

	                    END
                    select top(1) g.ID,g.name,g.arabic,g.meaning,g.published  from R_Names g,r_categories c 
                    where c.categoryname = '{cat}' and g.id= c.lastcontentid
                    order by g.ID asc";
            NameClass name = new NameClass();
            name.setFeeds(sql.Replace("{cat}", "Names"), Response);
        }
    }
}