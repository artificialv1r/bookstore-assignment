using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "Jugoslovenski pisac, dobitnik Nobelove nagrade za književnost 1961. godine. Najpoznatiji po romanima o Bosni.", new DateTime(1892, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Ivo Andrić" },
                    { 2, "Bosanskohercegovački i jugoslovenski pisac. Najpoznatiji po romanu 'Derviš i smrt'.", new DateTime(1910, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Meša Selimović" },
                    { 3, "Srpski romanopisac i političar, autor velikih romanesknih ciklusa o srpskoj istoriji.", new DateTime(1921, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Dobrica Ćosić" },
                    { 4, "Srpski pesnik, prozni pisac i književni istoričar. Najpoznatiji po romanu 'Hazarski rečnik'.", new DateTime(1929, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Milorad Pavić" },
                    { 5, "Jugoslovenski pisac jevrejskog porekla, jedan od najznačajnijih autora 20. veka.", new DateTime(1935, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Danilo Kiš" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "YearOfFirstAssignment" },
                values: new object[,]
                {
                    { 1, "Najpoznatija književna nagrada na svetu, dodeljena za izuzetan doprinos svetskoj književnosti.", "Nobelova nagrada za književnost", 1901 },
                    { 2, "Prestižna srpska književna nagrada za najbolji roman godine, dodeljena od strane lista NIN.", "NIN nagrada", 1954 },
                    { 3, "Srpska književna nagrada za najbolju pripovetku ili roman.", "Andrićeva nagrada", 1981 },
                    { 4, "Međunarodna književna nagrada za izuzetan doprinos evropskoj književnosti.", "Evropska književna nagrada", 1985 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Terazije 16, Beograd", "Prosveta", "https://www.prosveta.rs" },
                    { 2, "Resavska 33, Beograd", "Laguna", "https://www.laguna.rs" },
                    { 3, "Makedonska 30, Beograd", "Vulkan izdavaštvo", "https://www.vulkanizdavastvo.rs" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId", "YearAwarded" },
                values: new object[,]
                {
                    { 1, 1, 1961 },
                    { 1, 2, 1954 },
                    { 1, 3, 1982 },
                    { 2, 2, 1966 },
                    { 2, 3, 1985 },
                    { 2, 4, 1990 },
                    { 3, 2, 1962 },
                    { 3, 3, 1981 },
                    { 3, 4, 1988 },
                    { 4, 2, 1985 },
                    { 4, 3, 1990 },
                    { 4, 4, 1991 },
                    { 5, 2, 1973 },
                    { 5, 3, 1987 },
                    { 5, 4, 1989 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "978-86-01-00001-1", 352, new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Na Drini ćuprija" },
                    { 2, 1, "978-86-01-00002-8", 288, new DateTime(1942, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Travnička hronika" },
                    { 3, 1, "978-86-01-00003-5", 176, new DateTime(1954, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Prokleta avlija" },
                    { 4, 2, "978-86-01-00004-2", 368, new DateTime(1966, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Derviš i smrt" },
                    { 5, 2, "978-86-01-00005-9", 312, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Tvrđava" },
                    { 6, 3, "978-86-01-00006-6", 624, new DateTime(1954, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Koreni" },
                    { 7, 3, "978-86-01-00007-3", 544, new DateTime(1961, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Deobe" },
                    { 8, 3, "978-86-01-00008-0", 1200, new DateTime(1972, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Vreme smrti" },
                    { 9, 4, "978-86-01-00009-7", 336, new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Hazarski rečnik" },
                    { 10, 4, "978-86-01-00010-3", 192, new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Predeo slikan čajem" },
                    { 11, 5, "978-86-01-00011-0", 264, new DateTime(1965, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Bašta, pepeo" },
                    { 12, 5, "978-86-01-00012-7", 144, new DateTime(1976, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Grobnica za Borisa Davidoviča" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
