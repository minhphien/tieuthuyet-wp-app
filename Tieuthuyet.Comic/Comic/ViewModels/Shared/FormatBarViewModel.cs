using System.Threading.Tasks;
using Comic.Model;

namespace Comic.ViewModels.Shared
{
    public class FormatBarViewModel : ViewModelBase
    {
        private ContentFormat _format;

        public ContentFormat Format
        {
            get { return _format; }
            set
            {
                if (value != _format)
                {
                    
                    if ((_format == null || value.Size != _format.Size)&&(value.Size!=null))
                    {
                        eventAggregator.Publish(new Comic.Messages.ChangeContentFormatMessage(value.Size));
                    }
                    if ((_format == null || value.Style != _format.Style)&&(value.Style!=null))
                    {
                        eventAggregator.Publish(new Comic.Messages.ChangeContentFormatMessage(value.Style));
                    }
                    _format = value;
                    OnPropertyChanged(() => Format);
                }
            }
        }

        public FormatBarViewModel(ContentFormat format)
        {
            this.Format = format ?? new ContentFormat();
        }

        protected override void OnActivate()
        {
            
        }

        public void SizeChanged(string sizeName)
        {
            Format.Size = Format.SetSize(sizeName);
            eventAggregator.Publish(new Comic.Messages.ChangeContentFormatMessage(Format.Size), action =>
            {
                Task.Factory.StartNew(action);
            });
        }

    }
}
