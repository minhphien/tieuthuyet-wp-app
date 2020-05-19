using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.Messages
{
    public class OpenFormatBarMessage { }

    public class ChangeContentFormatMessage
    {
        public enum MessageTypes { Size, Style }
        public Model.ContentFormat.Sizes Size;
        public Model.ContentFormat.Styles Style;
        public MessageTypes type;
        public ChangeContentFormatMessage(Model.ContentFormat.Sizes size)
        {
            this.Size = size;
            type = MessageTypes.Size;
        }
        public ChangeContentFormatMessage(Model.ContentFormat.Styles style)
        {
            this.Style = style;
            type = MessageTypes.Style;
        }
    }
}
