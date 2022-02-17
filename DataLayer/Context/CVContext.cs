using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
	public class CVContext : DbContext
	{
		public CVContext()
		{

		}
		public CVContext(DbContextOptions<CVContext> options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Owner> Myselves { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<WorkExperience> WorkExperiences { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Skill>().Property(p => p.Id).IsRequired();
			modelBuilder.Entity<Skill>().HasKey(p=>p.Id);
			modelBuilder.Entity<Skill>().Property(p => p.Name).IsRequired();
			modelBuilder.Entity<Skill>().Property(p => p.PercentOfDominance).IsRequired();

			modelBuilder.Entity<WorkExperience>().Property(p=>p.Id).IsRequired();
			modelBuilder.Entity<WorkExperience>().HasKey(p=>p.Id);
			modelBuilder.Entity<WorkExperience>().Property(p=>p.Subject).IsRequired();
			modelBuilder.Entity<WorkExperience>().Property(p=>p.FromDate).IsRequired();

			modelBuilder.Entity<Owner>().Property(p => p.Id).IsRequired();
			modelBuilder.Entity<Owner>().HasKey(p=>p.Id);
			modelBuilder.Entity<Owner>().Property(p=>p.FullName);
			modelBuilder.Entity<Owner>().Property(p=>p.PassWord);
			modelBuilder.Entity<Owner>().Property(p=>p.About);
			modelBuilder.Entity<Owner>().Property(p=>p.Role);
			modelBuilder.Entity<Owner>().Property(p=>p.CreationDate);
			modelBuilder.Entity<Owner>().Property(p=>p.Phonenumber);

			modelBuilder.Entity<User>().HasKey(p=>p.Id);
			modelBuilder.Entity<User>().Property(p=>p.Id).IsRequired();
			modelBuilder.Entity<User>().Property(p=>p.PassWord).IsRequired();
			modelBuilder.Entity<User>().Property(p=>p.Role).IsRequired();
			modelBuilder.Entity<User>().Property(p=>p.Phonenumber).IsRequired();
			modelBuilder.Entity<User>().Property(p=>p.FullName).IsRequired();

			modelBuilder.Entity<Contact>().HasKey(p => p.Id);
			modelBuilder.Entity<Contact>().Property(p=>p.Subject).IsRequired();
			modelBuilder.Entity<Contact>().Property(p=>p.ContactText).IsRequired();
			modelBuilder.Entity<Contact>()
			.HasOne<User>(p=>p.User)
			.WithMany(p=>p.Comments)
			.HasForeignKey(p=>p.UserID);
		}
	}
}
