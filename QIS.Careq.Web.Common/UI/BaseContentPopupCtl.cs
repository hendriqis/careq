using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Web.Common.UI
{
    public abstract class BaseContentPopupCtl : System.Web.UI.UserControl 
    {
        #region Words
        protected List<Words> words;
        protected void LoadWords()
        {
            words = Helper.LoadWords(this);
        }
        public string GetLabel(string code)
        {
            return Helper.GetWordsLabel(words, code);
        }
        #endregion

        public virtual void LoadMasterControl()
        {
        }

        public virtual void InitializeControl(string param)
        {
            LoadWords();
        }
    }
}
