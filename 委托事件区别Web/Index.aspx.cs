using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 委托事件区别Web
{
    public partial class Index : System.Web.UI.Page
    {
        public delegate void WriteHander();//委托
        public event WriteHander writeEven;//事件
        protected void Page_Load(object sender, EventArgs e)
        {
            WriteHander wHander = new WriteHander(MyWrite);
            wHander += HeWrite;
            wHander();//用委托调用方法

            writeEven += MyWrite;
            writeEven += HeWrite;
            writeEven();//用事件调用方法
        }
        public void MyWrite()
        {
            Response.Write("1MyWrite");
            Response.Write("<br />");
        }
        public void HeWrite()
        {
            Response.Write("2HeWrite");
            Response.Write("<br />");
        }
    }
}