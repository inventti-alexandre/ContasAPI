using System;

namespace Contas.Tests.Api.Integration.DTO
{
    public class ContaDTO
    {
        public bool success { get; set; }
        public ContaData data { get; set; }
    }

    public class ContaData
    {
        public string id { get; set; }
        public string nome { get; set; }
        public DateTime data { get; set; }
        public float valor { get; set; }
        public bool parcelado { get; set; }
        public int numeroParcelas { get; set; }
        public string observacao { get; set; }
        public string idCategoria { get; set; }
        public string idUsuario { get; set; }
        public DateTime timestamp { get; set; }
        public string messageType { get; set; }
        public string aggregateId { get; set; }
    }

}