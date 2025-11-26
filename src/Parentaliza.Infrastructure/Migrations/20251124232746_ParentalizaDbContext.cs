using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentaliza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ParentalizaDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Conteudos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Categoria = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataPublicacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteudos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExameSus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NomeExame = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaFaixaEtaria = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdadeMinMesesExame = table.Column<int>(type: "int", nullable: true),
                    IdadeMaxMesesExame = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExameSus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoResponsavel = table.Column<int>(type: "int", nullable: false),
                    Senha = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FaseNascimento = table.Column<string>(type: "varchar(80)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VacinaSus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NomeVacina = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaFaixaEtaria = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdadeMinMesesVacina = table.Column<int>(type: "int", nullable: true),
                    IdadeMaxMesesVacina = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacinaSus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BebeGestacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResponsavelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataPrevista = table.Column<DateTime>(type: "date", nullable: false),
                    DiasDeGestacao = table.Column<int>(type: "int", nullable: false),
                    PesoEstimado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ComprimentoEstimado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BebeGestacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BebeGestacao_Responsaveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BebeNascido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResponsavelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    TipoSanguineo = table.Column<int>(type: "int", nullable: false),
                    IdadeMeses = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BebeNascido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BebeNascido_Responsaveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventoAgenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResponsavelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Evento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Especialidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localizacao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    Anotacao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoAgenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoAgenda_Responsaveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ControlesFralda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BebeNascidoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HoraTroca = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoFralda = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacoes = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesFralda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlesFralda_BebeNascido_BebeNascidoId",
                        column: x => x.BebeNascidoId,
                        principalTable: "BebeNascido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ControlesLeiteMaterno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BebeNascidoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Cronometro = table.Column<DateTime>(type: "datetime", nullable: false),
                    LadoDireito = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LadoEsquerdo = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesLeiteMaterno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlesLeiteMaterno_BebeNascido_BebeNascidoId",
                        column: x => x.BebeNascidoId,
                        principalTable: "BebeNascido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ControlesMamadeira",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BebeNascidoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    QuantidadeLeite = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Anotacao = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesMamadeira", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlesMamadeira_BebeNascido_BebeNascidoId",
                        column: x => x.BebeNascidoId,
                        principalTable: "BebeNascido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExamesRealizados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BebeNascidoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExameSusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataRealizacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Realizado = table.Column<ulong>(type: "bit", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamesRealizados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamesRealizados_BebeNascido_BebeNascidoId",
                        column: x => x.BebeNascidoId,
                        principalTable: "BebeNascido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamesRealizados_ExameSus_ExameSusId",
                        column: x => x.ExameSusId,
                        principalTable: "ExameSus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VacinasAplicadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BebeNascidoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VacinaSusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataAplicacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Aplicada = table.Column<ulong>(type: "bit", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(500)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lote = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocalAplicacao = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacinasAplicadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacinasAplicadas_BebeNascido_BebeNascidoId",
                        column: x => x.BebeNascidoId,
                        principalTable: "BebeNascido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacinasAplicadas_VacinaSus_VacinaSusId",
                        column: x => x.VacinaSusId,
                        principalTable: "VacinaSus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BebeGestacao_ResponsavelId",
                table: "BebeGestacao",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_BebeNascido_ResponsavelId",
                table: "BebeNascido",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlesFralda_BebeNascidoId",
                table: "ControlesFralda",
                column: "BebeNascidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlesLeiteMaterno_BebeNascidoId",
                table: "ControlesLeiteMaterno",
                column: "BebeNascidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlesMamadeira_BebeNascidoId",
                table: "ControlesMamadeira",
                column: "BebeNascidoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoAgenda_ResponsavelId",
                table: "EventoAgenda",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamesRealizados_BebeNascidoId_ExameSusId",
                table: "ExamesRealizados",
                columns: new[] { "BebeNascidoId", "ExameSusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamesRealizados_ExameSusId",
                table: "ExamesRealizados",
                column: "ExameSusId");

            migrationBuilder.CreateIndex(
                name: "IX_VacinasAplicadas_BebeNascidoId_VacinaSusId",
                table: "VacinasAplicadas",
                columns: new[] { "BebeNascidoId", "VacinaSusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacinasAplicadas_VacinaSusId",
                table: "VacinasAplicadas",
                column: "VacinaSusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BebeGestacao");

            migrationBuilder.DropTable(
                name: "Conteudos");

            migrationBuilder.DropTable(
                name: "ControlesFralda");

            migrationBuilder.DropTable(
                name: "ControlesLeiteMaterno");

            migrationBuilder.DropTable(
                name: "ControlesMamadeira");

            migrationBuilder.DropTable(
                name: "EventoAgenda");

            migrationBuilder.DropTable(
                name: "ExamesRealizados");

            migrationBuilder.DropTable(
                name: "VacinasAplicadas");

            migrationBuilder.DropTable(
                name: "ExameSus");

            migrationBuilder.DropTable(
                name: "BebeNascido");

            migrationBuilder.DropTable(
                name: "VacinaSus");

            migrationBuilder.DropTable(
                name: "Responsaveis");
        }
    }
}
