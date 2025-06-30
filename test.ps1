$BaseUrl = "https://localhost:7226"
$Headers = @{ "Content-Type" = "application/json" }

Write-Host "Создание клиента (ЮЛ)"
$clientUL = @{
    inn  = "1234567890"
    name = "ООО Ромашка"
    type = 0
} | ConvertTo-Json -Depth 2
Invoke-RestMethod -Method POST -Uri "$BaseUrl/api/clients" -Body $clientUL -Headers $Headers 
Write-Host ""

Write-Host "Создание клиента (ИП)"
$clientIP = @{
    inn  = "2345678901"
    name = "ИП Сидоров"
    type = 1
} | ConvertTo-Json -Depth 2
Invoke-RestMethod -Method POST -Uri "$BaseUrl/api/clients" -Body $clientIP -Headers $Headers
Write-Host ""

Write-Host "Создание учредителя и связь с ЮЛ"
$founder = @{
    inn      = "9876543210"
    fullName = "Иванов Иван"
} | ConvertTo-Json -Depth 2
Invoke-RestMethod -Method POST -Uri "$BaseUrl/api/founders?clientInns=1234567890" -Body $founder -Headers $Headers 
Write-Host ""

Write-Host "Все клиенты:"
Invoke-RestMethod -Uri "$BaseUrl/api/clients" | ConvertTo-Json -Depth 3
Write-Host ""

Write-Host "Все учредители:"
Invoke-RestMethod -Uri "$BaseUrl/api/founders" | ConvertTo-Json -Depth 3
Write-Host ""
