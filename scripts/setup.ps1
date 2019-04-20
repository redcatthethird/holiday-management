$base = "$($MyInvocation.MyCommand.Path | Split-Path | Split-Path)\src";
$project = "$base\HolidayManagement.DataAccess\HolidayManagement.DataAccess.csproj";
$startupProject = "$base\HolidayManagement.Api\HolidayManagement.Api.csproj";

dotnet ef --project $project --startup-project $startupProject database update