ALTER TABLE "public"."rls_project_person" DROP CONSTRAINT "FKrls_project_person_project_id";
ALTER TABLE "public"."rls_project_person" DROP CONSTRAINT "FKrls_person_project_person_id";
DROP TABLE IF EXISTS "public"."rls_project";
DROP TABLE IF EXISTS "public"."rls_project_person";
DROP TABLE IF EXISTS "public"."rls_person";
CREATE TABLE "public"."rls_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "rls_project_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."rls_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "rls_project_person_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."rls_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "rls_person_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."rls_project" DISABLE TRIGGER ALL;
ALTER TABLE "public"."rls_project_person" DISABLE TRIGGER ALL;
ALTER TABLE "public"."rls_person" DISABLE TRIGGER ALL;
INSERT INTO "public"."rls_project" ("project_name") VALUES ('Alpha');
INSERT INTO "public"."rls_project" ("project_name") VALUES ('Beta');
INSERT INTO "public"."rls_project" ("project_name") VALUES ('Charlie');
INSERT INTO "public"."rls_project" ("project_name") VALUES ('Zeta');
INSERT INTO "public"."rls_project" ("project_name") VALUES ('Delta');
INSERT INTO "public"."rls_project_person" ("project_id", "person_id", "hours") VALUES (1, 1, 5);
INSERT INTO "public"."rls_project_person" ("project_id", "person_id", "hours") VALUES (2, 1, 5);
INSERT INTO "public"."rls_project_person" ("project_id", "person_id", "hours") VALUES (3, 1, 245);
INSERT INTO "public"."rls_project_person" ("project_id", "person_id", "hours") VALUES (2, 2, 54);
INSERT INTO "public"."rls_project_person" ("project_id", "person_id", "hours") VALUES (1, 1, 5);
INSERT INTO "public"."rls_project_person" ("project_id", "person_id", "hours") VALUES (2, 1, 444);
INSERT INTO "public"."rls_person" ("person_name") VALUES ('Leo');
INSERT INTO "public"."rls_person" ("person_name") VALUES ('Mimmi');
INSERT INTO "public"."rls_person" ("person_name") VALUES ('Asd');
INSERT INTO "public"."rls_person" ("person_name") VALUES ('Chriff');
ALTER TABLE "public"."rls_project" ENABLE TRIGGER ALL;
ALTER TABLE "public"."rls_project_person" ENABLE TRIGGER ALL;
ALTER TABLE "public"."rls_person" ENABLE TRIGGER ALL;
ALTER TABLE "public"."rls_project_person" ADD CONSTRAINT "FKrls_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."rls_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."rls_project_person" ADD CONSTRAINT "FKrls_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."rls_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
