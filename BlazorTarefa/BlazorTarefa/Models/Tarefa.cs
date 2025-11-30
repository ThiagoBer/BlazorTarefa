namespace BlazorTarefa.Models 
{
    // Classe Tarefa herdada do Shared, com as regras de negocios, usada pelo Server
    public class Tarefa : Shared.Models.Tarefa
    {
        // Construtor
        public Tarefa() { }

        // Força o Entity a usar a coleção da classe rica (estava dando problema com o TarefaConfig sem essas linhas)
        public new virtual ICollection<Tarefa> SubTarefas { get; set; } = new List<Tarefa>();
        //

        // Para o Construtor Publico
        private Tarefa(string descricao, Guid? idPai = null)
        {
            Descricao = descricao;
            IdPai = idPai;
        }
        //
        // Metodos
        //
        // Criação
        public static Tarefa Criar(string descricao, Guid? idPai = null)
            => new Tarefa(descricao, idPai);
        // Concluir Tarefa
        public void MarcarConcluida()
        {
            if (Concluido && ConcluidoEm is not null) return;
            Concluido = true;
            ConcluidoEm = DateTime.UtcNow;
        }
        // Voltar Tarefa Concluida
        public void DesmarcarConcluida()
        {
            Concluido = false;
            ConcluidoEm = null;
        }
        // Alterar Descrição
        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao)) throw new ArgumentException("Descrição não pode ser vazia");
            Descricao = descricao.Trim();
        }
    }
}