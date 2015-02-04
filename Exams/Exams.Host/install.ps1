$serviceName = "ExamsService.Host"
$exePath = "C:\Users\Gniady\Source\Repos\Edu\Exams\Exams.Host\bin\Debug\Exams.Host.exe"

$existingService = Get-WmiObject -Class Win32_Service -Filter "Name='$serviceName'"

if ($existingService) 
{
  "'$serviceName' exists already. Stopping."
  Stop-Service $serviceName
  "Waiting 3 seconds to allow existing service to stop."
  Start-Sleep -s 3

  $existingService.Delete()
  "Waiting 5 seconds to allow service to be uninstalled."
  Start-Sleep -s 5  
}

"Installing the service."
"Provide credentials like: Gniady-PC\Gniady"
$cred = Get-Credential
New-Service -BinaryPathName $exePath -Name ExamsService.Host -credential $cred -DisplayName ExamsService.Host -StartupType Automatic

"Starting the service."
Start-Service $serviceName
"Completed."

