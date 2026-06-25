namespace MantaroInclusivo.Domain.Entities
{
    public class Destino
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Rampas { get; set; }
        public bool BanosAccesibles { get; set; }
        public bool EstacionamientoReservado { get; set; }
        public bool AudioGuias { get; set; }
        public bool RutasTactiles { get; set; }
        public bool Pictogramas { get; set; }
        public bool PersonalCapacitado { get; set; }
        public bool SenalizacionBraille { get; set; }
        public string ImagenURL { get; set; }
        public bool Activo { get; set; }
    }
}