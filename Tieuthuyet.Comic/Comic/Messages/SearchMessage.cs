using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.Messages
{
    public class SearchMessage
    {
        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }
        public SearchMessage(string keyword)
        {
            Keyword = keyword;
        }
        
    }
}
