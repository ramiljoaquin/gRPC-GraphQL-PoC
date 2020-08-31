using AccountService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Data
{
	public class ProfileDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.SetStringMaxLengthConvention(255);

			builder.Entity<User>(entity =>
			{
				entity.HasOne(q => q.Profile)
					.WithOne(q => q.User)
					.HasForeignKey<Profile>(q => q.ProfileId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.Property(q => q.LastEditedWhen)
					.HasDefaultValue(DateTime.UtcNow)
					.ValueGeneratedOnUpdate();

				entity.Property(q => q.CreatedWhen)
					.HasDefaultValue(DateTime.UtcNow)
					.ValueGeneratedOnAdd();
			});

			builder.Entity<Profile>(entity =>
			{
				entity.HasKey(q => q.ProfileId);

				entity.HasOne(q => q.User)
				.WithOne(q => q.Profile);

				entity.HasBaseEntity();

				entity.Ignore(q => q.FullName);

			});

			builder.Entity<RequestService>(entity =>
			{
				entity.HasKey(q => q.RequestServiceId);

				entity.HasOne(q => q.Requester);

				entity.Property(q => q.Description)
					.HasColumnType("nvarchar(500)");

				entity.HasOne(q => q.ServiceType)
				    .WithMany();

				entity.HasMany(q => q.ServiceStatuses);

				entity.Property(q => q.CreatedWhen)
				   .HasDefaultValue(DateTime.UtcNow)
				   .ValueGeneratedOnAdd();

			});

			builder.Entity<RequestServiceStatus>(entity =>
			{
			
				entity.HasKey(q => new { q.RequestServiceId, q.ServiceStatusId });

				entity.Property(q => q.RequestServiceId)
				  .ValueGeneratedNever();

				entity.Property(q => q.Description)
					.HasColumnType("nvarchar(500)");

				entity.HasBaseEntity();

			});

			builder.Entity<ServiceStatus>(entity =>
			{
				entity.HasKey(q => q.ServiceStatusId);
			});

			builder.Entity<ServiceType>(entity =>
			{
				entity.HasKey(q => q.ServiceTypeId);
			});

			base.OnModelCreating(builder);
		}

		public DbSet<Profile> Profiles { get; set; }
		public DbSet<RequestService> RequestServices { get; set; }
		public DbSet<RequestServiceStatus> RequestServiceStatuses { get; set; }
		public DbSet<ServiceStatus> ServiceStatuses { get; set; }
		public DbSet<ServiceType> ServiceTypes { get; set; }
	}
	internal static class ModelBuilderExtensions
	{
		public static void HasBaseEntity<T>(this EntityTypeBuilder<T> entity) where T : BaseDomain
		{
			entity.HasOne(q => q.CreatedBy)
				.WithMany()
				.HasForeignKey(q => q.CreatedById)
				.OnDelete(DeleteBehavior.NoAction);

			entity.HasOne(q => q.LastEditedBy)
				.WithMany()
				.HasForeignKey(q => q.LastEditedById);

			entity.Property(q => q.LastEditedWhen)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnUpdate();

			entity.Property(q => q.CreatedWhen)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnAdd();
		}

		public static void SetStringMaxLengthConvention(this ModelBuilder builder, int length)
		{
			foreach (var entity in builder.Model.GetEntityTypes())
			{
				foreach (var property in entity.GetProperties())
				{
					if (property.ClrType == typeof(string))
					{
						property.SetMaxLength(length);
					}
				}
			}
		}
	}
}
