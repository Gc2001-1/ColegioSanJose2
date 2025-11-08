using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Data;
using ColegioSanJose.Models;

namespace ColegioSanJose.Controllers
{
    public class MateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MateriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Materias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materias.ToListAsync());
        }

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var materia = await _context.Materias
                .FirstOrDefaultAsync(m => m.MateriaId == id);
            if (materia == null) return NotFound();

            return View(materia);
        }

        // GET: Materias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MateriaId,NombreMateria,Docente")] Materia materia)
        {
            // --- Depuración: mostrar lo que llega en Request.Form (temporal) ---
            Console.WriteLine("=== POST /Materias/Create recibido ===");
            foreach (var key in Request.Form.Keys)
            {
                Console.WriteLine($"Form key: {key} = '{Request.Form[key]}'");
            }

            // --- Depuración: mostrar errores de ModelState (si los hay) ---
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState inválido. Errores:");
                foreach (var kv in ModelState)
                {
                    foreach (var err in kv.Value.Errors)
                    {
                        var exMsg = err.Exception != null ? $" (Exception: {err.Exception.Message})" : "";
                        Console.WriteLine($" - Key: {kv.Key} Error: '{err.ErrorMessage}'{exMsg}");
                    }
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(materia);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Materia guardada correctamente. Id: {materia.MateriaId}");
                    return RedirectToAction(nameof(Index));
                }

                // Si llegamos aquí, ModelState no es válido: volver a mostrar la vista con los datos
                return View(materia);
            }
            catch (Exception ex)
            {
                // Registrar excepción completa para depuración
                Console.WriteLine("Excepción al intentar guardar la materia:");
                Console.WriteLine(ex.ToString());

                // Mostrar un error amigable en la vista
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar la materia. Revisa la salida del servidor para más detalles.");
                return View(materia);
            }
        }

        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return NotFound();
            return View(materia);
        }

        // POST: Materias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MateriaId,NombreMateria,Docente")] Materia materia)
        {
            if (id != materia.MateriaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(materia.MateriaId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(materia);
        }

        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var materia = await _context.Materias
                .FirstOrDefaultAsync(m => m.MateriaId == id);
            if (materia == null) return NotFound();

            return View(materia);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia != null)
            {
                _context.Materias.Remove(materia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.MateriaId == id);
        }
    }
}

