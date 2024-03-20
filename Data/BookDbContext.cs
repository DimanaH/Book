using Data.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace Data
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        // Конструктор, който подава връзката към базата данни
        public BookDbContext() : base("name=BookDbContext")
        {
            // Включване на автоматично създаване на базата данни
            Database.SetInitializer(new CreateDatabaseIfNotExists<BookDbContext>());

            // Вмъкване на първоначални данни при инициализация
            if (!Database.Exists())
            {
                Database.Initialize(true);
                SeedData();
            }

        }

        // Метод за вмъкване на първоначални данни
        private void SeedData()
        {
            var books = new List<Book>
            {
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Publisher = "J. B. Lippincott & Co.", Year = 1960 },
                new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Publisher = "Secker & Warburg", Year = 1949 },
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Publisher = "Charles Scribner's Sons", Year = 1925 },
                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Publisher = "T. Egerton, Whitehall", Year = 1813 },
                new Book { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Genre = "Fantasy", Publisher = "Bloomsbury", Year = 1997 }
            };

            foreach (var book in books)
            {
                Books.Add(book);
            }

            SaveChanges();
        }
    }
}
