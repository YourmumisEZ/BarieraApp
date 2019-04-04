using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;

namespace BarieraApp
{
    public static class Operations
    {
        public static string CurrentPhoneNumber { get; set; }

        public static bool CheckIfBarieraServiceIsRunning()
        {
            var serviceName = new BarieraService().Class.Name;
            IEnumerable<string> runningServices = GetRunningServices();
            return runningServices.Any(x => x == serviceName);
        }

        public static void SetPhoneNumber(string newPhoneNumber)
        {
            CurrentPhoneNumber = newPhoneNumber;
        }

        public static bool IsValidaPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("07") && phoneNumber.Length == 10 && IsDigitsOnly(phoneNumber))
            {
                return true;
            }
            return false;
        }

        private static IEnumerable<string> GetRunningServices()
        {
            var manager = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
            return manager.GetRunningServices(int.MaxValue).Select(
                service => service.Service.ClassName).ToList();
        }

        private static bool IsDigitsOnly(string phoneNumber)
        {
            foreach (char item in phoneNumber)
            {
                if (item < '0' || item > '9')
                {
                    return false;
                }
            }

            return true;
        }
    }
}