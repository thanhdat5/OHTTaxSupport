using System;    

namespace OHTTaxSupportApplication.Common
{
    public static class Utilities
    {
        public static byte[] EncryptData(string data)
        {
            var md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var encoder = new System.Text.UTF8Encoding();
            var hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string Md5(string data)
        {
            return BitConverter.ToString(EncryptData(data)).Replace("-", "").ToLower();
        }

        public static string TimeAgo(this DateTime date)
        {
            var timeSince = DateTime.Now.Subtract(date);
            if (timeSince.TotalMilliseconds < 1) return "not yet";
            if (timeSince.TotalMinutes < 1) return "vừa đây";
            if (timeSince.TotalMinutes < 2) return "1 phút trước";
            if (timeSince.TotalMinutes < 60) return $"{timeSince.Minutes} phút trước";
            if (timeSince.TotalMinutes < 120) return "1 hour ago";
            if (timeSince.TotalHours < 24) return $"{timeSince.Hours} giờ trước";
            if (timeSince.TotalDays < 2) return "hôm qua";
            if (timeSince.TotalDays < 7) return $"{timeSince.Days} ngày trước";
            if (timeSince.TotalDays < 14) return "tuần trước";
            if (timeSince.TotalDays < 21) return "2 tuần trước";
            if (timeSince.TotalDays < 28) return "3 tuần trước";
            if (timeSince.TotalDays < 60) return "tháng trước";
            if (timeSince.TotalDays < 365) return $"{Math.Round(timeSince.TotalDays / 30)} tháng trước";
            return timeSince.TotalDays < 730 ? "năm trước" : $"{Math.Round(timeSince.TotalDays / 365)} năm trước";
        }
    }
}
