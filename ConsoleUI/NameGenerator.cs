using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class NameGenerator
    {
        private string Name { get; set; }
        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;         //gets grandparent directory 

        //returns random name from Names.txt file
        public string GetNameFromFile()
        {
            string filePath = Path.Combine(Directory.GetParent(path).FullName, "Names.txt");        //get parent directory of path variable and combines it with Names.txt to full path
            Random rand = new Random();                             
            string[] names = File.ReadAllLines(filePath);                                           //get all linex from file
            int nameLine = rand.Next(names.Length);                                                 
            Name = names[nameLine];
            return Name;
        }
    }
}
