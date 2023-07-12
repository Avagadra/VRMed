using SQLite4Unity3d;
using System;

public class BDTeoryForOrgans
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int IdOrgan { get; set; }
    public string Name { get; set; }
    public string Teory { get; set; }
    public string Image { get; set; }
   

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, IdOrgan={1},  Name={2}, Teory={3}, Image={4}", ID, IdOrgan, Name, Teory, Image);
    }
}
