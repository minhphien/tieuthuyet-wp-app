using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.Messages
{
    public class FinishServerCallMessage
    {
        private int novelId;

        public int NovelId
        {
            get { return novelId; }
            set { novelId = value; }
        }

        private int chapterId;

        public int ChapterId
        {
            get { return chapterId; }
            set { chapterId = value; }
        }
        

        public FinishServerCallMessage(int novelId,int chapterId)
        {
            this.NovelId = novelId;
            this.ChapterId = chapterId;
        }
        
    }
}
