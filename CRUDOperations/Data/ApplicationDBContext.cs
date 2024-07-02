namespace CRUDOperations.Data;
using Microsoft.EntityFrameworkCore;
using CRUDOperations.Models;
using Microsoft.AspNetCore.Mvc;


public class ApplicationDbContext :DbContext
{
    
    public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options){

    }

     // Configure the keyless entity
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure SomeReadOnlyModel as keyless
        modelBuilder.Entity<LoginViewModel>().HasNoKey();
    }

    //A DbSet represents the collection of all entities in the context, or that can be queried from the database, of a given type. DbSet objects are created from a DbContext using the DbContext.Set method.
    public DbSet<UsersEntity> Users {get; set;}
    public DbSet<LoginViewModel> User {get; set;}

    //Delete this if it doesn't work
    public DbSet<InfoTable> InfoTable { get; set; }
    public DbSet<Asset> Assets  {get; set;}

   
}

