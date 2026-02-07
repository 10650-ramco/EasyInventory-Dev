
// Application/DTOs/CustomerDto.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Application.DTOs
{
    public class CustomerDto : INotifyPropertyChanged
    {
        public int CustomerId { get; set; }

        private string _customerCode = string.Empty;
        public string CustomerCode
        {
            get => _customerCode;
            set => SetProperty(ref _customerCode, value);
        }

        private string _customerName = string.Empty;
        public string CustomerName
        {
            get => _customerName;
            set => SetProperty(ref _customerName, value);
        }

        private string _customerType = string.Empty;
        public string CustomerType
        {
            get => _customerType;
            set => SetProperty(ref _customerType, value);
        }

        private string? _gstin;
        public string? GSTIN
        {
            get => _gstin;
            set => SetProperty(ref _gstin, value);
        }

        private string? _pan;
        public string? PAN
        {
            get => _pan;
            set => SetProperty(ref _pan, value);
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string? _phoneNumber;
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string? PlaceOfSupplyState { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string? GSTRegistrationType { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
