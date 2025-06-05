using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCore.Migrations
{
    /// <inheritdoc />
    public partial class fn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__3214EC0741FDB3FE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proyecto__3214EC07D37085AA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombredelatarea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechadeInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    TiempoEstimado = table.Column<double>(type: "float", nullable: true),
                    EstadoProgreso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProyectoId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tareas__3214EC072B780C36", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Tareas__Empleado__3C69FB99",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Tareas__Proyecto__3B75D760",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EmpleadoId",
                table: "Tareas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_ProyectoId",
                table: "Tareas",
                column: "ProyectoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Proyecto");
        }
    }
}
