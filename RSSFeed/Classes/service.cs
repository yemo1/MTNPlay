using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace RSSFeed.Classes
{
    public class service
    {
        public static void getFeeds(string SQL, HttpResponse rs)
        {
            
            string connectionString = WebConfigurationManager.ConnectionStrings["RssConnection"].ConnectionString;
            string currentPage = Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath);
            string category = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Url.AbsolutePath).ToUpper();
            string cat = HttpContext.Current.Request.QueryString["category"].ToString();
            
            
            
            rs.Clear();
            rs.ContentType = "text/xml";//Specify that the content is to be rendered as xml.
            XmlTextWriter objX = new XmlTextWriter(rs.OutputStream, Encoding.UTF8);
            
            objX.WriteStartDocument();
            objX.WriteStartElement("rss");
            objX.WriteAttributeString("version", "2.0");
            objX.WriteStartElement("channel");
            objX.WriteElementString("title", cat);
            //objX.WriteElementString("link", "http://Funliveweb.com");
            //objX.WriteElementString("description", "Funmobile Ltd sms contents for IMI CMS ingestion");
            objX.WriteElementString("pubDate", "2012-06-01");
            objX.WriteElementString("lastBuildDate", "2012-06-26");
            //objX.WriteElementString("copyright", "© Funmobile Ltd 2012");
            //objX.WriteElementString("ttl", "5");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@category", cat);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    objX.WriteStartElement("item");
                    //objX.WriteElementString("title", cat);
                    objX.WriteElementString("title", reader["title"].ToString());
                    objX.WriteElementString("link", reader["link"].ToString());


                    //objX.WriteElementString("description",objX.WriteCData(reader["description"].ToString());
                    objX.WriteStartElement("description");
                    objX.WriteCData(reader["description"].ToString());
                    objX.WriteEndElement();

                    objX.WriteStartElement("image");
                    objX.WriteCData(reader["image"].ToString());
                    objX.WriteEndElement();
                    //objX.WriteElementString("link", "http://Funliveweb.com/smsfeeds/services.aspx?category=" + reader[2].ToString());// + currentPage + "?id=" + reader.GetInt32(0).ToString());
                    objX.WriteStartElement("guid");
                    objX.WriteAttributeString("isPermaLink", "true");
                    objX.WriteString(reader.GetInt32(0).ToString());
                    objX.WriteEndElement();

                    objX.WriteStartElement("pubDate");
                    objX.WriteCData(string.Format("{0:MMMM,d,yyyy}", reader["published"].ToString()));
                    objX.WriteEndElement();

                    objX.WriteElementString("Author", "Funmobile Ltd");
                    objX.WriteElementString("Copyright", "© 2016");
                    //objX.WriteElementString("pubDate", string.Format("{0:R}", DateTime.Now));
                    objX.WriteEndElement();
                }
                reader.Close();
                objX.WriteEndElement();
                objX.WriteEndElement();
                objX.WriteEndDocument();
                objX.Flush();
                objX.Close();
                rs.End();
            }
        }
    }
}