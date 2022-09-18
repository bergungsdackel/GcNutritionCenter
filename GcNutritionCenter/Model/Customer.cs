using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace GcNutritionCenter
{
    internal class Customer : INotifyPropertyChanged
    {
        RandomNumberGenerator rng = RandomNumberGenerator.Create();

        private int _UserID;
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
                OnPropertyChanged();
            }
        }
        private string? _FirstName;
        public string? FirstName
        {
            get
            {
                return _FirstName;
            }
            set 
            { 
                _FirstName = value;
                OnPropertyChanged(); 
            }
        }
        private string? _LastName;
        public string? LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        private double _Balance;
        public double Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                _Balance = value;
                OnPropertyChanged();
            }
        }

        public Customer(string? firstName = "", string? lastName = "", double balance = 0)
        {
            UserID = Get5Digits();
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }

        public int Get5Digits()
        {
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            return (int)(BitConverter.ToUInt32(bytes, 0) % 100000);
        }


        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
