using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class PaymentRepository : AbstractRepository, IPaymentRepository
    {
        public AppDbContext DbContext;

        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Payment> CreateAsync(Payment entity)
        {
            await DbContext.Payments.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment?> DeleteAsync(string id)
        {
            var payment = await GetByIdAsync(id);
            DbContext.Payments.Remove(payment);

            await DbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(int page = 1)
        {
            var payments = await DbContext.Payments.Where(w => !w.IsDeleted)
                                          .Take(DefaultPageSize)
                                          .Skip(CalculatePagination(page))
                                          .ToListAsync();
            return payments;
        }

        public async Task<Payment?> GetByIdAsync(string id)
        {
            var payment = await DbContext.Payments.Where(w => w.Id == id && !w.IsDeleted).FirstOrDefaultAsync();
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