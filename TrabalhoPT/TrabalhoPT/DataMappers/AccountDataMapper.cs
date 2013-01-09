using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabalhoPT.Models;
using TrabalhoPT.Utils;

namespace TrabalhoPT.DataMappers
{
    public class AccountDataMapper : IDataMapper<AccountModel, String>
    {
        List<AccountModel> _accounts;
        private static AccountDataMapper _accountDataMapper;

        private AccountDataMapper()
        {
            _accounts = new List<AccountModel>();
            /*Insert Data*/
            var am1 = new AccountModel { Username = "piadmin", Email = "andre.fc.figueiredo@gmail.com", Password = "123ABC", Confirmed = true };
            LoginUtils.EncryptPassword(am1);
            am1.Roles.Add("Admin");
            //----------------------
            var am2 = new AccountModel { Username = "Gertrudes", Email = "gertrudes@gmail.com", Password = "1", Confirmed = true };
            LoginUtils.EncryptPassword(am2);
            am2.Roles.Add("Admin");
            //----------------------
            var am3 = new AccountModel { Username = "Andre", Email = "andre@gmail.com", Password = "1", Confirmed = true };
            LoginUtils.EncryptPassword(am3);
            am3.Roles.Add("Admin");
            //----------------------
            var am4 = new AccountModel { Username = "Filipe", Email = "filipe@gmail.com", Password = "1", Confirmed = true };
            LoginUtils.EncryptPassword(am4);
            am4.Roles.Add("Admin");
            //----------------------
            var am5 = new AccountModel { Username = "Joao", Email = "joao@gmail.com", Password = "1", Confirmed = true };
            LoginUtils.EncryptPassword(am5);
            am5.Roles.Add("Admin");
            /*************************/
            var bm = BoardDataMapper.GetBoardDataMapper();
            am1.AddBoard(bm.GetById(1));
            am1.AddBoard(bm.GetById(2));
            _accounts.Add(am1);
            _accounts.Add(am2);
            _accounts.Add(am3);
            _accounts.Add(am4);
            _accounts.Add(am5);
        }

        public static AccountDataMapper GetAccountDataMapper()
        {
            return _accountDataMapper ?? (_accountDataMapper = new AccountDataMapper());
        }

        public void Add(AccountModel t)
        {
            t.Username = t.Username.ToLower();
            _accounts.Add(t);
        }

        public void Remove(AccountModel t)
        {
            _accounts.Remove(t);
        }

        public AccountModel GetById(String name)
        {
            name = name.ToLower();
            return _accounts.Find(account => account.Username.ToLower() == name);
        }

        public IEnumerable<AccountModel> GetAll()
        {
            return _accounts;
        }
    }
}