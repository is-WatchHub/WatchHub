using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementDomain;

namespace Infrastructure
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplictionUser>
    {
        public void Configure(EntityTypeBuilder<ApplictionUser> builder)
        {
            builder.ToTable(UserTableConstants.USER_TABLE_NAME);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName(UserTableConstants.USER_ID_COLUMN_NAME)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasColumnName(UserTableConstants.USER_LOGIN_COLUMN_NAME)
                .HasMaxLength(UserTableConstants.USER_LOGIN_COLUMN_MAX_LENGTH)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnName(UserTableConstants.USER_PASSWORD_COLUMN_NAME)
                .HasMaxLength(UserTableConstants.USER_PASSWORD_COLUMN_MAX_LENGTH)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName(UserTableConstants.USER_EMAIL_COLUMN_NAME)
                .HasMaxLength(UserTableConstants.USER_EMAIL_COLUMN_MAX_LENGTH)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasColumnName(UserTableConstants.USER_ROLE_COLUMN_NAME)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
