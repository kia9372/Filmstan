using Domain.Aggregate.DomainAggregates.PostMagAggregate;
using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.UserAggregate
{
    public class User : Aggregates, IAggregateMarker
    {
        #region Backing Field
        private string _userName;
        private string _name;
        private string _family;
        private string _phoneNumber;
        private string? _email;
        private string? _photo;
        private Guid _securityStamp;
        private string _password;
        private bool _isActive;
        private bool _isLockedEnd;
        private bool _confirmPhoneNumber;
        private bool _confirmEmail;
        private int _accountFaile;
        private DateTimeOffset? _lockedEnd;
        #endregion
        #region Properties
        public string Username { get => _userName; set => SetWithNotify(value, ref _userName); }
        public string Name { get => _name; set => SetWithNotify(value, ref _name); }
        public string Family { get => _family; set => SetWithNotify(value, ref _family); }
        public string Password { get => _password; set => SetWithNotify(value, ref _password); }
        public string PhoneNumber { get => _phoneNumber; set => SetWithNotify(value, ref _phoneNumber); }
        public Guid SecurityStamp { get => _securityStamp; set => SetWithNotify(value, ref _securityStamp); }
        public string? Email { get => _email; set => SetWithNotify(value, ref _email); }
        public string? Photo { get => _photo; set => SetWithNotify(value, ref _photo); }
        public int AccountFaile { get => _accountFaile; set => SetWithNotify(value, ref _accountFaile); }
        public bool IsActive { get => _isActive; set => SetWithNotify(value, ref _isActive); }
        public bool IsLockedEnd { get => _isLockedEnd; set => SetWithNotify(value, ref _isLockedEnd); }
        public bool ConfirmPhoneNumber { get => _confirmPhoneNumber; set => SetWithNotify(value, ref _confirmPhoneNumber); }
        public bool ConfirmEmail { get => _confirmEmail; set => SetWithNotify(value, ref _confirmEmail); }
        public DateTimeOffset? LockedEnd { get => _lockedEnd; set => SetWithNotify(value, ref _lockedEnd); }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ActivationCode> ActivationCodes { get; set; }
        public ICollection<PostMagazine> PostMagazines { get; set; }
        #endregion
        #region CTOR
        public User()
        {

        }
        public User(string userName, string password, string name, string family, string phoneNumber, string email = null, string photo = null)
        {
            SetProperies(userName, name, family, phoneNumber, email, photo);
            ChangePassword(password);
        }
        #endregion

        #region SetValues
        public void SetProperies(string userName, string name, string family, string phoneNumber, string email = null, string photo = null)
        {
            Username = userName;
            Name = name;
            Family = family;
            Password = Password;
            PhoneNumber = phoneNumber;
            Email = email;
            Photo = photo;
            IsActive = false;
            IsLockedEnd = false;
            ConfirmEmail = false;
            ConfirmPhoneNumber = false;
            LockedEnd = null;
            UpdateSecurityStamp();
        }
        public void ConfirmedPhoneNumber()
        {
            ConfirmPhoneNumber = true;
            IsActive = true;
            UpdateSecurityStamp();
        }
        public void UserChangeActiveStatus(bool status)
        {
            IsActive = !status;
            UpdateSecurityStamp();
        }
        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            ConfirmPhoneNumber = false;
            IsActive = false;
            UpdateSecurityStamp();
        }
        public void ChangeEmail(string email)
        {
            UpdateSecurityStamp();
            Email = email;
            ConfirmEmail = false;
        }
        public void ConfirmedEmail()
        {
            ConfirmEmail = true;
            UpdateSecurityStamp();
        }
        public void AccountFailed()
        {
            AccountFaile++;
            if (AccountFaile > 5)
            {
                LockedEnabled();
            }
        }
        public void LockedEnabled()
        {
            IsLockedEnd = true;
            LockedEnd = DateTimeOffset.UtcNow.AddDays(5);
            UpdateSecurityStamp();
        }
        public void UpdateSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid();
        }
        public void ChangePassword(string password)
        {
            UpdateSecurityStamp();
            Password = password;
        }
        #endregion
    }
}
