using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Collections;
using System.Text.Json.Serialization;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using TeamDMA.Core.Model;

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

        [JsonConstructor]
        public Customer()
        {
            
        }
        public Customer(string? firstName = "", string? lastName = "", decimal balance = 0, IEnumerable<Customer>? collectionToCheckForID = null)
        {
            UserID = Get5Digits();

            //Set unique ID, check with a given list ?
            if (collectionToCheckForID != null)
            {
                while (collectionToCheckForID.FirstOrDefault(x => x.UserID == UserID) != null)
                {
                    UserID = Get5Digits();
                }
            }

            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }

        private int Get5Digits()
        {
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            int result = (int)(BitConverter.ToUInt32(bytes, 0) % 100000);
            if(CountDigits(result) != 5)
            {
                return Get5Digits();
            }
            return result;
        }

        private int CountDigits(int number)
        {
            return (int)Math.Floor(Math.Log10(number) + 1);
        }

        public Transaction ChangeBalance(decimal balance)
        {
            this.Balance += balance;
            Transaction transaction = new Transaction(this, balance);
            return transaction;
        }

    }
}
