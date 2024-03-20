using Data;
using Data.Model;
using System.Linq;

namespace Business
{
    public class BookBusiness
    {
        private BookDbContext bookDbContext;

        public List<Book> GetAll()
        {
            using (bookDbContext = new BookDbContext())
            {
                return bookDbContext.Books.ToList();
            }
        }

        public Book Get(int id)
        {
            using (bookDbContext = new BookDbContext())
            {
                return bookDbContext.Books.Find(id);
            }
        }

        public void Add(Book book)
        {
            using (bookDbContext = new BookDbContext())
            {
                bookDbContext.Books.Add(book);
                bookDbContext.SaveChanges();
            }
        }

        public void Update(Book book)
        {
            using (bookDbContext = new BookDbContext())
            {
                var item = bookDbContext.Books.Find(book.Id);
                if (item != null)
                {
                    bookDbContext.Entry(item).CurrentValues.SetValues(book);
                    bookDbContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (bookDbContext = new BookDbContext())
            {
                var book = bookDbContext.Books.Find(id);
                if (book != null)
                {
                    bookDbContext.Books.Remove(book);
                    bookDbContext.SaveChanges();
                }
            }
        }
    }
}