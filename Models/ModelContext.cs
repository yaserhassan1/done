using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Market_Store.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Addresst> Addressts { get; set; }
        public virtual DbSet<Cartt> Cartts { get; set; }
        public virtual DbSet<Categoryt> Categoryts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Creditcard> Creditcards { get; set; }
        public virtual DbSet<Homepage> Homepages { get; set; }
        public virtual DbSet<Ordert> Orderts { get; set; }
        public virtual DbSet<Productt> Productts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Storet> Storets { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Usert> Userts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User138;PASSWORD=osamapek8810;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER138");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.AboutusId)
                    .HasName("SYS_C00236301");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.AboutusId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTUS_ID");

                entity.Property(e => e.AboutSubject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ABOUT_SUBJECT");

                entity.Property(e => e.AboutTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ABOUT_TITLE");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<Addresst>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("SYS_C00229582");

                entity.ToTable("ADDRESST");

                entity.Property(e => e.AddressId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_2");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("POST_CODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("REGION");
            });

            modelBuilder.Entity<Cartt>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("SYS_C00229637");

                entity.ToTable("CARTT");

                entity.Property(e => e.CartId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CART_ID");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Cartts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_POROD_CART");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cartts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER_CART");
            });

            modelBuilder.Entity<Categoryt>(entity =>
            {
                entity.HasKey(e => e.Catid)
                    .HasName("SYS_C00220022");

                entity.ToTable("CATEGORYT");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATID");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("CONTACT");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Subject)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");
            });

            modelBuilder.Entity<Creditcard>(entity =>
            {
                entity.ToTable("CREDITCARD");

                entity.Property(e => e.Creditcardid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CREDITCARDID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cardtype)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CARDTYPE");

                entity.Property(e => e.Cvv)
                    .HasPrecision(3)
                    .HasColumnName("CVV");

                entity.Property(e => e.Expdata)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPDATA");

                entity.Property(e => e.Modifieddate)
                    .HasColumnType("DATE")
                    .HasColumnName("MODIFIEDDATE");
            });

            modelBuilder.Entity<Homepage>(entity =>
            {
                entity.HasKey(e => e.HomeId)
                    .HasName("SYS_C00237159");

                entity.ToTable("HOMEPAGE");

                entity.Property(e => e.HomeId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOME_ID");

                entity.Property(e => e.Imageslider2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGESLIDER2");

                entity.Property(e => e.Imgeslider1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMGESLIDER1");

                entity.Property(e => e.Logoimage)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LOGOIMAGE");

                entity.Property(e => e.TitleSlider1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TITLE_SLIDER1");

                entity.Property(e => e.TitleSlider2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TITLE_SLIDER2");
            });

            modelBuilder.Entity<Ordert>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("SYS_C00220064");

                entity.ToTable("ORDERT");

                entity.HasIndex(e => e.Codename, "SYS_C00220065")
                    .IsUnique();

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Codename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODENAME");

                entity.Property(e => e.DateFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_FROM");

                entity.Property(e => e.DateTo)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_TO");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Prodid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PRODID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.Orderts)
                    .HasForeignKey(d => d.Prodid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ORDER_PRODUCT");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orderts)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ORDER_USER");
            });

            modelBuilder.Entity<Productt>(entity =>
            {
                entity.HasKey(e => e.Prodid)
                    .HasName("SYS_C00220061");

                entity.ToTable("PRODUCTT");

                entity.Property(e => e.Prodid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODID");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.In_Stock)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IN_STOCK")
                    .IsFixedLength(true);

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RATING");

                entity.Property(e => e.Sale)
                    .HasColumnType("NUMBER(38,3)")
                    .HasColumnName("SALE");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STOREID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Productts)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PRODUCT_STORE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("SERVICES");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SERVICE_ID");

                entity.Property(e => e.Content)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");
            });

            modelBuilder.Entity<Storet>(entity =>
            {
                entity.HasKey(e => e.Storeid)
                    .HasName("SYS_C00220057");

                entity.ToTable("STORET");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STOREID");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATID");

                entity.Property(e => e.Dateclose)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECLOSE");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Dateopen)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEOPEN");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RATING");

                entity.Property(e => e.State)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.Storename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STORENAME");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Storets)
                    .HasForeignKey(d => d.Catid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_STORE_CATEGORY");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.TestimonialId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTIMONIAL_ID");

                entity.Property(e => e.Modifieddate)
                    .HasColumnType("DATE")
                    .HasColumnName("MODIFIEDDATE");

                entity.Property(e => e.TestimonialComment)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMONIAL_COMMENT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TESST_USER");
            });

            modelBuilder.Entity<Usert>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00220016");

                entity.ToTable("USERT");

                entity.HasIndex(e => e.Email, "SYS_C00220017")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "SYS_C00220018")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.AddresId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ADDRES_ID");

                entity.Property(e => e.Barthday)
                    .HasColumnType("DATE")
                    .HasColumnName("BARTHDAY");

                entity.Property(e => e.CarditcardId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDITCARD_ID");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Addres)
                    .WithMany(p => p.Userts)
                    .HasForeignKey(d => d.AddresId)
                    .HasConstraintName("FK_ADDRESS");

                entity.HasOne(d => d.Carditcard)
                    .WithMany(p => p.Userts)
                    .HasForeignKey(d => d.CarditcardId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USERT_CARDIT");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userts)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER_ROLE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
