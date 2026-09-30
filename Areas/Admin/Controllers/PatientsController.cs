using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientsController : Controller
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientsController(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var patients = await _patientRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);
            return View(patients);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetOneAsync(e => e.Id == id, tracked: false, include: p => p.Include(a => a.Appointments).ThenInclude(d => d.Doctor).Include(m => m.MedicalRecords), cancellationToken: cancellationToken);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetOneAsync(e => e.Id == id, tracked: false, cancellationToken: cancellationToken);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return View(patient);
            }
            _patientRepository.Update(patient);
            await _patientRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetOneAsync(e => e.Id == id, cancellationToken: cancellationToken);
            if (patient == null)
            {
                return NotFound();
            }
            _patientRepository.Delete(patient);
            await _patientRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));


        }
    }
}
