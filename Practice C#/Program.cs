using System;

namespace Practice_c_
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.practiceBoxingUnboxing();
            
        }

        void practiceBoxingUnboxing()
        {
            object[] arr = new object[3];
            arr[0] = 1;
            arr[1] = "ab";
            arr[2] = true;
            foreach(var i in arr)
            {
                Console.WriteLine(i.GetType());
            }


        }
    }
}
