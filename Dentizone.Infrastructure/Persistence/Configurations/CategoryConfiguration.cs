using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class CategoryConfiguration: IEntityTypeConfiguration<Category>
    { 

   

        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(c => c.Id);


            builder.Property(c => c.Id)
                   .IsRequired();


            builder.Property(c => c.Name)
                   .IsRequired();


            builder.Property(c => c.CreatedAt)
                   .IsRequired();


            builder.Property(c => c.UpdatedAt)
                   .IsRequired();

            builder.Property(c => c.IsDeleted)
                   .IsRequired();
                  

        }
    }
}
