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

            // test data
            //_CustomerList.Add(new Customer(firstName: "TestFirstName1", lastName: "TestLastName1"));
            //_CustomerList.Add(new Customer(firstName: "TestFirstName2", lastName: "TestLastName2"));
            // test data end

            // TODO: here maybe load from file/server?
            string jsonString = File.ReadAllText(fileName);
            _CustomerList = JsonSerializer.Deserialize<List<Customer>>(jsonString)!;
        }


        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                mUpdater ??= new UpdateCustomer(this); // if null create new UpdateCustomer
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        #region Commands

        private class UpdateCustomer : ICommand
        {
            BalanceViewModel? _parent;

            public UpdateCustomer(BalanceViewModel? parent)
            {
                if(parent != null)
                {
                    _parent = parent;
                }
            }

            #region ICommand Members 

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                if (_parent?.SelectedCustomer != null)
                {
                    Customer currentSelectedCustomer = _parent._CustomerList[_parent.CustomerList.FindIndex(x => x.UserID == _parent.SelectedCustomer.UserID)];
                    currentSelectedCustomer.FirstName = _parent.txtBoxFirstName;
                    currentSelectedCustomer.LastName = _parent.txtBoxLastName;
                    currentSelectedCustomer.Balance = Int32.Parse(_parent.txtBoxBalance);
                }


                // convert in json and save after every refresh
                string jsonString = JsonSerializer.Serialize(_parent?.CustomerList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, jsonString);
            }

            #endregion

        }

        #endregion
    }
}
