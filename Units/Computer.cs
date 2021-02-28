using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    public class Computer
    {
        private int id;
        private int number;

        public Computer(int _id, int _number)
        {
            id = _id;
            number = _number;
        }

        public int Id
        { get => id; }

        public int Number
        { get => number; }

    }
}
