using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Buzzword.Common.PhoneNumberValidations
{
    public class PhoneNumberUa
    {
        private readonly string _inputPhone;
        private bool _isValid;
        private bool _isNumberDigital;

        public static readonly int PhoneNumberLength = 9;

        public string Code => "+380";

        public string Number { get; private set; }

        public string PhoneNumber => Code + Number;

        public PhoneNumberUa(string inputPhone)
        {
            _inputPhone = inputPhone;
            ParseInputPhone();
        }

        public bool IsValidDigitalNumber()
        {
            return _isValid && _isNumberDigital;
        }

        public bool IsNotValidDigitalNumber()
        {
            return !IsValidDigitalNumber();
        }

        public long GetDigitalNumber()
        {
            if (!IsValidDigitalNumber())
            {
                return 0L;
            }

            string withoutSymbols = PhoneNumber.Replace("+", "");
            return long.Parse(withoutSymbols);
        }

        private void ParseInputPhone()
        {
            string phoneNumber = CleanSymbols(_inputPhone);
            Number = ExtractNumber(Code, phoneNumber);
            _isValid = IsPhoneNumberValid(Number);
            _isNumberDigital = ValidateIsNumberDigital(Number);
        }

        private static string CleanSymbols(string inputStr)
        {
            if (String.IsNullOrWhiteSpace(inputStr))
            {
                return String.Empty;
            }

            List<char> numberChars = new List<char>();
            foreach (var inputChar in inputStr)
            {
                if (Char.IsDigit(inputChar) || inputChar == '+')
                {
                    numberChars.Add(inputChar);
                }
            }

            string cleanPhoneNumber = String.Join("", numberChars);
            return cleanPhoneNumber;
        }

        private static string ExtractNumber(string inputCode, string inputStr)
        {
            if (String.IsNullOrWhiteSpace(inputStr))
            {
                return String.Empty;
            }

            Result<string> codeSubstract = StartsWithCode(inputCode, inputStr);
            if (codeSubstract.IsOk)
            {
                return codeSubstract.Value;
            }

            return inputStr;
        }

        private static Result<string> StartsWithCode(string inputCode, string inputStr)
        {
            int length = inputCode.Length;
            for (int startIndex = 0; startIndex < length; ++startIndex)
            {
                string code = inputCode.Substring(startIndex);
                if (inputStr.StartsWith(code))
                {
                    return Result<string>.Ok(inputStr.Substring(code.Length));
                }
            }
            return Result<string>.Error();
        }

        private static bool IsPhoneNumberValid(string number)
        {
            string phoneNumber = CleanSymbols(number);
            if (phoneNumber.Length < PhoneNumberLength || phoneNumber.Length > PhoneNumberLength)
            {
                return false;
            }

            return Regex.Match(phoneNumber, @"^\+?\d{9}$").Success;
        }

        private static bool ValidateIsNumberDigital(string number)
        {
            if (String.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            return !number.Any(c => !Char.IsDigit(c));
        }
    }
}
