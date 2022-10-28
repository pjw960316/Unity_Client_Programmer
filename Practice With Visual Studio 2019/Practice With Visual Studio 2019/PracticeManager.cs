using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practice_With_Visual_Studio_2019
{
    public struct str_data
    {
        public int a;
    }
    public class data
    {
        public int a;

        public data(int num)
        {
            a = num;
        }
    }

    class PracticeManager
    {
        static void Main()
        {
            PracticeManager practice_manager = new PracticeManager();
            practice_manager.printMemoryAddressWithUnsafe();
            practice_manager.testStruct();
            practice_manager.testClass();
        }

        private void printMemoryAddressWithUnsafe()
        {
            unsafe //unsafe block
            {
                int number = 27;
                int* pointerToNumber = &number;

                Console.WriteLine($"Address of the variable: {(long)pointerToNumber:X}");
            }
        }

        private void testStruct()
        {
            str_data obj_1 = new str_data();
            obj_1.a = 10;
            str_data obj_2 = new str_data();
            obj_2.a = 20;
            str_data obj_3 = obj_2;
            obj_3.a = 30; //obj_3은 obj_2를 복사한 독립적인 객체기 때문에 obj_3의 값을 변화시킨다고 해서 obj_2에 영향을 미치지 않는다.

            Console.WriteLine(obj_1.a + " " + obj_2.a + " " + obj_3.a); //10 20 30
        }

        private void testClass()
        {
            data obj_1 = new data(10);
            data obj_2 = new data(10);
            data obj_3 = obj_2; //참조

            obj_2.a = 20;
            obj_3.a = 30; //obj_2와 obj_3은 같은 힙 메모리를 참조하고 있으므로 obj_2.a의 값도 30으로 변할 것 이다.
     
            Console.WriteLine(obj_1.a + " " + obj_2.a + " " + obj_3.a); //10 30 30
        }
    }
}
