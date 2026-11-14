//Pila: Infija --> Postfija --> Evaluacion
//Por ejemplo: (5 + 3) * 2
//Convertir 5 3 + 2 *
//Obtener Resultado = 16
//Calculadora que convierta expresiones infijas a posfijas y posteriormente las evalue utilizando pilas
//Funciones: Validar expresion - Convertir infija --> postfija - Evaluar expresiones postfija - Manejar parentesis - Manejar operadores (+ - * / % p)
//Detectar errores matematicos
//ejemplo entrada: (10 + 5) * 2 - 8
//Postfilia?:

using System;
using System.Globalization;
using System.Text;
namespace P4_Calc_Pilas
{
    internal class Program
    {
        static int Prioridad(char operador)
        {
            switch (operador)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                case '%':
                    return 2;

                case '^':
                    return 3;

                default:
                    return 0;
            }
        }

        static string InfijaAPostfija(string expresion)
        {
            Pila pila = new Pila();
            StringBuilder salida = new StringBuilder();

            int i = 0;
            bool esperaOperando = true;

            while (i < expresion.Length)
            {
                char caracter = expresion[i];

                if (char.IsWhiteSpace(caracter))
                {
                    i++;
                    continue;
                }

                // Leer numeros completos, incluyendo decimales
                if (char.IsDigit(caracter) || caracter == '.')
                {
                    if (!esperaOperando)
                        throw new Exception("Falta un operador entre los numeros.");

                    StringBuilder numero = new StringBuilder();
                    bool punto = false;

                    while (i < expresion.Length &&
                          (char.IsDigit(expresion[i]) || expresion[i] == '.'))
                    {
                        if (expresion[i] == '.')
                        {
                            if (punto)
                                throw new Exception("Numero decimal incorrecto.");

                            punto = true;
                        }

                        numero.Append(expresion[i]);
                        i++;
                    }

                    double valor;

                    if (!double.TryParse(numero.ToString(),
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture, out valor))
                    {
                        throw new Exception("Numero no valido.");
                    }

                    salida.Append(numero + " ");
                    esperaOperando = false;
                    continue;
                }

                if (caracter == '(')
                {
                    if (!esperaOperando)
                        throw new Exception("Falta un operador antes del parentesis.");

                    pila.Apilar(caracter);
                }
                else if (caracter == ')')
                {
                    if (esperaOperando)
                        throw new Exception("Parentesis vacio o expresion incompleta.");

                    while (!pila.Vacia() && pila.Tope() != '(')
                    {
                        salida.Append(pila.Desapilar() + " ");
                    }

                    if (pila.Vacia())
                        throw new Exception("Parentesis sin abrir.");

                    pila.Desapilar();
                }
                else if ("+-*/%^".Contains(caracter))
                {
                    if (esperaOperando)
                        throw new Exception("Operador sin operando.");

                    while (!pila.Vacia() &&
                           pila.Tope() != '(' &&
                           (Prioridad(pila.Tope()) > Prioridad(caracter) ||
                           (Prioridad(pila.Tope()) == Prioridad(caracter) && caracter != '^')))
                    {
                        salida.Append(pila.Desapilar() + " ");
                    }

                    pila.Apilar(caracter);
                    esperaOperando = true;
                }
                else
                {
                    throw new Exception("Caracter no permitido: " + caracter);
                }

                i++;
            }

            if (esperaOperando)
                throw new Exception("La expresion esta incompleta.");

            while (!pila.Vacia())
            {
                char operador = pila.Desapilar();

                if (operador == '(')
                    throw new Exception("Parentesis sin cerrar.");

                salida.Append(operador + " ");
            }

            return salida.ToString().Trim();
        }

        static double EvaluarPostfija(string expresion)
        {
            PilaNumeros pila = new PilaNumeros();

            string[] elementos = expresion.Split(' ');

            foreach (string elemento in elementos)
            {
                double numero;

                if (double.TryParse(elemento,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture, out numero))
                {
                    pila.Apilar(numero);
                }
                else
                {
                    if (pila.Cantidad() < 2)
                        throw new Exception("Expresion postfija incorrecta.");

                    double b = pila.Desapilar();
                    double a = pila.Desapilar();
                    double resultado = 0;

                    switch (elemento)
                    {
                        case "+":
                            resultado = a + b;
                            break;

                        case "-":
                            resultado = a - b;
                            break;

                        case "*":
                            resultado = a * b;
                            break;

                        case "/":
                            if (b == 0)
                                throw new Exception("No se puede dividir entre cero.");

                            resultado = a / b;
                            break;

                        case "%":
                            if (b == 0)
                                throw new Exception("No se puede calcular modulo entre cero.");

                            resultado = a % b;
                            break;

                        case "^":
                            resultado = Math.Pow(a, b);
                            break;
                    }

                    if (double.IsNaN(resultado) || double.IsInfinity(resultado))
                        throw new Exception("El resultado matematico no es valido.");

                    pila.Apilar(resultado);
                }
            }

            if (pila.Cantidad() != 1)
                throw new Exception("La expresion no es valida.");

            return pila.Desapilar();
        }

        static void Main()
        {
            Console.WriteLine("==== CALCULADORA CON PILAS ====");
            Console.Write("Expresion: ");

            string expresion = Console.ReadLine();

            try
            {
                string postfija = InfijaAPostfija(expresion);
                double resultado = EvaluarPostfija(postfija);

                Console.WriteLine("\nExpresion postfija: " + postfija);
                Console.WriteLine("Resultado: " + resultado);
            }
            catch (Exception error)
            {
                Console.WriteLine("\nError: " + error.Message);
            }

            Console.ReadKey();
        }
    }
}
