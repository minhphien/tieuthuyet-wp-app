using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comic.ViewModels.Shared;

namespace Comic.Messages
{
    public class ChapterItemTapMessage
    {
        private int _chapterId;

        public int ChapterId
        {
            get { return _chapterId; }
            set { _chapterId = value; }
        }

        private int _novelId;

        public int NovelId
        {
            get { return _novelId; }
            set { _novelId = value; }
        }
        
        public ChapterItemTapMessage(int chapterId,int novelId )
        {
            this.ChapterId = chapterId;
            this.NovelId = novelId;
        }

        public ChapterItemTapMessage(int chapterId)
        {
            this.ChapterId = chapterId;
        }
    }
    public class ChapterListLoadedMessage
    {
        private ChapterListViewModel _chapterList;

        public ChapterListViewModel ChapterList
        {
            get { return _chapterList; }
            set { _chapterList = value; }
        }

        public ChapterListLoadedMessage(ChapterListViewModel chapterList)
        {
            this.ChapterList = chapterList;
        }
        
        
    }
}
