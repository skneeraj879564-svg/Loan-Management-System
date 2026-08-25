using Loan_Management_System_Business.Dtos.EmiCalculator;
using Loan_Management_System_Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class EmiCalculatorService:IEmiCalculatorService
    {
        public EmiCalculatorResponseDto CalculateEmi(
          EmiCalculatorDto model)
        {
            decimal monthlyInterestRate =
                model.AnnualInterestRate / 12 / 100;

            decimal monthlyEmi;

            if (monthlyInterestRate == 0)
            {
                monthlyEmi =
                    model.LoanAmount / model.TenureMonths;
            }
            else
            {
                double principal =
                    (double)model.LoanAmount;

                double rate =
                    (double)monthlyInterestRate;

                int months =
                    model.TenureMonths;

                double emi =
                    principal *
                    rate *
                    Math.Pow(1 + rate, months) /
                    (Math.Pow(1 + rate, months) - 1);

                monthlyEmi =
                    (decimal)emi;
            }

            monthlyEmi =
                Math.Round(monthlyEmi, 2);

            decimal totalPayment =
                monthlyEmi * model.TenureMonths;

            decimal totalInterest =
                totalPayment - model.LoanAmount;

            totalInterest =
                Math.Round(totalInterest, 2);

            totalPayment =
                Math.Round(totalPayment, 2);

            return new EmiCalculatorResponseDto
            {
                LoanAmount =
                    model.LoanAmount,

                AnnualInterestRate =
                    model.AnnualInterestRate,

                TenureMonths =
                    model.TenureMonths,

                MonthlyEMI =
                    monthlyEmi,

                TotalInterest =
                    totalInterest,

                TotalPayment =
                    totalPayment
            };
        }

    }
}
