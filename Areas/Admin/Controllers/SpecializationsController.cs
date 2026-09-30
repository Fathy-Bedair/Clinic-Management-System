using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecializationsController : Controller
    {
        public readonly IRepository<Specialization> _SpecializationRepository;

        public SpecializationsController(IRepository<Specialization> specializationRepository)
        {
            _SpecializationRepository = specializationRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var specializations = await _SpecializationRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);
            return View(specializations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Specialization specialization, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(specialization);
            }
            await _SpecializationRepository.AddAsync(specialization, cancellationToken);
            await _SpecializationRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var specialization = await _SpecializationRepository.GetOneAsync(s => s.Id == id, tracked: false, cancellationToken: cancellationToken);
            return View(specialization);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Specialization specialization, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return View(specialization);
            }
            _SpecializationRepository.Update(specialization);
            await _SpecializationRepository.CommitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var specialization = await _SpecializationRepository.GetOneAsync(s => s.Id == id, cancellationToken: cancellationToken);
            if (specialization is not null)
            {
                _SpecializationRepository.Delete(specialization);
                await _SpecializationRepository.CommitAsync(cancellationToken);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
