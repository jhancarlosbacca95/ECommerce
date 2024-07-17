using Ecommerce.DTO;

namespace Ecommerce.Vista.Servicios.Contrato
{
    public interface IVentaServicio
    {
        Task<ResponseDTO<VentaDTO>> Crear(VentaDTO modelo);
    }
}
