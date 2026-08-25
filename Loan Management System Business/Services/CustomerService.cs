using Loan_Management_System_Business.Dtos.Customer;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // =========================
        // GET BY CUSTOMER ID
        // =========================

        public async Task<CustomerDto?> GetByIdAsync(int customerId)
        {
            var customer =
                await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return null;
            }

            return MapToDto(customer);
        }


        // =========================
        // GET BY USER ID
        // =========================

        public async Task<CustomerDto?> GetByUserIdAsync(string userId)
        {
            var customer =
                await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null)
            {
                return null;
            }

            return MapToDto(customer);
        }


        // =========================
        // GET ALL CUSTOMERS
        // =========================

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers =
                await _customerRepository.GetAllAsync();

            return customers
                .Select(MapToDto)
                .ToList();
        }


        // =========================
        // CREATE CUSTOMER
        // =========================

        public async Task<CustomerDto> CreateAsync(
            string userId,
            CreateCustomerDto model)
        {
            var customer = new Customer
            {
                UserId = userId,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                CustomerCode = model.CustomerCode,
                PanNumber = model.PanNumber,
                AadhaarNumber = model.AadhaarNumber
            };

            // Address
            if (model.Address != null)
            {
                customer.Address = new Address
                {
                    AddressLine = model.Address.AddressLine,
                    City = model.Address.City,
                    State = model.Address.State,
                    PinCode = model.Address.PinCode,
                    Country = model.Address.Country,
                    AddressType = model.Address.AddressType
                };
            }

            // Employment
            if (model.EmploymentDetail != null)
            {
                customer.EmploymentDetail =
                    new EmploymentDetail
                    {
                        EmploymentType =
                            model.EmploymentDetail.EmploymentType,

                        CompanyName =
                            model.EmploymentDetail.CompanyName,

                        Designation =
                            model.EmploymentDetail.Designation,

                        MonthlyIncome =
                            model.EmploymentDetail.MonthlyIncome,

                        AnnualIncome =
                            model.EmploymentDetail.AnnualIncome,

                        TotalExperienceYears =
                            model.EmploymentDetail.TotalExperienceYears,

                        CurrentJobExperienceYears =
                            model.EmploymentDetail.CurrentJobExperienceYears,

                        CompanyAddress =
                            model.EmploymentDetail.CompanyAddress,

                        OfficePhoneNumber =
                            model.EmploymentDetail.OfficePhoneNumber,

                        JoiningDate =
                            model.EmploymentDetail.JoiningDate
                    };
            }

            // Bank Account
            if (model.BankAccount != null)
            {
                customer.BankAccount =
                    new BankAccount
                    {
                        BankName =
                            model.BankAccount.BankName,

                        AccountHolderName =
                            model.BankAccount.AccountHolderName,

                        AccountNumber =
                            model.BankAccount.AccountNumber,

                        IFSCCode =
                            model.BankAccount.IFSCCode,

                        AccountType =
                            model.BankAccount.AccountType,

                        BranchName =
                            model.BankAccount.BranchName,

                        IsPrimary =
                            model.BankAccount.IsPrimary
                    };
            }

            // Nominee
            if (model.Nominee != null)
            {
                customer.Nominee =
                    new Nominee
                    {
                        FullName =
                            model.Nominee.FullName,

                        Relationship =
                            model.Nominee.Relationship,

                        DateOfBirth =
                            model.Nominee.DateOfBirth,

                        PhoneNumber =
                            model.Nominee.PhoneNumber,

                        Address =
                            model.Nominee.Address,

                        AadhaarNumber =
                            model.Nominee.AadhaarNumber
                    };
            }

            var result =
                await _customerRepository.AddAsync(customer);

            return MapToDto(result);
        }


        // =========================
        // UPDATE CUSTOMER
        // =========================

        public async Task<CustomerDto?> UpdateAsync(
            int customerId,
            UpdateCustomerDto model)
        {
            var customer =
                await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return null;
            }

            customer.FullName = model.FullName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.Gender = model.Gender;
            customer.DateOfBirth = model.DateOfBirth;
            customer.PanNumber = model.PanNumber;
            customer.AadhaarNumber = model.AadhaarNumber;

            // Update Address
            if (model.Address != null &&
                customer.Address != null)
            {
                customer.Address.AddressLine =
                    model.Address.AddressLine;

                customer.Address.City =
                    model.Address.City;

                customer.Address.State =
                    model.Address.State;

                customer.Address.PinCode =
                    model.Address.PinCode;

                customer.Address.Country =
                    model.Address.Country;

                customer.Address.AddressType =
                    model.Address.AddressType;
            }

            // Update Employment
            if (model.EmploymentDetail != null &&
                customer.EmploymentDetail != null)
            {
                customer.EmploymentDetail.EmploymentType =
                    model.EmploymentDetail.EmploymentType;

                customer.EmploymentDetail.CompanyName =
                    model.EmploymentDetail.CompanyName;

                customer.EmploymentDetail.Designation =
                    model.EmploymentDetail.Designation;

                customer.EmploymentDetail.MonthlyIncome =
                    model.EmploymentDetail.MonthlyIncome;

                customer.EmploymentDetail.AnnualIncome =
                    model.EmploymentDetail.AnnualIncome;

                customer.EmploymentDetail.TotalExperienceYears =
                    model.EmploymentDetail.TotalExperienceYears;

                customer.EmploymentDetail.CurrentJobExperienceYears =
                    model.EmploymentDetail.CurrentJobExperienceYears;

                customer.EmploymentDetail.CompanyAddress =
                    model.EmploymentDetail.CompanyAddress;

                customer.EmploymentDetail.OfficePhoneNumber =
                    model.EmploymentDetail.OfficePhoneNumber;

                customer.EmploymentDetail.JoiningDate =
                    model.EmploymentDetail.JoiningDate;
            }

            // Update Bank Account
            if (model.BankAccount != null &&
                customer.BankAccount != null)
            {
                customer.BankAccount.BankName =
                    model.BankAccount.BankName;

                customer.BankAccount.AccountHolderName =
                    model.BankAccount.AccountHolderName;

                customer.BankAccount.AccountNumber =
                    model.BankAccount.AccountNumber;

                customer.BankAccount.IFSCCode =
                    model.BankAccount.IFSCCode;

                customer.BankAccount.AccountType =
                    model.BankAccount.AccountType;

                customer.BankAccount.BranchName =
                    model.BankAccount.BranchName;

                customer.BankAccount.IsPrimary =
                    model.BankAccount.IsPrimary;
            }

            // Update Nominee
            if (model.Nominee != null &&
                customer.Nominee != null)
            {
                customer.Nominee.FullName =
                    model.Nominee.FullName;

                customer.Nominee.Relationship =
                    model.Nominee.Relationship;

                customer.Nominee.DateOfBirth =
                    model.Nominee.DateOfBirth;

                customer.Nominee.PhoneNumber =
                    model.Nominee.PhoneNumber;

                customer.Nominee.Address =
                    model.Nominee.Address;

                customer.Nominee.AadhaarNumber =
                    model.Nominee.AadhaarNumber;
            }

            var result =
                await _customerRepository.UpdateAsync(customer);

            return MapToDto(result);
        }


        // =========================
        // DELETE CUSTOMER
        // =========================

        public async Task<bool> DeleteAsync(int customerId)
        {
            return await _customerRepository
                .DeleteAsync(customerId);
        }


        // =========================
        // MODEL → DTO
        // =========================

        private static CustomerDto MapToDto(
            Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                UserId = customer.UserId,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Gender = customer.Gender,
                DateOfBirth = customer.DateOfBirth,
                CustomerCode = customer.CustomerCode,
                PanNumber = customer.PanNumber,
                AadhaarNumber = customer.AadhaarNumber
            };
        }
    }
}