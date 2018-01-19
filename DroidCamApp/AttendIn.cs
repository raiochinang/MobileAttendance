using System;
using SQLite;
using SQLite.Net.Attributes;

namespace DroidCamApp
{
    public class AttendIn
    {
        [PrimaryKey, AutoIncrement]
        public int RecordID { get; set; }

        public string EmployeeID { get; set; }

        public DateTime Petsa { get; set; }

        //public override string ToString()
        //{
        //    return string.Format("[Person: ID={0}, FirstName={1}, LastName={2}]", ID, FirstName, LastName);
        //}

    }


}
