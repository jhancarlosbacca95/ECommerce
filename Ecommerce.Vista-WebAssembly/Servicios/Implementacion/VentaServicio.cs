using Ecommerce.DTO;
using Ecommerce.Vista.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.Vista.Servicios.Implementacion
{
    public class VentaServicio : IVentaServicio
    {
        private readonly HttpClient _httpClient;

        public VentaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDTO<VentaDTO>> Crear(VentaDTO modelo)
        {
            var response = await _httpClient.PostAsJsonAsync("Venta/Registrar", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<VentaDTO>>();
            return result!;
        }
    }
}
