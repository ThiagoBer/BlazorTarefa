using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorTarefa.Migrations
{
    /// <inheritdoc />
    public partial class TabelaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dados");

            migrationBuilder.CreateTable(
                name: "tarefas",
                schema: "dados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    concluido = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    criado_em = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    concluido_em = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    id_pai = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tarefas_tarefas_id_pai",
                        column: x => x.id_pai,
                        principalSchema: "dados",
                        principalTable: "tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tarefas_concluido",
                schema: "dados",
                table: "tarefas",
                column: "concluido");

            migrationBuilder.CreateIndex(
                name: "IX_tarefas_id_pai",
                schema: "dados",
                table: "tarefas",
                column: "id_pai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tarefas",
                schema: "dados");
        }
    }
}
