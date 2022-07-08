namespace Buzzword.Common.PhoneNumberValidations
{
    public static class PhoneNumberValidator
    {
        public static bool IsPhoneNumberValid(string inputPhone)
        {
            var phoneUa = new PhoneNumberUa(inputPhone);
            return phoneUa.IsValidDigitalNumber();
        }
    }
}
