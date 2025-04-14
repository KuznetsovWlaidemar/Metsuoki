using Metsuoki.Domain.Entities;
using Metsuoki.Domain.Entities.Products;
using Metsuoki.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Models.Entities;

namespace Metsuoki.Application.Common.Interfaces;
public interface IMetsuokiDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartItem> CartItems { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<UserActionLog> UserActionLogs { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}