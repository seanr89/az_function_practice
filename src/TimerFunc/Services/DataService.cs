

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DataService
{
    private readonly ILogger _logger;
    private readonly AppDbContext _appDbContext;
    public DataService(ILoggerFactory loggerFactory,
        AppDbContext appDbContext)
    {
        _logger = loggerFactory.CreateLogger<DataService>();
        _appDbContext = appDbContext;
    }

    public async Task<Person> GetPerson(int id)
    {
        return await _appDbContext.People.FindAsync(id);
    }

    public async Task UpdatePerson(Person person)
    {
        _appDbContext.People.Update(person);
        await _appDbContext.SaveChangesAsync();
    }
}