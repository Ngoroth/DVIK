using Microsoft.EntityFrameworkCore.Migrations;

namespace Dvik.Data.Migrations
{
    public partial class AddTrainersToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Trainer_TrainerId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer");

            migrationBuilder.RenameTable(
                name: "Trainer",
                newName: "Trainers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trainers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Trainers_TrainerId",
                table: "Courses",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Trainers_TrainerId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers");

            migrationBuilder.RenameTable(
                name: "Trainers",
                newName: "Trainer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trainer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Trainer_TrainerId",
                table: "Courses",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
