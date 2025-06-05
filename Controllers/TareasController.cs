﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniCore.Models;

namespace MiniCore.Controllers
{
    public class TareasController : Controller
    {
        private readonly MinicoreContext _context;
        private object fechaInicio;
        private object fechaFin;

        public TareasController(MinicoreContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            IQueryable<Tarea> query = _context.Tareas.Include(t => t.Empleado).Include(t => t.Proyecto);

            if (fechaInicio != null && fechaFin != null)
            {
                query = query.Where(t => t.FechadeInicio >= fechaInicio && t.FechadeInicio <= fechaFin);
            }

            var tareas = await query.ToListAsync();

            ViewData["fechaInicio"] = fechaInicio;
            ViewData["fechaFin"] = fechaFin;

            return View(tareas);
        }


        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Empleado)
                .Include(t => t.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombredelatarea,FechadeInicio,TiempoEstimado,EstadoProgreso,ProyectoId,EmpleadoId")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", tarea.EmpleadoId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", tarea.ProyectoId);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", tarea.EmpleadoId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", tarea.ProyectoId);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombredelatarea,FechadeInicio,TiempoEstimado,EstadoProgreso,ProyectoId,EmpleadoId")] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", tarea.EmpleadoId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", tarea.ProyectoId);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Empleado)
                .Include(t => t.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }
    }
}
