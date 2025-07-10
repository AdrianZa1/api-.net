using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecturasApi.Migrations
{
    /// <inheritdoc />
    public partial class FixClienteIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_Clientes_ClienteId1",
                table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_ClienteId1",
                table: "Medidores");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Medidores");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Medidores",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Medidores",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_ClienteId",
                table: "Medidores",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_Clientes_ClienteId",
                table: "Medidores",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidores_Clientes_ClienteId",
                table: "Medidores");

            migrationBuilder.DropIndex(
                name: "IX_Medidores_ClienteId",
                table: "Medidores");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Medidores",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "Medidores",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId1",
                table: "Medidores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medidores_ClienteId1",
                table: "Medidores",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidores_Clientes_ClienteId1",
                table: "Medidores",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
