﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RSSFeed.Classes;

namespace RSSFeed
{
    public partial class Word : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            General.getFeeds("FetchContents", Response);
        }
    }
}