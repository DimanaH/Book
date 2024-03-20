using Business;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConsoleApp
{
    public class Display
    {
        private BookBusiness bookBusiness;

        public Display()
        {
            bookBusiness = new BookBusiness(); 
            Run();
        }

        public void Run()
        {
            // Показване на меню за взаимодействие с приложението
            while (true)
            {
                string choice = Input("1. Списък на всички книги\n" +
                                      "2. Информация за книга по идентификатор\n" +
                                      "3. Добавяне на нова книга\n" +
                                      "4. Промяна на информация за книга\n" +
                                      "5. Изтриване на книга\n" +
                                      "6. Изход\n" +
                                      "Изберете опция: ");

                switch (choice)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        GetBookById(bookBusiness);
                        break;
                    case "3":
                        AddNewBook(bookBusiness);
                        break;
                    case "4":
                        UpdateBook(bookBusiness);
                        break;
                    case "5":
                        DeleteBook(bookBusiness);
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Невалиден избор. Моля, опитайте отново.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private string Input(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private void ShowAllBooks()
        {
            var books = bookBusiness.GetAll();
            if (books.Count == 0)
            {
                Console.WriteLine("Няма налични книги.");
                return;
            }

            Console.WriteLine("Списък на всички книги:");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}. {book.Title} - {book.Author} ({book.Year})");
            }
        }

        static void GetBookById(BookBusiness bookBusiness)
        {
            Console.Write("Въведете идентификатор на книгата: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var book = bookBusiness.Get(id);
                if (book != null)
                {
                    Console.WriteLine($"Информация за книга с идентификатор {id}:");
                    Console.WriteLine($"Заглавие: {book.Title}");
                    Console.WriteLine($"Автор: {book.Author}");
                    Console.WriteLine($"Жанр: {book.Genre}");
                    Console.WriteLine($"Издателство: {book.Publisher}");
                    Console.WriteLine($"Година: {book.Year}");
                }
                else
                {
                    Console.WriteLine("Книга с такъв идентификатор не е намерена.");
                }
            }
            else
            {
                Console.WriteLine("Невалиден идентификатор.");
            }
        }

        static void AddNewBook(BookBusiness bookBusiness)
        {
            Book newBook = new Book();
            Console.Write("Въведете заглавие: ");
            newBook.Title = Console.ReadLine();
            Console.Write("Въведете автор: ");
            newBook.Author = Console.ReadLine();
            Console.Write("Въведете жанр: ");
            newBook.Genre = Console.ReadLine();
            Console.Write("Въведете издателство: ");
            newBook.Publisher = Console.ReadLine();
            Console.Write("Въведете година: ");
            int year;
            if (int.TryParse(Console.ReadLine(), out year))
            {
                newBook.Year = year;
            }
            else
            {
                Console.WriteLine("Невалидна година.");
                return;
            }

            bookBusiness.Add(newBook);
            Console.WriteLine("Книгата е успешно добавена.");
        }

        static void UpdateBook(BookBusiness bookBusiness)
        {
            Console.Write("Въведете идентификатор на книгата, която искате да промените: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var existingBook = bookBusiness.Get(id);
                if (existingBook != null)
                {
                    Console.WriteLine($"Текуща информация за книга с идентификатор {id}:");
                    Console.WriteLine($"Заглавие: {existingBook.Title}");
                    Console.WriteLine($"Автор: {existingBook.Author}");
                    Console.WriteLine($"Жанр: {existingBook.Genre}");
                    Console.WriteLine($"Издателство: {existingBook.Publisher}");
                    Console.WriteLine($"Година: {existingBook.Year}");

                    Book updatedBook = new Book();
                    updatedBook.Id = id;
                    Console.WriteLine("Въведете нова информация за книгата:");
                    Console.Write("Въведете ново заглавие: ");
                    updatedBook.Title = Console.ReadLine();
                    Console.Write("Въведете нов автор: ");
                    updatedBook.Author = Console.ReadLine();
                    Console.Write("Въведете нов жанр: ");
                    updatedBook.Genre = Console.ReadLine();
                    Console.Write("Въведете ново издателство: ");
                    updatedBook.Publisher = Console.ReadLine();
                    Console.Write("Въведете нова година: ");
                    int year;
                    if (int.TryParse(Console.ReadLine(), out year))
                    {
                        updatedBook.Year = year;
                    }
                    else
                    {
                        Console.WriteLine("Невалидна година.");
                        return;
                    }

                    bookBusiness.Update(updatedBook);
                    Console.WriteLine("Книгата е успешно променена.");
                }
                else
                {
                    Console.WriteLine("Книга с такъв идентификатор не е намерена.");
                }
            }
            else
            {
                Console.WriteLine("Невалиден идентификатор.");
            }
        }

        static void DeleteBook(BookBusiness bookBusiness)
        {
            Console.Write("Въведете идентификатор на книгата, която искате да изтриете: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                var existingBook = bookBusiness.Get(id);
                if (existingBook != null)
                {
                    Console.WriteLine($"Информация за книга с идентификатор {id}:");
                    Console.WriteLine($"Заглавие: {existingBook.Title}");
                    Console.WriteLine($"Автор: {existingBook.Author}");
                    Console.WriteLine($"Жанр: {existingBook.Genre}");
                    Console.WriteLine($"Издателство: {existingBook.Publisher}");
                    Console.WriteLine($"Година: {existingBook.Year}");

                    Console.WriteLine("Сигурни ли сте, че искате да изтриете тази книга? (да/не)");
                    string confirmation = Console.ReadLine();
                    if (confirmation.ToLower() == "да")
                    {
                        bookBusiness.Delete(id);
                        Console.WriteLine("Книгата е успешно изтрита.");
                    }
                    else
                    {
                        Console.WriteLine("Изтриването е отказано.");
                    }
                }
                else
                {
                    Console.WriteLine("Книга с такъв идентификатор не е намерена.");
                }
            }
            else
            {
                Console.WriteLine("Невалиден идентификатор.");
            }
        }
    }
}

