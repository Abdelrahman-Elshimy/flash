using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data.Configurations.Entities
{
    public class ModeDetailsConfiguration : IEntityTypeConfiguration<ModeDetail>
    {
        public void Configure(EntityTypeBuilder<ModeDetail> builder)
        {
            builder.HasData(

                new ModeDetail
                {
                    ModeId = 2,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(7),
                    Id = 1
                }
            );
        }
    }
}
