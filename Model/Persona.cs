using SQLite;

namespace rmorenoS5.Model
{
    [Table("Persona")]
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(25)]
        public string Nombre { get; set; }
    }
}
