using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_With_Visual_Studio_2019
{
    class PracticeManager
    {
        static void Main()
        {
            PracticeManager practice_manager = new PracticeManager();
            practice_manager.printMemoryAddressWithUnsafe();
        }

        private void printMemoryAddressWithUnsafe()
        {
            unsafe //unsafe block
            {
                int number = 27;
                int* pointerToNumber = &number;

                Console.WriteLine($"Value of the variable: {number}");
                Console.WriteLine($"Address of the variable: {(long)pointerToNumber:X}");
            }
        }
    }
}
