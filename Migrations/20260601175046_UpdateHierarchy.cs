using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HierarchyNodes_ParentId",
                table: "HierarchyNodes",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HierarchyNodes_HierarchyNodes_ParentId",
                table: "HierarchyNodes",
                column: "ParentId",
                principalTable: "HierarchyNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HierarchyNodes_HierarchyNodes_ParentId",
                table: "HierarchyNodes");

            migrationBuilder.DropIndex(
                name: "IX_HierarchyNodes_ParentId",
                table: "HierarchyNodes");
        }
    }
}
