using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HairdresserAppointmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    district = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    telephone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    location = table.Column<Point>(type: "geometry", nullable: true),
                    verified = table.Column<bool>(type: "boolean", nullable: false),
                    createDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    workingType = table.Column<int>(type: "integer", nullable: false),
                    workingStartHour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    workingEndHour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    appointmentTimeInterval = table.Column<int>(type: "integer", nullable: false),
                    appointmentPeopleCount = table.Column<int>(type: "integer", nullable: false),
                    officialHolidayAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    fullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    createDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    imagePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    verified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    businessId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.id);
                    table.ForeignKey(
                        name: "BusinessFK",
                        column: x => x.businessId,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BusinessGallery",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    imagePath = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    size = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    businessId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessGallery", x => x.id);
                    table.ForeignKey(
                        name: "BusinessFK",
                        column: x => x.businessId,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessWorkingInfo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    startHour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    endHour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    appointmentTimeInterval = table.Column<int>(type: "integer", nullable: false),
                    appointmentPeopleCount = table.Column<int>(type: "integer", nullable: false),
                    businessId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessWorkingInfo", x => x.id);
                    table.ForeignKey(
                        name: "BusinessFK",
                        column: x => x.businessId,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    point = table.Column<double>(type: "double precision", nullable: false),
                    createDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    businessId = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.id);
                    table.ForeignKey(
                        name: "BusinessFK",
                        column: x => x.businessId,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    businessId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.id);
                    table.ForeignKey(
                        name: "BusinessFK",
                        column: x => x.businessId,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    businessId = table.Column<int>(type: "integer", nullable: false),
                    categoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategory", x => x.id);
                    table.ForeignKey(
                        name: "BusinessFK",
                        column: x => x.businessId,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "CategoryFK",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_businessId",
                table: "Appointment",
                column: "businessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategory_businessId",
                table: "BusinessCategory",
                column: "businessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategory_categoryId",
                table: "BusinessCategory",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessGallery_businessId",
                table: "BusinessGallery",
                column: "businessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessWorkingInfo_businessId",
                table: "BusinessWorkingInfo",
                column: "businessId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_businessId",
                table: "Rating",
                column: "businessId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_businessId",
                table: "Service",
                column: "businessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "BusinessCategory");

            migrationBuilder.DropTable(
                name: "BusinessGallery");

            migrationBuilder.DropTable(
                name: "BusinessWorkingInfo");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Business");
        }
    }
}
