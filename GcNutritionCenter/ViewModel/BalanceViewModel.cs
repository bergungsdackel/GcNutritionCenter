using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                // also update the textboxes
                txtBoxUserID = _SelectedCustomer.UserID.ToString();
                txtBoxFirstName = _SelectedCustomer.FirstName!;
                txtBoxLastName = _SelectedCustomer.LastName!;
                txtBoxBalance = _SelectedCustomer.Balance.ToString();
            }
        }

        private string _txtBoxUserID;
        public string txtBoxUserID
        {
            get
            {
                return _txtBoxUserID;
            }
            set
            {
                SetProperty(ref _txtBoxUserID, value);
            }
        }
        private string _txtBoxFirstName;
        public string txtBoxFirstName
        {
            get
            {
                return _txtBoxFirstName;
            }
            set
            {
                SetProperty(ref _txtBoxFirstName, value);
            }
        }
        private string _txtBoxLastName;
        public string txtBoxLastName
        {
            get
            {
                return _txtBoxLastName;
            }
            set
            {
                SetProperty(ref _txtBoxLastName, value);
            }
        }
        private string _txtBoxBalance;
        public string txtBoxBalance
        {
            get
            {
                return _txtBoxBalance;
            }
            set
            {
                SetProperty(ref _txtBoxBalance, value);
            }
        }

        public BalanceViewModel()
        {
            // TODO: here maybe load from file/server?
            CustomerList = JsonFile.ReadFromFile<ObservableCollection<Customer>>(fileName);
            if(CustomerList == null)
            {
                CustomerList = new ObservableCollection<Customer>();
            }

            // test data
            //_CustomerList.Add(new Customer(firstName: "TestFirstName1", lastName: "TestLastName1"));
            //_CustomerList.Add(new Customer(firstName: "TestFirstName2", lastName: "TestLastName2"));
            // test data end
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

        #region Commands definitions

        private ICommand _addBalanceCommand;
        public ICommand AddBalanceCommand
        {
            get
            {
                return _addBalanceCommand ?? (_addBalanceCommand = new RelayCommand(param => this.CanAddBalance(), param => this.AddBalance()));
            }
        }

        private ICommand _addCustomerCommand;
        public ICommand AddCustomerCommand
        {
            get
            {
                return _addCustomerCommand ?? (_addCustomerCommand = new RelayCommand(param => this.CanAddCustomer(), param => this.AddCustomer()));
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

                        //placeholder
                        _curCustomer!.Balance += 100.5;


                        //save after edit
                        this.Save();
                    }
                }
            }
        }

        private bool CanAddBalance()
        {
            return true;
        }
        private void AddBalance()
        {
            if(IsItemSelected)
            {
                string addBalanceValue = CustomDialog.Show("Wie viel Guthaben soll hinzugefügt werden?", "Guthaben hinzufügen", inputType: CustomDialog.InputType.Text);
                double addValue = Double.Parse(addBalanceValue);
            }
            

        }

        public bool CanAddCustomer()
        {
            return true;
        }

        public void AddCustomer()
        {
            //placeholder
            CustomerList.Add(new Customer(firstName: "TestNewCustomer", lastName: "TestNewCustomerLastName", balance: 1337.3));

            // TODO: Dialog with create customer

            this.Save();
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
