using System;

public class Animal { public string Name = "Animal"; }
public class Dog : Animal { public Dog() { Name = "Dog"; } }

// Covariant (out) → produces values
public interface IProducer<out T>
{
    T Produce();
}

// Contravariant (in) → consumes values
public interface IConsumer<in T>
{
    void Consume(T item);
}

public class DogProducer : IProducer<Dog>
{
    public Dog Produce() => new Dog();
}

public class AnimalConsumer : IConsumer<Animal>
{
    public void Consume(Animal item)
    {
        Console.WriteLine($"Consumed: {item.Name}");
    }
}

public class GenericVariance
{
    public static void Main()
    {
        // Thanks to variance, these are now allowed:
        IProducer<Animal> p = new DogProducer();   // Dog → Animal (Covariance)
        IConsumer<Dog> c = new AnimalConsumer();   // AnimalConsumer can consume Dog (Contravariance)

        Use(p, c);
    }

    public static void Use(IProducer<Animal> producer, IConsumer<Dog> consumer)
    {
        Animal a = producer.Produce();   // actually returns Dog

        if (a is Dog d)                  // safely cast if it's Dog
        {
            consumer.Consume(d);
        }
    }
}
