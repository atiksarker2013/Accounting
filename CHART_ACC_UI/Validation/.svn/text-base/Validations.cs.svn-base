using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace CHART_ACC_UI.Validation
{
    public class Validations
    {
        // ADD BY AMIN FOR VALIDATION CHECK
        //EDITED BY REFAT

        public static bool IsDouble(string strNumber)
        {
            if (strNumber.Contains(' '))
            {
                return false;
            }

            string[] a = strNumber.Split('.');

            if (a[0].Length > 18)
            {
                return false;
            }
            if (a.Length > 1)
            {
                if (a[1].Length > 2)
                {
                    return false;
                }
            }

            //Regex obDouble = new Regex(@"^([-]{1})?[0-9]*(\.{1})?[^(\.)0-9]*$");//Commented by Bindu,dATE:2009.11.07            
            if (strNumber.IndexOf('.') + 1 == strNumber.Length && strNumber != string.Empty)
            {
                strNumber = strNumber.Remove(strNumber.Length - 1, 1);
            }
            Regex obDouble = new Regex(@"^[-+]?\b\d+(\.\d+)?\b$");
            return obDouble.IsMatch(strNumber);

        }
        public static bool IsDouble(string strNumber, bool IsRate)
        {
            if (strNumber.Contains(' '))
            {
                return false;
            }

            string[] a = strNumber.Split('.');

            if (a[0].Length > 18)
            {
                return false;
            }
            if (a.Length > 1)
            {
                if (a[1].Length > 4)
                {
                    return false;
                }
            }

            //Regex obDouble = new Regex(@"^([-]{1})?[0-9]*(\.{1})?[^(\.)0-9]*$");//Commented by Bindu,dATE:2009.11.07            
            if (strNumber.IndexOf('.') + 1 == strNumber.Length && strNumber != string.Empty)
            {
                strNumber = strNumber.Remove(strNumber.Length - 1, 1);
            }
            Regex obDouble = new Regex(@"^[-+]?\b\d+(\.\d+)?\b$");
            return obDouble.IsMatch(strNumber);

        }

        public static bool TestEmailRegex(string EmailAddress)
        {
            Regex obEmail = new Regex(@"^([a-zA-Z0-9])+([\.a-zA-Z0-9_-])*@([a-zA-Z0-9])+(\.[a-zA-Z0-9_-]+)+$");
            return obEmail.IsMatch(EmailAddress);
        }
        public static bool IsWebsite(string strWebAddress)
        {
            Regex obWebSite = new Regex(@"^(http://)?www.[a-z0-9_-]+(\.){1}[a-z]+((\.){1})?([a-z])*[^\.]$");
            return obWebSite.IsMatch(strWebAddress);
        }
        public static bool IsInteger(string strInteger)
        {
            if (strInteger.Length > 18)
            {
                return false;
            }
            Regex obInteger = new Regex(@"^[0-9]*$");
            return obInteger.IsMatch(strInteger);
        }
        public static bool IsAlpha(string strString)
        {
            Regex obAlpha = new Regex(@"^[a-zA-Z]*$");
            return obAlpha.IsMatch(strString);
        }
        public static bool IsPhoneFax(string strPhoneFax)
        {
            Regex obPhoneFax = new Regex(@"^[+]?[0-9]*$");
            return obPhoneFax.IsMatch(strPhoneFax);
        }
        public static bool IsDecimal(string strNumber)
        {
            if (strNumber.Contains(' '))
            {
                return false;
            }

            string[] a = strNumber.Split('.');

            if (a[0].Length > 18)
            {
                return false;
            }
            if (a.Length > 1)
            {
                if (a[1].Length > 4)
                {
                    return false;
                }
            }

            //Regex obDouble = new Regex(@"^([-]{1})?[0-9]*(\.{1})?[^(\.)0-9]*$");//Commented by Bindu,dATE:2009.11.07            
            if (strNumber.IndexOf('.') + 1 == strNumber.Length && strNumber != string.Empty)
            {
                strNumber = strNumber.Remove(strNumber.Length - 1, 1);
            }
            Regex obDouble = new Regex(@"^\$?[+-]?[\d,]*(\.\d*)?$");
            return obDouble.IsMatch(strNumber);

        }


        public static bool IsEmail(string emailAddress)
        {

            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                  + @"[a-zA-Z]{2,}))$";

            Regex reStrict = new Regex(patternStrict);

            bool isStrictMatch = reStrict.IsMatch(emailAddress);

            return isStrictMatch;
        }
        

        public static bool IsValidTime(string thetime)
        {
            Regex checktime =
             new Regex(@"^(20|21|22|23|[01]d|d)(([:][0-5]d){1,2})$");

            return checktime.IsMatch(thetime);
        }

    }
}
