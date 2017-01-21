using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;

namespace CHART_ACC_REPORT.DAL
{
    public class DayBoookDAL
    {
        ReportDataContext objDataContext = new ReportDataContext();

        public List<EDailyBook> GetAllDailyTransaction(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE ==Convert.ToDateTime(Journal_Date) orderby dayBook.JV_CREDIT_AMOUNT select dayBook ;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount =Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount =Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date =Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetAllDailyTransactionReceive(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE == Convert.ToDateTime(Journal_Date) && (dayBook.JV_BANK_REMARK == "Cheque Deposit" || dayBook.JV_BANK_REMARK == "Cash Deposit") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetAllDailyTransactionPayment(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE == Convert.ToDateTime(Journal_Date) && (dayBook.JV_BANK_REMARK == "Cheque Payment" || dayBook.JV_BANK_REMARK == "Cash Payment") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetAllDailyTransactionJournal(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE == Convert.ToDateTime(Journal_Date) && (dayBook.JV_BANK_REMARK == "Journal") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetAllDailyTransactionContra(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE == Convert.ToDateTime(Journal_Date) && (dayBook.JV_BANK_REMARK == "Contra Voucher") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public int GetTotalDepositVoucher(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            int total_Deposit = 0;
            foreach (var c in objDataContext.SP_TOTAL_DEPOSIT_VOUCHER_FOR_DAY_BOOK_BY_DATE(Convert.ToDateTime(Journal_Date)))
            {
                total_Deposit = Convert.ToInt32(c.TOTAL_DEBIT);
            }
            return total_Deposit;
        }

        public int GetTotalPaymentVoucher(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            int total_Payment = 0;
            foreach (var c in objDataContext.SP_TOTAL_PAYMENT_VOUCHER_FOR_DAY_BOOK_BY_DATE(Convert.ToDateTime(Journal_Date)))
            {
                total_Payment = Convert.ToInt32(c.TOTAL_CREDIT);
            }
            return total_Payment;
        }

        public int GetTotalJournalVoucher(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            int total_Journal = 0;
            foreach (var c in objDataContext.SP_TOTAL_JOURNAL_VOUCHER_FOR_DAY_BOOK_BY_DATE(Convert.ToDateTime(Journal_Date)))
            {
                total_Journal = Convert.ToInt32(c.TOTAL_JOURNAL);
            }
            return total_Journal;
        }

        public int GetTotalContraVoucher(string Journal_Date)
        {
            objDataContext = new ReportDataContext();
            int total_Contra = 0;
            foreach (var c in objDataContext.SP_TOTAL_CONTRA_VOUCHER_FOR_DAY_BOOK_BY_DATE(Convert.ToDateTime(Journal_Date)))
            {
                total_Contra = Convert.ToInt32(c.TOTAL_CONTRA);
            }
            return total_Contra;
        }

        // Between Date Renge

        public List<EDailyBook> GetTransaction_DateWise(string From_Date,string To_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE >= Convert.ToDateTime(From_Date) && dayBook.JV_DATE <= Convert.ToDateTime(To_Date) orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetTransaction_DateWise_Receive(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where (dayBook.JV_DATE >= Convert.ToDateTime(From_Date) && dayBook.JV_DATE <= Convert.ToDateTime(To_Date)) && (dayBook.JV_BANK_REMARK == "Cheque Deposit" || dayBook.JV_BANK_REMARK == "Cash Deposit") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetTransaction_DateWise_Payment(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where (dayBook.JV_DATE >= Convert.ToDateTime(From_Date) && dayBook.JV_DATE <= Convert.ToDateTime(To_Date)) && (dayBook.JV_BANK_REMARK == "Cheque Payment" || dayBook.JV_BANK_REMARK == "Cash Payment") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetTransaction_DateWise_Journal(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE >= Convert.ToDateTime(From_Date) && dayBook.JV_DATE <= Convert.ToDateTime(To_Date) && (dayBook.JV_BANK_REMARK == "Journal") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public List<EDailyBook> GetTransaction_DateWise_Contra(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            List<EDailyBook> DailyBookList = new List<EDailyBook>();

            var query = from dayBook in objDataContext.JOURNAL_VOUCHARs where dayBook.JV_DATE >= Convert.ToDateTime(From_Date) && dayBook.JV_DATE <= Convert.ToDateTime(To_Date) && (dayBook.JV_BANK_REMARK == "Contra Voucher") orderby dayBook.JV_CREDIT_AMOUNT select dayBook;

            foreach (var c in query)
            {
                EDailyBook objEDailyBook = new EDailyBook();
                objEDailyBook.Account_Name = c.JV_ACCOUNT_NAME;
                objEDailyBook.Account_Code = c.JV_ACCOUNT_CODE;
                objEDailyBook.Voucher_Id = c.JV_ID;
                objEDailyBook.Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEDailyBook.Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEDailyBook.Access_By = c.JV_ACCESS_BY;
                objEDailyBook.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEDailyBook.Journal_Notes = c.JV_NOTES;
                DailyBookList.Add(objEDailyBook);
            }
            return DailyBookList;
        }

        public int GetTotalDepositVoucher_DateWise(string From_Date,string To_Date)
        {
            objDataContext = new ReportDataContext();
            int totalDeposit = 0;
            foreach (var c in objDataContext.SP_TOTAL_DEPOSIT_VOUCHER_FOR_DAY_BOOK_BETWEEN_DATE(Convert.ToDateTime(From_Date),Convert.ToDateTime(To_Date)))
            {
                totalDeposit = Convert.ToInt32(c.TOTAL_DEBIT);
            }
            return totalDeposit;
        }

        public int GetTotalPaymentVoucher_DateWise(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            int totalPayment = 0;
            foreach (var c in objDataContext.SP_TOTAL_PAYMENT_VOUCHER_FOR_DAY_BOOK_BETWEEN_DATE(Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date)))
            {
                totalPayment = Convert.ToInt32(c.TOTAL_CREDIT);
            }
            return totalPayment;
        }

        public int GetTotalJournalVoucher_DateWise(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            int totalJournal = 0;
            foreach (var c in objDataContext.SP_TOTAL_JOURNAL_VOUCHER_FOR_DAY_BOOK_BETWEEN_DATE(Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date)))
            {
                totalJournal = Convert.ToInt32(c.TOTAL_JOURNAL);
            }
            return totalJournal;
        }

        public int GetTotalContraVoucher_DateWise(string From_Date, string To_Date)
        {
            objDataContext = new ReportDataContext();
            int totalContra = 0;
            foreach (var c in objDataContext.SP_TOTAL_CONTRA_VOUCHER_FOR_DAY_BOOK_BETWEEN_DATE(Convert.ToDateTime(From_Date), Convert.ToDateTime(To_Date)))
            {
                totalContra = Convert.ToInt32(c.TOTAL_CONTRA);
            }
            return totalContra;
        }

    }
}
