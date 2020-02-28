using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeTest.Models;
using CodeTest.Services;

namespace CodeTest.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {        
        private readonly ICustomerService _customerService;

        public CustomerController(CodeTestDBContext context, ICustomerService customerService)
        {           
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        [Route("api/customers")]
        public IEnumerable<Customers> GetCustomers()
        {
           return _customerService.GetCustomers();
        }

        // GET: api/custome/5 [HttpGet("{iban}")] //[FromRoute]
        [HttpGet]
        [Route("api/customer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }              

        // PUT: api/Customer/NL72RABO4358076548
        [HttpPut]
        [Route("api/customer/{iban}")]
        public async Task<IActionResult> PutCustomer([FromRoute]string iban, [FromBody] Customers customer)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerService.CreateNewCustomer(customer, iban);

                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // POST: api/account
        [HttpPost]
        [Route("api/account/{iban}")]
        public async Task<IActionResult> PostAccount([FromRoute] string iban, [FromBody] Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerService.CreateNewAccount(customer.Id, iban);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/saveaccount
        [HttpPost]
        [Route("api/saveaccount/{amount}")]
        public IActionResult PostAmount([FromRoute] double amount, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _customerService.SaveMoney(account.IBAN, amount);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}