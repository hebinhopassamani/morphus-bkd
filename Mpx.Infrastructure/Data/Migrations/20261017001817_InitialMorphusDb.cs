using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mpx.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class _20261017001817_InitialMorphusDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "corporation",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                System = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                FullName = table.Column<string>(type: "text", nullable: true),
                Description = table.Column<string>(type: "text", nullable: true),
                CorporationId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_corporation", x => x.Id);
                table.ForeignKey(
                    name: "FK_corporation_corporation_CorporationId",
                    column: x => x.CorporationId,
                    principalTable: "corporation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Emails",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Subject = table.Column<string>(type: "text", nullable: true),
                HtmlBody = table.Column<string>(type: "text", nullable: true),
                TextBody = table.Column<string>(type: "text", nullable: true),
                IsHtmlBody = table.Column<bool>(type: "boolean", nullable: false),
                MailSent = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Emails", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "menu",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Label = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                Application = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                System = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                CorporationId = table.Column<Guid>(type: "uuid", nullable: false),
                Url = table.Column<string>(type: "text", nullable: true),
                Icon = table.Column<string>(type: "text", nullable: true),
                Description = table.Column<string>(type: "text", nullable: true),
                MenuId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_menu", x => x.Id);
                table.ForeignKey(
                    name: "FK_menu_corporation_CorporationId",
                    column: x => x.CorporationId,
                    principalTable: "corporation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_menu_menu_MenuId",
                    column: x => x.MenuId,
                    principalTable: "menu",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "module",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                System = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                CorporationId = table.Column<Guid>(type: "uuid", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_module", x => x.Id);
                table.ForeignKey(
                    name: "FK_module_corporation_CorporationId",
                    column: x => x.CorporationId,
                    principalTable: "corporation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UserStatus",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Key = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                System = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                CorporationId = table.Column<Guid>(type: "uuid", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserStatus", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserStatus_corporation_CorporationId",
                    column: x => x.CorporationId,
                    principalTable: "corporation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "EmailsBcc",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Mail = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                EmailId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailsBcc", x => x.Id);
                table.ForeignKey(
                    name: "FK_EmailsBcc_Emails_EmailId",
                    column: x => x.EmailId,
                    principalTable: "Emails",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EmailsCc",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Mail = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                EmailId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailsCc", x => x.Id);
                table.ForeignKey(
                    name: "FK_EmailsCc_Emails_EmailId",
                    column: x => x.EmailId,
                    principalTable: "Emails",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EmailsFrom",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Mail = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                EmailId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailsFrom", x => x.Id);
                table.ForeignKey(
                    name: "FK_EmailsFrom_Emails_EmailId",
                    column: x => x.EmailId,
                    principalTable: "Emails",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EmailsTo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Mail = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                EmailId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailsTo", x => x.Id);
                table.ForeignKey(
                    name: "FK_EmailsTo_Emails_EmailId",
                    column: x => x.EmailId,
                    principalTable: "Emails",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "subModule",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                System = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                CorporationId = table.Column<Guid>(type: "uuid", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_subModule", x => x.Id);
                table.ForeignKey(
                    name: "FK_subModule_corporation_CorporationId",
                    column: x => x.CorporationId,
                    principalTable: "corporation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_subModule_module_ModuleId",
                    column: x => x.ModuleId,
                    principalTable: "module",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                PhoneConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                TermsConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                System = table.Column<bool>(type: "boolean", nullable: false),
                UserStatusId = table.Column<Guid>(type: "uuid", nullable: false),
                CorporationId = table.Column<Guid>(type: "uuid", nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: true),
                NickName = table.Column<string>(type: "text", nullable: true),
                Picture = table.Column<string>(type: "text", nullable: true),
                PhoneNumber = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_UserStatus_UserStatusId",
                    column: x => x.UserStatusId,
                    principalTable: "UserStatus",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_User_corporation_CorporationId",
                    column: x => x.CorporationId,
                    principalTable: "corporation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_corporation_CorporationId",
            table: "corporation",
            column: "CorporationId");

        migrationBuilder.CreateIndex(
            name: "IX_EmailsBcc_EmailId",
            table: "EmailsBcc",
            column: "EmailId");

        migrationBuilder.CreateIndex(
            name: "IX_EmailsCc_EmailId",
            table: "EmailsCc",
            column: "EmailId");

        migrationBuilder.CreateIndex(
            name: "IX_EmailsFrom_EmailId",
            table: "EmailsFrom",
            column: "EmailId");

        migrationBuilder.CreateIndex(
            name: "IX_EmailsTo_EmailId",
            table: "EmailsTo",
            column: "EmailId");

        migrationBuilder.CreateIndex(
            name: "IX_menu_CorporationId",
            table: "menu",
            column: "CorporationId");

        migrationBuilder.CreateIndex(
            name: "IX_menu_MenuId",
            table: "menu",
            column: "MenuId");

        migrationBuilder.CreateIndex(
            name: "IX_module_CorporationId",
            table: "module",
            column: "CorporationId");

        migrationBuilder.CreateIndex(
            name: "IX_subModule_CorporationId",
            table: "subModule",
            column: "CorporationId");

        migrationBuilder.CreateIndex(
            name: "IX_subModule_ModuleId",
            table: "subModule",
            column: "ModuleId");

        migrationBuilder.CreateIndex(
            name: "IX_User_CorporationId",
            table: "User",
            column: "CorporationId");

        migrationBuilder.CreateIndex(
            name: "IX_User_UserStatusId",
            table: "User",
            column: "UserStatusId");

        migrationBuilder.CreateIndex(
            name: "IX_UserStatus_CorporationId",
            table: "UserStatus",
            column: "CorporationId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EmailsBcc");

        migrationBuilder.DropTable(
            name: "EmailsCc");

        migrationBuilder.DropTable(
            name: "EmailsFrom");

        migrationBuilder.DropTable(
            name: "EmailsTo");

        migrationBuilder.DropTable(
            name: "menu");

        migrationBuilder.DropTable(
            name: "subModule");

        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "Emails");

        migrationBuilder.DropTable(
            name: "module");

        migrationBuilder.DropTable(
            name: "UserStatus");

        migrationBuilder.DropTable(
            name: "corporation");
    }
}
