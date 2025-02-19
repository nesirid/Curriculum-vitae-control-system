using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateCandidatePhotoTableAndRelaliton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Candidates_CandidateId",
                table: "PhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber");

            migrationBuilder.RenameTable(
                name: "PhoneNumber",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_CandidateId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_CandidateId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId1",
                table: "CandidateCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CandidatePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatePhotos_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCompanies_CompanyId1",
                table: "CandidateCompanies",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePhotos_CandidateId",
                table: "CandidatePhotos",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCompanies_Companies_CompanyId1",
                table: "CandidateCompanies",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Candidates_CandidateId",
                table: "PhoneNumbers",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCompanies_Companies_CompanyId1",
                table: "CandidateCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Candidates_CandidateId",
                table: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "CandidatePhotos");

            migrationBuilder.DropIndex(
                name: "IX_CandidateCompanies_CompanyId1",
                table: "CandidateCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "CandidateCompanies");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_CandidateId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Candidates_CandidateId",
                table: "PhoneNumber",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
