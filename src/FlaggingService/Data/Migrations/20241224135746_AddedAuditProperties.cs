using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlaggingService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flagging_Establishments_EstablishmentId",
                table: "Flagging");

            migrationBuilder.DropForeignKey(
                name: "FK_Flagging_Flags_FlagId",
                table: "Flagging");

            migrationBuilder.DropForeignKey(
                name: "FK_Flagging_Users_FlaggedBy",
                table: "Flagging");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flagging",
                table: "Flagging");

            migrationBuilder.DropColumn(
                name: "FlagCount",
                table: "Flagging");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Establishments",
                newName: "FlagCount");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlagCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Flags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Flags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Flags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Flags",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Flagging",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Flagging",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Flagging",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Flagging",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EstablishmentType",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EstablishmentType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "EstablishmentType",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "EstablishmentType",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ContactIds",
                table: "Establishments",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Establishments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Establishments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Establishments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Establishments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flagging",
                table: "Flagging",
                columns: new[] { "FlagId", "EstablishmentId", "FlaggedBy" });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    TelephoneNo = table.Column<string>(type: "text", nullable: true),
                    SocialsInfo = table.Column<Dictionary<string, string>>(type: "hstore", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    EstablishmentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Establishments_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "Establishments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flagging_FlagId_EstablishmentId_FlaggedBy",
                table: "Flagging",
                columns: new[] { "FlagId", "EstablishmentId", "FlaggedBy" });

            migrationBuilder.CreateIndex(
                name: "IX_Establishments_Name",
                table: "Establishments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_EstablishmentId",
                table: "Contact",
                column: "EstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flagging_Establishments_EstablishmentId",
                table: "Flagging",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flagging_Flags_FlagId",
                table: "Flagging",
                column: "FlagId",
                principalTable: "Flags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flagging_Users_FlaggedBy",
                table: "Flagging",
                column: "FlaggedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flagging_Establishments_EstablishmentId",
                table: "Flagging");

            migrationBuilder.DropForeignKey(
                name: "FK_Flagging_Flags_FlagId",
                table: "Flagging");

            migrationBuilder.DropForeignKey(
                name: "FK_Flagging_Users_FlaggedBy",
                table: "Flagging");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flagging",
                table: "Flagging");

            migrationBuilder.DropIndex(
                name: "IX_Flagging_FlagId_EstablishmentId_FlaggedBy",
                table: "Flagging");

            migrationBuilder.DropIndex(
                name: "IX_Establishments_Name",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FlagCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Flagging");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Flagging");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Flagging");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Flagging");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "ContactIds",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Establishments");

            migrationBuilder.RenameColumn(
                name: "FlagCount",
                table: "Establishments",
                newName: "ContactId");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "FlagCount",
                table: "Flagging",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flagging",
                table: "Flagging",
                columns: new[] { "FlagId", "FlaggedBy", "EstablishmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Flagging_Establishments_EstablishmentId",
                table: "Flagging",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flagging_Flags_FlagId",
                table: "Flagging",
                column: "FlagId",
                principalTable: "Flags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flagging_Users_FlaggedBy",
                table: "Flagging",
                column: "FlaggedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
