using Android.Content;

namespace BarieraApp.Interfaces
{
    public interface IMainService
    {
        bool CheckIfBarieraServiceIsRunning();
        string[] GetNeededPermissions(Context context);
        string GetSelectedPhone();
        bool IsValidPhoneNumber(string phoneNumber);
        void SetPhoneNumber(string newPhoneNumber);
    }
}