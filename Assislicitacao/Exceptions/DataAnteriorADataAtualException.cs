namespace Assislicitacao.Exceptions {
    public class DataAnteriorADataAtualException : Exception{
        public DataAnteriorADataAtualException(): base($"Data informada é mais antiga que a data atual: {DateTime.Now.ToString("dd/MM/yyyy")}") { }
    }
}
