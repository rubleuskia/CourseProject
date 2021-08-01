[CmdletBinding()]
Param(
    [Parameter(Position=0,Mandatory=$true,ValueFromRemainingArguments=$true)]
    [string[]]$BuildArguments
)

Set-Location -Path "./WebApp"

& dotnet ef migrations $BuildArguments --project ../DataAccess

Set-Location -Path "./../"