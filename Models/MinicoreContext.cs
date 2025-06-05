using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniCore.Models;

public partial class MinicoreContext : DbContext
{
    public MinicoreContext()
    {
    }

    public MinicoreContext(DbContextOptions<MinicoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC0741FDB3FE");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07D37085AA");

            entity.ToTable("Proyecto");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tareas__3214EC072B780C36");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombredelatarea).HasMaxLength(50);

            entity.HasOne(d => d.Empleado).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("FK__Tareas__Empleado__3C69FB99");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("FK__Tareas__Proyecto__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
