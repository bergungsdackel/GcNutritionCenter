using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GcNutritionCenter
{
    internal class TransactionsViewModel : BaseViewModel
    {
        const string fileName = "transactions.json";

        private ObservableCollection<Transaction> _TransactionList;
        public ObservableCollection<Transaction> TransactionList
        {
            get
            {
                return _TransactionList;
            }
            set
            {
                _TransactionList = value;
            }
        }

        private Transaction _SelectedTransaction;
        public Transaction SelectedTransaction
        {
            get
            {
                return _SelectedTransaction;
            }
            set
            {
                _SelectedTransaction = value;
            }
        }

        public TransactionsViewModel(object parent) : base(parent)
        {
            TransactionList = new ObservableCollection<Transaction>();
            var tmpList = JsonFile.ReadFromFile<ObservableCollection<Transaction>>(fileName);
            if(tmpList != null)
            {
                TransactionList = new ObservableCollection<Transaction>(tmpList);
            }

            // add events
            TransactionList.CollectionChanged += TransactionListChanged;

        }

        private void TransactionListChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.Save();
        }

        ~TransactionsViewModel()
        {
            this.Dispose();
        }


        public void Save()
        {
            // convert in json and save
            JsonFile.SaveToFile(TransactionList, fileName);
        }

        #region Command definitions

        private ICommand _lostFocusCommand;
        public ICommand LostFocusCommand
        {
            get
            {
                return _lostFocusCommand ?? (_lostFocusCommand = new RelayCommand(param => true, param => this.OnLostFocus(param)));
            }
        }

        #endregion

        #region Command

        private void OnLostFocus(object param)
        {
            if (param != null && param is DataGrid)
            {
                DataGrid transactionsGrid = (DataGrid)param;
                transactionsGrid.UnselectAll();
            }
        }

        #endregion

        public override void Dispose()
        {
            this.Save();


            // call from base
            base.Dispose();
        }

    }
}
