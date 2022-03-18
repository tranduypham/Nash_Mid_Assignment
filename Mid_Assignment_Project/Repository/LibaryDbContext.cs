using Microsoft.EntityFrameworkCore;

namespace Mid_Assignment_Project.Models
{
    public class LibaryDbContext : DbContext
    {
        public LibaryDbContext() { }
        public LibaryDbContext(DbContextOptions<LibaryDbContext> options) : base(options) { }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=DESKTOP-C00IDIB;Initial Catalog=DBName;Integrated Security=True");
        // }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<BookBorrowingRequestDetail> BookBorrowingRequestDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>()
                        .HasKey(user => user.UserId);

            modelBuilder.Entity<User>()
                        .Property(user => user.UserId)
                        .HasColumnName("id")
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                        .Property(user => user.Username)
                        .HasColumnName("username")
                        .IsRequired()
                        .HasColumnType("nvarchar")
                        .HasMaxLength(30);

            modelBuilder.Entity<User>()
                        .Property(user => user.Password)
                        .HasColumnName("password")
                        .IsRequired()
                        .HasColumnType("nvarchar")
                        .HasMaxLength(255);

            modelBuilder.Entity<User>()
                        .Property(user => user.CreatedAt)
                        .HasColumnName("created_at")
                        .IsRequired()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<User>()
                        .Property(user => user.IsSuper)
                        .HasColumnName("isAdmin")
                        .HasDefaultValue(false);

            // Category
            modelBuilder.Entity<Category>()
                        .HasKey(cate => cate.CategoryId);

            modelBuilder.Entity<Category>()
                        .Property(cate => cate.CategoryId)
                        .HasColumnName("id")
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
                        .Property(cate => cate.CategoryName)
                        .HasColumnName("name")
                        .HasColumnType("nvarchar")
                        .HasMaxLength(30)
                        .HasDefaultValue("no name");

            modelBuilder.Entity<Category>()
                        .Property(cate => cate.CreatedAt)
                        .HasColumnName("created_at")
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(DateTime.Now);

            // Book
            modelBuilder.Entity<Book>()
                        .HasKey(book => book.BookId);

            modelBuilder.Entity<Book>()
                        .Property(book => book.BookId)
                        .HasColumnName("id")
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>()
                        .Property(book => book.BookName)
                        .HasColumnName("name")
                        .HasColumnType("nvarchar")
                        .HasMaxLength(50)
                        .HasDefaultValue("no name");

            modelBuilder.Entity<Book>()
                        .Property(book => book.CreatedAt)
                        .HasColumnName("created_at")
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(DateTime.Now);
            // Book - Category
            modelBuilder.Entity<Book>()
                        .HasOne<Category>()
                        .WithMany()
                        .HasForeignKey(book => book.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);

            // Book Borrowing Request
            modelBuilder.Entity<BookBorrowingRequest>()
                        .HasKey(request => request.BookBorrowingRequestId);

            modelBuilder.Entity<BookBorrowingRequest>()
                        .Property(request => request.BookBorrowingRequestId)
                        .HasColumnName("id")
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<BookBorrowingRequest>()
                        .Property(request => request.AuthorizeDate)
                        .HasColumnName("authprized_at")
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(null);

            modelBuilder.Entity<BookBorrowingRequest>()
                        .Property(request => request.CreatedAt)
                        .HasColumnName("created_at")
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(DateTime.Now);

            // Book Borrowing Request Detail
            modelBuilder.Entity<BookBorrowingRequestDetail>()
                        .HasKey(requestDetail => requestDetail.BookBorrowingRequestDetailId);

            modelBuilder.Entity<BookBorrowingRequestDetail>()
                        .Property(requestDetail => requestDetail.BookBorrowingRequestDetailId)
                        .HasColumnName("id")
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<BookBorrowingRequestDetail>()
                        .Property(requestDetail => requestDetail.BookName)
                        .HasColumnName("name")
                        .HasColumnType("nvarchar")
                        .HasMaxLength(255)
                        .HasDefaultValue("no-name");

            // Define Request detail - request relationship
            modelBuilder.Entity<BookBorrowingRequestDetail>()
                        .HasOne<BookBorrowingRequest>()
                        .WithMany()
                        .HasForeignKey(requestDetail => requestDetail.BookBorrowingRequestId)
                        .OnDelete(DeleteBehavior.Cascade);
            // Define Request detail - book relationship
            modelBuilder.Entity<BookBorrowingRequestDetail>()
                        .HasOne<Book>()
                        .WithMany()
                        .HasForeignKey(x => x.BookId);

