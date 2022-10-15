using CoreIntro.Models.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreIntro.Configurations
{
    public class EmployeeConfiguration:BaseConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.EmployeeProfile).WithOne(x => x.Employee).HasForeignKey<EmployeeProfile>(x => x.ID);
        }
    }
}
