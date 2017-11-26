SET COVERALLSIO=%USERPROFILE%\.nuget\packages\coveralls.io\1.4.2\tools\coveralls.net.exe
ECHO %COVERALLSIO%
%COVERALLSIO% --opencover %~dp0..\opencover\OpenCover.xml -r %COVERALLS_REPO_TOKEN%