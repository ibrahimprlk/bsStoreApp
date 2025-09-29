using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book() { Id = 1, Title = "Hacivat ve karagöz", Price = 350 },
                new Book() { Id = 2, Title = "La Fonteden Masallar", Price = 400 },
                new Book() { Id = 3, Title = "Harry Potter", Price = 800 }
                );
        }
    }
}
