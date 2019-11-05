using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TemplateSelectorDemo.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool Set<S>(ref S propertyField, S propertyValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<S>.Default.Equals(propertyField, propertyValue) == false)
            {
                propertyField = propertyValue;
                NotifyPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}