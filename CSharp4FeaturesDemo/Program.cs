using System;
using System.Collections.Generic;
using System.Drawing;

namespace CSharp4FeaturesDemo
{
    // Example class to demonstrate dynamic binding
    public class DynamicExample
    {
        public void SayHello(string name)
        {
            Console.WriteLine($"Hello, {name}!");
        }
    }

    // Interface to demonstrate covariance
    public interface IAnimal<out T>
    {
        T GetAnimal();
    }

    // Covariant implementation
    public class Dog : IAnimal<Dog>
    {
        public Dog GetAnimal() => new Dog();

        public override string ToString() => "I am a Dog.";
    }

    public class Cat : IAnimal<Cat>
    {
        public Cat GetAnimal() => new Cat();

        public override string ToString() => "I am a Cat.";
    }

    // Main Program
    class Program
    {
        // Method to demonstrate named and optional parameters
        public static void DisplayInfo(string message, string prefix = "Info", int repeat = 1)
        {
            for (int i = 0; i < repeat; i++)
            {
                Console.WriteLine($"[{prefix}] {message}");
            }
        }

        // Method demonstrating covariance
        public static void PrintAnimal(IAnimal<object> animal)
        {
            Console.WriteLine($"Animal: {animal.GetAnimal()}");
        }

        // 🔹 2. Value types (struct)
        struct Point
        {
            public int X, Y;
            public override string ToString() => $"Point({X}, {Y})";

        }
        // 🔹 3. Enum
        enum Colors { Red, Green, Blue }

        // 🔹 4. Delegates with covariance and contravariance
        delegate IAnimal<Dog> CovariantDelegate();  // out: return IAnimal o derivado
        delegate void ContravariantDelegate(Dog dog);  // in: accept Dog o base


        static void Main(string[] args)
        {
            // Dynamic binding
            Console.WriteLine("Dynamic Binding:");
            //dynamic is a keyword in C# that allows for dynamic typing
            // and late binding. It is used to bypass compile-time type checking.
            // The dynamic type can hold any type of object, and the actual type is resolved at runtime.
            //Represents an object whose operations will be resolved at runtime.
            dynamic dynamicObject = new DynamicExample();
            dynamicObject.SayHello("Alice"); // Runtime binding
            Console.WriteLine();

            // Named and Optional Parameters
            Console.WriteLine("Named and Optional Parameters:");
            DisplayInfo("This is a standard message."); // Using defaults
            DisplayInfo("This is a warning message.", prefix: "Warning"); // Named parameter
            DisplayInfo("Repeating message.", repeat: 3); // Mixing defaults and named parameters
            Console.WriteLine();

            // Covariance
            Console.WriteLine("Covariance: Types by reference\r\n");
            IAnimal<object> dog = new Dog(); // Covariance allows this assignment
            dog.GetAnimal(); // Returns a Dog
            IAnimal<object> cat = new Cat(); // Covariance allows this assignment
            cat.GetAnimal(); // Returns a Cat

            Console.WriteLine("\n=== Types by value (struct) ===");
            Point point = new Point { X = 10, Y = 20 };
            object o = point;
            Console.WriteLine($"Boxed Punto: {o}");

            Console.WriteLine("\n=== Enum ===");
            Colors color = Colors.Green;
            object objColor = color; // Boxing
            Console.WriteLine($"Color boxed como object: {objColor}");

            //Console.WriteLine("\n=== Covariant Delegate ===");
            //CovariantDelegate covariantDelegate = () => new Dog(); // OK: Dog -> IAnimal
            //IAnimal<Dog> catDelegate = covariantDelegate; // OK: Cat -> IAnimal

            //Console.WriteLine("\n=== Covariant Delegate ===");
            //CovariantDelegate del1 = () => new Dog(); // OK: Dog -> Animal
            //Animal result = del1();
            //result.Speak();

            //Console.WriteLine("\n=== Contravariant Delegate ===");
            //ContravariantDelegate del2 = (Animal ani) => ani.Speak(); // OK: Animal <- Dog
            //del2(d);

            //Console.WriteLine("\n=== Covariant Interface ===");
            //ICovariant<Animal> factory = new DogFactory(); // OK due to covariance (out)
            //Animal newAnimal = factory.Create();
            //newAnimal.Speak();

            //Console.WriteLine("\n=== Contravariant Interface ===");
            //IContravariant<Dog> processor = new AnimalProcessor(); // OK due to contravariance (in)
            //processor.Process(new Dog());

            Console.WriteLine("\n=== End of demonstration ===");


            PrintAnimal(dog);
            PrintAnimal(cat);
            Console.WriteLine();






            Console.WriteLine("C# 4.0 Features Demonstrated Successfully!");
        }
    }
}
