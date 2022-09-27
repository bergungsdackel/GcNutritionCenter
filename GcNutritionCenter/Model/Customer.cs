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
    internal class Customer : BaseModel
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
                SetProperty(ref _UserID, value);
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
                SetProperty(ref _FirstName, value);
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
                SetProperty(ref _LastName, value);
            }
        }
        private decimal _Balance;
        public decimal Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                SetProperty(ref _Balance, value);
            }
        }

        public Customer(string? firstName = "", string? lastName = "", decimal balance = 0)
        {
            // TODO: Set unique ID, check with a given list?
            UserID = Get5Digits();
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }

        private int Get5Digits()
        {
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            return (int)(BitConverter.ToUInt32(bytes, 0) % 100000);
        }

        public Transaction ChangeBalance(decimal balance)
        {
            this.Balance += balance;
            Transaction transaction = new Transaction(this, balance);
            return transaction;
        }

    }
}
