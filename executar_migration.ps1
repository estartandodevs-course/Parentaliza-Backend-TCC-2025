# Script PowerShell para executar o SQL via linha de comando MySQL
# Certifique-se de ter o MySQL CLI instalado e no PATH

$mysqlPath = "mysql"  # Ou o caminho completo: "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe"

$server = "db.mrfsolution.com.br"
$port = "3307"
$database = "devs-parentaliza"
$user = "devs-parentaliza"
# IMPORTANTE: Use variável de ambiente para senha (não commitar senha no código!)
# Defina a variável antes de executar: $env:DB_PASSWORD = "sua-senha"
$password = $env:DB_PASSWORD
if ([string]::IsNullOrEmpty($password)) {
    Write-Host "ERRO: Variável de ambiente DB_PASSWORD não definida!" -ForegroundColor Red
    Write-Host "Execute: `$env:DB_PASSWORD = 'sua-senha'" -ForegroundColor Yellow
    exit 1
}
$script = "mark_migration_applied.sql"

# Executa o script
& $mysqlPath -h $server -P $port -u $user -p"$password" $database -e "source $script"

Write-Host "Script executado com sucesso!" -ForegroundColor Green




