using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.Model
{
    [Table]
    public class UserHistories
    {
        private int _id;
        [Column(CanBeNull=false,IsPrimaryKey=true,IsDbGenerated=true)]
        public int Id
        {
            get { return _id; }
            private set { _id = value;}
        }
        
        [Column(CanBeNull=false)]
        public int NovelId { get; set; }
        
        [Column]
        public string NovelName { get; set; }
        
        [Column]
        public string Author { get; set; }
        
        [Column]
        public string Genre { get; set; }
        
        [Column]
        public string Image { get; set; }
        
        [Column(CanBeNull=false)]
        public int ChapterId { get; set; }
       
        [Column]
        public string ChapterName { get; set; }

        [Column]
        public DateTime TimeCreated { get; set; }
    }
}
