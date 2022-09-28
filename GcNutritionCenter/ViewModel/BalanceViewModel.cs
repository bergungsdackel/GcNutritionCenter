using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GcNutritionCenter
{
    internal class BalanceViewModel : BaseViewModel
    {
        const string fileName = "data.json";


        private ObservableCollection<Customer> _CustomerList;
        public ObservableCollection<Customer> CustomerList
        {
            get 
            { 
                return _CustomerList; 
            }
            set
            {
                _CustomerList = value;
            }
        }

        public bool IsItemSelected
        {
            get
            {
                return _SelectedCustomer != null;
            }
        }

        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }
            set
            {
                _SelectedCustomer = value;
            }
        }

        public BalanceViewModel(object parent) : base(parent)
        {
            CustomerList = new ObservableCollection<Customer>();
            // TODO: here maybe load from file/server?
            var tmpList = JsonFile.ReadFromFile<ObservableCollection<Customer>>(fileName);
            if (tmpList != null)
            {
                CustomerList = tmpList;
            }
            
            // add events
            foreach(Customer customer in CustomerList)
            {
                customer.PropertyChanged += CustomerChanged;
            }
            CustomerList.CollectionChanged += CustomerListChanged;
        }

        ~BalanceViewModel()
        {
            this.Dispose();
        }

        public void Save()
        {
            // convert in json and save
            JsonFile.SaveToFile(CustomerList, fileName);
        }

        #region Events

        private void CustomerChanged(object? sender, EventArgs e)
        {
            this.Save();
        }

        private void CustomerListChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(Customer customer in e.NewItems!)
                {
                    customer.PropertyChanged -= CustomerChanged;
                    customer.PropertyChanged += CustomerChanged;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Customer customer in e.NewItems!)
                {
                    customer.PropertyChanged -= CustomerChanged;
                }
            }

            this.Save();
        }

        #endregion

        #region Commands definitions

        private ICommand _addCustomerCommand;
        public ICommand AddCustomerCommand
        {
            get
            {
                return _addCustomerCommand ?? (_addCustomerCommand = new RelayCommand(param => this.CanAddCustomer(), param => this.AddCustomer()));
            }
        }

        private ICommand _deleteCustomerCommand;
        public ICommand DeleteCustomerCommand
        {
            get
            {
                return _deleteCustomerCommand ?? (_deleteCustomerCommand = new RelayCommand(param => this.CanDeleteCustomer(), param => this.DeleteCustomer()));
            }
        }

        private ICommand _changeCustomerBalanceCommand;
        public ICommand ChangeCustomerBalanceCommand
        {
            get
            {
                return _changeCustomerBalanceCommand ?? (_changeCustomerBalanceCommand = new RelayCommand(param => this.CanChangeCustomerBalance(), param => this.ChangeCustomerBalance(param)));
            }
        }

        #endregion

        #region Commands

        private bool CanChangeCustomerBalance()
        {
            return true;
        }
        private void ChangeCustomerBalance(object param)
        {
            if(param != null && param is System.Windows.Controls.DataGridCellInfo)
            {
                System.Windows.Controls.DataGridCellInfo cell = (System.Windows.Controls.DataGridCellInfo)param;

                if(cell.Column.Header.Equals("Guthaben"))
                {
                    if(cell.Item != null && cell.Item is Customer)
                    {
                        Customer? _curCustomer = cell.Item as Customer;

                        // TODO: Dialog to add or subtract balance
                        if (IsItemSelected)
                        {
                            string addBalanceValue = CustomDialog.Show("Wie viel Guthaben soll hinzugefügt werden?", "Guthaben hinzufügen", inputType: CustomDialog.InputType.Text);
                            if(addBalanceValue != String.Empty)
                            {
                                addBalanceValue = addBalanceValue.Replace('.', ',');
                                decimal addValue;
                                if(Decimal.TryParse(addBalanceValue, out addValue))
                                {
                                    Transaction generatedTransaction = _curCustomer!.ChangeBalance(addValue);
                                    MainWindowViewModel? parentVM = ParentViewModel as MainWindowViewModel;
                                    if(parentVM != null)
                                    {
                                        parentVM.TransactionsViewModel.TransactionList.Add(generatedTransaction);
                                    }
                                }
                                else
                                {
                                    // not a number
                                }                           
                            }
                        }
                    }
                }
            }
        }

        public bool CanAddCustomer()
        {
            return true;
        }
        public void AddCustomer()
        {
            //placeholder
            CustomerList.Add(new Customer(firstName: "TestNewCustomer", lastName: "TestNewCustomerLastName"));

            // TODO: Dialog with create customer

        }

        public bool CanDeleteCustomer()
        {            
            return (SelectedCustomer != null);
        }
        public void DeleteCustomer()
        {
            try
            {
                if(SelectedCustomer != null)
                {
                    // TODO: ask if also delete every transaction related to customer
                    if(true) // placeholder for dialog
                    {
                        MainWindowViewModel? parentVM = ParentViewModel as MainWindowViewModel;
                        if(parentVM != null)
                        {
                            foreach(Transaction transaction in parentVM.TransactionsViewModel.TransactionList.ToArray())
                            {
                                if(transaction.Customer!.UserID == SelectedCustomer.UserID)
                                {
                                    parentVM.TransactionsViewModel.TransactionList.Remove(transaction);
                                }
                            }
                        }
                    }

                    // TODO: Dialog, "are you sure..."
                    CustomerList.Remove(SelectedCustomer);           
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion

        public override void Dispose()
        {
            // TODO: Save to file/server
            this.Save();


            // call from base
            base.Dispose();
        }
    }
}
