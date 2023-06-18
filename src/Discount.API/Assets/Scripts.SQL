CREATE DATABASE "DiscountDb";

GO

SELECT EXISTS (SELECT 1 FROM pg_catalog.pg_class c JOIN pg_catalog.pg_namespace n ON n.oid=c.relnamespace WHERE c.relname='__EFMigrationsHistory')

GO

SELECT "MigrationId", "ProductVersion"
      FROM "__EFMigrationsHistory"
      ORDER BY "MigrationId";

GO

CREATE TABLE "Coupons" (
    "CouponId" text NOT NULL,
    CONSTRAINT "PK_Coupons" PRIMARY KEY ("CouponId")
);

GO

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
      VALUES ('20230618080730_InitialCreate', '7.0.7')