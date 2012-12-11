using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Etapa1.DataMappers;
using Etapa1.Models;

namespace Etapa1.Providers
{
    public class AccountRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            IEnumerable<string> roles = AccountDataMapper.GetAccountDataMapper().GetById(username).Roles;
            return roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var acc = AccountDataMapper.GetAccountDataMapper().GetById(username);
            if(acc == null)
                return new string[]{};
            return acc.Roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}