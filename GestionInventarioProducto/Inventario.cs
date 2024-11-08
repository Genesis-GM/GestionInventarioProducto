using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventarioProducto
{
    public class Inventario
    {
        private List<Producto> productos;

        public Inventario()
        {
            productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }

        public IEnumerable<Producto> FiltrarYOrdenarProductos(decimal precioMinimo)
        {
            return productos
                .Where(p => p.Precio > precioMinimo)
                .OrderBy(p => p.Precio);
        }

        // Actualizar precio de un producto por nombre
        public bool ActualizarPrecio(string nombre, decimal nuevoPrecio)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                Console.WriteLine($"El precio de {nombre} se ha actualizado a {nuevoPrecio:C}.");
                return true;
            }
            return false;
        }

        // Eliminar producto por nombre
        public bool EliminarProducto(string nombre)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                productos.Remove(producto);
                Console.WriteLine($"{nombre} ha sido eliminado del inventario.");
                return true;
            }
            return false;
        }

        // Conteo y agrupación de productos por rango de precio
        public void ContarYAgruparProductos()
        {
            var menosDe100 = productos.Count(p => p.Precio < 100);
            var entre100Y500 = productos.Count(p => p.Precio >= 100 && p.Precio <= 500);
            var masDe500 = productos.Count(p => p.Precio > 500);

            Console.WriteLine($"Productos con precio menor a 100: {menosDe100}");
            Console.WriteLine($"Productos con precio entre 100 y 500: {entre100Y500}");
            Console.WriteLine($"Productos con precio mayor a 500: {masDe500}");
        }

        // Reporte resumido del inventario
        public void GenerarReporteResumido()
        {
            int totalProductos = productos.Count;
            decimal precioPromedio = totalProductos > 0 ? productos.Average(p => p.Precio) : 0;
            var productoMasCaro = productos.OrderByDescending(p => p.Precio).FirstOrDefault();
            var productoMasBarato = productos.OrderBy(p => p.Precio).FirstOrDefault();

            Console.WriteLine($"Número total de productos: {totalProductos}");
            Console.WriteLine($"Precio promedio de todos los productos: {precioPromedio:C}");
            if (productoMasCaro != null)
            {
                Console.WriteLine($"Producto con el precio más alto: {productoMasCaro.Nombre}, Precio: {productoMasCaro.Precio:C}");
            }
            if (productoMasBarato != null)
            {
                Console.WriteLine($"Producto con el precio más bajo: {productoMasBarato.Nombre}, Precio: {productoMasBarato.Precio:C}");
            }
        }

        // Verificar si el producto existe por nombre
        public bool ProductoExiste(string nombre)
        {
            return productos.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
