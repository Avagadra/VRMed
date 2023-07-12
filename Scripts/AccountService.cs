using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountService 
{
    DB dB;
    public AccountService()
    {
        dB = new DB();
    }

    //управление таблицей аккаунтов
    public void CreateAccountTable()
    {
        dB.GetConnection().DropTable<BDAccount>();
        dB.GetConnection().CreateTable<BDAccount>();
    }
    public int AddAccount(BDAccount account)
    {
        return dB.GetConnection().Insert(account);
    }
    public int DeleteAccount(BDAccount account)
    {
        return dB.GetConnection().Delete(account);
    }
    public int UpdateAccount(BDAccount account)
    {
        return dB.GetConnection().Update(account);
    }
    public IEnumerable<BDAccount> GetAccounts()
    {
        return dB.GetConnection().Table<BDAccount>();
    }
    public BDAccount GetAccountId(int id)
    {
        return dB.GetConnection().Table<BDAccount>().Where(x => x.Id == id).FirstOrDefault();
    }
    public IEnumerable<BDAccount> GetAccountForNameSurname(string name, string surname)
    {
        return dB.GetConnection().Table<BDAccount>().Where(x => x.Name == name).Where(y => y.Surname == surname);
    }
    public BDAccount GetAccountAuth(string login, string password)
    {
        return dB.GetConnection().Table<BDAccount>().Where(x => x.Login == login).Where(y => y.Password == password).FirstOrDefault();
    }
    public BDAccount GetAccountNameSurname(string name, string surname)
    {
        return dB.GetConnection().Table<BDAccount>().Where(x => x.Name == name).Where(y => y.Surname == surname).FirstOrDefault();
    }

    //управление таблицей органов
    public int AddOrgan(BDOrgans organ)
    {
        return dB.GetConnection().Insert(organ);
    }
    public int DeleteOrgan(BDOrgans organ)
    {
        return dB.GetConnection().Delete(organ);
    }
    public int UpdateOrgan(BDOrgans organ)
    {
        return dB.GetConnection().Update(organ);
    }
    public IEnumerable<BDOrgans> GetOrgans()
    {
        return dB.GetConnection().Table<BDOrgans>();
    }
    public BDOrgans GetOrganId(int id)
    {
        return dB.GetConnection().Table<BDOrgans>().Where(x => x.ID == id).FirstOrDefault();
    }
    public BDOrgans GetOrganByName(string name)
    {
        return dB.GetConnection().Table<BDOrgans>().Where(x => x.Name == name).FirstOrDefault();
    }
    public BDOrgans GetOrganByType(int idType)
    {
        return dB.GetConnection().Table<BDOrgans>().Where(x => x.IdType == idType).FirstOrDefault();
    }
    public IEnumerable<BDOrgans> GetOrgansByType(int idType)
    {
        return dB.GetConnection().Table<BDOrgans>().Where(x => x.IdType == idType);
    }

    //управление таблицей типов органов
    public int AddTypeOrgan(BDTypeOrgan type)
    {
        return dB.GetConnection().Insert(type);
    }
    public int DeleteTypeOrgan(BDTypeOrgan type)
    {
        return dB.GetConnection().Delete(type);
    }
    public int UpdateTypeOrgan(BDTypeOrgan type)
    {
        return dB.GetConnection().Update(type);
    }
    public IEnumerable<BDTypeOrgan> GetTypeOrgan()
    {
        return dB.GetConnection().Table<BDTypeOrgan>();
    }
    public BDTypeOrgan TypeOrgan(int id)
    {
        return dB.GetConnection().Table<BDTypeOrgan>().Where(x => x.ID == id).FirstOrDefault();
    }

    //управление таблицей теории
    public int AddTeory(BDTeoryForOrgans teory)
    {
        return dB.GetConnection().Insert(teory);
    }      
    public int DeleteTeory(BDTeoryForOrgans teory)
    {
        return dB.GetConnection().Delete(teory);
    }
    public int UpdateTeory(BDTeoryForOrgans teory)
    {
        return dB.GetConnection().Update(teory);
    }
    public IEnumerable<BDTeoryForOrgans> GetTeory()
    {
        return dB.GetConnection().Table<BDTeoryForOrgans>();
    }
    public BDTeoryForOrgans GetTeoryId(int id)
    {
        return dB.GetConnection().Table<BDTeoryForOrgans>().Where(x => x.ID == id).FirstOrDefault();
    }
    public IEnumerable<BDTeoryForOrgans> GetTeoryByOrgan(int idOrgan)
    {
        return dB.GetConnection().Table<BDTeoryForOrgans>().Where(x => x.IdOrgan == idOrgan);
    }
    public BDTeoryForOrgans GetTeoryByName(string name)
    {
        return dB.GetConnection().Table<BDTeoryForOrgans>().Where(x => x.Name == name).FirstOrDefault();
    }


}
