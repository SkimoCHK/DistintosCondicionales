using System.Diagnostics.CodeAnalysis;

namespace AdiosIFS
{
    public interface IOperation
    {
        void Operation(decimal total, ref decimal tax, ref decimal discount);
    }

    public class Operation1 : IOperation
    {
        public void Operation(decimal total, ref decimal tax, ref decimal discount)
            => tax = total * 0.2m;

    }

    public class Operation2 : IOperation
    {
        public void Operation(decimal total, ref decimal tax, ref decimal discount)
            => tax = total * 0.1m;

    }

    public class Operation3 : IOperation
    {
        public void Operation(decimal total, ref decimal tax, ref decimal discount)
            => discount = total * 0.2m;

    }

    public class Operation4 : IOperation
    {
        public void Operation(decimal total, ref decimal tax, ref decimal discount)
            => discount = total * 0.3m;

    }

    internal class Program
    {


        static void Main(string[] args)
        {
            decimal total = 1000;
            decimal discount = 0;
            decimal tax = 0;

            //primera forma de evaluar...
            //if (total < 10)
            //{
            //    tax = total * 0.2m;
            //}
            //else if (total >= 10 && total <= 100)
            //{
            //    tax = total * 0.1m;
            //}
            //else if (total >= 100 && total < 1000)
            //{
            //    discount = total * 0.2m;
            //}
            //else
            //{
            //    discount = total * 0.3m;
            //}

            //segunda forma de evaluar..
            //switch (total)
            //{
            //    case decimal t when (t < 10):
            //        tax = total * 0.2m;
            //        break;

            //    case decimal t when (t >= 10 && t < 100):
            //        tax = total * 0.1m;
            //        break;

            //    case decimal t when (t >= 100 && t < 1000):
            //        discount = total * 0.2m;
            //        break;
            //    default:
            //        discount = total * 0.3m;
            //        break;


            //}


            //Tercera forma de evaluar

            //var actions = new Dictionary<Predicate<decimal>, Action>
            //{
            //    {t => t < 10, () => tax = total * 0.2m},
            //    {t => t >=10 && t <100, () => tax = total * 0.1m},
            //    {t => t >= 100 && t < 100, () => discount = total * 0.2m},
            //    {t => t >= 1000, () => discount = total * 0.3m},

            //};

            //foreach(var action in actions)
            //{
            //    if (action.Key(total))
            //    {
            //        action.Value();
            //        break;
            //    }
            //}

            //Cuarta forma con patron de diseño
            //necesitaremos crear una interfaz, estara hasta arriba todo el codigo de las clases e interfaces..
            //ok aqui en vez de tener un diccionario con predicados y Action
            //vamos a tener predicados, y algo que implemente la interface

            var actions = new Dictionary<Predicate<decimal>, IOperation>
            {
                {t => t < 10, new Operation1()},
                {t => t >= 10 && t<100, new Operation2()},
                {t => t >= 100 && t<1000, new Operation3()},
                {t => t >=1000, new Operation4()},

            };

            var operation = actions.ToList().Find(d => d.Key(total)).Value;
            operation.Operation(total, ref tax, ref discount);


            Console.WriteLine(total + tax - discount);



        }

        //Hice este metodo nomas pa entender como funcionan los diccionarios
        static void EjemploDiccionarios()
        {
            Dictionary<string, int> EstudiantesEdad = new Dictionary<string, int>()
            { 
                //Clave - valor
                {"alextavo",14},
                {"Ivan", 12 }
            };
            EstudiantesEdad.Add("Alex", 22);
            EstudiantesEdad.Add("Skimo", 30);
            
                            //Este metodo sirve para verificar la existencia de un elemento mediante su clave
            if (EstudiantesEdad.ContainsKey("alextavo"))
            {
                Console.WriteLine("Existe alextavo!!\n");
            }
            else 
            { 
                Console.WriteLine("No existe\n"); 
            }

            //aqui recorremos con un foreach los valores del diccionario

            foreach(var estudiante in EstudiantesEdad)
            {
                Console.WriteLine($"Clave {estudiante.Key} \n Valor: {estudiante.Value} \n");
            }



        }
    }
}
