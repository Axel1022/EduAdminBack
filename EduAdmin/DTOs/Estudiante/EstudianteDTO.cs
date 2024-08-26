namespace EduAdmin.DTOs.Estudiante
{
    public class EstudianteDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public int? IdCarrera { get; set; }
        public int? IdAdmitidoComo { get; set; }
    }
}
