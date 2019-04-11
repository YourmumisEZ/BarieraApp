using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;

namespace BarieraApp
{
    public class MainService
    {
        public readonly List<string> callPermissions = new List<string>()
        {
            Manifest.Permission.CallPhone,
        };

        public readonly List<string> smsPermissions = new List<string>()
        {
            Manifest.Permission.ReadSms,
            Manifest.Permission.ReceiveSms
        };

        public static string CurrentPhoneNumber { get; set; }

        public static int PermissionCode { get; set; }


        public bool CheckIfBarieraServiceIsRunning()
        {
            var serviceName = new BarieraService().Class.Name;
            IEnumerable<string> runningServices = GetRunningServices();
            return runningServices.Any(x => x == serviceName);
        }

        public void SetPhoneNumber(string newPhoneNumber)
        {
            CurrentPhoneNumber = newPhoneNumber;
        }

        public bool IsValidaPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("07") && phoneNumber.Length == 10 && IsDigitsOnly(phoneNumber))
            {
                return true;
            }
            return false;
        }

        public string[] GetNeededPermissions(Context context)
        {
            var result = new List<string>();
            if (context.CheckSelfPermission(callPermissions.ElementAt(0)) != (int)Permission.Granted)
            {
                result.AddRange(callPermissions);
            }
            if (context.CheckSelfPermission(smsPermissions.ElementAt(0)) != (int)Permission.Granted)
            {
                result.AddRange(smsPermissions);
            }

            return result.ToArray();
        }

        private IEnumerable<string> GetRunningServices()
        {
            var manager = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
            return manager.GetRunningServices(int.MaxValue).Select(service => service.Service.ClassName).ToList();
        }

        private bool IsDigitsOnly(string phoneNumber)
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