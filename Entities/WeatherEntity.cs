using System.ComponentModel.DataAnnotations;

namespace redesAPI.Entities
{
    public class WeatherEntity
    {
        public int Id { get; set; }  // Mapea a la columna "id"
        public DateTime Date { get; set; }  // Mapea a la columna "date"
        public int Temperature { get; set; }  // Mapea a la columna "temperature"
        public string? Summary { get; set; }  // Mapea a la columna "summary"
    }
}
