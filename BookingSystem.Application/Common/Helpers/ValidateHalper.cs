namespace BookingSystem.Application.Common.Helpers
{
    public static class ValidateHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            // Basic email format check
            return email.Contains("@") && email.Contains(".");
        }
    }
}