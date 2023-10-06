using Hospital.Dados.Contexto;
using Hospital.Dominio.Base;

namespace Hospital.Dados.Repositorios.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IId
{
    protected readonly HospitalContext _context;

    public RepositoryBase(HospitalContext context)
    {
        _context = context;
    }

    public void Adicionar(T entidade)
    {
        _context.Add(entidade);
        _context.SaveChanges();
    }

    public void Alterar(T entidade)
    {
        _context.Update(entidade);
        _context.SaveChanges();
    }

    public List<T> ConsultaLista(Func<T, bool> filtro)
    {
        return _context.Set<T>().Where(filtro).ToList();
    }

    public T ConsultaUnica(Func<T, bool> filtro)
    {
        return _context.Set<T>().Where(filtro).FirstOrDefault();
    }

    public void Excluir(int id)
    {
        var entidade = SelecionarPorId(id);
        if (entidade == null) return;
        _context.Remove(entidade);
        _context.SaveChanges();
    }

    public T SelecionarPorId(int id)
    {
        var entidade = _context.Set<T>().Where(m => m.Id == id).FirstOrDefault();
        return entidade;

    }

    public IEnumerable<T> SelecionarTodos()
    {
        return _context.Set<T>().ToList();
    }
}
