namespace ListaDeTarefas.Application.DTO
{
    public record TarefaDTO(int TarefaId, string Titulo, string Descricao, DateTime DataEntrega, bool Finalizada, int FkUsuario);
}
