using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly IRepository<Appointment> _appointmentRepository;

        public AppointmentsController(IRepository<Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IActionResult> Index(DateTime? searchDate, Status? searchStatus, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAsync(tracked: false, include: a => a.Include(a => a.Patient).Include(a => a.Doctor).AsQueryable(), cancellationToken: cancellationToken);

            if (searchDate is not null)
            {
                appointments = appointments.Where(a => a.AppointmentDate == searchDate);
                ViewBag.SearchDate = searchDate.Value.ToString("yyyy-MM-dd");
            }

            if (searchStatus is not null)
            {
                appointments = appointments.Where(a => a.Status == searchStatus);
                ViewBag.SearchStatus = searchStatus;
            }

            return View(appointments.AsEnumerable());
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetOneAsync(e => e.Id == id, tracked: false, include: a => a.Include(a => a.Patient).Include(a => a.Doctor), cancellationToken: cancellationToken);
            if (appointment is null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetOneAsync(e => e.Id == id, cancellationToken: cancellationToken);
            if (appointment is null)
            {
                return NotFound();
            }

            appointment.Status = Status.Cancelled;
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.CommitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
    }
}
