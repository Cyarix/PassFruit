﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using PassFruit.Contracts;
using PassFruit.Ui.Wp.Views.Controls;

namespace PassFruit.Ui.Wp.Views {

    public class AccountDetailsPageViewModel : Screen {

        private IAccount _account;
        private bool _passwordLoaded;

        public AccountDetailsPageViewModel() {

        }

        public IEnumerable<TagViewModel> Tags {
            get { return _account.Tags.Select(accountTag => new TagViewModel(accountTag)); }
        }

        private Guid _accountId;
        public Guid AccountId {
            get { return _accountId; }
            set {
                _accountId = value;
                LoadAccount();
            }
        }

        public void LoadAccount() {
            _passwordLoaded = false;
            var repository = Init.GetRepository();
            _account = repository.Accounts[_accountId];
            NotifyOfPropertyChange(() => AccountName);
            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => Email);
            NotifyOfPropertyChange(() => IsEmailEnabled);
            NotifyOfPropertyChange(() => UserName);
            NotifyOfPropertyChange(() => IsUserNameEnabled);
            NotifyOfPropertyChange(() => Notes);
            NotifyOfPropertyChange(() => Tags);
            Password = "*****";
            AccountIcon = new AccountProviderIconViewModel(_account.Provider, 32);
        }

        public void PopulatePassword() {
            if (!_passwordLoaded) {
                Password = _account.GetPassword();
            }
        }

        protected override void OnDeactivate(bool close) {
            SaveAccount();
            base.OnDeactivate(close);
        }

        private void SaveAccount() {
            if (_passwordLoaded) {
                _account.SetPassword(Password);
            }
            _account.Save();
        }

        public Visibility IsUserNameEnabled {
            get {
                return _account.Provider.HasUserName
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility IsEmailEnabled {
            get {
                return _account.Provider.HasEmail
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public string Title {
            get { return "PASSFRUIT | " + AccountName; }
        }

        public string AccountName {
            get { return _account.GetAccountName(); }
        }

        public string Email {
            get {
                return _account.Provider.HasEmail
                    ? _account.GetDefaultField(FieldTypeName.Email).Value
                    : "";
            }
            set {
                if (!_account.Provider.HasEmail) return;

                _account.GetDefaultField(FieldTypeName.Email).Value = value;

                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string UserName {
            get {
                return _account.Provider.HasUserName
                    ? _account.GetDefaultField(FieldTypeName.UserName).Value
                    : "";
            }
            set {
                if (!_account.Provider.HasUserName) return;

                _account.GetDefaultField(FieldTypeName.UserName).Value = value;

                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string Notes {
            get { return _account.Notes; }
            set { _account.Notes = value; NotifyOfPropertyChange(() => Notes); }
        }

        public void CopyPassword() {
            Clipboard.SetText(_account.GetPassword());
            MessageBox.Show("Password copied");
        }

        public void ShowPassword() {
            MessageBox.Show("Password is '" + _account.GetPassword() + "'");
        }

        public void CopyUserName() {
            Clipboard.SetText(UserName);
            MessageBox.Show("Username copied: '" + UserName + "'");
        }

        public void CopyEmail() {
            Clipboard.SetText(Email);
            MessageBox.Show("Email copied: '" + Email + "'");
        }

        public string AccountType {
            get { return _account.Provider.Name; }
        }

        private AccountProviderIconViewModel _accountIcon;
        public AccountProviderIconViewModel AccountIcon {
            get { return _accountIcon; }
            set {
                _accountIcon = value;
                NotifyOfPropertyChange(() => AccountIcon);
            }
        }

        private string _password;
        public string Password {
            get { return _password; }
            set { _password = value; NotifyOfPropertyChange(() => Password);} }
        }

}