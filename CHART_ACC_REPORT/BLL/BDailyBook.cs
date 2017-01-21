using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;

namespace CHART_ACC_REPORT.BLL
{
    public class BDailyBook
    {
        DayBoookDAL objDayBookDal = new DayBoookDAL();

        public List<EDailyBook> GetAllDailyTransaction(string Journal_Date)
        {
            return objDayBookDal.GetAllDailyTransaction(Journal_Date);
        }

        public List<EDailyBook> GetAllDailyTransactionReceive(string Journal_Date)
        {
            return objDayBookDal.GetAllDailyTransactionReceive(Journal_Date);
        }

        public List<EDailyBook> GetAllDailyTransactionPayment(string Journal_Date)
        {
            return objDayBookDal.GetAllDailyTransactionPayment(Journal_Date);
        }

        public List<EDailyBook> GetAllDailyTransactionJournal(string Journal_Date)
        {
            return objDayBookDal.GetAllDailyTransactionJournal(Journal_Date);
        }

        public List<EDailyBook> GetAllDailyTransactionContra(string Journal_Date)
        {
            return objDayBookDal.GetAllDailyTransactionContra(Journal_Date);
        }

        public int GetTotalDepositVoucher(string Journal_Date)
        {
            return objDayBookDal.GetTotalDepositVoucher(Journal_Date);
        }

        public int GetTotalPaymentVoucher(string Journal_Date)
        {
            return objDayBookDal.GetTotalPaymentVoucher(Journal_Date);
        }

        public int GetTotalJournalVoucher(string Journal_Date)
        {
            return objDayBookDal.GetTotalJournalVoucher(Journal_Date);
        }

        public int GetTotalContraVoucher(string Journal_Date)
        {
            return objDayBookDal.GetTotalContraVoucher(Journal_Date);
        }

        // Between Date Range

        public List<EDailyBook> GetTransaction_DateWise(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTransaction_DateWise(From_Date, To_Date);
        }

        public List<EDailyBook> GetTransaction_DateWise_Receive(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTransaction_DateWise_Receive(From_Date, To_Date);
        }

        public List<EDailyBook> GetTransaction_DateWise_Payment(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTransaction_DateWise_Payment(From_Date, To_Date);
        }

        public List<EDailyBook> GetTransaction_DateWise_Journal(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTransaction_DateWise_Journal(From_Date, To_Date);
        }

        public List<EDailyBook> GetTransaction_DateWise_Contra(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTransaction_DateWise_Contra(From_Date, To_Date);
        }

        public int GetTotalDepositVoucher_DateWise(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTotalDepositVoucher_DateWise(From_Date, To_Date);
        }

        public int GetTotalPaymentVoucher_DateWise(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTotalPaymentVoucher_DateWise(From_Date, To_Date);
        }

        public int GetTotalJournalVoucher_DateWise(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTotalJournalVoucher_DateWise(From_Date, To_Date);
        }

        public int GetTotalContraVoucher_DateWise(string From_Date, string To_Date)
        {
            return objDayBookDal.GetTotalContraVoucher_DateWise(From_Date, To_Date);
        }
    }
}
