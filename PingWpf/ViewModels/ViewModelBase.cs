using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Apex.MVVM;



//ERROR MANEJADOS

namespace PingWpf.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public Command HideErrorMessage { get; private set; }

        private string errorMessage = "";
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage == value) return;
                errorMessage = value;
                RaisePropertyChanged();
            }
        }

        private bool errorVisible = false;
        public bool ErrorVisible
        {
            get { return errorVisible; }
            set
            {
                if (errorVisible == value) return;
                errorVisible = value;
                RaisePropertyChanged();
            }
        }

        public ViewModelBase()
        {
            HideErrorMessage = new Command(() => ErrorVisible = false);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ShowError(string errorMessage, TimeSpan hideAfter = new TimeSpan())
        {
            ErrorMessage = errorMessage;
            ErrorVisible = true;
            if (hideAfter > TimeSpan.Zero)
                HideErrorMessageAfter(hideAfter);
        }

        //muestra mensaje temporal de error en caso de ingresar url erronea
        protected async void HideErrorMessageAfter(TimeSpan delayTime)
        {
            await Task.Delay(delayTime);
            ErrorVisible = false;
        }
    }
}
