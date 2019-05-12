using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacedOutLibrary
{
    class BookView
    {
        //props
        public Book Displaybook { get; set; }

        //const
        public BookView (Book DisplayBook)
        {
            this.Displaybook = DisplayBook;
        }

        //meth
        public void View()
        {
            Console.WriteLine("Here is your book's information: ");
            Console.WriteLine();
            Console.WriteLine($"Title: {Displaybook.Title}");
            Console.WriteLine($"Author: {Displaybook.Author}");
            Console.WriteLine($"Status: {(Displaybook.Status == true ? "CHECKED OUT" : "Available" )}");
            Console.WriteLine($"Due Date: {(Displaybook.Status == false ? "" : Displaybook.DueDate )}");
        }
    }
}
