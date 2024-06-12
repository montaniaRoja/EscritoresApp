
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteMaxLength = SQLite.MaxLengthAttribute;


namespace EscritoresApp.Models
{
    [SQLite.Table("Autor")]
    public class Autor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [SQLiteMaxLength(255), NotNull]
        public string? Nombres { get; set; }

        [SQLiteMaxLength(255), NotNull]
        public string? Nacionalidad { get; set; }
        public string? Foto { get; set; }
    }
}
