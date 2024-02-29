param(
    [string]$SolutionPath = "SduNetCheckTool.sln",
    [string]$Configuration = "Debug",
    [string]$Platform = "x64",
    [string]$RuntimeIdentifier = "win-x64"
)

# 函数用于检测是否安装了需要的工具和依赖
function CheckPrerequisites {
    Write-Host "Checking prerequisites for build..."

    # 检测 MSBuild
    if (-not (Get-Command -Name "msbuild" -ErrorAction SilentlyContinue)) {
        Write-Host "MSBuild is not installed or not in PATH. Please install MSBuild." -ForegroundColor Red
        exit 1
    }

    Write-Host "Prerequisites check passed for build."
}

# 函数用于构建项目
function BuildProject {
    # 使用 MSBuild 进行 WPF 应用程序的恢复
    $restoreArgs = "/t:Restore /p:Configuration=$Configuration /p:Platform=$Platform /p:RuntimeIdentifier=$RuntimeIdentifier"
    $restoreCommand = "msbuild $SolutionPath $restoreArgs"

    Write-Host "`n Restoring WPF project... `n" -ForegroundColor Cyan
    Write-Host "$ $restoreCommand `n"
    Invoke-Expression $restoreCommand
    

    # 构建 wapproj 项目
    $buildArgs = "/p:Configuration=$Configuration /p:Platform=$Platform /p:RuntimIdentifier=$RuntimeIdentifier /p:UapAppxPackageBuildMode=StoreUpload /p:AppxBundle=Never /p:GenerateAppInstallerFile=False /p:AppxPackageSigningEnabled=False"
    $buildCommand = "msbuild $SolutionPath $buildArgs"
    Write-Host "`n Building WPF project... `n" -ForegroundColor Cyan
    Write-Host "$ $buildCommand `n"
    Invoke-Expression $buildCommand
    
}


# 检查先决条件
CheckPrerequisites

# 构建项目
BuildProject

Write-Host "Build completed."

Write-Host "Build result: SDUNetCheckTool.GUI\bin\$Platform\$Configuration\SDUNetCheckTool.GUI.exe"
