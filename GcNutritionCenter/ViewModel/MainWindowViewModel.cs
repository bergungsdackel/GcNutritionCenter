using System;
using System.Windows;
using System.Windows.Input;
using TeamDMA.Core.ViewModel;
using TeamDMA.Core.Helper;

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

        private Visibility _ttHomeVisibility;
        public Visibility TtHomeVisibility
        {
            get
            {
                return _ttHomeVisibility;
            }
            set
            {
                SetProperty(ref _ttHomeVisibility, value);
            }
        }
        private Visibility _ttBalanceVisibility;
        public Visibility TtBalanceVisibility
        {
            get
            {
                return _ttBalanceVisibility;
            }
            set
            {
                SetProperty(ref _ttBalanceVisibility, value);
            }
        }
        private Visibility _ttTransactionsVisibility;
        public Visibility TtTransactionsVisibility
        {
            get
            {
                return _ttTransactionsVisibility;
            }
            set
            {
                SetProperty(ref _ttTransactionsVisibility, value);
            }
        }
        private Visibility _ttSettingsVisibility;
        public Visibility TtSettingsVisibility
        {
            get
            {
                return _ttSettingsVisibility;
            }
            set
            {
                SetProperty(ref _ttSettingsVisibility, value);
            }
        }

        private bool _TgBtnIsChecked =  false;
        public bool TgBtnIsChecked
        {
            get
            {
                return _TgBtnIsChecked;
            }
            set
            {
                SetProperty(ref _TgBtnIsChecked, value);
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

        // HOME
        private Home _HomeView;
        public Home HomeView
        {
            get
            {
                return _HomeView;
            }
        }
        private HomeViewModel _HomeViewModel;
        public HomeViewModel HomeViewModel
        {
            get
            {
                return _HomeViewModel;
            }
        }
        // BALANCE
        private Balance _BalanceView;
        public Balance BalanceView
        {
            get
            {
                return _BalanceView;
            }
        }
        private BalanceViewModel _BalanceViewModel;
        public BalanceViewModel BalanceViewModel
        {
            get
            {
                return _BalanceViewModel;
            }
        }
        // TRANSACTION
        private Transactions _TransactionsView;
        public Transactions TransactionsView
        {
            get
            {
                return _TransactionsView;
            }
        }
        private TransactionsViewModel _TransactionsViewModel;
        public TransactionsViewModel TransactionsViewModel
        {
            get
            {
                return _TransactionsViewModel;
            }
        }
        //SETTINGS
        private Settings _SettingsView;
        public Settings SettingsView
        {
            get
            {
                return _SettingsView;
            }
        }
        private SettingsViewModel _SettingsViewModel;
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return _SettingsViewModel;
            }
        }

        private const int countViews = 4;

        public MainWindowViewModel(object parent) : base(parent)
        {
            // HOME
            _HomeViewModel = new HomeViewModel(this);
            _HomeView = new Home()
            {
                DataContext = _HomeViewModel
            };
            // BALANCE
            _BalanceViewModel = new BalanceViewModel(this);
            _BalanceView = new Balance()
            {
                DataContext = _BalanceViewModel
            };
            // TRANSACTION
            _TransactionsViewModel = new TransactionsViewModel(this);
            _TransactionsView = new Transactions()
            {
                DataContext = _TransactionsViewModel
            };
            // SETTINGS
            _SettingsViewModel = new SettingsViewModel(this);
            _SettingsView = new Settings()
            {
                DataContext = _SettingsViewModel
            };

            CurrentView = HomeView; // home

            // if crash
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // save everything
            BalanceViewModel.Save();
            TransactionsViewModel.Save();
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

        private ICommand _mouseEnterCommand;
        public ICommand MouseEnterCommand
        {
            get
            {
                return _mouseEnterCommand ?? (_mouseEnterCommand = new RelayCommand(param => this.CanMouseEnter(), param => this.MouseEnter()));
            }
        }

        private ICommand _previewMouseLeftButtonDownCommand;
        public ICommand PreviewMouseLeftButtonDownCommand
        {
            get
            {
                return _previewMouseLeftButtonDownCommand ?? (_previewMouseLeftButtonDownCommand = new RelayCommand(param => this.CanPreviewMouseLeftButtonDown(), param => this.PreviewMouseLeftButtonDown()));
            }
        }


        #endregion

        public void CloseNavigationBar()
        {
            TgBtnIsChecked = false;
        }

        #region Commands

        private bool CanSelectView()
        {
            return true;
        }
        private void SelectView(object param)
        {
            if(param != null && param is int && (int)param < countViews)
            {
                switch((int)param)
                {
                    case 0:
                        CurrentView = HomeView;
                        break;
                    case 1:
                        CurrentView = BalanceView;
                        break;
                    case 2:
                        CurrentView = TransactionsView;
                        break;
                    case 3:
                        CurrentView = SettingsView;
                        break;
                    default:
                        break;
                }
                CloseNavigationBar();
            }
        }

        private bool CanPreviewMouseLeftButtonDown()
        {
            return true;
        }
        private void PreviewMouseLeftButtonDown()
        {
            CloseNavigationBar();
        }

        private bool CanMouseEnter()
        {
            return true;
        }
        private void MouseEnter()
        {
            // Set tooltip visibility

            if (TgBtnIsChecked == true)
            {
                TtHomeVisibility = Visibility.Collapsed;
                TtBalanceVisibility = Visibility.Collapsed;
                TtTransactionsVisibility = Visibility.Collapsed;
                TtSettingsVisibility = Visibility.Collapsed;
            }
            else
            {
                TtHomeVisibility = Visibility.Visible;
                TtBalanceVisibility = Visibility.Visible;
                TtTransactionsVisibility = Visibility.Visible;
                TtSettingsVisibility = Visibility.Visible;
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
            BalanceViewModel.Save();
            TransactionsViewModel.Save();

            System.Windows.Application.Current.Shutdown();
        }

        #endregion

    }
}
