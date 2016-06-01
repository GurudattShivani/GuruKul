using System.Collections.Generic;
using System.ComponentModel;

namespace Gurukul.Web.ViewModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class CustomerList
    {
        public string ListName { get; set; }
        public IList<Customer> Customers { get; set; }
    }
}