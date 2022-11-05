using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamDMA.Core.Model;

namespace GcNutritionCenter.Model
{
    internal class Configuration : BaseModel
    {
        private bool _deleteCustomerIfZero;
        public bool DeleteCustomerIfZero
        {
            get
            {
                return _deleteCustomerIfZero;
            }
            set
            {
                SetProperty(ref _deleteCustomerIfZero, value);
            }
        }
    }
}
