using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementDomain;

namespace Infrastructure
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(Constants.USER_TABLE_NAME);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(Constants.USER_ID_COLUMN_NAME).IsRequired();
            builder.Property(x => x.Login).HasColumnName(Constants.USER_LOGIN_COLUMN_NAME).HasMaxLength(45).IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnName(Constants.USER_PASSWORD_COLUMN_NAME).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasColumnName(Constants.USER_EMAIL_COLUMN_NAME).HasMaxLength(45).IsRequired();
            builder.Property(x => x.Role).HasColumnName(Constants.USER_ROLE_COLUMN_NAME).HasConversion<int>().IsRequired();
        }
    }
}
