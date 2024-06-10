using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ShipmentModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ShipmentModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Shipment> Shipments { get; set; }

        public async Task OnGetAsync()
        {
            Shipments = await _httpClient.GetFromJsonAsync<IList<Shipment>>("https://localhost:7103/api/ShipmentController");
        }

        public class Shipment
        {
            public int Id { get; set; }
            public int TruckId { get; set; }
            public int WarehouseId { get; set; }
            public string Status { get; set; }
        }
    }
}
