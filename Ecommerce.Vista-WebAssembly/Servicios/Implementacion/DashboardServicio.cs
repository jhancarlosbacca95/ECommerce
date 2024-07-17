using Ecommerce.DTO;
using Ecommerce.Vista.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.Vista.Servicios.Implementacion
{
    public class DashboardServicio : IDashboardServicio
    {
        private readonly HttpClient _httpClient;
        public DashboardServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDTO<DashboardDTO>> Resumen()
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<DashboardDTO>>("Dashboard/Resumen");

        }
    }
}
