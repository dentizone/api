using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class PaymentRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IPaymentRepository
    {
        public async Task<Payment> CreateAsync(Payment entity)
        {
            await DbContext.Payments.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment?> FindBy(Expression<Func<Payment, bool>> condition,
            Expression<Func<Payment, object>>[]? includes)
        {
            IQueryable<Payment> query = DbContext.Payments;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }


        public async Task<Payment?> GetByIdAsync(string id)
        {
            var payment = await DbContext.Payments.Where(w => w.Id == id).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            DbContext.Payments.Update(entity);

            await DbContext.SaveChangesAsync();

            return entity;
        }
    }
}