using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using Blazored.Toast;
using Blazored.LocalStorage;
using Blazored.Toast.Services;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ISyncLocalStorageService _syncLocalStorageService;
        private readonly IToastService _toastService;

        public CarritoServicio(
            ILocalStorageService localStorageService,
            ISyncLocalStorageService syncLocalStorageService,
            IToastService toastService
       
            )
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;
        }

        public event Action? MostrarItems;

        public async Task AgregarCarrito(CarritoDTO modelo)
        {
            try
            {
                // Obtiene el carrito actual del almacenamiento local (local storage).
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");

                // Si el carrito es null (es decir, no existe aún), inicializa una nueva lista.
                if (carrito == null)
                    carrito = new List<CarritoDTO>();

                // Busca un producto en el carrito que tenga el mismo IdProducto que el producto que se va a agregar.
                var encontrado = carrito.FirstOrDefault(c => c.Producto.IdProducto == modelo.Producto.IdProducto);

                // Si el producto ya existe en el carrito, lo elimina de la lista.
                if (encontrado != null)
                    carrito.Remove(encontrado);

                // Agrega el nuevo producto (o el actualizado) al carrito.
                carrito.Add(modelo);

                // Guarda el carrito actualizado en el almacenamiento local.
                await _localStorageService.SetItemAsync("carrito", carrito);

                // Muestra un mensaje de éxito dependiendo de si el producto fue actualizado o agregado.
                if (encontrado != null)
                    _toastService.ShowSuccess("El producto fue actualizado en carrito");
                else
                    _toastService.ShowSuccess("El producto fue agregado al carrito");

                // Invoca el evento MostrarItems, posiblemente para actualizar la interfaz de usuario.
                MostrarItems.Invoke();
            }
            catch
            {
                // En caso de una excepción, muestra un mensaje de error.
                _toastService.ShowError("No se pudo agregar al carrito");
            }
        }


        public int CantidadProductos()
        {
            var carrito = _syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");
            return carrito == null ? 0 : carrito.Count;
        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
            if (carrito == null)
                carrito = new List<CarritoDTO>();
            return carrito;
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito !=null)
                {
                    var elemento = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                    if (elemento!=null)
                    {
                        carrito.Remove(elemento);
                        await _localStorageService.SetItemAsync("carrito", carrito);

                        MostrarItems.Invoke();
                    }
                }
            }
            catch
            {
            }
        }

        public async Task LimpiarCarrito()
        {
            await _localStorageService.RemoveItemAsync("carrito");
            MostrarItems.Invoke();
        }
    }
}
