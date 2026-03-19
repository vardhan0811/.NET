using System.Text.RegularExpressions;

namespace StudentPortalDb.Helpers
{
    public static class MaskingHelper
    {
        public static string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return email;

            return Regex.Replace(email,
                @"(^.).*(@.*$)",
                "$1*****$2");
        }
    }
}