            // Token
            modelBuilder.Entity<Token>()
                        .HasKey(token => token.TokenId);

            modelBuilder.Entity<Token>()
                        .Property(token => token.TokenId)
                        .HasColumnName("id")
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Token>()
                        .Property(token => token.Payload)
                        .HasColumnName("payload")
                        .IsRequired()
                        .HasColumnType("nvarchar")
                        .HasMaxLength(255);

            modelBuilder.Entity<Token>()
                        .Property(token => token.CreatedAt)
                        .HasColumnName("created_at")
                        .IsRequired()
                        .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Token>()
                        .Property(token => token.UserId)
                        .HasColumnName("user_id")
                        .IsRequired();
                        
            // Define Token - user relationship
            modelBuilder.Entity<Token>()
                        .HasOne<User>()
                        .WithOne()
                        .HasForeignKey<Token>(token => token.UserId);
            // Seed data
            modelBuilder.Entity<User>()
                        .HasData(
                            new User{
                                UserId = 1,
                                Username = "admin",
                                IsSuper = true
                            },
                            new User{
                                UserId = 2,
                                Username = "user1"
                            },
                            new User{
                                UserId = 3,
                                Username = "user2"
                            }
                        );

            modelBuilder.Entity<Category>()
                        .HasData(
                            new Category{
                                CategoryId = 1,
                                CategoryName = "Science"
                            },
                            new Category{
                                CategoryId = 2,
                                CategoryName = "History"
                            },
                            new Category{
                                CategoryId = 3,
                                CategoryName = "Literature"
                            }
                        );

            modelBuilder.Entity<Book>()
                        .HasData(
                            new Book{
                                BookId = 1,
                                BookName = "Campell Biology",
                                CategoryId = 1
                            },
                            new Book{
                                BookId = 2,
                                BookName = "Time and space",
                                CategoryId = 1
                            },
                            new Book{
                                BookId = 3,
                                BookName = "WW1",
                                CategoryId = 2
                            },
                            new Book{
                                BookId = 4,
                                BookName = "Vietnam and the communitism",
                                CategoryId = 2
                            },
                            new Book{
                                BookId = 5,
                                BookName = "The phantom of the opera",
                                CategoryId = 3
                            },
                            new Book{
                                BookId = 6,
                                BookName = "Kieu",
                                CategoryId = 3
                            }
                        );

            modelBuilder.Entity<BookBorrowingRequest>()
                        .HasData(
                            new BookBorrowingRequest{
                                BookBorrowingRequestId = 1,
                                UserId = 2,
                                Username = "user1"
                            },
                            new BookBorrowingRequest{
                                BookBorrowingRequestId = 2,
                                UserId = 3,
                                Username = "user2"
                            }
                        );

            modelBuilder.Entity<BookBorrowingRequestDetail>()
                        .HasData(
                            new BookBorrowingRequestDetail{
                                BookBorrowingRequestDetailId = 1,
                                BookBorrowingRequestId = 1,
                                BookId = 1,
                                BookName = "Campell Biology Edit"
                            },
                            new BookBorrowingRequestDetail{
                                BookBorrowingRequestDetailId = 2,
                                BookBorrowingRequestId = 1,
                                BookId = 2,
                                BookName = "Time and space"
                            },
                            new BookBorrowingRequestDetail{
                                BookBorrowingRequestDetailId = 3,
                                BookBorrowingRequestId = 2,
                                BookId = 2,
                                BookName = "Time and space"
                            },
                            new BookBorrowingRequestDetail{
                                BookBorrowingRequestDetailId = 4,
                                BookBorrowingRequestId = 2,
                                BookId = 3,
                                BookName = "WW1"
                            },
                            new BookBorrowingRequestDetail{
                                BookBorrowingRequestDetailId = 5,
                                BookBorrowingRequestId = 2,
                                BookId = 4,
                                BookName = "Vietnam and the communitism"
                            },
                            new BookBorrowingRequestDetail{
                                BookBorrowingRequestDetailId = 6,
                                BookBorrowingRequestId = 2,
                                BookId = 5,
                                BookName = "The phantom of the opera"
                            }
                        );
        }
    }
}