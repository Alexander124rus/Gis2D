using GeoMVC7.Domain.Entities;
using GeoMVC7.Domain.Entities.GeoEntitie;
using GeoMVC7.Domain.Exceptions;
using GeoMVC7.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Channels;

namespace GeoMVC7.Domain
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<UserModel> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {

            base.SavingChanges += (sender, args) =>
            {
                Console.WriteLine($"Saving changes for {((ApplicationContext)
                sender)!.Database!.GetConnectionString()}");
            };
            base.SavedChanges += (sender, args) =>
            {

                Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for {((ApplicationContext)sender)!.Database!.GetConnectionString() }");
            };
            base.SaveChangesFailed += (sender, args) =>
            {
                Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
            };

            //ChangeTracker.Tracked += ChangeTracker_Tracked;
            //ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        }
        //public virtual DbSet<CustomerOrderViewModel>? CustomerOrderViewModels { get; set; }

        public DbSet<MyGeometry>? MyGeometries { get; set; }
        public DbSet<MyPage>? MyPages { get; set; }
        //public DbSet<MyPropertie>? MyProperties { get; set; }
        public DbSet<SeriLogEntry>? LogEntries { get; set; }


        //private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
        //{
        //    var source = (e.FromQuery) ? "Database" : "Code";
        //    if (e.Entry.Entity is MyGeometry c)
        //    {
        //        Console.WriteLine($"Geometry entry {c.Id} was added from {source}");
        //    }
        //}
        //private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
        //{

        //    if (e.Entry.Entity is not MyGeometry c)
        //    {
        //        return;
        //    }
        //    var action = string.Empty;
        //    Console.WriteLine($"Geometry {c.Id} was {e.OldState} before the state changed to { e.NewState}");
        //    switch (e.NewState)
        //    {
        //        case EntityState.Unchanged:
        //            action = e.OldState switch
        //            {
        //                EntityState.Added => "Added",
        //                EntityState.Modified => "Edited", 
        //                _=> action
        //            };
        //            Console.WriteLine($"The object was {action}");
        //            break;
        //    }
        //}

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Произошла ошибка параллелизма.
                // Подлежит регистрации в журнале и надлежащей обработке,
                throw new CustomConcurrencyException("A concurrency error happened.", ex);
                // Произошла ошибка параллелизма.
            }
            catch (RetryLimitExceededException ex)
            {
                // Превышен предел на количество повторных попыток DbResiliency.
                // Подлежит регистрации в журнале и надлежащей обработке,
                throw new CustomRetryLimitExceededException(
                "There is a problem with SQL Server.", ex);
                // Возникла проблема c SQL Server.
            }
            catch (DbUpdateException ex)
            {
                // Подлежит регистрации в журнале и надлежащей обработке,
                throw new CustomDbUpdateException(
                "An error occurred updating the database.", ex);
                // Произошла ошибка при обновлении базы данных.
            }
            catch (Exception ex)
            {
                // Подлежит регистрации в журнале и надлежащей обработке,
                throw new CustomException(
                "An error occurred updating the database.", ex);
                // Произошла ошибка при обновлении базы данных.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            //{
            //    Id = "3b62472e-4f66-49fa-a20f-e7685b9565d8",
            //    UserName = "admin",
            //    NormalizedUserName = "ADMIN",
            //    Email = "it@kkkm.ru",
            //    NormalizedEmail = "IT@KKKM.RU",
            //    EmailConfirmed = false,
            //    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "123456789"),
            //    SecurityStamp = string.Empty
            //});
        }
    }
}
