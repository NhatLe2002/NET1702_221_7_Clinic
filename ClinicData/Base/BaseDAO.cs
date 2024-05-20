using ClinicData.Models;
using Microsoft.EntityFrameworkCore;

public class BaseDAO<T> where T : class
{
    protected readonly NET1702_PRN221_ClinicContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseDAO()
    {
        _context = new NET1702_PRN221_ClinicContext();
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public void Create(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }
    public void Update(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
        _context.SaveChanges();
    }
    public bool Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
        return true;
    }
    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }
    public T GetById(string code)
    {
        return _dbSet.Find(code);
    }
}