namespace Hospital.Dominio.Base;

public interface IRepositoryBase<T> where T : class, IId
{
    void Adicionar(T entidade);
    void Alterar(T entidade);
    void Excluir(int id);

    T ConsultaUnica(Func<T, bool> filtro);
    List<T> ConsultaLista(Func<T, bool> filtro);
    T SelecionarPorId(int id);

    IEnumerable<T> SelecionarTodos();
}
