using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
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


        private List<Customer> _CustomerList;
        public List<Customer> CustomerList
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
            _CustomerList = new List<Customer>();

            // TODO: here maybe load from file/server?
            string jsonString = File.ReadAllText(fileName);
            _CustomerList = JsonSerializer.Deserialize<List<Customer>>(jsonString)!;

            // test data
            //_CustomerList.Add(new Customer(firstName: "TestFirstName1", lastName: "TestLastName1"));
            //_CustomerList.Add(new Customer(firstName: "TestFirstName2", lastName: "TestLastName2"));
            // test data end
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

        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new RelayCommand(param => this.CanUpdate(), param => this.Update()));
            }
        }

        #endregion


        #region Commands

        private bool CanAddBalance()
        {
            return true;
        }
        private void AddBalance()
        {
            string addBalanceValue = CustomDialog.Show("Wie viel Guthaben soll hinzugefügt werden?", "Guthaben hinzufügen", inputType: CustomDialog.InputType.Text);
        }

        public bool CanUpdate()
        {
            return true;
        }

        public void Update()
        {
            if (SelectedCustomer != null)
            {
                Customer currentSelectedCustomer = _CustomerList[CustomerList.FindIndex(x => x.UserID == SelectedCustomer.UserID)];
                currentSelectedCustomer.FirstName = txtBoxFirstName;
                currentSelectedCustomer.LastName = txtBoxLastName;
                currentSelectedCustomer.Balance = Int32.Parse(txtBoxBalance);
            }


            // convert in json and save after every refresh
            string jsonString = JsonSerializer.Serialize(CustomerList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }

        #endregion
    }
}
