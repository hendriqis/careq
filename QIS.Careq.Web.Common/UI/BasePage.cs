using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Web.Common.UI
{
    public abstract class BasePage : Page
    {
        protected List<Words> words;

        protected void LoadWords()
        {
            words = Helper.LoadWords(this);
        }

        public string GetLabel(string code)
        {
            return Helper.GetWordsLabel(words, code);
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (!Page.IsCallback)
            {
                LoadWords();
            }
        }
    }
}
