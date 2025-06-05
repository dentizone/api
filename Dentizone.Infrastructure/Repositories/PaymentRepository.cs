using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class PaymentRepository : AbstractRepository, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Payment> CreateAsync(Payment entity)
        {
            await dbContext.Payments.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment?> FindBy(Expression<Func<Payment, bool>> condition,
            Expression<Func<Payment, object>>[]? includes)
        {
            IQueryable<Payment> query = dbContext.Payments;
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
            var payment = await dbContext.Payments.Where(w => w.Id == id).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            dbContext.Payments.Update(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}