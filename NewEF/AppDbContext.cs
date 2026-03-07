using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewEF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF
{
    public class AppDbContext : DbContext
    {
        //private readonly string _connectionString = "Data Source=NewEF.db";

        private readonly string SQLInstance = null!;

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }

        public AppDbContext()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            SQLInstance = config.GetConnectionString("DefaultConnection")!;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(SQLInstance);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Student>().Property(s => s.Scholarship).HasColumnType("money");

            modelBuilder.Entity<Teacher>(b =>
            {
                b.Property(t => t.Salary)
                    .HasColumnType("decimal(8,2)")
                    .HasDefaultValue(25_000);

                b.Property(t => t.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(t => t.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                b.ToTable(t => t.HasCheckConstraint("CK_Salary_MoreThenZero", "[Salary] > 0"));

                b.ToTable(x => x.HasCheckConstraint(
                    "CK_Teacher_FirstName_NotEmpty",
                    "FirstName <> ''"
                ));

                b.ToTable(x => x.HasCheckConstraint(
                    "CK_Teacher_LastName_NotEmpty",
                    "LastName <> ''"
                ));
            });

            modelBuilder.Entity<Student>(b =>
            {
                b.Property(s => s.Scholarship)
                    .HasColumnType("decimal(6,2)");

                b.HasIndex(s => s.Email)
                    .IsUnique();

                b.ToTable(s => s.HasCheckConstraint(
                    "CK_Email_Pattern",
                    "[Email] LIKE '%_@_%._%'"
                ));

                b.Property(s => s._attendanceForm)
                    .HasConversion<string>();

                b.ToTable(s => s.HasCheckConstraint(
                    "CK_AttendanceForms",
                    "[_attendanceForm] IN ('Hybrid', 'Offline', 'Online')"
                ));

                b.HasOne(s => s.Passport).WithOne(p => p.Student).HasForeignKey<Passport>(p => p.StudentId);

                b.Property(s => s.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(s => s.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                b.ToTable(x => x.HasCheckConstraint(
                    "CK_Student_FirstName_NotEmpty",
                    "FirstName <> ''"
                ));

                b.ToTable(x => x.HasCheckConstraint(
                    "CK_Student_LastName_NotEmpty",
                    "LastName <> ''"
                ));
            });

            modelBuilder.Entity<Group>(g =>
            {
                g.Property(x => x.Name)
                    .HasMaxLength(10)
                    .IsRequired();

                g.HasIndex(x => x.Name)
                    .IsUnique();

                g.ToTable(t => t.HasCheckConstraint(
                    "CK_Group_Name_NotEmpty",
                    "Name <> ''"
                ));
            });

            modelBuilder.Entity<Passport>(p =>
            {
                p.Property(x => x.Number)
                    .HasMaxLength(9)
                    .IsRequired();

                p.ToTable(t => t.HasCheckConstraint(
                    "CK_Passport_Number",
                    "LEN(Number) = 9 AND Number NOT LIKE '%[^0-9]%'"
                ));
            });

            modelBuilder.Entity<Subject>(s =>
            {
                s.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                s.ToTable(x => x.HasCheckConstraint(
                    "CK_Subject_Name_NotEmpty",
                    "Name <> ''"
                ));
            });

            modelBuilder.Entity<Department>(d =>
            {
                d.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();
                d.ToTable(x => x.HasCheckConstraint(
                    "CK_Department_Name_NotEmpty",
                    "Name <> ''"
                ));
            });
        }
    }
}
