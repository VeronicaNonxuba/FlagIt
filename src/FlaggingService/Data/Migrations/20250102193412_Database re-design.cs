using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlaggingService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Databaseredesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flagging");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    FlagId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstablishmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.FlagId, x.EstablishmentId, x.FlaggedBy, x.FlaggedOn });
                    table.ForeignKey(
                        name: "FK_Ratings_Establishments_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "Establishments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Flags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "Flags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_FlaggedBy",
                        column: x => x.FlaggedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EstablishmentId",
                table: "Ratings",
                column: "EstablishmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_FlaggedBy",
                table: "Ratings",
                column: "FlaggedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_FlagId_EstablishmentId_FlaggedBy_FlaggedOn",
                table: "Ratings",
                columns: new[] { "FlagId", "EstablishmentId", "FlaggedBy", "FlaggedOn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.CreateTable(
                name: "Flagging",
                columns: table => new
                {
                    FlagId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstablishmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FlaggedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    FlaggedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flagging", x => new { x.FlagId, x.EstablishmentId, x.FlaggedBy });
                    table.ForeignKey(
                        name: "FK_Flagging_Establishments_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "Establishments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flagging_Flags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "Flags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flagging_Users_FlaggedBy",
                        column: x => x.FlaggedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flagging_EstablishmentId",
                table: "Flagging",
                column: "EstablishmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Flagging_FlaggedBy",
                table: "Flagging",
                column: "FlaggedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Flagging_FlagId_EstablishmentId_FlaggedBy",
                table: "Flagging",
                columns: new[] { "FlagId", "EstablishmentId", "FlaggedBy" });
        }
    }
}
