using SQLite4Unity3d;
using System;


public class BDTypeOrgan
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return string.Format("[Person: Id={0}, Name={2}", ID, Name);
        }
    }
