[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$releaseUri = "https://api.github.com/repos/sendgrid/sendgrid-csharp/releases?sort=created"

$lastRelease = Invoke-RestMethod -Uri $releaseUri
$lastReleaseName = $lastRelease[0].name
$lastReleaseDate = [datetime]$lastRelease[0].created_at

Write-Output "Last release: $lastReleaseName"
Write-Output "Last release date: $lastReleaseDate"
Write-Output "Only closed and merged pull requests from this date will be included"

$version = Read-Host -Prompt 'Input release version'

$page = 1
$fetchMore = $True
while($fetchMore -eq $True) { 
    $uri = "https://api.github.com/repos/sendgrid/sendgrid-csharp/pulls?state=closed&page=$page&per_page=100"
    $response = Invoke-RestMethod -Uri $uri
    $allRequests += $response
    $fetchMore = $response.length -ne 0
    $page += 1
}

$filteredRequests = $allRequests | Where-Object ({ $_.merged_at -ne $null -and ([datetime]$_.merged_at).Date -ge $lastReleaseDate })
$allRequestsCount = $allRequests.length
$filteredCount = $filteredRequests.length
Write-Output "Total closed pull requests: $allRequestsCount"
Write-Output "Pull requests for release: $filteredCount"

$changes = [System.Text.StringBuilder]::new()
$currentDate = Get-Date
$currentDateFormatted = $currentDate.ToString('yyyy-MM-dd')
$changes.AppendLine("## [$version] - $currentDateFormatted")
$changes.AppendLine("## Added")


ForEach($request in $filteredRequests) {
$prNumber = $request.number
$prLink = $request.url
$title = $request.title
[void]$changes.AppendLine("- [PR #$prNumber]($prLink) $title")
}

$MyFileName = "changes.md"
$filebase = Join-Path $PSScriptRoot $MyFileName
$changes.ToString() | Out-File $filebase

Write-Output "New pull requests added to changes.md file"