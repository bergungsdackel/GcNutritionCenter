using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace GcNutritionCenter
{
    internal class MainWindowViewModel : BaseViewModel
    {

        private double _bgOpacity = 1.0;
        public double BgOpacity
        {
            get
            {
                return _bgOpacity;
            }
            set
            {
                SetProperty(ref _bgOpacity, value);
            }
        }


        private object _currentView;
        public object CurrentView
        {
            get 
            { 
                return _currentView; 
            }
            set
            {
                SetProperty(ref _currentView, value);
            }
        }

        private List<object> _views;

        public MainWindowViewModel()
        {
            _views = new List<object> {
                new Home(),
                new Balance(),
                new Transactions(),
                new Settings()
            };

            CurrentView = _views[0]; // home
        }

        #region Commands definitions

        private ICommand _selectViewCommand;
        public ICommand SelectViewCommand
        {
            get
            {
                return _selectViewCommand ?? (_selectViewCommand = new RelayCommand(param => this.CanSelectView(), param => this.SelectView(param)));
            }
        }

        private ICommand _tgBtnUncheckedCommand;
        public ICommand TgBtnUncheckedCommand
        {
            get
            {
                return _tgBtnUncheckedCommand ?? (_tgBtnUncheckedCommand = new RelayCommand(param => this.CanTgBtnUnchecked(), param => this.TgBtnUnchecked()));
            }
        }

        private ICommand _tgBtnCheckedCommand;
        public ICommand TgBtnCheckedCommand
        {
            get
            {
                return _tgBtnCheckedCommand ?? (_tgBtnCheckedCommand = new RelayCommand(param => this.CanTgBtnChecked(), param => this.TgBtnChecked()));
            }
        }

        private ICommand _closeBtnCommand;
        public ICommand CloseBtnCommand
        {
            get
            {
                return _closeBtnCommand ?? (_closeBtnCommand = new RelayCommand(param => this.CanClose(), param => this.Close()));
            }
        }


        #endregion

        #region Commands

        private bool CanSelectView()
        {
            return true;
        }
        private void SelectView(object param)
        {
            if(param != null && param is int && (int)param < _views.Count)
            {
                CurrentView = _views[(int)param];
            }
        }

        private bool CanTgBtnUnchecked()
        {
            return true;
        }
        private void TgBtnUnchecked()
        {
            BgOpacity = 1;
        }

        private bool CanTgBtnChecked()
        {
            return true;
        }
        private void TgBtnChecked()
        {
            BgOpacity = 0.3;
        }

        private bool CanClose()
        {
            return true;
        }
        private void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

    }
}
