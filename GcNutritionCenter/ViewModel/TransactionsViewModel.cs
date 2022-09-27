using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            // TODO: here maybe load from file/server?
            var tmpList = JsonFile.ReadFromFile<ObservableCollection<Transaction>>(fileName);
            if(tmpList != null)
            {
                TransactionList = tmpList;
            }           

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


        public override void Dispose()
        {
            // TODO: Save to file/server
            this.Save();


            // call from base
            base.Dispose();
        }

    }
}
