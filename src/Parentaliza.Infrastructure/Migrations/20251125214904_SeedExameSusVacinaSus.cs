using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentaliza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedExameSusVacinaSus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed data para Exames SUS - Usando SQL direto para garantir compatibilidade com MySQL
            migrationBuilder.Sql(@"
                INSERT IGNORE INTO ExameSus (Id, NomeExame, Descricao, CategoriaFaixaEtaria, IdadeMinMesesExame, IdadeMaxMesesExame, CreatedAt, UpdatedAt) VALUES
                ('11111111-1111-1111-1111-111111111111', 'Teste do Pezinho', 'Triagem neonatal para detectar doenças genéticas e metabólicas', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111112', 'Teste da Orelhinha', 'Triagem auditiva neonatal para detectar deficiência auditiva', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111113', 'Teste do Olhinho', 'Triagem visual para detectar problemas oculares', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111114', 'Teste do Coraçãozinho', 'Triagem cardíaca para detectar cardiopatias congênitas', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111115', 'Teste da Linguinha', 'Triagem para detectar anquiloglossia (língua presa)', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111116', 'Hemograma Completo', 'Exame de sangue para avaliar saúde geral', 'Geral', 1, 120, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111117', 'Glicemia', 'Exame para medir os níveis de açúcar no sangue', 'Geral', 1, 120, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111118', 'Hemoglobina Glicada', 'Exame para monitoramento de diabetes', 'Geral', 6, 120, NOW(), NULL),
                ('11111111-1111-1111-1111-111111111119', 'Exame de Urina', 'Análise de urina para detectar infecções e problemas renais', 'Geral', 1, 120, NOW(), NULL),
                ('11111111-1111-1111-1111-11111111111A', 'Raio-X de Tórax', 'Exame de imagem para avaliar pulmões e coração', 'Geral', 1, 120, NOW(), NULL);
            ");

            // Seed data para Vacinas SUS - Calendário Nacional de Vacinação
            migrationBuilder.Sql(@"
                INSERT IGNORE INTO VacinaSus (Id, NomeVacina, Descricao, CategoriaFaixaEtaria, IdadeMinMesesVacina, IdadeMaxMesesVacina, CreatedAt, UpdatedAt) VALUES
                ('22222222-2222-2222-2222-222222222221', 'BCG', 'Protege contra formas graves de tuberculose', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222222', 'Hepatite B', 'Protege contra hepatite B', 'Recém-nascido', 0, 0, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222223', 'Pentavalente (DTP + Hib + Hepatite B)', 'Protege contra difteria, tétano, coqueluche, hepatite B e meningite', '2 meses', 2, 2, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222224', 'VIP (Poliomielite Inativada)', 'Protege contra poliomielite', '2 meses', 2, 2, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222225', 'Rotavírus', 'Protege contra diarreia causada por rotavírus', '2 meses', 2, 2, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222226', 'Pneumocócica 10V', 'Protege contra pneumonia e meningite', '2 meses', 2, 2, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222227', 'Meningocócica C', 'Protege contra meningite meningocócica', '3 meses', 3, 3, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222228', 'Pentavalente (2ª dose)', 'Segunda dose da vacina pentavalente', '4 meses', 4, 4, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222229', 'VIP (2ª dose)', 'Segunda dose da vacina contra poliomielite', '4 meses', 4, 4, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222222A', 'Rotavírus (2ª dose)', 'Segunda dose da vacina contra rotavírus', '4 meses', 4, 4, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222222B', 'Pneumocócica 10V (2ª dose)', 'Segunda dose da vacina pneumocócica', '4 meses', 4, 4, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222222C', 'Meningocócica C (2ª dose)', 'Segunda dose da vacina meningocócica C', '5 meses', 5, 5, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222222D', 'Pentavalente (3ª dose)', 'Terceira dose da vacina pentavalente', '6 meses', 6, 6, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222222E', 'VIP (3ª dose)', 'Terceira dose da vacina contra poliomielite', '6 meses', 6, 6, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222222F', 'Pneumocócica 10V (3ª dose)', 'Terceira dose da vacina pneumocócica', '6 meses', 6, 6, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222230', 'Influenza (Gripe)', 'Protege contra gripe', '6 meses', 6, 120, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222231', 'Tríplice Viral (SCR)', 'Protege contra sarampo, caxumba e rubéola', '12 meses', 12, 12, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222232', 'Pneumocócica 10V (Reforço)', 'Reforço da vacina pneumocócica', '12 meses', 12, 12, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222233', 'Meningocócica C (Reforço)', 'Reforço da vacina meningocócica C', '12 meses', 12, 12, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222234', 'Varicela (Catapora)', 'Protege contra varicela', '15 meses', 15, 15, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222235', 'DTP (Reforço)', 'Reforço contra difteria, tétano e coqueluche', '15 meses', 15, 15, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222236', 'Hepatite A', 'Protege contra hepatite A', '15 meses', 15, 15, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222237', 'Tetra Viral (SCRV)', 'Protege contra sarampo, caxumba, rubéola e varicela', '15 meses', 15, 15, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222238', 'VIP (Reforço)', 'Reforço da vacina contra poliomielite', '15 meses', 15, 15, NOW(), NULL),
                ('22222222-2222-2222-2222-222222222239', 'DTP (2º Reforço)', 'Segundo reforço contra difteria, tétano e coqueluche', '4 anos', 48, 48, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222223A', 'VIP (2º Reforço)', 'Segundo reforço da vacina contra poliomielite', '4 anos', 48, 48, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222223B', 'Tríplice Viral (2ª dose)', 'Segunda dose da vacina tríplice viral', '4 anos', 48, 48, NOW(), NULL),
                ('22222222-2222-2222-2222-22222222223C', 'Varicela (2ª dose)', 'Segunda dose da vacina contra varicela', '4 anos', 48, 48, NOW(), NULL);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove os dados de seed usando os GUIDs fixos
            migrationBuilder.Sql(@"
                DELETE FROM ExameSus WHERE Id IN (
                    '11111111-1111-1111-1111-111111111111',
                    '11111111-1111-1111-1111-111111111112',
                    '11111111-1111-1111-1111-111111111113',
                    '11111111-1111-1111-1111-111111111114',
                    '11111111-1111-1111-1111-111111111115',
                    '11111111-1111-1111-1111-111111111116',
                    '11111111-1111-1111-1111-111111111117',
                    '11111111-1111-1111-1111-111111111118',
                    '11111111-1111-1111-1111-111111111119',
                    '11111111-1111-1111-1111-11111111111A'
                );
            ");

            migrationBuilder.Sql(@"
                DELETE FROM VacinaSus WHERE Id IN (
                    '22222222-2222-2222-2222-222222222221',
                    '22222222-2222-2222-2222-222222222222',
                    '22222222-2222-2222-2222-222222222223',
                    '22222222-2222-2222-2222-222222222224',
                    '22222222-2222-2222-2222-222222222225',
                    '22222222-2222-2222-2222-222222222226',
                    '22222222-2222-2222-2222-222222222227',
                    '22222222-2222-2222-2222-222222222228',
                    '22222222-2222-2222-2222-222222222229',
                    '22222222-2222-2222-2222-22222222222A',
                    '22222222-2222-2222-2222-22222222222B',
                    '22222222-2222-2222-2222-22222222222C',
                    '22222222-2222-2222-2222-22222222222D',
                    '22222222-2222-2222-2222-22222222222E',
                    '22222222-2222-2222-2222-22222222222F',
                    '22222222-2222-2222-2222-222222222230',
                    '22222222-2222-2222-2222-222222222231',
                    '22222222-2222-2222-2222-222222222232',
                    '22222222-2222-2222-2222-222222222233',
                    '22222222-2222-2222-2222-222222222234',
                    '22222222-2222-2222-2222-222222222235',
                    '22222222-2222-2222-2222-222222222236',
                    '22222222-2222-2222-2222-222222222237',
                    '22222222-2222-2222-2222-222222222238',
                    '22222222-2222-2222-2222-222222222239',
                    '22222222-2222-2222-2222-22222222223A',
                    '22222222-2222-2222-2222-22222222223B',
                    '22222222-2222-2222-2222-22222222223C'
                );
            ");
        }
    }
}
