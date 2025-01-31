using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sentinel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresa",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    tenant_id = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "papel",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_papeis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissao",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recurso = table.Column<string>(type: "text", nullable: false),
                    acao = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_permissoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plano",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    valor_mensal = table.Column<decimal>(type: "numeric", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_planos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario_sentinel",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    nome_completo = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_usuarios_sentinel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario_empresa",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    empresa_id = table.Column<long>(type: "bigint", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    nome_completo = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_usuarios_empresa", x => x.id);
                    table.ForeignKey(
                        name: "f_k_usuarios_empresa_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "papel_permissao",
                columns: table => new
                {
                    papeis_id = table.Column<long>(type: "bigint", nullable: false),
                    permissoes_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_papel_permissao", x => new { x.papeis_id, x.permissoes_id });
                    table.ForeignKey(
                        name: "f_k_papel_permissao__papeis_papeis_id",
                        column: x => x.papeis_id,
                        principalTable: "papel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_papel_permissao__permissoes_permissoes_id",
                        column: x => x.permissoes_id,
                        principalTable: "permissao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contrato",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    empresa_id = table.Column<long>(type: "bigint", nullable: false),
                    plano_id = table.Column<long>(type: "bigint", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultima_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_contratos", x => x.id);
                    table.ForeignKey(
                        name: "f_k_contratos__empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_contratos__planos_plano_id",
                        column: x => x.plano_id,
                        principalTable: "plano",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_empresa_papel",
                columns: table => new
                {
                    papeis_id = table.Column<long>(type: "bigint", nullable: false),
                    usuarios_empresa_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_papel_usuario_empresa", x => new { x.papeis_id, x.usuarios_empresa_id });
                    table.ForeignKey(
                        name: "f_k_papel_usuario_empresa__papeis_papeis_id",
                        column: x => x.papeis_id,
                        principalTable: "papel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_papel_usuario_empresa__usuarios_empresa_usuarios_empresa_id",
                        column: x => x.usuarios_empresa_id,
                        principalTable: "usuario_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_contratos_empresa_id",
                table: "contrato",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "i_x_contratos_plano_id",
                table: "contrato",
                column: "plano_id");

            migrationBuilder.CreateIndex(
                name: "IX_empresa_cnpj",
                table: "empresa",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empresa_tenant_id",
                table: "empresa",
                column: "tenant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_papel_permissao_permissoes_id",
                table: "papel_permissao",
                column: "permissoes_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissao_recurso_acao",
                table: "permissao",
                columns: new[] { "recurso", "acao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_empresa_empresa_id_email",
                table: "usuario_empresa",
                columns: new[] { "empresa_id", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_empresa_empresa_id_login",
                table: "usuario_empresa",
                columns: new[] { "empresa_id", "login" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_papel_usuario_empresa_usuarios_empresa_id",
                table: "usuario_empresa_papel",
                column: "usuarios_empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_sentinel_email",
                table: "usuario_sentinel",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_sentinel_login",
                table: "usuario_sentinel",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contrato");

            migrationBuilder.DropTable(
                name: "papel_permissao");

            migrationBuilder.DropTable(
                name: "usuario_empresa_papel");

            migrationBuilder.DropTable(
                name: "usuario_sentinel");

            migrationBuilder.DropTable(
                name: "plano");

            migrationBuilder.DropTable(
                name: "permissao");

            migrationBuilder.DropTable(
                name: "papel");

            migrationBuilder.DropTable(
                name: "usuario_empresa");

            migrationBuilder.DropTable(
                name: "empresa");
        }
    }
}
