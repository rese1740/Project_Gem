using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJW_2025137033 : MonoBehaviour
{
    
    class Test
    {
        public int Power(int x)
        {
            return x * x;
        }
    }

    class TestC
    {
        public int Sum(int min, int max)
        {
            int output = 0;
            for(int i = min; i <= max; i++)
            {
                output += 1;
            }
            return output;
        }
    }

    class MyMath
    {
        public static int Abs(int input)
        {
            if (input < 0)
            {
                return -input;
            }
            else
            {
                return input;
            }
        }
    }

    class MyMath1
    {
        public static int Abs(int input)
        {
            if (input < 0) { return -input; }
            else { return input; }
        }

        public static double Abs(double input)
        {
            if (input < 0) { return -input; }
            else { return input; }
        }

        public static long Abs(long input)
        {
            if (input < 0) { return -input; }
            else { return input; }
        }
    }



    class Product
    {
        public static int counter = 0;
        public int id;
        public string name;
        public int price;

        public Product(string name, int price)
        {
            Product.counter = counter + 1;
            this.id = counter;
            this.name = name;
            this.price = price;
        }
    }

    class Product2
    {
        public string name;
        public int price;

        public Product2(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        ~Product2()
        {
            Debug.Log(this.name + "의 소멸자 호출");
        }
    }


    void Start()
    {
        Test test = new Test();
        Debug.Log(test.Power(10));
        Debug.Log(test.Power(20));

        TestC testC = new TestC();
        Debug.Log(testC.Sum(1, 100));

        Debug.Log(MyMath.Abs(52));
        Debug.Log(MyMath.Abs(-273));

        // int
        Debug.Log(MyMath1.Abs(52));
        Debug.Log(MyMath1.Abs(-273));

        // double
        Debug.Log(MyMath1.Abs(52.273));
        Debug.Log(MyMath1.Abs(-32.103));

        // long
        Debug.Log(MyMath1.Abs(21474836470));
        Debug.Log(MyMath1.Abs(-21474836470));




        Product productA = new Product("감자", 2000);
        Product productB = new Product("고구마", 3000);

        Debug.Log(productA.id + ":" + productA.name);
        Debug.Log(productB.id + ":" + productB.name);
        Debug.Log(Product.counter + "개 생성되었습니다.");

        Product2 product = new Product2("과자", 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
