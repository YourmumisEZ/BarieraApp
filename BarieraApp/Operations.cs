using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;

namespace BarieraApp
{
    public static class Operations
    {
        public static string CurrentPhoneNumber { get; set; }

        private static IEnumerable<string> GetRunningServices()
        {
            var manager = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
            return manager.GetRunningServices(int.MaxValue).Select(
                service => service.Service.ClassName).ToList();
        }

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
    }
}