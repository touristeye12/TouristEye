using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TouristEye.ViewModels
{
    // PropertyChanged.Fody will take care of boiler plate code ralted to INotifyPropertyChanged
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _refresh;
        public bool refresh { get { return _refresh; } set { _refresh = value; OnPropertyChanged(); } }
        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                errorMsg = value;
                OnPropertyChanged();
            }
        }
        private bool isBusy;
        private bool error;
        public bool IsBusy { get { return isBusy; } set { isBusy = value; OnPropertyChanged(); } }
        public bool Error { get { return error; } set { error = value; OnPropertyChanged(); } }
    }
}
