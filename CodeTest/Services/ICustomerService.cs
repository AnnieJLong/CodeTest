using CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Services
{
    public interface ICustomerService
    {
        Task CreateNewCustomer(Customers customer, string iban);

        Task CreateNewAccount(int customerId, string iban);

        IEnumerable<Customers> GetCustomers();

        bool CustomerExists(int id);

        Task<Customers> GetCustomer(int id);

        void SaveMoney(string iban, double amount);

        void TransferMoney(string ibanSend, string ibanReceived, double amount);
    }
}
