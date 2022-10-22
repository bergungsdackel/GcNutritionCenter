using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamDMA.Core.Model;

namespace GcNutritionCenter
{
    internal class Transaction : BaseModel
    {
        private ulong _TransactionID;
        public ulong TransactionID
        {
            get
            {
                return _TransactionID;
            }
            set
            {
                SetProperty(ref _TransactionID, value);
            }
        }

        private DateTime _TimeStamp;
        public DateTime TimeStamp
        {
            get
            {
                return _TimeStamp;
            }
            set
            {
                SetProperty(ref _TimeStamp, value);
            }
        }

        private Customer? _Customer;
        public Customer? Customer
        {
            get
            {
                return _Customer;
            }
            set
            {
                SetProperty(ref _Customer, value);
            }
        }

        private decimal _ChangedBalance;
        public decimal ChangedBalance
        {
            get
            {
                return _ChangedBalance;
            }
            set
            {
                SetProperty(ref _ChangedBalance, value);
            }
        }

        public Transaction(Customer? customer, decimal changedBalance = 0)
        {
            Customer = customer;
            ChangedBalance = changedBalance;
            TimeStamp = DateTime.Now;
            TransactionID = Hash(TimeStamp);
        }

        private ulong Hash(DateTime timestamp)
        {
            ulong kind = (ulong)(int)timestamp.Kind;
            return (kind << 62) | (ulong)timestamp.Ticks;
        }

    }
}
