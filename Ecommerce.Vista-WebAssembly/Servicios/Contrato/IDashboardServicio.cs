using Ecommerce.DTO;
namespace Ecommerce.Vista.Servicios.Contrato
{
    public interface IDashboardServicio
    {
        Task<ResponseDTO<DashboardDTO>> Resumen();
    }
}
