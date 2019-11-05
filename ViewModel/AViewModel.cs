namespace TemplateSelectorDemo.ViewModel
{
    public class AViewModel : ViewModelBase
    {
        private int _Counter;
        private string _Name;

        public int Counter
        {
            get => _Counter;
            set => Set(ref _Counter, value);
        }        
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }        
        public override string ToString()
        {
            return $"{Name} = {Counter}";
        }
    }
}
