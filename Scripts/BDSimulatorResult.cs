using SQLite4Unity3d;
using System;

public class BDSimulatorResult
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IDStudent { get; set; }
    public string DatePassage { get; set; }
    public string NameSimulator { get; set; }
    public string Passed { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, IDStudent={1},  DatePassage={2}, NumSimulator={3}, Passed={4}]", Id, IDStudent, DatePassage, NameSimulator, Passed);
    }
}
