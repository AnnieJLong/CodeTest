
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTest.Models;

namespace CodeTest.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CodeTestDBContext _context;

        public CustomerService(CodeTestDBContext context)
        {
            _context = context;
        }

        public async Task CreateNewCustomer(Customers customer, string iban)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            await CreateNewAccount(customer.Id, iban);
        }

        public async Task CreateNewAccount(int customerId, string iban)
        {
            try
            {
                var existCustomer = _context.Customers.Find(customerId);
                if (existCustomer == null)
                {
                    throw new Exception("Customer not found");
                }

                var existAccount = _context.Account.FirstOrDefault(x => x.IBAN == iban);
                if (existAccount != null)
                {
                    throw new Exception("iban has been used.");
                }

                var account = new Account
                {
                    IBAN = iban,
                    Balance = 0,
                    Customer_Id = customerId
                };
                _context.Account.Add(account);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Customers> GetCustomers()
        {
            return _context.Customers;
        }

        public async Task<Customers> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public void SaveMoney(string iban, double amount)
        {
            var existAccount = _context.Account.FirstOrDefault(x => x.IBAN == iban);
            if (existAccount == null)
            {
                throw new Exception("Account not found");                
            }

            var balance = existAccount.Balance;

            existAccount.Balance = balance + amount;
            _context.Account.Update(existAccount);
        }

        public void TransferMoney(string ibanSend, string ibanReceived, double amount)
        {
            var sendAccount = _context.Account.FirstOrDefault(x => x.IBAN == ibanSend);
            if (sendAccount == null)
            {
                throw new Exception("Send account not found");
            }

            var receivedAccount = _context.Account.FirstOrDefault(x => x.IBAN == ibanReceived);
            if (receivedAccount == null)
            {
                throw new Exception("Received account not found");
            }

            var balanceSend = sendAccount.Balance;
            var balanceReceived = receivedAccount.Balance;

            if(amount > balanceSend)
            {
                throw new Exception("send account not enough amount");
            }

            sendAccount.Balance = balanceSend - amount;
            _context.Account.Update(sendAccount);

            receivedAccount.Balance = balanceReceived + amount;
            _context.Account.Update(receivedAccount);
        }
    }
}
