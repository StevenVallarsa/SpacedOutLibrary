﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacedOutLibrary
{
    public class BookListView
    {
        //prop
        public List<Book> Books { get; set; } // "Books" list for
        public List<Book> Results { get; set; }
        //public List<Book> BooksOut { get; set; }

        //const
        public BookListView(List<Book> Books)
        {
            this.Books = Books;
        }

        public Controller c = new Controller(); // "c" variable used to bring in "Controller" class functionality

        //meth
        public void Display()
        {
            Console.Clear(); // clear screen and start fresh

            bool libraryOpen = true;
            while (libraryOpen)
            {

                bool run = true;
                while (run)
                {
                    Console.WriteLine("Welcome to the Spaced Out Library.");
                    Console.WriteLine("We offer a unique selection of sci-fi literature.");
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine();
                    Console.WriteLine(" 1) List all Books");
                    Console.WriteLine(" 2) Search by Title");
                    Console.WriteLine(" 3) Search by Author");
                    Console.WriteLine(" 4) Return Book");

                    Console.WriteLine();
                    Console.Write("Please choose 1, 2, 3 or 4: ");
                    string input = Console.ReadLine();

                    if (input == "1" || input.ToLower() == "list")
                    {
                        ListBooks();
                        Console.WriteLine();
                        int result = c.Checkout(Books);

                        if (result == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            GetCheckoutDate(result - 1);
                        }

                    }
                    else if (input == "2" || input.ToLower() == "title")
                    {

                        List<Book> titleResults = c.SearchTitle(Books);
                        View(titleResults);
                        if (titleResults.Count == 0)
                        {
                            Console.WriteLine();
                            break;
                        }

                        int selection = c.Checkout(titleResults);

                        if (selection == 0)
                        {
                            Console.Clear();
                            titleResults.Clear();
                            break;
                        }

                        int index = titleResults[selection - 1].Index;

                        GetCheckoutDate(index);
                        titleResults.Clear();

                    }
                    else if (input == "3" || input.ToLower() == "author")
                    {
                        List<Book> authorResults = c.SearchAuthor(Books);
                        View(authorResults);
                        if (authorResults.Count == 0)
                        {
                            Console.WriteLine();
                            break;
                        }

                        int selection = c.Checkout(authorResults);

                        if (selection == 0)
                        {
                            Console.Clear();
                            authorResults.Clear();
                            break;
                        }

                        int index = authorResults[selection - 1].Index;
                        
                        GetCheckoutDate(index);
                        authorResults.Clear();
                    }

                    //else if (input == "5")
                    //{

                    //}

                    else if (input == "4" || input.ToLower() == "return")
                    {
                        CheckedOutBooks();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Your selection was lost in time, like tears in rain.");
                        Console.WriteLine();
                    }

                }
            
            }

        }


        public void ListBooks()
        {
            Console.Clear();
            Console.WriteLine("Here is The Spaced Out Library's book selection.");
            Console.WriteLine();
            Console.WriteLine(" # TITLE:                    AUTHOR:             DUE DATE:");
            for (int i = 0; i < Books.Count; i++)
            {
                Console.WriteLine($"{i + 1,2} {Books[i].Title,-25} {Books[i].Author,-19} {(Books[i].Status == false ? "Available" : Books[i].DueDate)}");
            }

        }


        public void View(List<Book> Results)
        {
            if (Results.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("He's dead Jim! Your search came out NULL.");
            }
            else
            {
                Console.WriteLine(" # TITLE:                    AUTHOR:             DUE DATE:");
                for (int i = 0; i < Results.Count; i++)
                {
                    Console.WriteLine($"{i + 1,2} {Results[i].Title,-25} {Results[i].Author,-19} {(Results[i].Status == false ? "Available" : Results[i].DueDate)}");
                }
            }
        }

        public void CheckedOutBooks()
        {
            List<Book> BooksOut = new List<Book>();

            foreach (Book b in Books)
            {
                if (b.Status == true)
                {
                    BooksOut.Add(b);
                }
            }

            if (BooksOut.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Great Scotts!!! You have no books checked out.");
                Console.WriteLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Here are the awesome books you have checked out.");
                Console.WriteLine();
                Console.WriteLine(" # TITLE:                    AUTHOR:             DUE DATE:");
                
                for (int i = 0; i < BooksOut.Count; i++)
                {
                    Console.WriteLine($"{i + 1,2} {BooksOut[i].Title,-25} {BooksOut[i].Author,-19} {(BooksOut[i].DueDate)}");
                }

                int result = 0;
                bool run = true;
                while (run)
                {
                    Console.WriteLine();
                    Console.Write("Which book would you like to return | or \nenter 0 (zero) to return to the main menu: ");
                    string input = Console.ReadLine();

                    if (input == "0" || input.ToLower() == "zero")
                    {
                        run = false;
                    }

                    try
                    {
                        result = int.Parse(input);
                        if (result < 1 || result > BooksOut.Count)
                        {
                            throw new Exception("That does not compute. Try again.");
                        }
                        else
                        {
                            run = false;
                        }
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("I don't speak Klingon. Try again.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }

                int index = BooksOut[result - 1].Index;
                Books[index].Status = false;
                Books[index].DueDate = "";
                Console.Clear();
                Console.WriteLine($"You returned \"{Books[index].Title}\". May the Force be with you.");
                Console.WriteLine();
            }

        }

        public void GetCheckoutDate(int index)
        {

            Console.Clear();
            Books[index].Status = true;
            DateTime thismoment = DateTime.Today;
            DateTime twoWeeksFromNow = thismoment.AddDays(14);
            string date = twoWeeksFromNow.ToString();
            string date2 = date.Substring(0, date.IndexOf(' '));
            Console.WriteLine($"You checked out \"{Books[index].Title}\" and will be due back on {date2}.");
            Console.WriteLine();
            Books[index].DueDate = date2;

        }

    }

}