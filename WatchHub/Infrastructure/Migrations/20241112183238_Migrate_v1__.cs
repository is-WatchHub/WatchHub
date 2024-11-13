using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrate_v1__ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "integration",
                columns: table => new
                {
                    integration_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration", x => x.integration_id);
                });

            migrationBuilder.CreateTable(
                name: "platform",
                columns: table => new
                {
                    platform_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platform", x => x.platform_id);
                });

            migrationBuilder.CreateTable(
                name: "movie_platform_association",
                columns: table => new
                {
                    movie_platform_association_id = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: false),
                    external_platform_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_platform_association", x => x.movie_platform_association_id);
                    table.ForeignKey(
                        name: "FK_movie_platform_association_integration_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "integration",
                        principalColumn: "integration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_platform_association_platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "platform",
                        principalColumn: "platform_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movie_platform_association_IntegrationId",
                table: "movie_platform_association",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_platform_association_PlatformId",
                table: "movie_platform_association",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_platform_association");

            migrationBuilder.DropTable(
                name: "integration");

            migrationBuilder.DropTable(
                name: "platform");
        }
    }
}
