using SQLite4Unity3d;
using System;

public class BDAccount
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int AccessLevel { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string DateOfBirth { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, AccessLevel={1},  Name={2}, Surname={3}, DateOfBirth={4},  Login={5}, Password={6}]", Id, AccessLevel, Name, Surname, DateOfBirth, Login, Password);
    }
}
