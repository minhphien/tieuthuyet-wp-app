using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Comic.ViewModels.Shared;


namespace Comic.ModelStorage
{

    public class ContentFormatModelStorage : StorageHandler<FormatBarViewModel>
    {

        public override void Configure()
        {
            Id(item => item.DisplayName);
            Property(item => item.Format).InAppSettings().RestoreAfterViewLoad();
        }
    }
     
}
