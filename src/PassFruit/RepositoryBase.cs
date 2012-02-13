﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PassFruit.Contracts;

namespace PassFruit {
    
    public abstract class RepositoryBase : IRepository {
        
        private IAccounts _accounts;

        private IAccountTags _accountTags;
        
        protected RepositoryBase() {
            
        }
        
        public abstract string Name { get; }

        public abstract string Description { get; }
        
        public IAccounts Accounts {
            get {
                if (_accounts == null) {
                    _accounts = GetAllAccounts();
                }
                return _accounts;
            }
        }

        public IAccountTags AccountTags {
            get {
                if (_accountTags == null) {
                    _accountTags = GetAllAccountTags();
                }
                return _accountTags;
            }
        }

        public abstract string GetPassword(Guid accountId);

        public abstract void SetPassword(Guid accountId, string password);

        protected abstract IAccounts GetAllAccounts();

        protected abstract IAccountTags GetAllAccountTags();
    }

}
