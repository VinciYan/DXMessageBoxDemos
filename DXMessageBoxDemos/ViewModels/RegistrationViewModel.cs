using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMessageBoxDemos.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public string UserName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
