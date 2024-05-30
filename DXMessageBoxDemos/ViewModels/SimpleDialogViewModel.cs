using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMessageBoxDemos.ViewModels
{
    public class SimpleDialogViewModel : ViewModelBase
    {
        public MessageResult DialogResult
        {
            get { return GetProperty(() => DialogResult); }
            set { SetProperty(() => DialogResult, value); }
        }
        protected ICurrentDialogService CurrentDialogService { get { return GetService<ICurrentDialogService>(); } }

        [Command]
        public void Close()
        {
            CurrentDialogService.Close(DialogResult);
        }
    }
}
