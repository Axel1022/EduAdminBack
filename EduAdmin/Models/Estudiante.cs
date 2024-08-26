namespace EduAdmin.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public int? IdCarrera { get; set; }
        public Carrera? Carrera { get; set; }
        public int? IdAdmitidoComo { get; set; }
        public AdmitidoComo? AdmitidoComo { get; set; }
    }
}
