namespace BlazorTarefa.Shared.Models 
{
    // Classe Tarefa apenas com os Campos, para carregar no Client
    public class Tarefa
    {
        // Chave
        public Guid Id { get; set; }

        // Dados
        public string Descricao { get; set; } = string.Empty;
        public bool Concluido { get; set; }
        public DateTimeOffset CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? ConcluidoEm { get; set; }

        // Auto-Referência
        public Guid? IdPai { get; set; }

        // Navegação
        public List<Tarefa> SubTarefas { get; set; } = new List<Tarefa>();
    }
}