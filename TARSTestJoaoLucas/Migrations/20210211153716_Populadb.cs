using Microsoft.EntityFrameworkCore.Migrations;

namespace TARSTestJoaoLucas.Migrations
{
    public partial class Populadb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO \"Workers\"(\"Name\") VALUES('Joao')");
            migrationBuilder.Sql("INSERT INTO \"Workers\"(\"Name\") VALUES('Maria')");
            migrationBuilder.Sql("INSERT INTO \"Workers\"(\"Name\") VALUES('Jose')");
            migrationBuilder.Sql("INSERT INTO \"Workers\"(\"Name\") VALUES('Carlos')");
            migrationBuilder.Sql("INSERT INTO \"Workers\"(\"Name\") VALUES('Rafael')");

            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 1', 'TARS 1 Project', 2, (Select \"Id\" from \"Workers\" where \"Name\"='Joao'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 2', 'TARS 2 Project', 4, (Select \"Id\" from \"Workers\" where \"Name\"='Joao'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 3', 'TARS 3 Project', 6, (Select \"Id\" from \"Workers\" where \"Name\"='Joao'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 4', 'TARS 4 Project', 5, (Select \"Id\" from \"Workers\" where \"Name\"='Joao'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 5', 'TARS 5 Project', 1, (Select \"Id\" from \"Workers\" where \"Name\"='Joao'))");

            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 6', 'TARS 6 Project', 2, (Select \"Id\" from \"Workers\" where \"Name\"='Maria'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 7', 'TARS 7 Project', 4, (Select \"Id\" from \"Workers\" where \"Name\"='Maria'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 8', 'TARS 8 Project', 6, (Select \"Id\" from \"Workers\" where \"Name\"='Maria'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 9', 'TARS 9 Project', 5, (Select \"Id\" from \"Workers\" where \"Name\"='Maria'))");

            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 10', 'TARS 10 Project', 2, (Select \"Id\" from \"Workers\" where \"Name\"='Jose'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 11', 'TARS 11 Project', 4, (Select \"Id\" from \"Workers\" where \"Name\"='Jose'))");
            migrationBuilder.Sql("INSERT INTO \"Projects\"(\"Name\", \"Description\", \"WorkedHours\", \"WorkerId\") VALUES('TARS 12', 'TARS 12 Project', 6, (Select \"Id\" from \"Workers\" where \"Name\"='Jose'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from \"Projects\"");
            migrationBuilder.Sql("Delete from \"Workers\"");
        }
    }
}
