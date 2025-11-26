-- Script para marcar a migration como aplicada no banco de dados
-- Execute este script no seu banco MySQL se as tabelas já existem

USE `devs-parentaliza`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20251124232746_ParentalizaDbContext', '8.0.2')
ON DUPLICATE KEY UPDATE `MigrationId` = `MigrationId`;


