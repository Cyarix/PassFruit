﻿using System;
using System.Collections.Generic;

namespace PassFruit.DataStore.Contracts {

    public interface IDataStore {

        string Name { get; }

        string Description { get; }

        IEnumerable<Guid> GetActiveAccountIds();

        IEnumerable<IAccountDto> GetActiveAccountDtos();

        IEnumerable<Guid> GetDeletedAccountIds();

        IAccountDto GetAccountDto(Guid accountId);

        void SaveAccountDto(IAccountDto accountDto);

        void DeleteAccount(Guid accountId);

        IEnumerable<IPasswordDto> GetPasswordDtos(Guid accountId);

        void SavePasswordDto(Guid accountId, IPasswordDto passwordDto);

        void DeleteAccountPasswords(Guid accountId);

    }

}
