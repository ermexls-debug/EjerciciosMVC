using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private const int OpeningHour = 8;
        private const int ClosingHour = 18;
        private const int SlotMinutes = 30;

        private readonly DataContext _context;

        public ReservationsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? serviceId, DateTime? date)
        {
            ReservationSearchViewModel model = new ReservationSearchViewModel
            {
                ServiceId = serviceId,
                Date = date ?? DateTime.Today,
                Services = await GetServicesAsync()
            };

            if (serviceId == null || date == null)
            {
                return View(model);
            }

            if (date.Value.Date < DateTime.Today)
            {
                model.Message = "No se pueden hacer reservas en fechas pasadas";
                return View(model);
            }

            Service? service = await _context.Services.FindAsync(serviceId.Value);

            if (service == null)
            {
                return NotFound();
            }

            model.SelectedService = service;
            model.AvailableSlots = await GetAvailableSlotsAsync(service, date.Value.Date);

            if (model.AvailableSlots.Count == 0)
            {
                model.Message = "No hay horarios disponibles para ese día. Pruebe con otra fecha";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int serviceId, DateTime start)
        {
            Service? service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            bool available = await IsAvailableAsync(start, start.AddMinutes(service.DurationMinutes));

            if (!available || start <= DateTime.Now)
            {
                TempData["Error"] = "Ese horario ya no está disponible";
                return RedirectToAction(nameof(Index), new { serviceId = serviceId, date = start.ToString("yyyy-MM-dd") });
            }

            ReservationFormViewModel model = new ReservationFormViewModel();
            FillServiceData(model, service, start);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationFormViewModel model)
        {
            Service? service = await _context.Services.FindAsync(model.ServiceId);

            if (service == null)
            {
                return NotFound();
            }

            DateTime end = model.StartAt.AddMinutes(service.DurationMinutes);
            bool available = await IsAvailableAsync(model.StartAt, end);

            if (!available || model.StartAt <= DateTime.Now)
            {
                TempData["Error"] = "Ese horario acaba de ser reservado por otra persona. Escoja otro";
                return RedirectToAction(nameof(Index), new { serviceId = model.ServiceId, date = model.StartAt.ToString("yyyy-MM-dd") });
            }

            if (!ModelState.IsValid)
            {
                FillServiceData(model, service, model.StartAt);
                return View(model);
            }

            Reservation reservation = new Reservation
            {
                ServiceId = service.Id,
                CustomerName = model.CustomerName!.Trim(),
                CustomerPhone = model.CustomerPhone!,
                StartAt = model.StartAt,
                EndAt = end,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = reservation.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Reservation? reservation = await _context.Reservations.Include(r => r.Service).FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        private void FillServiceData(ReservationFormViewModel model, Service service, DateTime start)
        {
            model.ServiceId = service.Id;
            model.ServiceName = service.Name;
            model.DurationMinutes = service.DurationMinutes;
            model.Price = service.Price;
            model.StartAt = start;
        }

        private async Task<List<DateTime>> GetAvailableSlotsAsync(Service service, DateTime date)
        {
            DateTime dayStart = date.Date;
            DateTime dayEnd = dayStart.AddDays(1);

            List<Reservation> reservations = await _context.Reservations
                .Where(r => r.StartAt >= dayStart && r.StartAt < dayEnd && r.Status != ReservationStatus.Cancelled)
                .ToListAsync();

            List<DateTime> slots = new List<DateTime>();
            DateTime slot = dayStart.AddHours(OpeningHour);
            DateTime closing = dayStart.AddHours(ClosingHour);

            while (slot.AddMinutes(service.DurationMinutes) <= closing)
            {
                DateTime slotEnd = slot.AddMinutes(service.DurationMinutes);
                bool busy = reservations.Any(r => r.StartAt < slotEnd && r.EndAt > slot);

                if (!busy && slot > DateTime.Now)
                {
                    slots.Add(slot);
                }

                slot = slot.AddMinutes(SlotMinutes);
            }

            return slots;
        }

        private async Task<bool> IsAvailableAsync(DateTime start, DateTime end)
        {
            if (start.Hour < OpeningHour || end > start.Date.AddHours(ClosingHour))
            {
                return false;
            }

            bool busy = await _context.Reservations.AnyAsync(r => r.Status != ReservationStatus.Cancelled && r.StartAt < end && r.EndAt > start);

            return !busy;
        }

        private async Task<List<SelectListItem>> GetServicesAsync()
        {
            return await _context.Services
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name + " (" + s.DurationMinutes.ToString() + " min)"
                })
                .ToListAsync();
        }
    }
}
