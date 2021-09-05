using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace db_course_project.Database
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Автомобили> Автомобили { get; set; }
        public virtual DbSet<Должности> Должности { get; set; }
        public virtual DbSet<Заявки> Заявки { get; set; }
        public virtual DbSet<Клиенты> Клиенты { get; set; }
        public virtual DbSet<Сотрудники> Сотрудники { get; set; }
        public virtual DbSet<Тарифы> Тарифы { get; set; }
        public virtual DbSet<Тип_автомобиля> Тип_автомобиля { get; set; }
        public virtual DbSet<Услуги> Услуги { get; set; }
        public virtual DbSet<Заказы> Заказы { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Автомобили>()
                .Property(e => e.Марка)
                .IsFixedLength();

            modelBuilder.Entity<Должности>()
                .Property(e => e.Название_должности)
                .IsFixedLength();

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.ФИО)
                .IsFixedLength();

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Номер_телефона)
                .IsFixedLength();

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Адрес_проживания)
                .IsFixedLength();

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.ФИО)
                .IsFixedLength();

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.Номер_телефона)
                .IsFixedLength();

            modelBuilder.Entity<Сотрудники>()
                .Property(e => e.Адрес_проживания)
                .IsFixedLength();

            modelBuilder.Entity<Сотрудники>()
                .HasMany(e => e.Автомобили)
                .WithOptional(e => e.Сотрудники)
                .HasForeignKey(e => e.Код_водителя)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Сотрудники>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.Сотрудники)
                .HasForeignKey(e => e.Код_менеджера);

            modelBuilder.Entity<Тарифы>()
                .Property(e => e.Название_тарифа)
                .IsFixedLength();

            modelBuilder.Entity<Тип_автомобиля>()
                .Property(e => e.Название_типа)
                .IsFixedLength();

            modelBuilder.Entity<Тип_автомобиля>()
                .HasMany(e => e.Автомобили)
                .WithRequired(e => e.Тип_автомобиля1)
                .HasForeignKey(e => e.Тип_автомобиля);

            modelBuilder.Entity<Услуги>()
                .Property(e => e.Название_услуги)
                .IsFixedLength();

            modelBuilder.Entity<Услуги>()
                .Property(e => e.Описание_услуги)
                .IsFixedLength();
        }
    }
}
