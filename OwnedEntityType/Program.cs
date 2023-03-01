// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
AppDbContext context = new();
List<Person> personel = context.Persons.ToList(); //OwnedEntityType'de relationship yoktur, sorgusuz sualsiz birlesik entity'de veriyi getirir
Console.WriteLine("");


public class Person
{
    public int ID { get; set; }
    public PersonelDigerbilgiler? PersonelDigerbilgiler { get; set; }
    public ICollection<Order> Orders { get; set; }

}
public class Order
{
    public string? Urun { get; set; }
    public int Fiyat { get; set; }
}
public class PersonelDigerbilgiler
{
    public string? Adres { get; set; }
    public string? Departman { get; set; }
}
public class AppDbContext:DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=OLGUNPC\\SQLEXPRESS; Initial Catalog=ABC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasKey(p=> p.ID);
        modelBuilder.Entity<Person>().OwnsOne(p => p.PersonelDigerbilgiler);
        modelBuilder.Entity<Person>().OwnsMany(p => p.Orders, builder =>
        {
            builder.WithOwner().HasForeignKey("OwnedEmpoyeeID");
            builder.Property<int>("ID");
            builder.HasKey("ID");
            builder.Property(p => p.Urun).HasColumnName("UrunAdi");
            builder.Property(p => p.Fiyat).HasColumnName("Fiyat").HasDefaultValue(0);
        });
    }

}