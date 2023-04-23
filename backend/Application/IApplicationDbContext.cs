using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Topic> Topics { get; }
    DbSet<Comment> Comments { get; }
    DbSet<Material> Materials { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}