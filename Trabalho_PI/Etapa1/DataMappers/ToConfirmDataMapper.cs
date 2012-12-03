using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Etapa1.Models;

namespace Etapa1.DataMappers
{
    public class ToConfirmDataMapper : IDataMapper<AccountModel, String>
    {
        List<AccountModel> _toConfirm;
        private static ToConfirmDataMapper _toConfirmDataMapper;

        private ToConfirmDataMapper()
        {
            _toConfirm = new List<AccountModel>();
        }

        public static ToConfirmDataMapper GetAccountDataMapper()
        {
            return _toConfirmDataMapper ?? (_toConfirmDataMapper = new ToConfirmDataMapper());
        }

        public void Add(AccountModel t)
        {
            _toConfirm.Add(t);
        }

        public void Remove(AccountModel t)
        {
            _toConfirm.Remove(t);
        }

        public AccountModel GetById(String confirmationCode)
        {
            return _toConfirm.Find(account => account.ConfirmationCode == confirmationCode);
        }

        public IEnumerable<AccountModel> GetAll()
        {
            throw new NotSupportedException();
        }
    }
}