using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TemplateSelectorDemo.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ViewModelsCollection = new ObservableCollection<ViewModelBase>();
        }
        
        #region Plumbing
        private Action CloseAction;
        public void SetCloseAction(Action closeAction)
        {
            CloseAction = closeAction;
        }
        #endregion

        #region Model
        private ViewModelBase _CurrentViewModel;
        private ViewModelBase _SelectedViewModel;
        private ObservableCollection<ViewModelBase> _ViewModelsCollection;
        #endregion

        #region ViewModel Properties
        public ViewModelBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set => Set(ref _CurrentViewModel, value);
        }

        public ViewModelBase SelectedViewModel
        {
            get => _SelectedViewModel;
            set => Set(ref _SelectedViewModel, value);
        }

        public ObservableCollection<ViewModelBase>  ViewModelsCollection
        {
            get => _ViewModelsCollection;
            set => Set(ref _ViewModelsCollection, value);
        }
        #endregion

        #region CommandInvokers
        private void SelectACommandInvoker(object commandProperty)
        {
            CurrentViewModel = new AViewModel()
            {
                Counter = MockData.MockDataGenerator.Default.GetCounter,
                Name = MockData.MockDataGenerator.Default.GetName
            };
            ViewModelsCollection.Add(CurrentViewModel);
        }
        private void SelectBCommandInvoker(object commandProperty)
        {
            CurrentViewModel = new BViewModel()
            {
                Quantity = MockData.MockDataGenerator.Default.GetQuantity,
                UnitOfMeasure = MockData.MockDataGenerator.Default.GetUom
            };
            ViewModelsCollection.Add(CurrentViewModel);
        }
        private void CloseWindowCommandInvoker(object CommandParameter)
        {
            CloseAction?.Invoke();
        }
        #endregion

        #region ICommand
        private ICommand _SelectACommand;
        private ICommand _SelectBCommand;
        private ICommand _CloseWindowCommand;
        public ICommand SelectACommand
        {
            get
            {
                if (_SelectACommand == null)
                    _SelectACommand = new RelayCommand(SelectACommandInvoker);
                return _SelectACommand;
            }
        }
        public ICommand SelectBCommand
        {
            get
            {
                if (_SelectBCommand == null)
                    _SelectBCommand = new RelayCommand(SelectBCommandInvoker);
                return _SelectBCommand;
            }
        }
        public ICommand CloseWindowCommand
        {
            get
            {
                if (_CloseWindowCommand == null)
                    _CloseWindowCommand = new RelayCommand(CloseWindowCommandInvoker);
                return _CloseWindowCommand;
            }
        }
        #endregion
    }
}