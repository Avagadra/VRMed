using SQLite4Unity3d;
using System;

public class BDOrgans
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string NameLatin { get; set; }
    public string Name { get; set; }
    public int IdType { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, LatinName={2}, Name={3}, IdType = {4}", ID, NameLatin, Name, IdType);
    }
}
