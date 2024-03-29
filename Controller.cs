﻿using System;
using System.Collections.Generic;

namespace SpacedOutLibrary
{
    public class Controller
    {
        public List<Book> Books = new List<Book>();

        List<string> book = new List<string>() {"Farenheit 451", "Ray Radbury", "Dune",
                "Frank Herbert", "The Callista Chronicles", "Callista Gloss", "Frankenstein", "Mary Shelly",
            "A Wrinkle in Time", "Madeleine L'Engle", "2001", "Arthur C. Clarke",
                "Ender's Game", "Orson Scott Card", "Star Wars", "George Scott Lucas",
            "I,Robot","Issac Asimov", "Jurassic Park", "Michael Crichton", "1984", "George Orwell", "Avengers End Game", "Stan Lee",
            };


        public List<Book> Results = new List<Book>();

        public Controller()
        {
            for (int i = 0; i < book.Count; i += 2)
            {
                Book b = new Book();

                b.Index = i / 2;
                b.Title = book[i];
                b.Author = book[i + 1];
                b.Status = false;
                b.DueDate = "";

                Books.Add(b);
            }

        }


        public int Checkout(List<Book> Results)
        {
            int selection = 0;
            bool run = true;
            while (run)
            {
                Console.WriteLine();
                Console.Write("Select a book number to check out, or \n\"0\" (zero) to return to the main menu: ");
                string input = Console.ReadLine();

                if (input == "0" || input.ToLower() == "zero")
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        selection = int.Parse(input);
                        if (selection < 1 || selection > Results.Count)
                        {
                            throw new Exception("KAAAAAAAHN!!! Select a book in the above range.");
                        }

                        int index = Results[selection - 1].Index;
                        if (Books[index].Status == true)
                        {
                            throw new Exception("Your selection is not logical for it is already being enjoyed \nby another Carbon Unit.");
                        }

                        run = false;

                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("I'm familiar with over six millions forms of communication,"); 
                        Console.WriteLine("but not that one. Please try again.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                    }
                }

            }
            return selection;
        }


        public void Run()
        {
            BookListView blv = new BookListView(Books);
            blv.Display();

        }


        public List<Book> SearchAuthor(List<Book> bs)
        {
            Console.WriteLine();
            Console.Write("What AUTHOR are you searching for? ");
            string input = Console.ReadLine().ToLower();
            Console.WriteLine();

            foreach (Book b in bs)
            {
                if (b.Author.ToLower().Contains(input))
                {
                    Results.Add(b);
                }

            }
            return Results;
        }


        public List<Book> SearchTitle(List<Book> bs)
        {
            Console.WriteLine();
            Console.Write("What TITLE are you searching for? ");
            string input = Console.ReadLine().ToLower();
            Console.WriteLine();

            foreach (Book b in bs)
            {
                if (b.Title.ToLower().Contains(input))
                {
                    Results.Add(b);
                }

            }
            return Results;
        }

        //public void PrintBooks()
        //{
        //    for(int i = 0; i < Books.Count; i++)
        //    {
        //        Console.WriteLine($"{i + 1,2} {Books[i].Title,-25} {Books[i].Author,-19} {(Books[i].Status == false ? "Available" : Books[i].DueDate)}");
        //    }

        //}

    }

}