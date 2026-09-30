using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorsController : Controller
    {
        private readonly IRepository<Doctor> _DoctorRepository;
        private readonly IRepository<Specialization> _SpecializationRepository;

        public DoctorsController(IRepository<Doctor> doctorRepository, IRepository<Specialization> specializationRepository)
        {
            _DoctorRepository = doctorRepository;
            _SpecializationRepository = specializationRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            
            var doctors = await _DoctorRepository.GetAsync(tracked: false, include:d => d.Include(s => s.Specialization), cancellationToken: cancellationToken);
            return View(doctors);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var doctor = await _DoctorRepository.GetOneAsync(e=>e.Id == id, tracked: false, include:d => d.Include(s => s.Specialization), cancellationToken: cancellationToken);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var specializations = await _SpecializationRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);
            ViewBag.Specializations = new SelectList(specializations,nameof(Specialization.Id),nameof(Specialization.Name));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var specializations = await _SpecializationRepository.GetAsync(cancellationToken: cancellationToken);
                ViewBag.Specializations = new SelectList(specializations,nameof(Specialization.Id),nameof(Specialization.Name));
                return View(doctor);
            }

            // Normalize the phone number to start with "0" if it starts with "+20" or "0020"
            if (doctor.PhoneNumber.StartsWith("+20"))
            {
                doctor.PhoneNumber = "0" + doctor.PhoneNumber.Substring(3);
            }

            else if (doctor.PhoneNumber.StartsWith("0020"))
            {
                doctor.PhoneNumber = "0" + doctor.PhoneNumber.Substring(4);
            }

            await _DoctorRepository.AddAsync(doctor, cancellationToken: cancellationToken);
            await _DoctorRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var doctor = await _DoctorRepository.GetOneAsync(e => e.Id == id, tracked: false, cancellationToken: cancellationToken);
            if (doctor == null)
            {
                return NotFound();
            }

            var specializations = await _SpecializationRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);
            ViewBag.Specializations = new SelectList(specializations, nameof(Specialization.Id), nameof(Specialization.Name));

            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var specializations = await _SpecializationRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);
                ViewBag.Specializations = new SelectList(specializations, nameof(Specialization.Id), nameof(Specialization.Name));
                return View(doctor);
            }
            // Normalize the phone number to start with "0" if it starts with "+20" or "0020"
            if (doctor.PhoneNumber.StartsWith("+20"))
            {
                doctor.PhoneNumber = "0" + doctor.PhoneNumber.Substring(3);
            }
            else if (doctor.PhoneNumber.StartsWith("0020"))
            {
                doctor.PhoneNumber = "0" + doctor.PhoneNumber.Substring(4);
            }

            _DoctorRepository.Update(doctor);
            await _DoctorRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var doctor = await _DoctorRepository.GetOneAsync(e => e.Id == id, cancellationToken: cancellationToken);
            if (doctor == null)
            {
                return NotFound();
            }
            _DoctorRepository.Delete(doctor);
            await _DoctorRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }

    }
}
