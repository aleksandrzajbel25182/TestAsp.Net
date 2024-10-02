using ProductDB.Entitys;
using System.Collections.ObjectModel;

namespace ClientTest.Wpf.Service
{
    internal interface IPhoneApiService
    {
        Task<ObservableCollection<Phone>> GetPhonesAsync();
        Task<Phone> GetPhoneByIdAsync(int phoneId);
        Task<bool> AddPhoneAsync(Phone phone);
        Task<bool> UpdatePhoneAsync(Phone phone);
        Task<bool> DeletePhoneAsync(int phoneId);
    }
}
