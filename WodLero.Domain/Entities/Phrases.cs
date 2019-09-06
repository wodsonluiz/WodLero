using System;

namespace WodLero.Domain.Entities
{
    public class Phrases
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTime Data_Registro { get; set; }
        public string Autor { get; set; }
    }
}
