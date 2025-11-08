using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Data;
using ColegioSanJose.Models;
using System.Linq;

namespace ColegioSanJose.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpedientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expedientes
        public async Task<IActionResult> Index()
        {
            var expedientes = _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia);

            return View(await expedientes.ToListAsync());
        }

        // GET: Expedientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);

            if (expediente == null) return NotFound();

            return View(expediente);
        }

        // GET: Expedientes/Create
        public IActionResult Create()
        {
            PopulateAlumnoMateriaSelects();
            return View();
        }

        // POST: Expedientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores, repoblar selects y devolver la vista
            PopulateAlumnoMateriaSelects(expediente.AlumnoId, expediente.MateriaId);
            return View(expediente);
        }

        // GET: Expedientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var expediente = await _context.Expedientes.FindAsync(id);
            if (expediente == null) return NotFound();

            PopulateAlumnoMateriaSelects(expediente.AlumnoId, expediente.MateriaId);
            return View(expediente);
        }

        // POST: Expedientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (id != expediente.ExpedienteId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedienteExists(expediente.ExpedienteId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateAlumnoMateriaSelects(expediente.AlumnoId, expediente.MateriaId);
            return View(expediente);
        }

        // GET: Expedientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);

            if (expediente == null) return NotFound();

            return View(expediente);
        }

        // POST: Expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.Expedientes.FindAsync(id);
            if (expediente != null)
            {
                _context.Expedientes.Remove(expediente);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PromedioPorAlumno()
        {
            var promedios = await _context.Expedientes
                .Include(e => e.Alumno)
                .GroupBy(e => new { e.Alumno.AlumnoId, e.Alumno.Nombre, e.Alumno.Apellido })
                .Select(g => new AlumnoPromedioViewModel
                {
                    AlumnoId = g.Key.AlumnoId,
                    NombreCompleto = g.Key.Nombre + " " + g.Key.Apellido,
                    Promedio = g.Average(x => x.NotaFinal),
                    CantMaterias = g.Count()
                })
                .ToListAsync();

            return View(promedios);
        }

        private bool ExpedienteExists(int id)
        {
            return _context.Expedientes.Any(e => e.ExpedienteId == id);
        }

        // ----------------------------
        // Helper: popular SelectLists
        // ----------------------------
        private void PopulateAlumnoMateriaSelects(int? selectedAlumnoId = null, int? selectedMateriaId = null)
        {
            var alumnos = _context.Alumnos
                .OrderBy(a => a.Nombre)
                .ThenBy(a => a.Apellido)
                .Select(a => new
                {
                    Id = a.AlumnoId,
                    NombreCompleto = a.Nombre + " " + a.Apellido
                })
                .ToList();

            var materias = _context.Materias
                .OrderBy(m => m.NombreMateria)
                .Select(m => new
                {
                    Id = m.MateriaId,
                    NombreMateria = m.NombreMateria
                })
                .ToList();

            ViewData["AlumnoId"] = new SelectList(alumnos, "Id", "NombreCompleto", selectedAlumnoId);
            ViewData["MateriaId"] = new SelectList(materias, "Id", "NombreMateria", selectedMateriaId);
        }
    }
}
