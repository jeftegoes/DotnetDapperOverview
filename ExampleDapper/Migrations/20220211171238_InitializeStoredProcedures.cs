﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleDapper.Migrations
{
    public partial class InitializeStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROC StpGetCompany
                    @CompanyId int
                AS 
                BEGIN 
                    SELECT
                        *
                    FROM
                        Company
                    WHERE
                        CompanyId = @CompanyId
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC StpGetALLCompany
                AS 
                BEGIN 
                    SELECT
                        *
                    FROM
                        Company
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC StpAddCompany
                    @CompanyId int OUTPUT,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN
                    INSERT INTO Company
                        (Name,
                         Address,
                         City,
                         State,
                         PostalCode)
                    VALUES
                        (@Name,
                         @Address,
                         @City,
                         @State,
                         @PostalCode);

	                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC StpUpdateCompany
	                @CompanyId int,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    UPDATE Company  
	                SET 
		                Name = @Name, 
		                Address = @Address,
		                City=@City, 
		                State=@State, 
		                PostalCode=@PostalCode
	                WHERE
                        CompanyId=@CompanyId;

	                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC StpRemoveCompany
                    @CompanyId int
                AS 
                BEGIN 
                    DELETE FROM
                        Company
                    WHERE
                        CompanyId  = @CompanyId
                END
                GO	
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
