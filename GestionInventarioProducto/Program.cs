using System;

namespace GestionInventarioProducto
{
    using System;

    namespace GestionInventarioProducto
    {
        class Program
        {
            public static void Main(string[] args)
            {
                Inventario inventario = new Inventario();
                Console.WriteLine("Bienvenido al sistema de gestión de inventario.");

                // Ingreso de productos por el usuario
                Console.WriteLine("¿Cuántos productos desea ingresar? ");
                if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
                {
                    Console.WriteLine("Por favor, ingrese una cantidad válida de productos.");
                    return;
                }

                for (int i = 0; i < cantidad; i++)
                {
                    Console.WriteLine($"\nProducto {i + 1}: ");
                    string nombre;
                    do
                    {
                        Console.WriteLine("Nombre: ");
                        nombre = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            Console.WriteLine("El nombre no puede estar vacío.");
                        }
                    } while (string.IsNullOrWhiteSpace(nombre));

                    decimal precio;
                    do
                    {
                        Console.WriteLine("Precio: ");
                        if (!decimal.TryParse(Console.ReadLine(), out precio) || precio < 0)
                        {
                            Console.WriteLine("Ingrese un precio válido y positivo.");
                        }
                    } while (precio < 0);

                    Producto producto = new Producto(nombre, precio);
                    inventario.AgregarProducto(producto);
                }

                // Filtro por precio mínimo
                Console.WriteLine("\nIngrese el precio mínimo para filtrar los productos: ");
                decimal precioMinimo;
                while (!decimal.TryParse(Console.ReadLine(), out precioMinimo) || precioMinimo < 0)
                {
                    Console.WriteLine("Por favor, ingrese un precio mínimo válido.");
                }

                // Filtrar y mostrar productos
                var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);
                Console.WriteLine("\nProductos filtrados y ordenados: ");
                foreach (var producto in productosFiltrados)
                {
                    Console.WriteLine(producto);
                }

                // Actualización de precio de producto
                Console.WriteLine("\n¿Desea actualizar el precio de algún producto? (si/no)");
                string respuesta = Console.ReadLine().ToLower();
                if (respuesta == "si")
                {
                    bool productoEncontrado = false;
                    string nombreProducto = string.Empty;

                    while (!productoEncontrado)
                    {
                        Console.WriteLine("Ingrese el nombre del producto a actualizar:");
                        nombreProducto = Console.ReadLine();

                        if (inventario.ProductoExiste(nombreProducto))
                        {
                            productoEncontrado = true;
                        }
                        else
                        {
                            Console.WriteLine("Producto no encontrado. Por favor, intente de nuevo.");
                        }
                    }

                    Console.WriteLine("Ingrese el nuevo precio:");
                    if (decimal.TryParse(Console.ReadLine(), out decimal nuevoPrecio) && nuevoPrecio >= 0)
                    {
                        inventario.ActualizarPrecio(nombreProducto, nuevoPrecio);
                    }
                    else
                    {
                        Console.WriteLine("Precio inválido.");
                    }
                }

                // Eliminación de producto
                Console.WriteLine("\n¿Desea eliminar algún producto? (si/no)");
                respuesta = Console.ReadLine().ToLower();
                if (respuesta == "si")
                {
                    bool productoEncontrado = false;
                    string nombreProducto = string.Empty;

                    while (!productoEncontrado)
                    {
                        Console.WriteLine("Ingrese el nombre del producto a eliminar:");
                        nombreProducto = Console.ReadLine();

                        if (inventario.ProductoExiste(nombreProducto))
                        {
                            productoEncontrado = true;
                        }
                        else
                        {
                            Console.WriteLine("Producto no encontrado. Por favor, intente de nuevo.");
                        }
                    }

                    inventario.EliminarProducto(nombreProducto);
                }

                // Generar y mostrar el reporte resumido del inventario
                inventario.GenerarReporteResumido();
            }
        }
    }
}