using SQLite4Unity3d;
using System;

public class BDTestResult
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IDStudent { get; set; }
    public string DatePassage { get; set; }
    public string NameTest { get; set; }
    public int Score { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, IDStudent={1},  DatePassage={2}, NameTest={3}, Score={4}]", Id, IDStudent, DatePassage, NameTest, Score);
    }
}
