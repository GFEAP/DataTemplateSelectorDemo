namespace TemplateSelectorDemo.ViewModel
{
    public class BViewModel : ViewModelBase
    {
        private double _Quantity;
        private string _UnitOfMeasure;
        public double Quantity
        {
            get => _Quantity;
            set => Set(ref _Quantity, value);
        }
        public string UnitOfMeasure
        {
            get => _UnitOfMeasure;
            set => Set(ref _UnitOfMeasure, value);
        }
        public override string ToString()
        {
            return $"{Quantity} {UnitOfMeasure}";
        }
    }
}