using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Payment> DeleteAsync(int id)
        {
            var deleted_payment = DbContext.Payments.FirstOrDefault(x => x.Id == id.ToString() && !x.IsDeleted);
            if (deleted_payment != null)
            {
                deleted_payment.IsDeleted = true;
                deleted_payment.UpdatedAt = DateTime.UtcNow;
                DbContext.Payments.Update(deleted_payment);
            }


            await DbContext.SaveChangesAsync();
            return deleted_payment;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(int page = 1)
        {
            var payments = await DbContext.Set<Payment>().Where(w => !w.IsDeleted).ToListAsync();
            return payments;
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            var payment = await DbContext.Set<Payment>().Where(w => w.Id == id.ToString() && !w.IsDeleted).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            var existingPayment = await DbContext.Payments.FirstOrDefaultAsync(w => w.Id == entity.Id && !w.IsDeleted);
            if (existingPayment != null)
            {
                existingPayment.Amount = entity.Amount;
                existingPayment.Status = entity.Status;
                existingPayment.Method= entity.Method;
                existingPayment.UpdatedAt = DateTime.UtcNow;
                DbContext.Payments.Update(existingPayment);

                
            }
            await DbContext.SaveChangesAsync();

            return existingPayment;

        }
    }
}
