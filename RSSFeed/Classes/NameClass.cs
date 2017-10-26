using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.Text;
using System.Xml;



namespace RSSFeed.Classes
{
    public class NameClass
    {
        protected string removeIllegalCharacters(object input)
        {
            string data = input.ToString();
            data = data.Replace("&", "&amp;");
            data = data.Replace("\"", "&quot;");
            data = data.Replace("'", "&apos;");
            data = data.Replace("<", "&lt;");
            data = data.Replace(">", "&gt;");
            return data;
        }

        public void setFeed(string SQL, HttpResponse rs)
        {
            //string category = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Url.AbsolutePath);
            string connectionString = WebConfigurationManager.ConnectionStrings["Names"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                //cmd.Parameters.AddWithValue("@category", category);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string n = reader["name"].ToString();
                    string arb = reader["arabic"].ToString();
                    string mn = reader["meaning"].ToString();
                    //string contents = n + " -- " + "<b>" + arb + "</b>" + " -- " + mn;
                    rs.Write(n + " -- " + "<b>" + arb + "</b>" + " -- " + mn);
                    //rs.Write(reader["meaning"].ToString());
                }
                reader.Close();
            }
        }

        public void setFeeds(string SQL, HttpResponse rs)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Names"].ConnectionString;
            //string currentPage = Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath);
            //string category = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Url.AbsolutePath);

            rs.Clear();
            rs.ContentType = "text/xml";//Specify that the content is to be rendered as xml.
            XmlTextWriter objX = new XmlTextWriter(rs.OutputStream, Encoding.UTF8);
            objX.WriteStartDocument();
            objX.WriteStartElement("rss");
            objX.WriteAttributeString("version", "2.0");
            objX.WriteStartElement("channel");
            objX.WriteElementString("title", "FunMobile sms contents");
            objX.WriteElementString("link", "http://Funliveweb.com");
            objX.WriteElementString("description", "Funmobile Ltd sms contents for IMI CMS ingestion");
            objX.WriteElementString("pubDate", "2012-06-01");
            objX.WriteElementString("lastBuildDate", "2012-06-26");
            objX.WriteElementString("copyright", "© Funmobile Ltd 2012");
            objX.WriteElementString("ttl", "5");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                //cmd.Parameters.AddWithValue("@category", category);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string n = reader["name"].ToString();
                    string arb = reader["arabic"].ToString();
                    string mn = reader["meaning"].ToString();
                    string content = n + " --- " + arb + " --- " + mn;
                    //string encodeContent = HttpUtility.HtmlDecode(content);
                    objX.WriteStartElement("item");
                    objX.WriteElementString("title", "99 NAMES OF ALLAH");
                    objX.WriteElementString("description", content);
                    objX.WriteElementString("link", "http://funliveweb.com/smsfeeds"); 
                    objX.WriteStartElement("guid");
                    objX.WriteAttributeString("isPermaLink", "false");
                    objX.WriteString(reader.GetInt32(0).ToString());
                    objX.WriteEndElement();
                    objX.WriteElementString("pubDate", string.Format("{0:R}", reader["Published"]));
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